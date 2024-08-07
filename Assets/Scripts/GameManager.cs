using SolarStudios;
using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomPool = new List<GameObject>();
    public GameObject player;

    private int preMoney;
    private float preFireRate;
    private int preMaxHealth;
    private int bulletDamage;
    private GameObject gunType;
    private void Start()
    {
        player = GameObject.Find("Player");
        preFireRate = player.GetComponentInChildren<Gun>().fireRate;
        //bulletDamage = player.GetComponentInChildren<Bullet>().damage;
        preMaxHealth = player.GetComponent<Health>().maxHealth;
        preMoney = player.GetComponent<WalletManager>().coin;
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Gun"))
            {
                gunType = child.gameObject;
                break;
            }
        }

    }

    public void ClearRooms()
    {
        foreach (GameObject pool in roomPool)
        {
            pool.GetComponent<ObjectPool>().RecycleAll();
        }
        Debug.Log("Cleared all rooms");
    }

    public void ClearAI()
    {
        WaveSpawner[] WS = FindObjectsOfType<WaveSpawner>();

        foreach (WaveSpawner spawner in WS)
        {
            foreach (GameObject obj in spawner.enemiesSpawned)
            {
                Destroy(obj);
            }
            spawner.enemiesSpawned.Clear();
        }
    }

    public void PlayerReset()
    {
        Debug.Log("Resetting player to prerun");
        player.GetComponentInChildren<Gun>().fireRate = preFireRate;
        player.GetComponent<Health>().maxHealth = preMaxHealth;
        player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;
        player.GetComponent<WalletManager>().coin = preMoney;
        //player.GetComponentInChildren<Bullet>().damage = bulletDamage;
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Gun"))
            {
                child.gameObject.SetActive(false);
                gunType.SetActive(true);
                break;
            }
        }
    }

    public void SavePlayerData()
    {
        Debug.Log("Saving player data!");
        player = GameObject.Find("Player");
        preFireRate = player.GetComponentInChildren<Gun>().fireRate;
        //bulletDamage = player.GetComponentInChildren<Bullet>().damage;
        preMaxHealth = player.GetComponent<Health>().maxHealth;
        player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;
        preMoney = player.GetComponent<WalletManager>().coin;
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Gun"))
            {
                gunType = child.gameObject;
                break;
            }
        }
    }
}
