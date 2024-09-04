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
public class AIScript : MonoBehaviour
{
    

    public LayerMask whatIsGround, whatIsPlayer;
    public NavMeshSurface surface;

 
    public NavMeshAgent agent;

    private NavMeshPath path;
    public Transform player;

    public Transform enemy;

    public AudioSource audio;
    public AudioSource audio2;

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

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
       
        healthScript = GetComponent<Health>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Linerally scaling enemies per run.
        healthScript.maxHealth = Mathf.Min(healthScript.maxHealth + (10 * gameManager.run), 500);
        bulletDamage = Mathf.Min(bulletDamage + (10 * gameManager.run), 50);
        meleeDamage = Mathf.Min(meleeDamage + (10 * gameManager.run), 50);
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
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            if (attackRange > 5)
            {
                GameObject bullet = Instantiate(projectile, firePoint.position, transform.rotation);
                bullet.GetComponent<Bullet>().damage = bulletDamage;

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                audio.Play();
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

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    audio2.Play();

                }
            }
        }
       


    }
    private void ResetAttack()
    
    {
        alreadyAttacked = false;
    }


}
