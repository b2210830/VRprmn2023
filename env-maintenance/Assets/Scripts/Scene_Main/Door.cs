using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NsUnityVr.Systems;

public class Door : MonoBehaviour
{
    float _pos_x = -1.28f;
    [SerializeField] Slider _slider = default;
    bool _isOpen = false;
    float _secCounter = 0f;
    bool _isRunning = false;
    Tweener _open;

    void FixedUpdate()
    {
        if(_isOpen) return;

        var val = _slider.value;
        if(val < .6f)
        {
            _slider.value = _slider.maxValue;
            val = _slider.minValue;
            if(!_isRunning)
            {
                OpenDoor(val * _pos_x);
            }
            return;
        }

        var movement = val * _pos_x;
        var p = transform.localPosition;
        transform.localPosition = new Vector3(movement, p.y, p.z);
    }

    public void OpenDoor(float movement)
    {
        _isOpen = true;
        _isRunning = true;
        SEManager.Instance.PlaySE(SE.door);
        var openSec = 1f;
        _open = transform.DOLocalMoveX(movement, openSec)
            .OnComplete(() => {
                _isRunning = false;
                // _slider.interactable = false;
            });

        if(GameManager.Instance.CurrentGameState.Value == GameState.Main) Clipboard.Instance.ChangeNaviText(1); // リザルトのときは通らない
    }

    public void CloseDoor()
    {
        _open.Kill();
        if(!_isOpen) return;
        _isOpen = false;

        _isRunning = true;
        var movement = -1.28f;
        SEManager.Instance.PlaySE(SE.door);
        var closeSec = 1f;
        transform.DOLocalMoveX(movement, closeSec)
            .OnComplete(() => {
                _slider.value = _slider.maxValue;
                _isRunning = false;
                // _slider.interactable = true;
            });
    }
}
