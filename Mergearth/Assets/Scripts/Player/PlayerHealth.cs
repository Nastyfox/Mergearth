using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    #region Variables
    //Variable for instance of the player health (singleton)
    public static PlayerHealth SharedInstance;

    //Variables to display health
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private float flashSpeed;
    [SerializeField] private float invincibilityDurationAfterHit;

    //Variables for invincibility
    private bool isInvincible = false;

    //Variables for animation
    [SerializeField] private Animator playerAnim;
    private float deathDuration;

    //Variables for audio playing
    [SerializeField] private AudioClip soundEffect;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Awake()
    {
        //Get the instance for player health
        SharedInstance = this;

        //Get fadein duration
        foreach (AnimationClip clip in playerAnim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "PlayerDeath")
            {
                deathDuration = clip.length;
            }
        }
    }

    private void Start()
    {
        //Set current health to max health if we do not load data
        if (!LoadScene.SharedInstance.GetDataToLoad())
        {
            SetHealth(maxHealth);
        }
    }
    #endregion

    #region Getters & Setters
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        HealthBar.SharedInstance.SetHealth(currentHealth);
    }
    #endregion

    #region Methods
    public void RestartAnimationAfterDeath()
    {
        playerAnim.SetTrigger("RetryLevel");
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            //Play sound effect
            AudioManager.SharedInstance.PlaySoundEffect(soundEffect);

            //The player's health is reduced if he is not invincible and his life is > 0
            currentHealth = Mathf.Max(Constants.MINHEALTHPLAYER, currentHealth - damage);

            //Display health
            HealthBar.SharedInstance.SetHealth(currentHealth);

            //If the health is at the minimum, the player is dead
            if (currentHealth == Constants.MINHEALTHPLAYER)
            {
                Death();
                return;
            }

            //Start invincibility
            StartCoroutine(InvincibilityTime());
        }
    }

    public void Heal(int heal)
    {
        //The player's health is increased if his life is not max
        currentHealth = Mathf.Min(maxHealth, currentHealth + heal);

        //Display health
        HealthBar.SharedInstance.SetHealth(currentHealth);
    }

    private void Death()
    {
        StartCoroutine(DeathToGameOver());
    }

    public void Respawn()
    {
        //Get current health back to max
        SetHealth(maxHealth);
    }
    #endregion

    #region IEnumerators
    private IEnumerator InvincibilityFlash()
    {
        //Make the player flash to show that he is invincible
        while(isInvincible)
        {
            playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 0f);
            yield return new WaitForSeconds(flashSpeed);
            playerSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1f);
            yield return new WaitForSeconds(flashSpeed);
        }
    }

    private IEnumerator InvincibilityTime()
    {
        //Wait a certain time before removing invincibility
        isInvincible = true;
        StartCoroutine(InvincibilityFlash());
        yield return new WaitForSeconds(invincibilityDurationAfterHit);
        isInvincible = false;
    }

    private IEnumerator DeathToGameOver()
    {
        //Lauch death animation and wait until it's finished
        playerAnim.SetTrigger("Death");
        yield return new WaitForSeconds(deathDuration);

        //Load GameOver UI
        GameOverMenu.SharedInstance.GameOverUI();
    }
    #endregion
}
