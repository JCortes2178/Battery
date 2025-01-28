using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] public float jumpAmount = 10f;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private Rigidbody2D _rigidbody2D;
    private bool isFacingRight = true;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rigidbody2D.linearVelocity = new Vector2(horizontal * moveSpeed, _rigidbody2D.linearVelocity.y);
        Debug.Log(transform.rotation.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpAmount);
        }
        //Flip the sprite to match the direction we are facing. Default is right
        /*if (horizontal > 0)
        {
            transform.localScale = new Vector3(10, 10, 1);
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-10, 10, 1);
        }*/
        if (horizontal > 0 && !isFacingRight)
        {
            Flip();
        }

        if (horizontal < 0 && isFacingRight)
        {
            Flip();
        }

    }

    // Flip the facing direction
    public void Flip()
    {
        isFacingRight = !isFacingRight;

        // Rotates the player 180 degrees on the Y-axis.
        transform.Rotate(0f, 180f, 0f);
    }

    // boolean to tell if the player is on the ground. 
    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //No way to see the boxcast in the editor. This will outline it. 
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
