using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UniRx;


// [System.Serializable]
// 患者の返答クラス
public class PatientReply : MonoBehaviour
{
    [SerializeField] Patient _patient;
    [SerializeField] Animator _animator = default;
    [SerializeField] OVRScreenFade _fade;
    [SerializeField] OVRGrabbable _tenteki;
    [SerializeField] Curtain _curtain;

    // "ユーザの発話内容", "患者の返答"
    public Dictionary<string[], string[]> _replyDict = new Dictionary<string[], string[]>
    {
        {
            new string[]{"(い|な|ね|の|よ|ょ)？"},
            new string[]{"..."}
        },
        {
            new string[]{"よろしくお願いします"},
            new string[]{"こちらこそよろしくね"}
        },
        {
            new string[]{"おはようございます"},
            new string[]{"おはよう"}
        },
        {
            new string[]{"こんにちは"},
            new string[]{"こんにちは"}
        },
        {
            new string[]{"こんばんは"},
            new string[]{"こんばんは"}
        },
        {
            new string[]{"カーテン", "開", "か？"},
            new string[]{"いいよ"}
        },
        {
            new string[]{"(調子|体調|具合)", "(いかが|大丈夫|どう)", "か？"},
            new string[]{"大丈夫だよ", "元気だよ"}
        },
        {
            new string[]{"暗","か"},
            new string[]{"問題ないよ"}
        },
        {
            new string[]{"ベッド", "整", "か？"},
            new string[]{"それならデイルームに行ってこようかな"}
        },
    };

    public string GetReply(string question)
    {
        var reply = "";

        // 発話内容に対する返事を辞書から検索
        var judge = false;
        foreach(var key in _replyDict.Keys)
        {
            foreach(var item in key)
            {
                judge = Regex.IsMatch(question, item);
                if(!judge) break;
            }

            if(judge)
            {
                var replyArray = _replyDict[key];
                var rnd = Random.Range(0, replyArray.Length);
                reply = replyArray[rnd];
                break;
            }
        }

        if(reply != "") SetAction(reply);
        return reply;
    }

    /// <summary>
    /// 3dモデルの頭部の座標を取得する
    /// </summary>
    /// <returns>3dモデルの頭部の座標</returns>
    public Vector3 GetHeadPosition()
    {
        return _animator.GetBoneTransform(HumanBodyBones.Head).position;
    }

    /// <summary>
    /// アニメーションを反映する
    /// </summary>
    /// <param name="reply">返答内容</param>
    private void SetAction(string reply)
    {
        switch(reply)
        {
            case "いいよ":
                _curtain.CanOpen.Value = true;
                break;

            case "それならデイルームに行ってこようかな":
                SitDownTask();
                _tenteki.allowOffhandGrab = true; // 点滴をつかめるように
                Clipboard.Instance.ChangeNaviText(3);
                break;
        }
    }

    private async Task SitDownTask()
    {
        await StartCoroutine(SitDownCoroutine());

        _fade.FadeIn();
    }

    private IEnumerator SitDownCoroutine()
    {
        yield return new WaitForSeconds(1f);

        _fade.FadeOut();

        yield return new WaitForSeconds(2f);

        _animator.SetBool("Sitting_chair", true);
        _patient.SitDown();

        yield return new WaitForSeconds(1f);
    }
}
