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
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rigidbody2D.linearVelocity = new Vector2(horizontal * moveSpeed, _rigidbody2D.linearVelocity.y);
        Debug.Log(transform.rotation.y);
        //Debug.Log(horizontal);
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
        if (horizontal > 0 && transform.rotation.y == -1f)
        {
            //_spriteRenderer.flipX = false; // Default facing right
            transform.Rotate(0f, 180f, 0f);
            Debug.Log(horizontal);
            Debug.Log("Facing Right");
            Debug.Log(transform.rotation.y);
        }

        if (horizontal < 0 && transform.rotation.y == 0f)
        {
            //_spriteRenderer.flipX = true; // Default facing right

           // transform.Rotate(0f, 180f, 0f);
            transform.Rotate(0f, -180f, 0f);
            Debug.Log(horizontal);
            Debug.Log("Facing Left");
            Debug.Log(transform.rotation.y);
        }
        
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
