using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardUI : MonoBehaviour
{
    public Camera _camera;

    private void Start()
    {
        if(_camera == null)
        {
            _camera = Camera.main;
        }
    }
    void LateUpdate()
    {
        //　カメラと同じ向きに設定
        transform.rotation = _camera.transform.rotation;
    }
}
