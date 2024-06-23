using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class pickup : MonoBehaviour
{
    public int value;
    public float rotationSpeed = 25f;

    void Update()
    {
        gameObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
                WalletManager.instance.AddCoin(value);
            Destroy(gameObject);
        }
    }
}
