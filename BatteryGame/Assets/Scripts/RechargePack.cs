using UnityEngine;

public class RechargePack : MonoBehaviour
{
    public float healthHealed = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayerHealth playerHP = hitInfo.GetComponentInChildren<PlayerHealth>();
            if (playerHP != null)
            {
                Debug.Log("Healing: " + healthHealed);
                playerHP.GainHealth(healthHealed);
                Destroy(gameObject);
            }
        }
    }
}
