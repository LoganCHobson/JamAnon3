using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using UnityEngine.Events;

public class MaxHealthUpgrade : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public int increaseHealth = 10;
    public UnityEvent pickup;
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
            if(playerHealth != null)
            {
                playerHealth.maxHealth += increaseHealth;
                playerHealth.currentHealth = playerHealth.maxHealth;
            }

            gameObject.SetActive(false);
        }
    }
}
