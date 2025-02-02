using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyStrafer: EnemyArtillery
{
    //Rigidbody2D rigidBody2D;
    Vector2 startingPosition;
    public float moveDistance = 5f;
    private int moveDirection = 1; // 1 = right, -1 = left
    void Start()
    {
        startingPosition = transform.position;//store the starting position
       //rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 rotation = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;//Gets the angle in degrees
        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);//returns a rotation based on the line above.
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            timer = 0;
            base.Shoot();
        }
        Move();
    }
    void Move()
    {
        transform.position += new Vector3(speed * moveDirection * Time.deltaTime, 0, 0);

        // If the enemy moves beyond the set distance, reverse direction
        if (Mathf.Abs(transform.position.x - startingPosition.x) >= moveDistance)
        {
            moveDirection *= -1; // Flip direction
        }
        /*rigidBody2D.linearVelocity = new Vector2 (transform.position.x * speed, 0);
        //transform.Translate(Vector2.left * speed);
        if (transform.position.x <= -5)
        {
            transform.Translate(Vector2.right * speed);
        }*/
    }
}
