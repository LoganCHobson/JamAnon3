using UnityEngine;
public abstract class Pickup : MonoBehaviour
{
    public float rotationSpeed = 25f;
    private AudioSource pickupAudio;
    private bool used;
    [HideInInspector]
    public bool permanent = true;

    public abstract void onPickupEvent();

    private void Start()
    {
        pickupAudio = GetComponent<AudioSource>();
        if (pickupAudio == null)
        {
            Debug.LogError("Did you forget to add audio to your pickup?");
        }
    }
    void Update()
    {
        gameObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!used && other.transform.CompareTag("Player"))
        {
            used = true;
            if(pickupAudio == null)
            {
                pickupAudio = GetComponent<AudioSource>();
            }
            pickupAudio.Play();
            onPickupEvent();

            if (permanent)
            {
                Invoke("Delay", 1);
            }
            else //Remove loot related pickups.
            {
                Destroy(gameObject, 1);
            }
        }


        /* if (gameObject.transform.parent == null) //Adding non perma pickups to a list.
         {
             if (other.gameObject.layer == 7)
             {
                 other.gameObject.GetComponent<RoomManager>().temporaryObjs.Add(gameObject);
                 gameObject.transform.SetParent(other.gameObject.transform);
             }
         }*/
    }

    private void Delay()
    {
        used = false;
        gameObject.SetActive(false);
    }
}


