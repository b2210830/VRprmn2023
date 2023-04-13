using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NsUnityVr.Systems;

/// <summary>
/// 整備されるオブジェクトにアタッチするコンポーネント
/// </summary>
public class MaintainedObjects : MonoBehaviour
{
    /// <summary>初期位置</summary>
    [SerializeField] Vector3 _startPosition = default;

    /// <summary>初期回転</summary>
    [SerializeField] Vector3 _startRotation = default;

    /// <summary>正しい位置</summary>
    [SerializeField] Vector3 _correctPosition = default;

    /// <summary>正しい回転</summary>
    [SerializeField] Vector3 _correctRotation = default;

    /// <summary>正しい位置と受け入れ可能な半径</summary>
    [SerializeField] float _acceptableRadius = 1f;

    /// <summary>正しい位置と受け入れ可能な位置の範囲.([0] < 0 < [1])</summary>
    private List<Vector3> _correctPositionRange = new List<Vector3>();

    /// <summary>正しい位置と受け入れ可能な回転の範囲.([0] < 0 < [1])</summary>
    [SerializeField] private List<Vector3> _correctRotationRange = new List<Vector3>();

    /// <summary>正しい位置への移動時間</summary>
    [SerializeField] float _moveDurationSeconds = 1f;

    /// <summary>整備されたかどうか</summary>
    public bool _IsMaintained = false;

    /// <summary>整備後コライダーを保持するか</summary>
    public bool _HoldCollider = true;

    public MaintenanceType Type = MaintenanceType.None;

    // public Text _text; // debug

    void Start()
    {
        // 正しい位置が未入力のとき、現在位置を正しい位置とする
        if(_correctPosition == Vector3.zero && _correctRotation == Vector3.zero)
        {
            _correctPosition = transform.localPosition;
            _correctRotation = transform.localEulerAngles;
        }

        // 初期位置が入力されているとき、初期位置を現在位置とする
        if(_startPosition != Vector3.zero || _startRotation != Vector3.zero)
        {
            transform.localPosition = _startPosition;
            transform.localRotation = Quaternion.Euler(_startRotation);
        }

        // すでに現在位置が正しい位置なら整備完了とする
        if(_correctPosition == transform.localPosition && _correctRotation == transform.localEulerAngles)
        {
            _IsMaintained = true;
        }

        // 受け入れ可能な位置と回転の範囲を定義
        for (var i = 0; i < 2; i++)
        {
            var radius = _acceptableRadius;
            var angle = 90f;
            if (i % 2 == 0)
            {
                radius *= -1;
                angle *= -1;
            }

            var pos_x = _correctPosition.x + radius;
            var pos_y = _correctPosition.y + radius;
            var pos_z = _correctPosition.z + radius;
            var pos_vec = new Vector3(pos_x, pos_y, pos_z);
            _correctPositionRange.Add(pos_vec);

            var rot_x = _correctRotation.x + angle;
            var rot_y = _correctRotation.y + angle;
            var rot_z = _correctRotation.z + angle;
            var rot_vec = new Vector3(rot_x, rot_y, rot_z);
            _correctRotationRange.Add(rot_vec);
        }
    }

    void Update()
    {
        if (_IsMaintained) return;

        CheckPosition();
    }

    /// <summary>
    /// オブジェクトの位置を調べる
    /// </summary>
    private void CheckPosition()
    {
        var currentPosition = transform.localPosition;
        var currentRotation = transform.localEulerAngles;

        // x,y,z軸それぞれが正しい位置の受け入れ可能範囲に収まっているか見る
        var state_posx = _correctPositionRange[0].x <= currentPosition.x && currentPosition.x <= _correctPositionRange[1].x;
        var state_posy = _correctPositionRange[0].y <= currentPosition.y && currentPosition.y <= _correctPositionRange[1].y;
        var state_posz = _correctPositionRange[0].z <= currentPosition.z && currentPosition.z <= _correctPositionRange[1].z;
        var state_pos = state_posx && state_posy && state_posz;

        var state_rotx = _correctRotationRange[0].x <= currentRotation.x && currentRotation.x <= _correctRotationRange[1].x;
        var state_roty = /*_correctRotationRange[0].y <= currentRotation.y && currentRotation.y <= _correctRotationRange[1].y;*/true; // y軸回転の判定あるとごみばこ激むずだった
        var state_rotz = _correctRotationRange[0].z <= currentRotation.z && currentRotation.z <= _correctRotationRange[1].z;
        var state_rot = state_rotx && state_roty && state_rotz;

        // 全ての軸が範囲に収まっていたら整備完了として、正しい位置へ移動させる
        if (state_pos && state_rot)
        {
            _IsMaintained = true;
            if(GetComponent<Renderer>())
            {
                var ovrGrabbable = GetComponent<OVRGrabbable>();
                GetComponent<Renderer>().material.shader = ovrGrabbable._DefaultShader;
            };
            Destroy(GetComponent<OVRGrabbable>()); // つかめなくする
            MoveToCorrectPosition(_moveDurationSeconds);
        }
    }

    /// <summary>
    /// 正しい位置へ移動させる
    /// </summary>
    /// <param name="moveSec">移動にかかる秒数</param>
    private void MoveToCorrectPosition(float moveSec)
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(_correctPosition, moveSec))
            .Join(transform.DOLocalRotate(_correctRotation, moveSec))
            .OnComplete(() =>
            {
                SEManager.Instance.PlaySE(SE.kira);
                var effect_pre = Resources.Load<GameObject>("Prefabs/Effect");
                var effect = Instantiate<GameObject>(effect_pre, transform.position, Quaternion.identity);
                effect.transform.SetParent(transform);

                if(!_HoldCollider)
                {
                    var rb = GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    var collider = GetComponents<Collider>();
                    foreach(var col in collider)
                    {
                        col.isTrigger = true;
                    }
                }
            });
    }

}

[System.Serializable]
public enum MaintenanceType
{
    None,
    Bed_clean,
    Bed_maintenance,
    Patient_Obstacle,
    Warn_env,
    Bed_Obstacle,
    Check_position,
    Patient_env,
    Bed_lock,
    Check_trash,
    Meter,
}
