using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

public class MaintainedWithoutGrabbing : MonoBehaviour
{
    [SerializeField] protected string _actionTitle;
    [SerializeField] protected GameObject _canvas;
    public Button _btn;

    public bool _IsMaintained = false;

    public MaintenanceType Type = MaintenanceType.None;

    protected virtual void Start()
    {
        HideActionTitle();
        _btn = _canvas.transform.Find("Button").GetComponent<Button>();
        _btn.onClick.AddListener(() => MaintenanceAction());
        var text = _btn.transform.Find("Text").GetComponent<Text>();
        text.text = _actionTitle;
    }

    public virtual void ShowActionTitle()
    {
        if(_canvas == null) return;
        _canvas.SetActive(true);
    }

    public virtual void HideActionTitle()
    {
        if(_canvas == null) return;
        _canvas.SetActive(false);
    }

    protected virtual void MaintenanceAction()
    {
        SEManager.Instance.PlaySE(SE.kira);
        var effect_pre = Resources.Load<GameObject>("Prefabs/Effect");
        var effect = Instantiate<GameObject>(effect_pre, transform.position, Quaternion.identity);
        // effect.transform.SetParent(transform);
		_IsMaintained = true;
		GetComponent<Collider>().enabled = false;
		HideActionTitle();
    }

    protected void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ShowActionTitle();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            HideActionTitle();
        }
    }
}