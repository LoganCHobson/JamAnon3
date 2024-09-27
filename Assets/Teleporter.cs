using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public UnityEvent onTeleport;
    private GameManager gameManager = GameManager.instance;

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
                GameObject.Find("TeleporterToHub").GetComponent<Teleporter>().onTeleport.Invoke();
                gameManager.PlayerReset();

            
        }
    }
}
