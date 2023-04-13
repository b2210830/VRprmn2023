using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

public class Knock : MonoBehaviour
{
    [SerializeField] Slider _sd;

    private void Start()
    {
        if(GameManager.Instance.CurrentGameState.Value == GameState.Result) return;
        _sd.interactable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Knock")
        {
            SEManager.Instance.PlaySE(SE.knock);
            _sd.interactable = true;
        }
    }
}
