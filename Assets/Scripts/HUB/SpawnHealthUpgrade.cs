using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Manager;

public class SpawnHealthUpgrade : MonoBehaviour
{
    public GameObject purchaseTag;
    public GameObject prefab;
    public int price = 200;
    public bool insideCollider = false;
    
    // Start is called before the first frame update
    void Start()
    {
        purchaseTag.SetActive(false);
    }

    void Update()
    {
        if(insideCollider == true)
        {
            purchaseTag.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && WalletManager.instance.coin >= price)
            {
                Debug.Log("Player Buys the Health Pack");
                /*Spawn GameObject*/
                GameObject tmp = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
                tmp.GetComponent<MaxHealthUpgrade>().permanent = true;
                WalletManager.instance.coin -= price;
            }
        }else
        {
            purchaseTag.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            insideCollider = true;
        }
    }

    public void OnTriggerExit(Collider other) 
    { 
        if(other.gameObject.CompareTag("Player"))
        {
            insideCollider = false;
        }
    }
}
