using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireRateUpgrade : MonoBehaviour
{
    public float increaseFireRate = -0.02f;
    public float rotationSpeed = 25f;

    public UnityEvent pickup;
    private AudioSource audio;

    // Update is called once per frame

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        gameObject.transform.Rotate(0f,rotationSpeed * Time.deltaTime,0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            pickup.Invoke();
            Gun playersGun = other.GetComponentInChildren<Gun>();
            if(playersGun != null)
            {
                playersGun.fireRate -= increaseFireRate;
            }

            gameObject.SetActive(false);
        }
    }
}
