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

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
   
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

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                audio.Play();
            }
            if (attackRange <= 5)
            {
                if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= attackRange)
                {
                    player.GetComponent<Health>().Damage(4);
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
