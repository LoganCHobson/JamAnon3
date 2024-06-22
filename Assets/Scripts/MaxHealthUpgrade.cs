using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MaxHealthUpgrade : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public int increaseHealth = 10;
    void Update()
    {
        gameObject.transform.Rotate(0f,rotationSpeed * Time.deltaTime,0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.maxHealth += increaseHealth;
                playerHealth.currentHealth = playerHealth.maxHealth;
            }

            Destroy(gameObject);
        }
    }
}
