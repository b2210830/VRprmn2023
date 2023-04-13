using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUserGuide : MonoBehaviour
{
    private bool _counting = false;
    private float _sec = 1f;
    private float _currentSec = 0f;

    private void Update()
    {
        if(!_counting) return;

            UserGuide.Instance.Show("アクションする", OVRInput.Button.One);
        if(_currentSec >= _sec)
        {
                    print("ACTION------------------------");

        }
        _currentSec += Time.deltaTime;
    }

    public void OnPointerEnter()
    {
        // ガイド表示
        _counting = true;
    }

    public void OnPointerExit()
    {
        // ガイド表示を破棄
        _counting = false;
        _currentSec = 0f;
        UserGuide.Instance.DeleteUserGuidePanel();
    }
}
