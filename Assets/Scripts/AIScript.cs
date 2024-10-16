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

    private WallChecker wallChecker;

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
    private GameManager gameManager = GameManager.instance;
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
    private bool isFlashing = false;
    private float flashDuration = 0.2f;  
    private float flashTimer = 0f;       
    private Texture originalTexture;
    private Color originalColor;
    private MeshRenderer rend;

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

        if (isFlashing)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= flashDuration)
            {
                rend.material.color = originalColor;
                rend.material.mainTexture = originalTexture;
                isFlashing = false;
            }
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
        //Setting Y of Lookat player vector to reduce tilting
        Vector3 playerPosition = player.transform.position;
        playerPosition = new Vector3(playerPosition.x, playerPosition.y-1f, playerPosition.z);

        //anim.SetTrigger("Fire");
        agent.SetDestination(transform.position);
        

        if (!alreadyAttacked)
        {
            if (attackRange > 5)
            {
                transform.LookAt(new Vector3(playerPosition.x, playerPosition.y-1, playerPosition.z));
                GameObject bullet = Instantiate(projectile, firePoint.position, transform.rotation);
                bullet.GetComponent<Bullet>().damage = bulletDamage;
                attack.Invoke();
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
            if (attackRange <= 5)
            {
                transform.LookAt(playerPosition);
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= attackRange)
                {
                    player.GetComponent<Health>().Damage(meleeDamage);

                    if (!player.GetChild(4).GetComponent<WallChecker>().wall)
                    {
                        kbDirection = (player.transform.position - gameObject.transform.position).normalized;
                        kbDirection = new Vector3(kbDirection.x, 0, kbDirection.z);
                        KnockBack(kbDirection);
                        
                    }
                   
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

    public void DamageFlash(MeshRenderer renderer)
    {
        if (!isFlashing)
        {
            rend = renderer;
            originalTexture = rend.material.mainTexture;
            originalColor = rend.material.color;

            rend.material.color = Color.red;
            rend.material.mainTexture = null;

            isFlashing = true;
            flashTimer = 0f;
        }
    }

    public void KnockBack(Vector3 _direction)
    {
        player.GetComponent<Rigidbody>().AddForce(_direction * kbStrength, ForceMode.Impulse);
    }

    public void AddScore(int value)
    {
        ScoreManager.instance.AddScore(value);
    }
}
