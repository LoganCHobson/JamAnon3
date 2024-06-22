using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpgrade : MonoBehaviour
{
    public float increaseFireRate = -0.02f;
    public float rotationSpeed = 25f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f,rotationSpeed * Time.deltaTime,0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Gun playersGun = other.GetComponentInChildren<Gun>();
            if(playersGun != null)
            {
                playersGun.fireRate -= increaseFireRate;
            }

            Destroy(gameObject);
        }
    }
}
