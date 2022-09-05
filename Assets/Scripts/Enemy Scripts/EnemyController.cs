using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator ZombieEnemyAnimator;
    [SerializeField] private string IsIdling = "IsIdling";
    [SerializeField] private string IsRunning = "IsRunning";
    [SerializeField] private string IsWalking = "IsWalking";
    [SerializeField] private string IsAttacking = "IsAttacking";
    [SerializeField] private string IsDead = "IsDead";


    public bool isBoss = false;
    public bool isRangedEnemy = false;
    private PlayerCollectableStats playerCollectableStats;

    public NavMeshAgent agent;
    public Transform player;
    public PlayerDeathController playerDeathController;
    public bool playerIsDead = false;
    
    public LayerMask whatIsGround, whatIsPlayer;

    public float enemyHealth;
    public bool enemyDead = false;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float minSpeed, MaxSpeed;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform projectileSpawn;
    public int enemyDamageStrength = 1;
    
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float despawnRange;
    public bool playerInDespawnRange;

    public GameObject DeathExplosion;
    public LootSpawner lootSpawner;

    // AudioSource audioSource;
    // public AudioClip normalSound;
    // public AudioClip deathSound;
    

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Bullet" && enemyDead == false && enemyHealth >= 0)
        {
            TakeDamage(1);
            // Destroy(collisionInfo.gameObject);
            Debug.Log("Got Hit");
        }
    }


    private void Start()
    {
        playerDeathController = GameObject.Find("Player Character").GetComponent<PlayerDeathController>();
        lootSpawner = GameObject.Find("Loot Spawner").GetComponent<LootSpawner>();
        player = GameObject.Find("Player Character").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.GetComponent<Animator>().speed = UnityEngine.Random.Range(minSpeed, MaxSpeed);

        float RandomDeathAnimation = UnityEngine.Random.Range(0,10);
        Debug.Log("My Random Number"+ RandomDeathAnimation);
        ZombieEnemyAnimator.SetFloat("DeathBlendAnimation", RandomDeathAnimation);

        // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        getIfPlayerIsDead();
        //Check for sight and attack range
        playerInDespawnRange = Physics.CheckSphere(transform.position, despawnRange, whatIsPlayer);

        if(getIfActive() == true)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (enemyHealth <= 0 && enemyDead == false)
            {
                PlayerKilledEnemy();
            }
            else if (enemyHealth >= 0 && enemyDead == false)
            {   
                if(!playerInSightRange && !playerInAttackRange)
                {
                    Debug.Log("Enemy is Walking");
                    ZombieEnemyAnimator.SetBool(IsRunning, false);
                    ZombieEnemyAnimator.SetBool(IsIdling, false);
                    ZombieEnemyAnimator.SetBool(IsAttacking, false);
                    ZombieEnemyAnimator.SetBool(IsWalking, true);
                    agent.speed = UnityEngine.Random.Range(minSpeed, MaxSpeed)*1 * Time.timeScale;
                    Patroling();
                }

                else if(playerInSightRange && !playerInAttackRange && !playerIsDead)
                {
                    Debug.Log("Enemy is Walking");
                    ZombieEnemyAnimator.SetBool(IsIdling, false);
                    ZombieEnemyAnimator.SetBool(IsWalking, false);
                    ZombieEnemyAnimator.SetBool(IsAttacking, false);
                    ZombieEnemyAnimator.SetBool(IsRunning, true);
                    agent.speed = UnityEngine.Random.Range(minSpeed, MaxSpeed)*5 * Time.timeScale;
                    ChasePlayer();
                }

                else if(playerInAttackRange && playerInSightRange && !playerIsDead)
                {
                    ZombieEnemyAnimator.SetBool(IsRunning, false);
                    ZombieEnemyAnimator.SetBool(IsWalking, false);
                    ZombieEnemyAnimator.SetBool(IsIdling, false);
                    ZombieEnemyAnimator.SetBool(IsAttacking, true);
                    agent.speed = 0;
                    AttackPlayer();
                }
                else
                {
                    ZombieEnemyAnimator.SetBool(IsRunning, false);
                    ZombieEnemyAnimator.SetBool(IsWalking, false);
                    ZombieEnemyAnimator.SetBool(IsAttacking, false);
                    ZombieEnemyAnimator.SetBool(IsIdling, true);
                    agent.speed = 0;
                }
            }
        }
    }

    private void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        int attackState = UnityEngine.Random.Range(1,5);

        if(attackState > 3)
        {
            isRangedEnemy = true;
        }
        else if(attackState < 3)
        {
            isRangedEnemy = false;
        }

        walkPoint = new Vector3(transform.position.x + randomX , transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }   

    private void ChasePlayer()
    {
        Vector3 myPositionVector = new Vector3(UnityEngine.Random.Range(player.position.x + 5,player.position.x - 5), 
                                                player.position.y,
                                                UnityEngine.Random.Range(player.position.z + 5,player.position.z - 5));
        agent.SetDestination(myPositionVector);

        float dist = Vector3.Distance(transform.position, myPositionVector);
        float distCloseToPlayer = Vector3.Distance(transform.position, player.position);

        if(dist < 1 || distCloseToPlayer < 1)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //Attack code here
            playerDeathController.TakeDamage(enemyDamageStrength);
            
            //Anything
            if(isRangedEnemy)
            {
                attackRange = 10f;
                Rigidbody rb = Instantiate(projectile, projectileSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 62f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            }

            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void PlayerKilledEnemy()
    {
        Debug.Log("Player Killed Enemy");
        enemyDead = true;
        enemyHealth -= 1;
        agent.speed = 0;
        agent.SetDestination(transform.position);

        ZombieEnemyAnimator.SetBool(IsRunning, false);
        ZombieEnemyAnimator.SetBool(IsWalking, false);
        ZombieEnemyAnimator.SetBool(IsAttacking, false);
        ZombieEnemyAnimator.SetBool(IsIdling, false);
        ZombieEnemyAnimator.SetBool(IsDead, true);
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth == 0) 
        {
            enemyHealth = 0;
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        playerCollectableStats = GameObject.Find("Player Settings").GetComponent<PlayerCollectableStats>();

        if(isBoss)
        {
            playerCollectableStats.addToBossesKilled(1);
        }
        else if(!isBoss)
        {
            playerCollectableStats.addToEnemiesKilled(1);
        }

        Collider collider = GetComponent<Collider>();
        collider.enabled = !collider.enabled;
        Destroy(gameObject,10);
        Instantiate(DeathExplosion, transform.position, Quaternion.identity);
        lootSpawner.spawnLoot(transform);
    }

    void getIfPlayerIsDead()
    {
        playerIsDead =  playerDeathController.isPlayerDead;
    }

    private bool getIfActive()
    {

            if(!playerInDespawnRange)
            {
                Destroy(gameObject);
                return (false);
            }    
            else if(playerInDespawnRange)
            {
                gameObject.SetActive(true);
                return (true);
            }
            return (false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, despawnRange);
    }
}

