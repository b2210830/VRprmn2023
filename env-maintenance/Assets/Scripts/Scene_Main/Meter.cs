using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NsUnityVr.Systems;

public class Meter : MonoBehaviour
{
    [SerializeField] Button _windowBtn;
    [SerializeField] Button _towelBtn;
    [SerializeField] Text _windowTxt;
    private bool _openingWindow = false;
    private bool _hangingTowel = false;
    [SerializeField] Text _towelTxt;
    [SerializeField] Text _time;
    [SerializeField] Text _timeColon;
    [SerializeField] Text _ondoText;
    [SerializeField] Text _ShitsudoText;

    private float _idealOndo = 24f;
    private float _idealShitsudo = 55f;
    private float _ondo;
    private float _shitsudo;

    public MaintenanceType Type = MaintenanceType.None;

    private void Start()
    {
        _ondo = UnityEngine.Random.Range(0, 2) == 0 ? 24 : 28; // 室温は24か28℃
        UpdateNum(_ondo, _ondoText);
        _shitsudo = UnityEngine.Random.Range(0, 2) == 0 ? 45 : 55; // 湿度は45か55%
        UpdateNum(_shitsudo, _ShitsudoText);

        _windowBtn.onClick.AddListener(() => SwitchWindow());
        _towelBtn.onClick.AddListener(() => SwitchTowel());
        TimeColonAnimation();
    }

    private void FixedUpdate()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        var dt = DateTime.Now;
        var now = dt.ToString("HH  mm");
        _time.text = now;
    }

    private void UpdateNum(float n, Text txt)
    {
        txt.text = n.ToString();
    }

    private void TimeColonAnimation()
    {
        _timeColon.DOFade(0f, .5f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void SwitchWindow()
    {
        SEManager.Instance.PlaySE(SE.tenteki); // ガラガラ音
        if(_openingWindow)
        {
            print("窓を閉めた");
            _ondo += 4f;
            UpdateNum(_ondo, _ondoText);

            _windowTxt.text = "窓 を 開 け る";
        }
        else
        {
            print("窓を開けた");
            _ondo -= 4f;
            UpdateNum(_ondo, _ondoText);

            _windowTxt.text = "窓 を 閉 め る";
        }
        _openingWindow = _openingWindow? false : true;
    }

    public void SwitchTowel()
    {
        SEManager.Instance.PlaySE(SE.putting_S);
        if(_hangingTowel)
        {
            print("タオルを取り除いた");
            _shitsudo -= 10f;
            UpdateNum(_shitsudo, _ShitsudoText);

            _towelTxt.text = "濡れたタオルを干す";
        }
        else
        {
            print("タオルを干した");
            _shitsudo += 10f;
            UpdateNum(_shitsudo, _ShitsudoText);

            _towelTxt.text = "濡れたタオルを取る";
        }
        _hangingTowel = _hangingTowel? false : true;
    }

    public bool CheckOndo()
    {
        return _ondo == _idealOndo ? true : false;
    }

    public bool CheckShitsudo()
    {
        return _shitsudo == _idealShitsudo ? true : false;
    }
}
