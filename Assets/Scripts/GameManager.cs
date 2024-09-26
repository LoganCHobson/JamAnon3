using SolarStudios;
using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ObjectPool> roomPool = new List<ObjectPool>();
    public GameObject player;

    private int preMoney;
    private float preFireRate;
    private int preMaxHealth;
    private int bulletDamage;
    [HideInInspector]
    public int preRunCount;
    private GameObject gunType;

    private RunCounter runCounter;


    public Transform spawn;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

       
    }

    private void Start()
    {
        runCounter = player.GetComponentInChildren<RunCounter>();

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

        if (roomPool.Count <= 0)
        {
            GameObject obj = GameObject.Find("ObjectPoolMaster");
            foreach (Transform pool in obj.transform)
            {
                roomPool.Add(pool.gameObject.GetComponent<ObjectPool>());
            }
        }


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
        runCounter.runCounter = preRunCount;
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
        player.transform.position = spawn.position;

        ClearAI();
    }

    public void SavePlayerData()
    {
        preRunCount = runCounter.runCounter;
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
