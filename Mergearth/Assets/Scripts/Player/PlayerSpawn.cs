using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    #region Variables

    //Variable for instance of the player spawn (singleton)
    public static PlayerSpawn SharedInstance;

    //Variables for spawning elements not destroyed to a certain position
    private Vector3 respawnPoint;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Awake()
    {
        //Get the instance for player spawn
        SharedInstance = this;

        respawnPoint = this.gameObject.transform.position;
    }

    private void Start()
    {
        //Check if datas need to be loaded and load them
        if(LoadScene.SharedInstance.GetDataToLoad())
        {
            LoadAndSaveData.SharedInstance.LoadGameDatas();
        }
    }

    #endregion

    #region Getters & Setters
    public void SetRespawnPoint(Vector3 position)
    {
        respawnPoint = position;
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }

    #endregion

    #region Methods
    public void SpawnPlayer()
    {
        //Spawn the player to the respawn point
        this.transform.position = respawnPoint;
    }

    #endregion
}
