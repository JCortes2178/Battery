using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float maximumHealth = 100f;
    [SerializeField] public float health = 100f;
    [SerializeField] public float healthLostPerSecond = 1f;
    [SerializeField] public float dashCost = 5f;
    [SerializeField] public float attackCost = 10f;
    [SerializeField] public float jumpCost = 3f;
    
    public HealthbarScript healthbar;
    public bool isInvincible = false;
    public GameStateScript gameState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //COPIED FROM PlayerMovement.cs
    //Constants - constants are capitalized, values match gradient of healthbar
    private float THRESH_LOW = 20f; //Low is Orange | Red
    private float THRESH_MID = 60f; //Mid is Yellow | Orange
    private float THRESH_HIGH = 90f; //High is Green | Yellow
    void Start()
    {
        health = 100;
        healthbar.SetMaxHealth(maximumHealth);
        InvokeRepeating("LoseHealthOverTime", 1f, 1f);
    }

    void Update()
    {
        //If we want to give players extra second than we do less than 0
        if (health <= 0)
        {
            gameState.GameOver();
        }
        else if (health <= THRESH_LOW)
        {
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
    }

    // Update is called once per frame
    public void LoseHealthDash()
    {
        LoseHealthFromSelf(dashCost);
    }

    public void LoseHealthJump()
    {
        LoseHealthFromSelf(jumpCost);
    }

    public void LoseHealthAttack()
    {
        LoseHealthFromSelf(attackCost);
    }

    //Seperate fucntion to differentiate self damage vs invulnerable to enemy damage when in red
    public void LoseHealthFromSelf(float damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
        //code for if health <=0 DIE ->might be able to check this in update function
    }

    //This is used by enemies to inflict damage
    public void LoseHealth(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            healthbar.SetHealth(health);
            //code for if health<=0 DIE ->might be able to check this in update function
        }

    }

    void LoseHealthOverTime()
    {
        health -= healthLostPerSecond;
        healthbar.SetHealth(health);
    }
    public void GainHealth(float regen)
    {
        health += regen;
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }
        healthbar.SetHealth(health);
    }
}
