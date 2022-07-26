﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private Animator CompanionHumanAnimator;
    [SerializeField] private string IsIdling = "IsIdling";
    [SerializeField] private string IsRunning = "IsRunning";
    [SerializeField] private string IsWalking = "IsWalking";
    [SerializeField] private string IsAttacking = "IsAttacking";
    [SerializeField] private string IsDead = "IsDead";

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
    public List<Transform> enemy = new List<Transform>();



    //States
    public float sightRange, attackRange, patrolRange;
    public bool playerInSightRange, enemyInAttackRange, enemyInPatrolRange;

    public bool freePatrol = false;

    void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag =="Zombie Enemy" && companionDead == false && companionHealth >= 0)
        {
            TakeDamage(1);
            Destroy(collisionInfo.gameObject);
            Debug.Log("Got Hit");
        }
    }

    void Awake()
    {
        GameObject enemyTemp = GameObject.Find("Enemies Section");

        for(var i = 0; i < 5; i++)
        {
            enemy.Add(enemyTemp.gameObject.transform.GetChild(i));
        }

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.GetComponent<Animator>().speed = Random.Range(minSpeed, MaxSpeed);

        float RandomDeathAnimation = Random.Range(0,10);
        Debug.Log("My Random Number"+ RandomDeathAnimation);
        CompanionHumanAnimator.SetFloat("DeathBlendAnimation", RandomDeathAnimation);
    }

    private void Update()
    {
        //Check for sight and attack range
        enemyInPatrolRange = Physics.CheckSphere(transform.position, patrolRange, whatToFollow);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatToFollow);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (companionHealth <= 0 && companionDead == false)
        {
            CompanionKilledEnemy();
        }
        else if (companionHealth >= 0 && companionDead == false)
        {
            if (enemyInPatrolRange && playerInSightRange)
            {
                freePatrol = true;
            }

            if(!enemyInPatrolRange)
            {
                freePatrol = false;
            }

            if(freePatrol == true && !enemyInAttackRange)
            {
                Debug.Log("Enemy is Walking");
                CompanionHumanAnimator.SetBool(IsRunning, false);
                CompanionHumanAnimator.SetBool(IsIdling, false);
                CompanionHumanAnimator.SetBool(IsAttacking, false);
                CompanionHumanAnimator.SetBool(IsWalking, true);
                agent.speed = Random.Range(minSpeed, MaxSpeed)*1;
                Patroling();
            }
            
            else if(freePatrol == false)
            {
                Debug.Log("Enemy is Walking");
                CompanionHumanAnimator.SetBool(IsIdling, false);
                CompanionHumanAnimator.SetBool(IsWalking, false);
                CompanionHumanAnimator.SetBool(IsAttacking, false);
                CompanionHumanAnimator.SetBool(IsRunning, true);
                agent.speed = Random.Range(minSpeed, MaxSpeed)*5;
                ChasePlayer();
            }

            else if(enemyInAttackRange && enemyInPatrolRange && freePatrol == true|| 
                    enemyInAttackRange && freePatrol == true)
            {
                Debug.Log("COVERING FIRE");
                CompanionHumanAnimator.SetBool(IsRunning, false);
                CompanionHumanAnimator.SetBool(IsWalking, false);
                CompanionHumanAnimator.SetBool(IsIdling, false);
                CompanionHumanAnimator.SetBool(IsAttacking, true);
                agent.speed = 0;
                AttackPlayer();
            }
            else
            {
                CompanionHumanAnimator.SetBool(IsRunning, false);
                CompanionHumanAnimator.SetBool(IsWalking, false);
                CompanionHumanAnimator.SetBool(IsAttacking, false);
                CompanionHumanAnimator.SetBool(IsIdling, true);
                agent.speed = 0;
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
        // for(var i = 0; i < enemy.Count; i++)
        // {
        //     float dist = Vector3.Distance(enemy[i].position, transform.position);
        //     Debug.Log("MY DISTANCE: "+dist);
        //     enemyDistance.Add(dist);
        // }

        // //Make sure enemy doesn't move
        // for(var i = 0; i < enemy.Count; i++)
        // {
        //     for(var j = 0; j < enemy.Count; j++)
        //     {
        //         if(enemyDistance[i] < enemyDistance[j])
        //         {
        //             enemyIndex = i;
        //         }
        //     }
        // }

        // for(var i = 0; i < enemy.Count; i++)
        // {
        //     if(enemy[i].gameObject.tag != "Zombie Enemy")
        //     {
        //         StartCoroutine(RemoveCells());
        //     }
        //     else if (enemy[i].gameObject.tag == "Zombie Enemy")
        //     {
        //         if(enemy[i].gameObject.GetComponent<EnemyController>().enemyHealth >= 0)
        //         {
        //             agent.SetDestination(transform.position);
                    
        //             transform.LookAt(enemy[i].position);

        //             Debug.Log("Attacking: " + enemy[i]);
        //         }
        //     }
        // }

        float closestDistance = float.MaxValue;
        int closestIndex = 0;
        for(int i = 0; i < enemy.Count; i++)
        {
            if(enemy[closestIndex].gameObject.GetComponent<EnemyController>().enemyHealth <= 0)
            {
                enemy[closestIndex] = null;
                StartCoroutine(RemoveCells());
            }
            else
            {
                float dist = Vector3.Distance(transform.position, enemy[i].position);
                if(dist < closestDistance)
                {
                    closestDistance = dist;
                    closestIndex = i;
                }
            }
        }

        agent.SetDestination(transform.position);
                    
        transform.LookAt(enemy[closestIndex].position);


        if(!alreadyAttacked)
        {
            //Attack code here
            //Anything
            Rigidbody rb = Instantiate(projectile, projectileSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 62f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

     private IEnumerator RemoveCells()
     {
         yield return 0;
 
        enemy.RemoveAll(null);
 
     }

    private void CompanionKilledEnemy()
    {
        Debug.Log("Companion Killed Enemy");
        companionDead = true;
        companionHealth -= 1;
        agent.speed = 0;
        agent.SetDestination(transform.position);

        CompanionHumanAnimator.SetBool(IsRunning, false);
        CompanionHumanAnimator.SetBool(IsWalking, false);
        CompanionHumanAnimator.SetBool(IsAttacking, false);
        CompanionHumanAnimator.SetBool(IsIdling, false);
        CompanionHumanAnimator.SetBool(IsDead, true);
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

    private void DestroyCompanion()
    {
        Destroy(gameObject,60);
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