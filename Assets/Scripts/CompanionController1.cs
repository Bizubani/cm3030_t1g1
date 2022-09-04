using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CompanionController1 : MonoBehaviour
{
    // [SerializeField] private Animator CompanionHumanAnimator;
    // [SerializeField] private string IsIdling = "IsIdling";
    // [SerializeField] private string IsRunning = "IsRunning";
    // [SerializeField] private string IsWalking = "IsWalking";
    // [SerializeField] private string IsAttacking = "IsAttacking";
    // [SerializeField] private string IsDead = "IsDead";

    private GameObject CM;

    public UnityEngine.AI.NavMeshAgent agent;

    public Transform player;
    // public Transform enemy;
    
    public LayerMask whatIsGround, whatIsEnemy, whatToFollow;

    public float companionHealth;
    public bool companionDead = false;

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

    private Coroutine EnemyCoroutine;
    public List<Transform> enemy = new List<Transform>();

    //States
    public float sightRange, attackRange, patrolRange;
    public bool playerInSightRange, enemyInAttackRange, playerInPatrolRange;

    public bool setPatrol = false;
    public bool patrol = false;

    public bool chase = false;
    
    public bool attack = false;
    
    public bool idle = false;

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Zombie Enemy" && companionDead == false && companionHealth >= 0)
        {
            TakeDamage(1);
            Destroy(collisionInfo.gameObject);
            Debug.Log("Got Hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Zombie Enemy" && other.gameObject.GetComponent<EnemyController>().enemyHealth > 1)
        {
            enemy.Add(other.gameObject.transform);
            attack = true;
        }
        else
        {
            attack = false; 
            clearNullEnemies();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Zombie Enemy" && other.gameObject.GetComponent<EnemyController>().enemyHealth > 1)
        {
            enemy.Remove(other.gameObject.transform);

            attack = false;
            clearNullEnemies();
        }
    }

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.GetComponent<Animator>().speed = UnityEngine.Random.Range(minSpeed, MaxSpeed);

        //float RandomDeathAnimation = UnityEngine.Random.Range(0,10);
        //Debug.Log("My Random Number"+ RandomDeathAnimation);
        //CompanionHumanAnimator.SetFloat("DeathBlendAnimation", RandomDeathAnimation);
    }

    private void Update()
    {
        clearNullEnemies();
        try 
        {
            CM = GameObject.Find("Menu");

            if (CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatToFollow);
            playerInPatrolRange = Physics.CheckSphere(transform.position, patrolRange, whatToFollow);
            enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

            if(enemyInAttackRange && playerInPatrolRange ||
            enemyInAttackRange && idle)
            {
                attack = true;
                chase = false;
                idle = false;
            }
            else if(playerInPatrolRange)
            {
                if(playerInSightRange)
                {
                    setPatrol = true;

                    if(setPatrol != false)
                    {
                        patrol = true;
                        chase = false;
                        attack = false;
                        idle = false;
                    }
                }
            }
            else if(!playerInPatrolRange && !playerInSightRange && enemyInAttackRange ||
                    !playerInPatrolRange && !playerInSightRange && !enemyInAttackRange)
            {
                chase = true;
                setPatrol = false;
                patrol = false;
                attack = false;
                idle = false;
            }
            else if(idle)
            {
                idle = true;
                attack = false;
                chase = false;
                setPatrol = false;
                idle = false;
            }

            if (companionHealth <= 0 && companionDead == false)
            {
                CompanionKilledEnemy();
            }
            else if (companionHealth >= 0 && companionDead == false)
            {
                if(patrol)
                {
                    Debug.Log("Enemy is Walking");
                    // CompanionHumanAnimator.SetBool(IsRunning, false);
                    // CompanionHumanAnimator.SetBool(IsIdling, false);
                    // CompanionHumanAnimator.SetBool(IsAttacking, false);
                    // CompanionHumanAnimator.SetBool(IsWalking, true);
                    // agent.speed = UnityEngine.Random.Range(minSpeed, MaxSpeed)*1;
                    Patroling();
                }
                
                else if(chase)
                {
                    Debug.Log("Enemy is Walking");
                    // CompanionHumanAnimator.SetBool(IsIdling, false);
                    // CompanionHumanAnimator.SetBool(IsWalking, false);
                    // CompanionHumanAnimator.SetBool(IsAttacking, false);
                    // CompanionHumanAnimator.SetBool(IsRunning, true);
                    agent.speed = UnityEngine.Random.Range(minSpeed, MaxSpeed)*5;
                    ChasePlayer();
                }

                else if(attack)
                {
                    Debug.Log("COVERING FIRE");
                    // CompanionHumanAnimator.SetBool(IsRunning, false);
                    // CompanionHumanAnimator.SetBool(IsWalking, false);
                    // CompanionHumanAnimator.SetBool(IsIdling, false);
                    // CompanionHumanAnimator.SetBool(IsAttacking, true);
                    agent.speed = 0;
                    AttackEnemy();
                }
                else
                {
                    // CompanionHumanAnimator.SetBool(IsRunning, false);
                    // CompanionHumanAnimator.SetBool(IsWalking, false);
                    // CompanionHumanAnimator.SetBool(IsAttacking, false);
                    // CompanionHumanAnimator.SetBool(IsIdling, true);
                    agent.speed = 0;
                }
            }
        }

        Debug.Log(companionHealth);
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

    private void AttackEnemy()
    {
        float closestDistance = float.MaxValue;
        int closestIndex = 0;
        for(int i = 0; i < enemy.Count; i++)
        {
            if(enemy[i].position != null)
            {
                float dist = Vector3.Distance(transform.position, enemy[i].position);
                if(dist < closestDistance && enemy[closestIndex].gameObject.GetComponent<EnemyController>().enemyHealth > 0)
                {
                    closestDistance = dist;
                    closestIndex = i;

                }
            }
            else
            {
                enemy.Remove(enemy[i]);
                
                attack = false;
                return;
            }
        }

        clearNullEnemies();
        
        agent.SetDestination(transform.position);
        

        transform.LookAt(enemy[closestIndex].position, Vector3.up);


        if(!alreadyAttacked)
        {
            //Attack code here
            //Anything
            Rigidbody rb = Instantiate(projectile, projectileSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void CompanionKilledEnemy()
    {
        Debug.Log("Companion Killed Enemy");
        companionDead = true;
        companionHealth -= 1;
        agent.speed = 0;
        agent.SetDestination(transform.position);

        // CompanionHumanAnimator.SetBool(IsRunning, false);
        // CompanionHumanAnimator.SetBool(IsWalking, false);
        // CompanionHumanAnimator.SetBool(IsAttacking, false);
        // CompanionHumanAnimator.SetBool(IsIdling, false);
        // CompanionHumanAnimator.SetBool(IsDead, true);
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        companionHealth -= damage;

        if(companionHealth == 0) 
        {
            companionHealth = 0;
            Invoke(nameof(DestroyCompanion), 0.5f);
        }
    }

    public void GetEnemies(Transform enemySpawnName)
    {
        // GameObject enemyTemp = GameObject.Find(enemySpawnName);

        for(var i = 0; i < enemySpawnName.childCount; i++)
        {
            enemy.Add(enemySpawnName.GetChild(i));
        }

        // for(var i = 0; i < enemyTemp.transform.childCount; i++)
        // {
        //     enemy.Add(enemyTemp.gameObject.transform.GetChild(i));
        // }
    }

    public void CleanReferences()
    {
        enemy.Clear();
        enemy.TrimExcess();
        enemy = new List<Transform>();
    }

    private void DestroyCompanion()
    {
        Destroy(gameObject,60);
    }

    void clearNullEnemies()
    {
        for(var i = enemy.Count - 1; i > -1; i--)
        {
            if (enemy[i] == null)
            {
                enemy.RemoveAt(i);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
}
