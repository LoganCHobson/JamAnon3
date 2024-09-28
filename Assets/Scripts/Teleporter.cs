using UnityEngine;
using UnityEngine.Events;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public UnityEvent onTeleport;
    private GameManager gameManager = GameManager.instance;

    public bool death = false;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !death)
        {
            gameManager.Teleport();
            other.gameObject.transform.position = destination.position;
            other.gameObject.transform.rotation = destination.rotation;
            onTeleport.Invoke();
        }

        if (death && other.CompareTag("Player"))
        {
            gameManager.Teleport();
            GameObject.Find("TeleporterToHub").GetComponent<Teleporter>().onTeleport.Invoke();
            gameManager.PlayerReset();
        }

    }
}
