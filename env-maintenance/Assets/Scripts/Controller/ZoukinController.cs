using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoukinController : MonoBehaviour
{
    [SerializeField] GameObject zoukin;
    [SerializeField] GameObject Player;
    Vector3 currentPos;
    Vector3 zoukinSpone;
    bool f;

    void Start()
    {
        f = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        if(f == false){
        Transform current = Player.transform;
        currentPos = current.position;
        zoukinSpone = currentPos + Player.transform.forward*2;
        GameObject newZoukin = Instantiate(zoukin, zoukinSpone, Quaternion.identity); 
        f = true;
        }else{
            GameObject desobj = GameObject.Find("zoukin1(Clone)");
            Destroy(desobj);
            f = false;
        }
    }
}
