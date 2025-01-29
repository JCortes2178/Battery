using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    [SerializeField] public float healthLostPerSecond = 1f;
    [SerializeField] public float dashCost = 5f;
    [SerializeField] public float attackCost = 10f;
    [SerializeField] public float jumpCost = 3f;
    
    public HealthbarScript healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
        healthbar.SetMaxHealth(health);
        InvokeRepeating("LoseHealthOverTime", 1f, 1f);
    }

    // Update is called once per frame
    public void LoseHealthDash()
    {
        LoseHealth(dashCost);
    }

    public void LoseHealthJump()
    {
        LoseHealth(jumpCost);
    }

    public void LoseHealthAttack()
    {
        LoseHealth(attackCost);
    }

    public void LoseHealth(float damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
    }

    void LoseHealthOverTime()
    {
        health -= healthLostPerSecond;
        healthbar.SetHealth(health);
    }
}
