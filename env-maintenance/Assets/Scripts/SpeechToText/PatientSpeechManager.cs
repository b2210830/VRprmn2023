using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Microsoft.CognitiveServices.Speech;

using System;

public class PatientSpeechManager : MonoBehaviour
{
    [SerializeField] PatientReply _patientReply = new PatientReply();
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _speechText;
    private Text _text;


    string subscriptionKey = "cda966b7b15a4433b63113054af1c2d1";

    string serviceResion = "eastasia";

    string speechSynthesisLanguage = "ja-JP";
    string speechName = "ja-JP-KeitaNeural";

    [SerializeField] float _speechSec = .5f;
    [SerializeField] string _defaultReply = "";
    [SerializeField] Vector3 _offset = default;
    // [SerializeField] AudioSource _audioSource = default;

    [SerializeField] AudioSource _audioSource;
    private float _timer = 0f;
    private float _hideTime = 5f;

    void Start()
    {
        _text = _speechText.transform.Find("ResultText").GetComponent<Text>();
        HideText();
    }

    void Update()
    {
        // テキストは5秒後に非表示にする
        if (_speechText.activeSelf)
        {
            _timer += Time.deltaTime;
            if(_timer > _hideTime)
            {
                HideText();
            }
        } else _timer = 0f;

        // オブジェクトを頭部座標まで移動する
        var headPos = _patientReply.GetHeadPosition();
        _canvas.transform.position = headPos + _offset;
    }

    public async void Reply(string playerSpeech)
    {
        HideText();

        var speechConfig = SpeechConfig.FromSubscription(subscriptionKey, serviceResion);
        speechConfig.SpeechSynthesisLanguage = speechSynthesisLanguage;
        speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw16Khz16BitMonoPcm);
        var synthesizer = new SpeechSynthesizer(speechConfig, null);

        // var patientReply = new PatientReply();
        var reply = _patientReply.GetReply(playerSpeech);
        if(reply == "") reply = _defaultReply;


        var result = await synthesizer.SpeakTextAsync(reply);
        Debug.Log("result.reason : " + result.Reason);
        OutputSpeechSynthesisResult(result);

        var audioSource = gameObject.AddComponent<AudioSource>();
        var audioData = new float[result.AudioData.Length / 2];
        for (var i = 0; i < result.AudioData.Length/2; ++i)
        {
            audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
        }

        var audioClip = AudioClip.Create("SynthesizedAudio", result.AudioData.Length/2, 1, 16000, false);
        audioClip.SetData(audioData, 0);
        audioSource.clip = audioClip;
        audioSource.Play();
        ShowText(reply);
    }

    public void ShowText(string txt)
    {
        var beforeTxt = _text.text;
        _text.DOText(txt, _speechSec)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                var currentTxt = _text.text;
                if(currentTxt == beforeTxt) return;

                // _audioSource.Play();
                beforeTxt = currentTxt;
            });
        _speechText.SetActive(true);
    }

    public void HideText()
    {
        _speechText.SetActive(false);
        _text.text = "";
    }

    public void OutputSpeechSynthesisResult(SpeechSynthesisResult result)
    {
        switch (result.Reason)
                {
                    case ResultReason.SynthesizingAudioCompleted:
                        Debug.Log(result);
                        break;
                    case ResultReason.Canceled:
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        // Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
                        Debug.Log("CANCELED: Reason = " + cancellation.Reason);

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            // Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            // Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Debug.Log("CANCELED: ErrorCode = " + cancellation.ErrorCode);
                            Debug.Log("CANCELED: ErrorDetails = " + cancellation.ErrorDetails);
                        }
                        break;
                    default:
                        break;
                }
        }
}