using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NsUnityVr.Systems;

public class LoadingModal : MonoBehaviour
{
    public bool IsDebug = false;
    [SerializeField] GameObject _canvas = default;
    [SerializeField] Transform _whitePanel = default;
    [SerializeField] float _startY = default;
    [SerializeField] float _targetY = default;
    [SerializeField] float _moveSec = 1f;
    [SerializeField] Text _text = default;
    [SerializeField] Text _subText = default;

    private string _txt = "";
    [SerializeField] string[] _subTexts = new string[]{
        "病室の前まで移動中",
        "決定ボタンを押して演習を開始しよう"
    };
    [SerializeField] float _showSec = 3f;
    private bool _finishedSubTextAnimation = false;
    [SerializeField] AudioSource _audioSource = default;
    [SerializeField] SimpleCapsuleWithStickMovement _player = default;

    void OnEnable()
    {
        if(IsDebug)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if(!_canvas.activeSelf) _canvas.SetActive(true);
        _player.EnableLinearMovement = false;
        var p = _whitePanel.localPosition;
        _whitePanel.localPosition = new Vector3(p.x, _startY, p.z);
        var cg = _canvas.GetComponent<CanvasGroup>();
        cg.alpha = 1f; // 非透明化
        ShowModal();
    }

    void Update()
    {
        if(!_finishedSubTextAnimation) return;

        if((OVRInput.GetDown(OVRInput.RawButton.A))
        || (OVRInput.GetDown(OVRInput.RawButton.X))
        || (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        || (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        || (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        || (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger)))
        {
            HideModal();
        }
    }

    private void OnDisable()
    {
        var gm = NsUnityVr.Systems.GameManager.Instance;
        if(gm == null) return;

        var state = gm.CurrentGameState.Value;
        // ステートに応じて文章を更新する
        switch(state)
        {
            case GameState.Ready:
                gm.CurrentGameState.Value = GameState.Main;
                _subTexts = new string[]{
                    "採点中",
                    "決定ボタンを押して病室内をもう一度確認しよう"
                };
                break;
            case GameState.Result:
                gm.CurrentGameState.Value = GameState.Result;
                _subTexts = new string[]{
                    "",
                    "決定ボタンを押すとタイトルに戻ります"
                };
                break;
        }
        _text.text = "";
    }

    void ShowModal()
    {
        var dt = DateTime.Now;
        var now = dt.ToString("MM月dd日HH時mm分");
        var state = NsUnityVr.Systems.GameManager.Instance.CurrentGameState.Value;
        // ステートに応じて文章を更新する
        switch(state)
        {
            case GameState.Ready:
                _txt = now + "\n\nこれから〇〇さんの\n様子を伺いに行く";
                break;
            case GameState.Result:
                _txt = now + "\n\n〇〇さんの病室の\n環境整備を行った";
                break;
            case GameState.End:
                _txt = "お疲れさまでした。\n\n患者さんにとって、\n安全で安楽、\nさらに自立を考えた環境に\n整えることができましたか？";
                break;
        }

        _whitePanel.DOLocalMoveY(_targetY, _moveSec)
            .OnComplete(() =>
            {
                StartAnimation();
            });
    }

    void HideModal()
    {
        var state = NsUnityVr.Systems.GameManager.Instance.CurrentGameState.Value;
        if(state == GameState.End) SceneLoader.Instance.LoadTheScene(Scene.Title);

        _whitePanel.DOLocalMoveY(_startY, _moveSec)
        .OnComplete(() =>
        {
            var cg = _canvas.GetComponent<CanvasGroup>();
            var sec = _moveSec / 2;
            cg.DOFade(0f, sec).SetEase(Ease.Linear).OnComplete(() =>
            {
                _player.EnableLinearMovement = true;
                _finishedSubTextAnimation = false;
                this.gameObject.SetActive(false);
            });
        });
    }

    void StartAnimation()
    {
        StartCoroutine(SubTextAnimation());
        ShowText(_txt);
    }

    private void ShowText(string txt)
    {
        var beforeTxt = _text.text;
        _text.text = "";
        _text.DOText(txt, _showSec)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                var currentTxt = _text.text;
                if(currentTxt == beforeTxt) return;

                _audioSource.Play();
                beforeTxt = currentTxt;
            });
    }

    private IEnumerator SubTextAnimation()
    {
        var suffix = ".";
        var num = 2;
        var interval = .5f;

        for(var n = 0; n < 3; n++)
        {
            var txt = _subTexts[0];
            for(var i = 0; i < num; i++)
            {
                txt += suffix;
                _subText.text = txt;
                yield return new WaitForSeconds(interval);
            }
        }

        // 点滅
        _subText.text = _subTexts[1];
        _subText.DOFade(.2f, .5f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);

        _finishedSubTextAnimation = true;
    }
}
