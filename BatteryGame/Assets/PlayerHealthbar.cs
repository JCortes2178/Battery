using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] float playerMaxHealth = 100f;

    [SerializeField] private float healthLostPerSecond = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Dont worry");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
