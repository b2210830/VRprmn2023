using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NsUnityVr.Systems
{
    /// <summary>
    /// サウンドエフェクト
    /// </summary>
    public enum SE
    {
        submit,
        door,
        kira,
        curtain,
        tenteki,
        turnOn,
        polish,
        putting_L,
        putting_S,
        sheets,
        knock,
        shodoku,
        zoukin,
    }

    public class SEManager : SingletonMonoBehaviour<SEManager>
    {
        [SerializeField] AudioClip _submit = null;
        [SerializeField] AudioClip _door = null;
        [SerializeField] AudioClip _kira = null;
        [SerializeField] AudioClip _curtain = null;
        [SerializeField] AudioClip _tenteki = null;
        [SerializeField] AudioClip _turnOn = null;
        [SerializeField] AudioClip _polish = null;
        [SerializeField] AudioClip _putting_L = null;
        [SerializeField] AudioClip _putting_S = null;
        [SerializeField] AudioClip _sheets = null;
        [SerializeField] AudioClip _knock = null;
        [SerializeField] AudioClip _shodoku = null;
        [SerializeField] AudioClip _zoukin = null;

        AudioSource _as;
        Dictionary<SE, AudioClip> _seDictionary;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);

            _as = GetComponent<AudioSource>();
            // SE辞書を初期化
            _seDictionary = new Dictionary<SE, AudioClip> {
                {SE.submit, _submit},
                {SE.door, _door},
                {SE.kira, _kira},
                {SE.curtain, _curtain},
                {SE.tenteki, _tenteki},
                {SE.turnOn, _turnOn},
                {SE.polish, _polish},
                {SE.putting_L, _putting_L},
                {SE.putting_S, _putting_S},
                {SE.sheets, _sheets},
                {SE.knock, _knock},
                {SE.shodoku, _shodoku},
                {SE.zoukin, _zoukin}
            };
        }

        /// <summary>
        /// 引数のSEを再生する
        /// </summary>
        /// <param name="se"> SE型SE名 </param>
        public void PlaySE(SE se)
        {
            _as.PlayOneShot(_seDictionary[se]);
        }
    }
}