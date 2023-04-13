using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
{
    Image _image;
    Text _text;
    Sequence _seq;
    [SerializeField] Color _textColor;
    float _duration = .5f;
    Vector3 _defaultScale;

    // void Start()
    // {
    //     _image = GetComponent<Image>();
    //     _text = transform.Find("Text").GetComponent<Text>();
    //     _defaultScale = transform.localScale;
    // }

    private void Initialize(float duration)
    {
        var target = 1f;
        var txtColor = Color.white;
        var scale = _defaultScale;
        _seq/* .Append(ChangeImageAlpha(target, duration)) */
            // .Join(ChangeTextColor(txtColor, duration))
            .Join(transform.DOScale(scale, duration));
    }

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<Text>();
        _defaultScale = transform.localScale;

        Initialize(0f);
    }

    private void OnDisable()
    {
        _seq.Kill();
        transform.localScale = _defaultScale;
    }

    /// <summary>
    /// ボタンホバー時
    /// </summary>
    public void ActivateButton()
    {
        var target = 1f;
        var txtColor = _textColor;
        var scale = _defaultScale * 1.2f;
        var duration = _duration;
        _seq/* .Append(ChangeImageAlpha(target, duration)) */
            // .Join(ChangeTextColor(txtColor, duration))
            .Append(transform.DOScale(scale, duration));
    }

    /// <summary>
    /// ボタンホバー外れ時
    /// </summary>
    public void DeactivateButton()
    {
        Initialize(_duration);
    }

    /// <summary>
    /// 画像の透明度を任意の値まで変化させる
    /// </summary>
    /// <param name="target">変化後の透明度[0~1]</param>
    /// <param name="duration">かかる時間</param>
    /// <returns>画像アニメーションのトゥイーン</returns>
    private Tween ChangeImageAlpha(float target, float duration)
    {
        return DOTween.ToAlpha(
            () => _image.color,
            color => _image.color = color,
            target,
            duration
        );
    }

    /// <summary>
    /// テキストの色を任意の値まで変化させる
    /// </summary>
    /// <param name="target">変化後の色</param>
    /// <param name="duration">かかる時間</param>
    /// <returns>テキストアニメーションのトゥイーン</returns>
    private Tween ChangeTextColor(Color target, float duration)
    {
        return DOTween.To(
            () => _text.color,
            color => _text.color = color,
            target,
            duration
        );
    }
}
