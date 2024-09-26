using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public UnityEvent onTeleport;

    public bool death = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !death)
        {
            Debug.Log("Teleported");
            other.gameObject.transform.position = destination.position;
            other.gameObject.transform.rotation = destination.rotation;
            onTeleport.Invoke();
        }

        if(death && other.CompareTag("Player"))
        {
            Debug.Log("Died");
                GameObject.Find("Teleporter").GetComponent<Teleporter>().onTeleport.Invoke();
                //other.gameObject.transform.position = destination.position;
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerReset();

            
        }
    }
}
