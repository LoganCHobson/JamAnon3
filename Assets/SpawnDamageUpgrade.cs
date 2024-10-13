using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageUpgrade : MonoBehaviour
{
    public GameObject purchaseTag;
    public GameObject prefab;
    public int price = 100;
    public bool insideCollider = false;
   
    void Start()
    {
        purchaseTag.SetActive(false);
    }

    void Update()
    {
        if (insideCollider == true)
        {
            purchaseTag.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && WalletManager.instance.coin >= price)
            {
                Debug.Log("Player Buys the Health Pack");
                /*Spawn GameObject*/
               GameObject temp = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<DamageUpgrade>().permanent = false;
                WalletManager.instance.coin -= price;
            }
        }
        else
        {
            purchaseTag.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideCollider = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            insideCollider = false;
        }
    }
}
