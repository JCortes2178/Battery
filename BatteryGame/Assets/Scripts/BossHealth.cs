using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public float maximumHealth = 100f;
    [SerializeField] public float health = 100f;
    
    public HealthbarScript healthbar;
    public bool isInvincible = false;
    
    private float THRESH_LOW = 20f; /// <summary>
                                    ///Can use this for the stages of the boss
                                    /// </summary>
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
        healthbar.SetMaxHealth(maximumHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= THRESH_LOW)
        {
            isInvincible = false; //TODO USE THIS IF NEEDED
        }
        else
        {
            isInvincible = false;
        }    
    }
    
    public void LoseHealth(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            healthbar.SetHealth(health);
            //code for if health<=0 DIE ->might be able to check this in update function
        }

    }
}
