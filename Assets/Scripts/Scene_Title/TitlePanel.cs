using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

namespace NsUnityVr.Title
{
    public class TitlePanel : MonoBehaviour
    {
        [SerializeField] Button _startBtn = null;
        [SerializeField] Button _tuterealBtn = null;

        [SerializeField] Scene _startScene = Scene.Main;
        [SerializeField] Scene _tuterealScene = Scene.Title; // WIP

        void Start()
        {
            _startBtn.onClick.AddListener(() => {
                SEManager.Instance.PlaySE(SE.submit);
                SceneLoader.Instance.LoadTheScene(_startScene);
            });
            _tuterealBtn.onClick.AddListener(() => {
                SEManager.Instance.PlaySE(SE.submit);
                SceneLoader.Instance.LoadTheScene(_tuterealScene);
            });
        }
    }
}