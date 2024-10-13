using SolarStudios;
using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<ObjectPool> roomPool = new List<ObjectPool>();
    public GameObject player;

    private int preScore;
    private int preMoney;
    private float preFireRate;
    private int preMaxHealth;
    private int preDamage;
    [HideInInspector]
    public int preRunCount;
    private GameObject gunType;

    [HideInInspector]
    public RunCounter runCounter;

    public Transform spawn;

    public static GameManager instance;

    private Animator playerAnim;

    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        preScore = ScoreManager.instance.score;
        playerAnim = player.GetComponentInChildren<Animator>();
        runCounter = player.GetComponentInChildren<RunCounter>();

        player = GameObject.Find("Player");
        preFireRate = player.GetComponentInChildren<Gun>().fireRate;
        preDamage = player.GetComponentInChildren<Gun>().damagePerShot;
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

    public void PlayerReset()
    {
        ScoreManager.instance.score = preScore;
        player.GetComponentInChildren<Slider>().value = preMaxHealth;
        Debug.Log("Resetting player to prerun");
        runCounter.runCounter = preRunCount;
        player.GetComponentInChildren<Slider>().value = preMaxHealth;
        player.GetComponentInChildren<Gun>().fireRate = preFireRate;
        player.GetComponent<Health>().maxHealth = preMaxHealth;
        player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;
        player.GetComponent<WalletManager>().coin = preMoney;
        player.GetComponentInChildren<Gun>().damagePerShot = preDamage;
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
    }

    public void SavePlayerData()
    {
        preScore = ScoreManager.instance.score;
        preRunCount = runCounter.runCounter;
        Debug.Log("Saving player data!");
        player = GameObject.Find("Player");
        preFireRate = player.GetComponentInChildren<Gun>().fireRate;
        preDamage = player.GetComponentInChildren<Gun>().damagePerShot;
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

    public void ClearRooms()
    {
        foreach (ObjectPool pool in roomPool)
        {
            pool.RecycleAll();
        }
        Invoke("SpawnerCorrect", 1);
    }

    public void Teleport()
    {
        StartCoroutine(TeleportSequence());
    }

    private IEnumerator TeleportSequence()
    {
        playerAnim.Play("TeleportEffect");
        TogglePlayerControl(false);
        yield return new WaitForSeconds(2);
        playerAnim.Play("TeleportEffectReverse");
        yield return new WaitForSeconds(2);
        TogglePlayerControl(true);
    }

    private void TogglePlayerControl(bool enable)
    {
        player.GetComponent<PlayerMovement>().enabled = enable;
        player.GetComponentInChildren<MouseLook>().enabled = enable;
    }

    public void Death()
    {
        PlayerReset();
        ClearRooms();
    }
    public void SpawnerCorrect()
    {
        spawner1.SetActive(true);
        spawner2.SetActive(true);
        spawner3.SetActive(true);
        spawner4.SetActive(true);

    }
}

