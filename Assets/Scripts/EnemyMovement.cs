using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float maxDistance = 5f;
    public float checkInterval = 1f;

    private Vector2 spawnPoint;
    private Vector2 targetPosition;
    private Vector2 playerPos;
    private float checkTimer;
    bool isWaiting = false;
    float waitTimer;
    float waitDuration = 2;
    EnemyAni ani;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        ani = GetComponent<EnemyAni>();
    }

    private void Start()
    {
        spawnPoint = transform.position;
        PickNewTargetPosition();
    }

    private void Update()
    {
        MoveTowardsTarget();

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            CheckDistanceFromSpawnPoint();
        }
    }

    private void MoveTowardsTarget()
    {
        if (!isWaiting) // Only move if not waiting
        {
            ani.SetMoving();
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            if (targetPosition.x - transform.position.x >= 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                isWaiting = true; // Start waiting
                waitTimer = 0f;
            }
        }
        else // If waiting, update wait timer
        {
            ani.SetIdle();
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                PickNewTargetPosition();
            }
        }
    }

    private void PickNewTargetPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(0f, maxDistance);
        targetPosition = playerPos + randomDirection * randomDistance;
    }

    private void CheckDistanceFromSpawnPoint()
    {
        if (Vector2.Distance(transform.position, spawnPoint) > maxDistance)
        {
            targetPosition = spawnPoint;
        }
    }
}
