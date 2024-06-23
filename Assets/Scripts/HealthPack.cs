using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class HealthPack : MonoBehaviour
{
    private AudioSource audio;
    public float rotationSpeed = 10f;

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
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                audio.Play();
                playerHealth.Heal(15);
            }

            gameObject.SetActive(false);
        }
    }
}
