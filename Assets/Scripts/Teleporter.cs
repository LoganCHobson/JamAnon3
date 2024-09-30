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
            //gameManager.Teleport();
            other.gameObject.transform.position = destination.position;
            other.gameObject.transform.rotation = destination.rotation;
            onTeleport.Invoke();
        }

        if (death && other.CompareTag("Player"))
        {
            GameManager.instance.Death();
        }
        if(other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
        }

    }
}
