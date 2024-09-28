using JetBrains.Annotations;
using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class AIScript : MonoBehaviour
{
    

    public LayerMask whatIsGround, whatIsPlayer;
    public NavMeshSurface surface;

 
    public NavMeshAgent agent;

    private NavMeshPath path;
    public Transform player;

    public Transform enemy;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform firePoint;

    //Utils
    private GameManager gameManager;
    private Health healthScript;
    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    //Values
    public int bulletDamage = 10;
    public int meleeDamage = 4;

    private int lastRun = 0;

    public Vector3 kbDirection;
    public int kbStrength = 5;

    public UnityEvent attack;

    private Animator anim;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        healthScript = GetComponent<Health>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Linerally scaling enemies per run.
        healthScript.maxHealth = Mathf.Min(healthScript.maxHealth + (10 * gameManager.preRunCount), 500);
        bulletDamage = Mathf.Min(bulletDamage + (10 * gameManager.preRunCount), 50);
        meleeDamage = Mathf.Min(meleeDamage + (10 * gameManager.preRunCount), 50);
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, walkPoint, NavMesh.AllAreas, path);

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                walkPointSet = true;
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition = new Vector3(playerPosition.x, playerPosition.y-1f, playerPosition.z);
        anim.SetTrigger("Fire");
        agent.SetDestination(transform.position);
        transform.LookAt(playerPosition);
        if (!alreadyAttacked)
        {
            if (attackRange > 5)
            {
                
                GameObject bullet = Instantiate(projectile, firePoint.position, transform.rotation);
                bullet.GetComponent<Bullet>().damage = bulletDamage;
                attack.Invoke();
                anim.SetTrigger("Fire");
                
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
            if (attackRange <= 5)
            {
                if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= attackRange)
                {
                    player.GetComponent<Health>().Damage(meleeDamage);

                    kbDirection =  (player.transform.position - gameObject.transform.position).normalized;
                    kbDirection = new Vector3(kbDirection.x, 0, kbDirection.z);

                    //player.GetComponent<Rigidbody>().AddForce(30,0,30);
                    player.GetComponent<Rigidbody>().AddForce(kbDirection * kbStrength);
                    attack.Invoke();
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
        }
    }
    private void ResetAttack()
    
    {
        alreadyAttacked = false;
    }

    public void DamageFlash(MeshRenderer rend)
    {
        
        StartCoroutine(FlashCoroutine(rend));
    }

    private IEnumerator FlashCoroutine(MeshRenderer rend)
    {
        Texture origText = rend.material.mainTexture;
        Color originalColor = rend.material.color;
        rend.material.color = Color.red;
        rend.material.mainTexture = null;
        yield return new WaitForSeconds(.2f);
        rend.material.color = originalColor;
        rend.material.mainTexture = origText;
    }
}
