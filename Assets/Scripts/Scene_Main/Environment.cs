using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NsUnityVr.Systems;

public class Environment : MonoBehaviour
{
    [SerializeField, Range(0.05f, 1f)] public float Brightness = 1f;
    private float _currentBrightness = 1f;
    [SerializeField] List<Light> _lights = new List<Light>();

    void Start()
    {

    }

    void Update()
    {
        ControlBrightness();
    }

    /// <summary>
    /// ライトの明るさを制御する
    /// </summary>
    private void ControlBrightness()
    {
        if(Brightness != _currentBrightness)
        {
            _currentBrightness = Brightness;
            foreach(var light in _lights)
            {
                light.intensity = _currentBrightness;
            }
        }
    }

    /// <summary>
    /// ライト点灯/消灯スイッチ
    /// </summary>
    public void TurnOn()
    {
        Brightness = 1f;
        SEManager.Instance.PlaySE(SE.turnOn);
    }

    public void TurnOff()
    {
        Brightness = .05f;
        SEManager.Instance.PlaySE(SE.turnOn);
    }
}
