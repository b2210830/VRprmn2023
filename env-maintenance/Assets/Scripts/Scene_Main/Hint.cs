using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hint
{
    Dictionary<MaintenanceType, string> _hintDict = new Dictionary<MaintenanceType, string>
    {
        {MaintenanceType.None, ""},
        {MaintenanceType.Bed_clean, "ベッドの上は清潔になっていますか？"},
        {MaintenanceType.Bed_maintenance, "ベッドの上は整っていますか？"},
        {MaintenanceType.Patient_Obstacle, "患者さんが歩く場所に障害物はありませんか？"},
        {MaintenanceType.Warn_env, "転倒の危険が考えられる環境の状態はありませんか？"},
        {MaintenanceType.Bed_Obstacle, "ベッドの周囲に障害になる物はありませんか？"},
        {MaintenanceType.Check_position, "定位置に置かれていますか？"},
        {MaintenanceType.Patient_env, "患者さんが歩行する際、安全な環境になっていますか？"},
        {MaintenanceType.Bed_lock, "ベッドは動かないよう安全が確保できていますか？"},
        {MaintenanceType.Check_trash, "不要なごみや物などはありませんでしたか？"},
        {MaintenanceType.Meter, "快適な室内環境になっていますか？"},
    };

    /// <summary>
    /// ヒントメッセージを取得する
    /// </summary>
    /// <param name="type">整備項目のタイプ</param>
    /// <returns>対応するメッセージ</returns>
    public string GetHintMessage(MaintenanceType type)
    {
        return _hintDict[type];
    }
}
