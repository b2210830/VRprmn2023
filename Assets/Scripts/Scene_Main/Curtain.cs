using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using NsUnityVr.Systems;

public class Curtain : MonoBehaviour
{
    public bool IsDebug = false;
    [SerializeField] GameObject _defaultCurtain;
    [SerializeField] GameObject _curtain;
    [SerializeField] Button _btn;
    [SerializeField] OVRScreenFade _fade = default;

    private bool _isRunning = false;
    public BoolReactiveProperty CanOpen = new BoolReactiveProperty(false);

    void Start()
    {
        if(!IsDebug)
        {
            CloseCurtain();
        }

        _btn.onClick.AddListener(() => OpenCurtainTask());
        CanOpen.Subscribe(x => _btn.gameObject.SetActive(x));
    }

    private async Task OpenCurtainTask()
    {
        if(_isRunning) return;
        _isRunning = true;

        _fade.FadeOut();

        SEManager.Instance.PlaySE(SE.curtain);
        await StartCoroutine(OpenCurtain());

        _fade.FadeIn();

        Clipboard.Instance.ChangeNaviText(2);
    }

    private IEnumerator OpenCurtain()
    {
        yield return new WaitForSeconds(2f);

        Destroy(_curtain);
        _defaultCurtain.transform.localScale = Vector3.one;
        var pos = _defaultCurtain.transform.localPosition;
        _defaultCurtain.transform.localPosition = new Vector3(pos.x, pos.y, -1f);

        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// 採点ステートで強制的にカーテンを開ける
    /// </summary>
    public void OpenCurtainForce()
    {
        if(_curtain == null) return;

        Destroy(_curtain);
        _defaultCurtain.transform.localScale = Vector3.one;
        var pos = _defaultCurtain.transform.localPosition;
        _defaultCurtain.transform.localPosition = new Vector3(pos.x, pos.y, -1f);
    }

    private void CloseCurtain()
    {
        _curtain.SetActive(true);
        _defaultCurtain.transform.localScale = new Vector3(1f, 1f, 5.5f);
        var pos = new Vector3(1.079f, -.21f, -.028f);
        _defaultCurtain.transform.localPosition = pos;
    }
}
