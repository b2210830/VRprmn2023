using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceManager : SingletonMonoBehaviour<MaintenanceManager>
{
    public List<MaintainedObjects> _MaintainedObjectsList = new List<MaintainedObjects>();
    public List<MaintainedWithoutGrabbing> _MWGObjectsList = new List<MaintainedWithoutGrabbing>();
    public Meter _Meter;
    // [SerializeField] Text _text = default; // debug
    private int _length = default;

    [SerializeField] GameObject _markCanvasPrefab = default;
    [SerializeField] Sprite _maru;
    [SerializeField] Sprite _batsu;

    void Start()
    {
        _length = _MaintainedObjectsList.Count;
    }

    void FixedUpdate()
    {
        var counter = 0;
        foreach(var obj in _MaintainedObjectsList)
        {
            if(obj._IsMaintained) counter++;
        }
        // _text.text = counter.ToString() + "/" + _length.ToString();
    }

    public void SetUpResultState()
    {
        foreach(var obj in _MaintainedObjectsList)
        {
            Destroy(obj.GetComponent<OVRGrabbable>()); // つかめなくする
            obj.GetComponent<Rigidbody>().isKinematic = true; // 動かなくする

            var markCanvas = SetMarkCanvas(obj.transform);

            // 判定マークを判定に応じて初期化する
            var image = markCanvas.transform.Find("Image");
            var sprite = obj._IsMaintained ? _maru : _batsu;
            image.GetComponent<Image>().sprite = sprite;

            // 整備されていない場合ヒントを表示
            if(!obj._IsMaintained)
            {
                var hintPanel = markCanvas.transform.Find("HintPanel");
                var text = hintPanel.transform.Find("Text").GetComponent<Text>();
                var hint = new Hint();
                text.text = hint.GetHintMessage(obj.Type);
                hintPanel.gameObject.SetActive(true);
            }
        }

        foreach(var obj in _MWGObjectsList)
        {
            obj._btn.interactable = false; // アクション出来なくする
            if(obj.GetComponent<OVRGrabbable>()) Destroy(obj.GetComponent<OVRGrabbable>());
            if(obj.GetComponent<Trash>() && obj.GetComponent<Trash>()._IsMaintained) return;

            var markCanvas = SetMarkCanvas(obj.transform);

            // 判定マークを判定に応じて初期化する
            var image = markCanvas.transform.Find("Image");
            var sprite = obj._IsMaintained ? _maru : _batsu;
            image.GetComponent<Image>().sprite = sprite;

            // 整備されていない場合ヒントを表示
            if(!obj._IsMaintained)
            {
                var hintPanel = markCanvas.transform.Find("HintPanel");
                var text = hintPanel.transform.Find("Text").GetComponent<Text>();
                var hint = new Hint();
                text.text = hint.GetHintMessage(obj.Type);
                hintPanel.gameObject.SetActive(true);
            }
        }

        // 湿温度-------------------------------------------------------------------
        var mc = SetMarkCanvas(_Meter.transform);
        // 判定マークを判定に応じて初期化する
        var ig = mc.transform.Find("Image");
        var isMaintained = _Meter.CheckOndo() && _Meter.CheckShitsudo();
        var sp = isMaintained ? _maru : _batsu;
        ig.GetComponent<Image>().sprite = sp;

        // 整備されていない場合ヒントを表示
        if(!isMaintained)
        {
            var hintPanel = mc.transform.Find("HintPanel");
            var text = hintPanel.transform.Find("Text").GetComponent<Text>();
            var hint = new Hint();
            text.text = hint.GetHintMessage(_Meter.Type);
            hintPanel.gameObject.SetActive(true);
        }
    }

    private GameObject SetMarkCanvas(Transform transform)
    {
        // オブジェクトの上部に判定マークのキャンバスを設置
        var objPos = transform.position;
        var offset_y = .5f;
        var targetPos = new Vector3(objPos.x, objPos.y + offset_y, objPos.z);
        var markCanvas = Instantiate<GameObject>(_markCanvasPrefab, targetPos, Quaternion.identity);
        markCanvas.transform.SetParent(transform);
        return markCanvas;
    }
}
