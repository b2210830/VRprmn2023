using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NsUnityVr.Systems
{
    /// <summary>
    /// ゲームステート
    /// </summary>
    public enum GameState
    {
        Ready,
        Main,
        Result,
        End
        // 仮内容
    }

    /// <summary>
    /// Mainシーンのゲームマネージャ
    /// </summary>
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        /// <summary> 現在のステート </summary>
        ReactiveProperty<GameState> _currentGameState;
        public ReactiveProperty<GameState> CurrentGameState {
            set { _currentGameState.Value = value.Value; }
            get { return _currentGameState; }
        }

        [SerializeField] LoadingModal _loadingModal = default;
        [SerializeField] OVRScreenFade _fade = default;
        [SerializeField] FollowCanvas _fc;
        [SerializeField] Curtain _ct;

        protected override void Awake()
        {
            base.Awake();

            var defaultGameState = GameState.Ready;
            _currentGameState = new ReactiveProperty<GameState>(defaultGameState);
            _currentGameState.Subscribe(x => GameStateEvent(x)); // _currentGameStateの値が更新されるたび呼ばれる
        }

        /// <summary>
        /// 引数のステートごとにイベントを呼ぶ
        /// </summary>
        /// <param name="state"> ゲームステート </param>
        private void GameStateEvent(GameState state)
        {
            switch(state)
            {
                case GameState.Ready: Ready(); break;
                case GameState.Main: MainState(); break;
                case GameState.Result: Result(); break;
                case GameState.End: End(); break;
            }
        }

        /// <summary>
        /// 準備ステートのとき行う
        /// </summary>
        private void Ready()
        {
            print("Ready.....");
            /*
            他のマネージャの初期化とか
            */
        }

        /// <summary>
        /// メインステートのとき行う
        /// </summary>
        private void MainState()
        {
            print("演習開始！");
            Clipboard.Instance.ChangeNaviText(0);
        }

        /// <summary>
        /// 採点（結果発表）ステートのとき行う
        /// </summary>
        private void Result()
        {
            _fade.FadeIn();
            _loadingModal.gameObject.SetActive(true);
            // 整備箇所の上部に〇×マーク、コメントを表示する
            MaintenanceManager.Instance.SetUpResultState();
            Clipboard.Instance.ChangeNaviText(5);
            _fc.gameObject.SetActive(false); // 発話/一時停止ボタンを見えなくする
            _ct.OpenCurtainForce();
        }

        /// <summary>
        /// 終了（フィードバック）ステートのとき行う
        /// </summary>
        private void End()
        {
            _fade.FadeIn();
            _loadingModal.gameObject.SetActive(true);
            Clipboard.Instance.gameObject.SetActive(false); // ボードを見えなくする
        }
    }
}