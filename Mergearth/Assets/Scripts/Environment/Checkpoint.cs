using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    #region Variables
    //Variables for player respawn position
    [SerializeField] SpriteRenderer checkpointSR;
    [SerializeField] BoxCollider2D checkpointBC;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        ActiveCheckpoint();
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player reaches the checkpoint, change spawn position and deactivate box collider to avoid player to retake checkpoint
        if(collision.CompareTag("Player"))
        {
            PlayerSpawn.SharedInstance.SetRespawnPoint(this.transform.position);
            checkpointBC.enabled = false;
        }
    }
    #endregion

    #region Methods
    private void ActiveCheckpoint()
    {
        //If the player spawn position is after the checkpoint, hide it (new checkpoint already checked)
        if (PlayerSpawn.SharedInstance.GetRespawnPoint().x > this.transform.position.x)
        {
            checkpointSR.color = new Color(checkpointSR.color.r, checkpointSR.color.g, checkpointSR.color.b, 0f);
        }
        //If the current checkpoint is active, new sprite
        else if (PlayerSpawn.SharedInstance.GetRespawnPoint().x == this.transform.position.x)
        {
            //Add new sprite
        }
    }
    #endregion
}
