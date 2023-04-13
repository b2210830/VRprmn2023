using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoukinButton : MonoBehaviour
{
    [SerializeField] GameObject zoukin;
    [SerializeField] GameObject Player;
    Transform zoukinStart;
    Vector3 startPos;
    Vector3 zoukinStartPos;
    Vector3 saveZoukin;
    Vector3 currentPos;
    bool flag;
    void Start()
    {
        flag = false;
        zoukin.SetActive(flag);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("成功1");
        if(flag == true){
         Transform current = Player.transform;
         currentPos = current.position;
         Vector3 move = currentPos - startPos;
         zoukinStartPos = saveZoukin;
         zoukinStartPos += move;
         zoukin.transform.position = zoukinStartPos;
         Debug.Log("成功2");
        }
    }

    public void onClick(){
            Debug.Log(1);
            flag = !flag;
            zoukin.SetActive(flag);
            if(flag == true){
                //ボタンを押したときのプレイヤーの位置
                Transform start = Player.transform;
                //はじめのプレイヤーの座標
                startPos = start.position;
                //ボタンを押したときの雑巾の位置
                zoukinStart = zoukin.transform;
                //はじめの雑巾の座標
                zoukinStartPos = zoukinStart.position;
                saveZoukin = zoukinStartPos;

    }
    }
}
