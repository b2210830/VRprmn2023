using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NsUnityVr.Systems;

namespace NsUnityVr.Controller
{
    public class ExitModal : MonoBehaviour
    {
        [SerializeField] Button _yesBtn = null;
        [SerializeField] Button _noBtn = null;
        [SerializeField] GameObject _player = default;
        [SerializeField] Vector3 _defaultPlayerPosition = default;

        // Start is called before the first frame update
        void Start()
        {
            _yesBtn.onClick.AddListener(() => MoveToResultState());
            _noBtn.onClick.AddListener(() => CloseExitModal());
        }

        /// <summary>
        /// YES→採点/終了ステートに遷移する
        /// </summary>
        private void MoveToResultState()
        {
            _player.transform.position = _defaultPlayerPosition;
            _player.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            var state = GameManager.Instance.CurrentGameState.Value;
            if(state != GameState.Result)
            {
                GameManager.Instance.CurrentGameState.Value = GameState.Result;
            }
            else
            {
                GameManager.Instance.CurrentGameState.Value = GameState.End;
            }
        }

        private void CloseExitModal()
        {
            gameObject.SetActive(false);
        }
    }
}
