using UnityEngine;

namespace NsUnityVr.Practices
{
    /// <summary>
    /// 福士練習用クラス
    /// </summary>
    public class PracticeFF : MonoBehaviour
    {
        private void Start()
        {
            var text = "Hello World!";
            PrintText(text);
        }

        /// <summary>
        /// 引数の文字列をコンソール表示する
        /// </summary>
        /// <param name="text"> 表示する文字列 </param>
        private void PrintText(string text)
        {
            print(text);
        }
    }
}