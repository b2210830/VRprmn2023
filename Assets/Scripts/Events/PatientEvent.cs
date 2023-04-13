using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientEvent : MonoBehaviour
{
    [SerializeField] GameObject _patient;
    [SerializeField] MaintainedObjects[] _objs;
    [SerializeField] PatientSpeechManager _psm;
    public bool IsActive = false;
    [SerializeField] OVRScreenFade _fade = default;
    [SerializeField] SimpleCapsuleWithStickMovement _player = default;
    [SerializeField] Duvet _duvet;
    [SerializeField] Sanitaize[] _sanitaizes;
    [SerializeField] OVRGrabberCustom[] _grabber;
    [SerializeField] GameObject _fc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsActive) return;

        foreach(var obj in _objs)
        {
            if(!obj._IsMaintained) return;
        }

        IsActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!IsActive) return;
            StartCoroutine(PatientExitCoroutine());
        }
    }

    private IEnumerator PatientExitCoroutine()
    {
        foreach(var item in _grabber)
        {
            item.enabled = false;
        }
        _player.EnableLinearMovement = false;
        _fc.gameObject.SetActive(false);
        Clipboard.Instance.ChangeNaviText(4);

        yield return new WaitForSeconds(1f);

        // 患者退室
        _fade.FadeOut();
        yield return new WaitForSeconds(2f);

        Destroy(_patient);
        foreach(var obj in _objs)
        {
            obj.gameObject.SetActive(false);
        }
        Destroy(_psm);

        yield return new WaitForSeconds(1f);
        _fade.FadeIn();

        _duvet.CanActivate = true;
        foreach(var item in _sanitaizes)
        {
            item.CanActivate = true;
        }
        _player.EnableLinearMovement = true;
        foreach(var item in _grabber)
        {
            item.enabled = true;
        }
        Destroy(this);
    }
}
