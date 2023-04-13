using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class Clipboard : SingletonMonoBehaviour<Clipboard>
{
    private bool _isVisible = true;
    public BoolReactiveProperty IsShowing = new BoolReactiveProperty(false);
    [SerializeField] CanvasGroup _cg;
    [SerializeField] Text _text;
    string[] _navi = {
        "・ノックをして\n入室しよう",
        "・あいさつをしよう\n・カーテンを開けてよいか\n聞いてみよう",
        "・ベッドの周りを\n整理してよいか\n聞いてみよう",
        "・移動の援助\nをしよう\n - 点滴\n - 靴\n - 危険の排除\n - 立ち位置",
        "・環境整備をしよう\n(終わったら退室しよう)",
        "・ふりかえりをしよう\n(終わったら退室しよう)",
    };

    void Start()
    {
        IsShowing.Where(_ => _isVisible)
            .Subscribe(x => {
            if(x) Show();
            else Hide();
        });
    }

    private void Show()
    {
        _cg.DOFade(1f, .5f).SetEase(Ease.Linear);
    }

    private void Hide()
    {
        _cg.DOFade(0f, .5f).SetEase(Ease.Linear);
    }

    public void ChangeNaviText(int index)
    {
        _text.text = _navi[index];
    }

    public void SwitchView()
    {
        if(_isVisible) Hide();
        else Show();
        _isVisible = _isVisible ? false : true;
    }
}
