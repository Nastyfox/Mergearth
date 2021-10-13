using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    #region Variables

    //Variables for pathfinding
    private Transform target;
    [SerializeField] Seeker seeker;
    [SerializeField] Rigidbody2D enemyRb;
    private float yEnemyOffset;
    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded;
    private float pathUpdateSeconds = 0.5f;
    private Vector2 currentPosition;

    //Variables for animation
    [SerializeField] private SpriteRenderer enemySR;

    //Variables for enemy stats
    [SerializeField] private EnemySO enemySO;
    private int enemyCurrentHP;

    #endregion

    #region UnityMethods

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the target is close to the enemy, make it follow it
        if (TargetInDistance())
        {
            PathFollow();
        }
    }

    private void Awake()
    {
        //Set enemy hp to max
        enemyCurrentHP = enemySO.maxHP;
        //Set the target for pathfinding as the player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Offset the position for pathfinding calculation to avoid it being on the head of the enemy
        yEnemyOffset = enemySR.bounds.size.y / 4;
        currentPosition = new Vector2(enemyRb.position.x, enemyRb.position.y - yEnemyOffset);
    }

    private void Start()
    {
        //Start updating path every x seconds
        InvokeRepeating("UpdatePath", 0, pathUpdateSeconds);
    }
    #endregion

    #region Methods
    private void OnPathComplete(Path p)
    {
        //Get a new path and reset the waypoints
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        //Set a new path between the enemy and the target
        if (seeker.IsDone() && TargetInDistance())
            seeker.StartPath(currentPosition, target.position, OnPathComplete);
    }

    private void PathFollow()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //If there is a path and we are not at the end of it, get the direction for the next point
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - currentPosition).normalized;
        Vector2 force = direction * enemySO.speed * Time.fixedDeltaTime;

        //Apply a jumping force if the enemy is a grounded one
        if(!enemySO.isFlying && isGrounded)
        {
            if (direction.y > enemySO.jumpNodeHeightRequirement)
            {
                enemyRb.AddForce(Vector2.up * enemySO.speed * enemySO.jumpModifier);
            }
        }

        //Apply the force to the enemy to make it move
        enemyRb.AddForce(force);

        //Set the new position
        currentPosition = new Vector2(enemyRb.position.x, enemyRb.position.y - yEnemyOffset);

        //Check if the distance is less than the minimum one to get to the next point
        float distance = Vector2.Distance(currentPosition, path.vectorPath[currentWaypoint]);

        if (distance < enemySO.nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Flip the sprite based on the enemy movement
        if(force.x <= -Constants.FLIPSPEEDVALUE)
        {
            enemySR.flipX = true;
        }
        else if(force.x >= Constants.FLIPSPEEDVALUE)
        {
            enemySR.flipX = false;
        }
    }

    private bool TargetInDistance()
    {
        //Check if the enemy is close enough to its target
        return Vector2.Distance(currentPosition, target.position) < enemySO.activationDistance;
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player walks on the head of enemy, destroy it
        if(collision.CompareTag("Player"))
        {
            enemyCurrentHP -= PlayerStats.SharedInstance.GetPlayerDamage();
            if(enemyCurrentHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Do damages to the player
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.SharedInstance.TakeDamage(enemySO.enemyDamage);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    //Detect that player is leaving the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    #endregion
}
