using SuperPupSystems.Manager;
using UnityEngine;

public class SpawnFireRateUpgrade : MonoBehaviour
{
    public GameObject purchaseTag;
    public GameObject prefab;
    public int price = 100;
    public bool insideCollider = false;

    // Start is called before the first frame update
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
                temp.GetComponent<FireRateUpgrade>().permanent = false;
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
