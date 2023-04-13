using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NsUnityVr.Systems
{
    /// <summary>
    /// 遷移可能なシーン
    /// </summary>
    public enum Scene
    {
        Title,
        Main
    }

    /// <summary>
    /// シーンをロードするクラス
    /// > SceneLoader.Instance.LoadTheScene(scene: Scene.hoge) でシーン遷移イベント発生可
    /// </summary>
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        Dictionary<Scene, string> _sceneDictionary;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);

            // シーン辞書を初期化
            _sceneDictionary = new Dictionary<Scene, string> {
                {Scene.Title, "TitleScene"},
                {Scene.Main, "MainScene"}
            };
        }

        /// <summary>
        /// 引数のシーンをロードする
        /// </summary>
        /// <param name="scene"> シーン型シーン名 </param>
        public void LoadTheScene(Scene scene)
        {
            var sceneName = _sceneDictionary[scene];
            SceneManager.LoadScene(sceneName);
        }
    }
}