using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

namespace NsUnityVr.Debugs
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] Text _text = null;
        [SerializeField] Button _btn = null;
        int _count = 0;

        [SerializeField] Scene _nextScene = Scene.Title;

        void Start()
        {
            _text.text = _count.ToString();

            _btn.onClick.AddListener(() => {
                SEManager.Instance.PlaySE(SE.submit);
                _count++;
                _text.text = _count.ToString();
                SceneLoader.Instance.LoadTheScene(_nextScene);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}