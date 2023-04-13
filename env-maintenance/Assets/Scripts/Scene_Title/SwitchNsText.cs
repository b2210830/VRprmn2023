using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchNsText : MonoBehaviour
{
    [SerializeField] GameObject _startText;
    [SerializeField] GameObject _tuterealText;

    public void ActivateStartText()
    {
        _tuterealText.SetActive(false);
        _startText.SetActive(true);
    }

    public void ActivateTuterealText()
    {
        _startText.SetActive(false);
        _tuterealText.SetActive(true);
    }
}
