using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class pickup : MonoBehaviour
{
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
           
                WalletManager.instance.AddCoin(value);
           
        }
    }
}
