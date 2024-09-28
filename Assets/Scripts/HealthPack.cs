using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using UnityEngine.Events;

public class HealthPack : MonoBehaviour
{
    private AudioSource audio;
    public float rotationSpeed = 10f;

    public UnityEvent pickup;

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
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                audio.Play();
                playerHealth.Heal(playerHealth.maxHealth/2);
            }

            gameObject.SetActive(false);
        }
    }
}
