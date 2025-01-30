using System;
using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpAmount = 10f;
    
    //Dash settings
    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashDuration = 0.2f;
    [SerializeField] public float dashCooldown = 1f;
    private float _dashTimeLeft;
    private float _lastDashTime;
    
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    private Rigidbody2D _rigidbody2D;

    private bool isFacingRight = true;

    private SpriteRenderer _spriteRenderer;
    
    //Flags
    private bool _canAttack = true;
    private bool _isDashing = false;
    public PlayerHealth playerHealth;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing)
        {
            float horizontal = Input.GetAxis("Horizontal");
            _rigidbody2D.linearVelocity = new Vector2(horizontal * moveSpeed, _rigidbody2D.linearVelocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                Jump();
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

            //Dash handlling
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded() && _lastDashTime + dashCooldown <= Time.time)
            {
                Dash();
                playerHealth.LoseHealthDash();
            }
        }

        else //_isDashing is True
        {
            _dashTimeLeft -= Time.deltaTime;
            if (_dashTimeLeft <= 0)
            {
                _isDashing = false;
                _canAttack = true;
                _rigidbody2D.linearVelocity = new Vector2(0, _rigidbody2D.linearVelocity.y); // Stop horizontal velocity after dash
            }
        }
        
    }

    // Flip the facing direction
    public void Flip()
    {
        isFacingRight = !isFacingRight;

        // Rotates the player 180 degrees on the Y-axis.
        transform.Rotate(0f, 180f, 0f);
    }

    //Function for jumping
    public void Jump()
    {
        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpAmount);
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

    public void Dash()
    {
        //Locking out attacking
        _canAttack = false;
        _isDashing = true;
        _dashTimeLeft = dashDuration;
        _lastDashTime = Time.time;
         
        float dashDirection = isFacingRight ? 1f : -1f;
        
        _rigidbody2D.linearVelocity = new Vector2(dashDirection * dashSpeed, _rigidbody2D.linearVelocity.y);

    }
}
