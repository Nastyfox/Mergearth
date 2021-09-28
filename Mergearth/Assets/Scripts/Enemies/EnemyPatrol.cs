using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    #region Variables

    //Variables for movement
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private float speed;
    private int indexWaypoint;

    //Variables for animation
    [SerializeField] private SpriteRenderer enemySR;

    #endregion

    #region UnityMethods

    // Update is called once per frame
    void Update()
    {
        //Move the enemy to the next waypoint
        this.transform.position = Vector3.MoveTowards(this.transform.position, waypoints[indexWaypoint], Time.deltaTime * speed);

        //If the enemy is close to the waypoint, go to the next waypoint and flip the sprite
        if (Vector3.Distance(this.transform.position, waypoints[indexWaypoint]) < Constants.MINDISTANCETOPOINT)
        {
            indexWaypoint = (indexWaypoint + 1) % waypoints.Length;
            enemySR.flipX = !enemySR.flipX; 
        }
    }

    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player walks on the head of enemy, destroy it
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Do damages to the player
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.SharedInstance.TakeDamage(Constants.SNAKEDAMAGE);
        }
    }

    #endregion
}
