using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveChange : MonoBehaviour
{

    public Material[] material;
    private int a;
    [SerializeField]GameObject HandR;
    [SerializeField]GameObject HandL;
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        HandR.GetComponent<Renderer>().material = material[a];
        HandL.GetComponent<Renderer>().material = material[a];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick(){
        if(a == 0){
            a = 1;
        }
        else{
            a = 0;
        }
        HandR.GetComponent<Renderer>().material = material[a];
        HandL.GetComponent<Renderer>().material = material[a];
    }
}
