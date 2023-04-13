using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SponeController : MonoBehaviour
{
    [SerializeField] GameObject sponeObject;
    BoxCollider col;
    [SerializeField] GameObject area1;
    [SerializeField] GameObject area2;
    BoxCollider col1;
    BoxCollider col2;
    Vector3 v1 = new Vector3(0,0,0);
    Vector3 v2 = new Vector3(0,0,0);
    int f;

    void Start()
    {
        col = sponeObject.GetComponent<BoxCollider>();
        RandomCreate();
    }

    void Update()
    {

    }
    private void OnTriggerStay(Collider other) {

    }

    public void RandomCreate(){
        f = Random.Range(1,3);

        switch(f){
            case 1:
                Transform area1transform = area1.transform;
                Vector3 pos1 = area1transform.position;
                col1 = area1.GetComponent<BoxCollider>();
                v1 = col1.size;
                float x1 = Random.Range(pos1.x-v1.x/2,pos1.x+v1.x/2);
                float z1 = Random.Range(pos1.z-v1.z/2,pos1.z+v1.z/2);
                if(sponeObject.tag == "Petbottle"){
                    sponeObject.transform.Rotate(new Vector3(0,50,0));
                }
                sponeObject.transform.position = new Vector3(x1,0,z1);
                break;

            case 2:
                Transform area2transform = area2.transform;
                Vector3 pos2 = area2transform.position;
                col2 = area2.GetComponent<BoxCollider>();
                v2 = col2.size;
                float x2 = Random.Range(pos2.x-v2.x/2,pos2.x+v2.x/2);
                float z2 = Random.Range(pos2.z-v2.z/2,pos2.z+v2.z/2);
                if(sponeObject.tag == "Petbottle"){
                    sponeObject.transform.Rotate(new Vector3(0,50,0));
                }
                sponeObject.transform.position =new Vector3(x2,0,z2);
                break;

        }
    }
}
