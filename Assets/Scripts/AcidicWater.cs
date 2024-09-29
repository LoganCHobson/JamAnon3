using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class AcidicWater : MonoBehaviour
{
    public int damage = 2;
    public float rateOfDamage = 2f;  

    private Health health;
    private float damageTimer = 0f;  
    private bool playerInWater = false;

    private AudioSource audio;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health = other.GetComponent<Health>();
            playerInWater = true; 
            damageTimer = 0f; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInWater = false; 
        }
    }

    private void Update()
    {
        if (playerInWater && health != null)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= rateOfDamage)
            {
                audio.Play();
                health.Damage(damage);   
                damageTimer = 0f;       
            }
        }
    }
}
