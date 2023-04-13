using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

namespace NsUnityVr.Debugs
{
    /// <summary>
    /// シーン遷移のデバッグクラス
    /// </summary>
    public class DebugLoadScene : MonoBehaviour
    {
        [SerializeField, Header("遷移先のシーン")] Scene _targetScene = Scene.Title;

        private void Start()
        {
            // アタッチしたオブジェクトがUIボタンのとき、ボタンイベントを追加
            var button = GetComponent<Button>();
            if(button)
            {
                button.onClick.AddListener(() => {
                    print(gameObject.name + " を押したよ!");
                    OnInputButton();
                });
            }
        }

        /// <summary>
        /// ボタンが押されたときのイベント
        /// </summary>
        public void OnInputButton()
        {
            SceneLoader.Instance.LoadTheScene(scene: _targetScene);
        }
    }
}