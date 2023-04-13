using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class FollowCanvas : MonoBehaviour
{
    public BoolReactiveProperty IsShowing = new BoolReactiveProperty(false);
    [SerializeField] CanvasGroup _cg;

    void Start()
    {
        IsShowing.Subscribe(x => {
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
}
