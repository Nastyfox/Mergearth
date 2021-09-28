using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables
    //Variable for instance of the health bar (singleton)
    public static HealthBar SharedInstance;

    //Variables for health display
    [SerializeField] Slider healthBarSlider;
    [SerializeField] Image filler;
    [SerializeField] Gradient gradient;
    [SerializeField] private float lerpTime;
    private float currentHealth;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        //Smooth transition between old and new health bar value and use gradient for smooth color
        healthBarSlider.value = Mathf.Lerp(healthBarSlider.value, currentHealth, lerpTime * Time.deltaTime);
        filler.color = gradient.Evaluate(healthBarSlider.normalizedValue);
    }

    private void Awake()
    {
        //Get the instance for health bar
        SharedInstance = this;
    }
    #endregion

    #region Getters & Setters
    public void SetHealth(float health)
    {
        //Set health value to current health
        currentHealth = health;
    }
    #endregion
}
