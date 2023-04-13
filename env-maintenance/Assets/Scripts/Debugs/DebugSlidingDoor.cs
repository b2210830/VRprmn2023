using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NsUnityVr.Systems;

namespace NsUnityVr.Debugs
{
    /// <summary>
    /// 病室のドア開閉のデバッグクラス
    /// </summary>
    public class DebugSlidingDoor : MonoBehaviour
    {
        Vector3 _defaultPosition;
        [SerializeField] Vector3 _opendPosition = Vector3.zero;
        [SerializeField] float _openSec = 1f;
        [SerializeField] float _openWaitSec = 3f;
        float _secCounter = 0f;
        bool _isOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            _defaultPosition = this.transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if(_isOpen)
            {
                _secCounter += Time.deltaTime;
                if(_secCounter > _openWaitSec)
                {
                    SlideDoor(_defaultPosition);
                    _secCounter = 0;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                SlideDoor(_opendPosition);
            }
        }

        void SlideDoor(Vector3 targetPos)
        {
            SEManager.Instance.PlaySE(SE.door);
            var isOpen = (targetPos == _opendPosition) ? true : false;
            transform.DOLocalMove(targetPos, _openSec)
                .OnComplete(() => {
                    _isOpen = isOpen;
                });
        }
    }
}