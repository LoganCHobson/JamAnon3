using SolarStudios;
using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class Pickup : MonoBehaviour
{
    public float rotationSpeed = 25f;
    private AudioSource audio;
    private bool used;

    public abstract void onPickupEvent();

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        if(audio == null )
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
        if(!used && other.transform.CompareTag("Player") )
        {
            used = true;
            audio.Play();   
            onPickupEvent();
            Invoke("Delay", 1);
        }
    }

    private void Delay()
    {
        used = false;
        gameObject.SetActive(false);
    }
}

