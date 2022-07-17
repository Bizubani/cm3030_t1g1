using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator ZombieEnemyAnimator;
    [SerializeField] private string IsIdling = "IsIdling";
    [SerializeField] private string IsRunning = "IsRunning";
    [SerializeField] private string IsWalking = "IsWalking";
    [SerializeField] private string IsAttacking = "IsAttacking";
    [SerializeField] private string IsDead = "IsDead";

    public NavMeshAgent agent;
    public Transform player;
    
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

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Bullet" && enemyDead == false && enemyHealth >= 0)
        {
            TakeDamage(1);
            Destroy(collisionInfo.gameObject);
            Debug.Log("Got Hit");
        }
    }

    void Awake()
    {
        player = GameObject.Find("Player Character").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.GetComponent<Animator>().speed = Random.Range(minSpeed, MaxSpeed);

        float RandomDeathAnimation = Random.Range(0,10);
        Debug.Log("My Random Number"+ RandomDeathAnimation);
        ZombieEnemyAnimator.SetFloat("DeathBlendAnimation", RandomDeathAnimation);
    }

    private void Update()
    {
        //Check for sight and attack range
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
                agent.speed = Random.Range(minSpeed, MaxSpeed)*1;
                Patroling();
            }

            else if(playerInSightRange && !playerInAttackRange)
            {
                Debug.Log("Enemy is Walking");
                ZombieEnemyAnimator.SetBool(IsIdling, false);
                ZombieEnemyAnimator.SetBool(IsWalking, false);
                ZombieEnemyAnimator.SetBool(IsAttacking, false);
                ZombieEnemyAnimator.SetBool(IsRunning, true);
                agent.speed = Random.Range(minSpeed, MaxSpeed)*5;
                ChasePlayer();
            }

            else if(playerInAttackRange && playerInSightRange)
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

        Debug.Log(enemyHealth);
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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX , transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }   

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //Attack code here
            //Anything
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

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
        Destroy(gameObject,60);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

