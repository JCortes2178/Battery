using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float damageDealt = 100f;
    public GameObject rechargePackPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Debug.Log(hitInfo.collider.name);
        if (hitInfo.collider.CompareTag("Player"))
        {
            PlayerHealth playerHP = hitInfo.collider.GetComponentInChildren<PlayerHealth>();
            if (playerHP != null)
            {
                playerHP.LoseHealth(damageDealt);
            }else
            {
                Debug.LogError("Error: Null Exception in Enemy.cs");
            }
        }
    }
    private void Death()
    {
        Destroy(gameObject);
        Instantiate(rechargePackPrefab, transform.position, Quaternion.identity);
    }
}
