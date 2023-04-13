using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.CognitiveServices.Speech;
using System;
public class TextToSpeech : MonoBehaviour
{
    SpeechConfig config;

    string speechSynthesisLanguage = "ja-JP";
    string subscriptionKey = "cda966b7b15a4433b63113054af1c2d1";

    string region = "eastasia";

    string speakText = "";


    // Start is called before the first frame update
    async void Start()
    {
          //TextToSpeechの設定
        config = SpeechConfig.FromSubscription(subscriptionKey, region);
        config.SpeechSynthesisLanguage = speechSynthesisLanguage; //話す言葉の設定

        //ノイズ処理のコード（省略）

        using (var synthesizer = new SpeechSynthesizer(config, null))
            {
            var result = await synthesizer.SpeakTextAsync(speakText);

            //取得した音声をaudio sourceコンポーネントで再生する
            var audioSource = gameObject.AddComponent<AudioSource>();
            var sampleCount = result.AudioData.Length / 2;
            var audioData = new float[sampleCount];

            for (var i = 0; i < sampleCount; ++i)
            {
                audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
            }
            var audioClip = AudioClip.Create("SynthesizeAudio", sampleCount, 1, 16000, false);
            audioClip.SetData(audioData, 0);
            audioSource.clip = audioClip;
            audioSource.Play();

            Debug.Log("success");
            }
    }
}
