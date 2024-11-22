using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM2 : MonoBehaviour
{
    public enum EnemyState { Normal, ChasePlayer, AttackPlayer }
    public EnemyState currentState;

    public Sight sightSensor;
    public float playerAttackDistance;
    private NavMeshAgent agent;

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float lastShootTime;

    public float wanderSpeed = 2f;
    public float chaseSpeed = 4f;
    public float wanderInterval = 3f;

    private float rotationTimer;

    private static bool isPlayerDetected = false;
    private static Transform playerTransform;

    void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on parent object! Please attach it.");
        }
    }

    void Start()
    {
        currentState = EnemyState.Normal;
    }

    void Update()
    {
        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            isPlayerDetected = true;
            playerTransform = sightSensor.detectedObject.transform;
        }
        else
        {
            isPlayerDetected = false;
        }

        if (isPlayerDetected && currentState == EnemyState.Normal)
        {
            currentState = EnemyState.ChasePlayer;
        }

        switch (currentState)
        {
            case EnemyState.Normal:
                Wander();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.AttackPlayer:
                AttackPlayer();
                break;
        }
    }

    void Wander()
    {
        if (agent == null) return;

        agent.isStopped = false;
        agent.speed = wanderSpeed;

        rotationTimer += Time.deltaTime;

        if (rotationTimer >= wanderInterval ||
            (agent.hasPath && agent.remainingDistance < 0.5f))
        {
            rotationTimer = 0f;

            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                agent.destination = hit.position;
            }
        }

        if (sightSensor != null && sightSensor.detectedObject != null)
        {
            playerTransform = sightSensor.detectedObject.transform;
            isPlayerDetected = true;
        }
    }

    void ChasePlayer()
    {
        if (sightSensor == null || sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Normal;
            isPlayerDetected = false;
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer()
    {
        if (sightSensor == null || sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Normal;
            isPlayerDetected = false;
            return;
        }

        agent.isStopped = true;

        LookTo(playerTransform.position);

        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
