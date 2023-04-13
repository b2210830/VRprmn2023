using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] Vector3 _sitPosition = default;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SitDown()
    {
        transform.localPosition = _sitPosition;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
