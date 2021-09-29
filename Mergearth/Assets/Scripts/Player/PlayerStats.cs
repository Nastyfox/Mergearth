using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Variables
    //Variable for instance of the player stats (singleton)
    public static PlayerStats SharedInstance;

    private int playerHealth = Constants.PLAYERMAXHEALTH;
    private int playerDamage = Constants.BASEPLAYERDAMAGE;
    private float playerMoveSpeed = Constants.BASEPLAYERSPEED;
    #endregion

    #region UnityMethods
    void Awake()
    {
        //Get the instance for player stats
        SharedInstance = this;
    }
    #endregion

    #region Getters & Setters
    public int GetPlayerDamage()
    {
        return playerDamage;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
        HealthBar.SharedInstance.SetHealth(playerHealth);
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public void SetPlayerMoveSpeed(float speed)
    {
        playerMoveSpeed = speed;
    }
    #endregion

}
