using System;
using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 5f; //TODO: 4 colors might be too much, thinking 3 is better?
    [SerializeField] public float MOVE_SPEED_GREEN = 5f;
    [SerializeField] public float MOVE_SPEED_YELLOW = 10f;
    [SerializeField] public float MOVE_SPEED_ORANGE = 15;
    [SerializeField] public float MOVE_SPEED_RED = 20f;
    
    [Header("Jump")]
    [SerializeField] public float jumpAmount = 10f;
    
    
    //Dash settings
    [Header("Dash")]
    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashDuration = 0.2f;
    [SerializeField] public float dashCooldown = 1f;
    [SerializeField] public float DASH_SPEED_GREEN = 15f;
    [SerializeField] public float DASH_SPEED_YELLOW = 30f;
    [SerializeField] public float DASH_SPEED_ORANGE = 45f;
    [SerializeField] public float DASH_SPEED_RED = 60f;
    
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
    
    //Constants - constants are capitalized, values match gradient of healthbar
    private float THRESH_LOW = 20f; //Low is Orange | Red
    private float THRESH_MID = 60f; //Mid is Yellow | Orange
    private float THRESH_HIGH = 90f; //High is Green | Yellow
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Camera follow player
        Camera.main.transform.position = new Vector3(
        transform.position.x,
        (float)0.3,
        (float)-10);

        //move speed math
        float currentHealth = playerHealth.health;
        if (THRESH_MID <= currentHealth &&  currentHealth <= THRESH_HIGH)
        {
            moveSpeed = MOVE_SPEED_YELLOW;
        }else if (THRESH_LOW <= currentHealth && currentHealth < THRESH_MID)
        {
            moveSpeed = MOVE_SPEED_ORANGE;
        }else if (0 < currentHealth && currentHealth < THRESH_LOW)
        {
            moveSpeed = MOVE_SPEED_RED;
        }
        else
        {
            moveSpeed = MOVE_SPEED_GREEN;
        }

        //dashSpeed = ;
        if (THRESH_MID <= currentHealth &&  currentHealth <= THRESH_HIGH)
        {
            dashSpeed = DASH_SPEED_YELLOW;
        }else if (THRESH_LOW <= currentHealth && currentHealth < THRESH_MID)
        {
            dashSpeed = DASH_SPEED_ORANGE;
        }else if (0 < currentHealth && currentHealth < THRESH_LOW)
        {
            dashSpeed = DASH_SPEED_RED;
        }
        else
        {
            dashSpeed = DASH_SPEED_GREEN;
        }


        //moveSpeed = ;


        //jumpSpeed = ;
        
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

        /* Rotates the player 180 degrees on the Y-axis.
        transform.Rotate(0f, 180f, 0f);*/
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
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
