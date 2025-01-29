using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] float timeToDissipate = 2f;
    public Rigidbody2D _rigidbody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse); //An alternate way to cause the projectile to move. More affected by the physics engine.
        _rigidbody2D.linearVelocity = transform.right * speed; //Makes projectile move at a fixed rate.
    }
    void Update()
    {
        Destroy(gameObject, timeToDissipate);
    }

    //Projectile behavior when it collides.
    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Debug.Log(hitinfo.name);
        Destroy(gameObject);
    }
}