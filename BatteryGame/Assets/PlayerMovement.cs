using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 20f;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rigidbody2D.linearVelocity = new Vector2(horizontal * moveSpeed, _rigidbody2D.linearVelocity.y);
        
        //Flip the sprite to match the direction we are facing. Default is right
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(10, 10, 1);
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-10, 10, 1);
        }

    }
}
