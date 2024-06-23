using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Manager;

public class FullyAuto : MonoBehaviour
{
    public GameObject purchaseTag;
    public int price = 100;
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
                Debug.Log("Player Buys The Fully Auto");
                SwitchGuns(GameObject.FindGameObjectWithTag("Player").transform, "FullyAutoGun");
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

    private void SwitchGuns(Transform playerTransform, string gunName)
    {
        Transform gunHolder = playerTransform.Find("PlayerCamera/GunHolder");
        if (gunHolder != null)
        {
            foreach (Transform gun in gunHolder)
            {
                gun.gameObject.SetActive(false);
            }

            Transform purchasedGun = gunHolder.Find(gunName);
            if (purchasedGun != null)
            {
                purchasedGun.gameObject.SetActive(true);
            }
            Debug.Log("Has Found the Gun " + gunName);
        }else
        {
            Debug.Log("gunHolder returned Null");
        }
    }
}
