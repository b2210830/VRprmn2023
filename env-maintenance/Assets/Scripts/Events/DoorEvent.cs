using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    [SerializeField] Door _door;
    [SerializeField] GameObject _exitModal;

    // Start is called before the first frame update
    void Start()
    {
        _exitModal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _door.CloseDoor();
            _exitModal.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _exitModal.SetActive(false);
        }
    }
}
