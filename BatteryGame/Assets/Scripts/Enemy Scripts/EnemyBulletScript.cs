using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] float timeToDissipate = 2f;
    [SerializeField] float bulletDamage = 100;
    //private GameObject player;
    public Rigidbody2D _rigidbody2D;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody2D.linearVelocity = transform.right * speed;
        //_rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse); //An alternate way to cause the projectile to move. More affected by the physics engine.
        //Vector2 direction = player.transform.position - transform.position;
        //_rigidbody2D.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed; //Makes projectile move at a fixed rate.
    }
    void Update()
    {
        Destroy(gameObject, timeToDissipate);
    }

    //Projectile behavior when it collides.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        if (hitInfo.CompareTag("Player"))
        {
            PlayerHealth playerHP = hitInfo.GetComponentInChildren<PlayerHealth>();
            if (playerHP != null)
            {
                playerHP.LoseHealth(bulletDamage);
            }
            else
            {
                Debug.LogError("Error: Null Exception in Enemy.cs");
            }

        }
        else if (hitInfo.name == "Tilemap")
        {
            Destroy(gameObject);
        }
    }
}
