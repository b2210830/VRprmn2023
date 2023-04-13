using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NsUnityVr.Systems;

namespace NsUnityVr.Controller
{
    public class MenuManager : MonoBehaviour
    {
        [Tooltip("Gamepad button to act as gaze click")]
        // public OVRInput.Button joyPadClickButton = OVRInput.Button.One;

        [SerializeField] GameObject _menuPanel = null;
        [SerializeField] Button _restartBtn = null;
        [SerializeField] Button _exitBtn = null;
        [SerializeField] SimpleCapsuleWithStickMovement _player = null;

        // Start is called before the first frame update
        void Start()
        {
            _restartBtn.onClick.AddListener(() => SwitchMenuActive());
            _exitBtn.onClick.AddListener(() => SceneLoader.Instance.LoadTheScene(Scene.Title));
        }

        // Update is called once per frame
        // void Update()
        // {
        //     if(OVRInput.GetDown(joyPadClickButton))
        //     {
        //         SwitchMenuActive();
        //     }
        // }

        public void SwitchMenuActive()
        {
            var active = _menuPanel.activeSelf;
            _player.EnableLinearMovement = active;
            _menuPanel.SetActive(!active);
        }
    }
}