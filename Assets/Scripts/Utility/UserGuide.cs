using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGuide : SingletonMonoBehaviour<UserGuide>
{
    #region Btn
        [SerializeField] GameObject _btn_A;
        [SerializeField] GameObject _btn_B;
        [SerializeField] GameObject _btn_X;
        [SerializeField] GameObject _trigger_Index_L;
        [SerializeField] GameObject _trigger_Hand_L;
        [SerializeField] GameObject _stick_L;
        [SerializeField] GameObject _trigger_Index_R;
        [SerializeField] GameObject _trigger_Hand_R;
        [SerializeField] GameObject _stick_R;
        private Dictionary<OVRInput.Button, GameObject> _controllerBtnsDict;
    #endregion

    [SerializeField] Transform _canvas;
    private GameObject _userGuidePanelPrefab;
    private GameObject _userGuidePanel;
    private LineRenderer _lineRenderer;
    private OVRInput.Button _currentBtn = OVRInput.Button.None;
    private RectTransform _panelRect;
    public bool _hasGuide = false;

    [SerializeField] GameObject[] _controller;

    [SerializeField] float _maxAppearSec = 3f;
    private float _counter = 0f;

    protected override void Awake()
    {
        base.Awake();

        _controllerBtnsDict = new Dictionary<OVRInput.Button, GameObject> {
            {OVRInput.Button.One, _btn_A},
            {OVRInput.Button.Two, _btn_B},
            {OVRInput.Button.Three, _btn_X},
            {OVRInput.Button.PrimaryIndexTrigger, _trigger_Index_L},
            {OVRInput.Button.PrimaryHandTrigger, _trigger_Hand_L},
            {OVRInput.Button.PrimaryThumbstick, _stick_L},
            {OVRInput.Button.SecondaryIndexTrigger, _trigger_Index_R},
            {OVRInput.Button.SecondaryHandTrigger, _trigger_Hand_R},
            {OVRInput.Button.SecondaryThumbstick, _stick_R},
        };

        _userGuidePanelPrefab = Resources.Load<GameObject>("Prefabs/UserGuidePanel");
        Hide();
    }

    private void Update()
    {
        if(_currentBtn == OVRInput.Button.None) return; // 初期値の場合抜ける

        var btn = _controllerBtnsDict[_currentBtn];
        if(!btn) print("このボタンは辞書に定義されていません"); // nullチェック

        if(!_hasGuide) return;

        // ボタンオブジェクトの座標を取得
        var panelPos = _panelRect.position;
        var btnPos = btn.transform.position;

        DrawBtnGuideLine(panelPos, btnPos);

        _counter += Time.deltaTime;
        if(_counter >= _maxAppearSec)
        {
            Hide();
        }
    }

    private void DrawBtnGuideLine(Vector3 from, Vector3 to)
    {
        _lineRenderer.SetPosition(0, from);
        _lineRenderer.SetPosition(1, to);
    }

    /// 以下
    /// - つかむ(OVRGrabber Custom .OnTriggerEnter/Exit)
    /// - UI操作(OVRRaycaster)　←多分
    /// で適宜呼び出す

    public void Show(string txt, OVRInput.Button btn)
    {
        for(var i = 0; i < _controller.Length; i++)
        {
            _controller[i].SetActive(true);
        }
        CreateUserGuidePanel(txt, btn);
        gameObject.SetActive(true);
    }

    public void ShowOnAction()
    {
        Show("アクションする", OVRInput.Button.SecondaryHandTrigger);
    }

    public void Hide()
    {
        _counter = 0f;
        for(var i = 0; i < _controller.Length; i++)
        {
            _controller[i].SetActive(false);
        }
        gameObject.SetActive(false);
        DeleteUserGuidePanel();
    }

    /// <summary>
    /// ガイドパネルを生成する
    /// </summary>
    /// <param name="txt">表示するテキスト</param>
    /// <param name="pos">パネルを表示する位置</param>
    /// <param name="btn">押してもらいたいボタン</param>
    public void CreateUserGuidePanel(string txt, OVRInput.Button btn)
    {
        var pos = transform.position;

        if(_userGuidePanel)
        {
            DeleteUserGuidePanel();
        }

        _userGuidePanel = Instantiate<GameObject>(_userGuidePanelPrefab, pos, Quaternion.identity);
        _userGuidePanel.transform.SetParent(_canvas, false);
        var text = _userGuidePanel.transform.Find("Text").GetComponent<Text>();
        text.text = txt;

        _lineRenderer = _userGuidePanel.GetComponent<LineRenderer>();
        var rect = _userGuidePanel.GetComponent<RectTransform>();
        _panelRect = rect;
        _currentBtn = btn;

        _hasGuide = true;
    }

    /// <summary>
    /// ガイドパネルを削除する
    /// </summary>
    public void DeleteUserGuidePanel()
    {
        Destroy(_userGuidePanel);
        _hasGuide = false;
    }
}
