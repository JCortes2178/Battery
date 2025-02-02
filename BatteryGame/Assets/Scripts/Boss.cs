using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float meleeDistance;
    public float speed;
    private Rigidbody2D _rigidbody;
    public bool isGrounded;
    public float jumpAmount = 10f;
    public float jumpCooldown = 3f;
    public float damageDealt = 10f;

    public enum BossState
    {
        Idling,
        Attacking,
        Moving,
        PhaseTransition
    }
    public BossState currentState = BossState.Idling;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //StartCoroutine(JumpLoop());
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if(isGrounded == true)
        {
        JumpAttack();
        }
        /*switch (currentState)
        {
            case BossState.Idling:
                //
                break;
        }*/
    }
    //for when the boss spawns and is in between other states
    void Idle()
    {

    }
    void Attack()
    {
        if (distance <= meleeDistance)
        {
            //Will determine if he tries to jump on you or slam attack you
            int attackType = Random.Range(0, 1);
        }
    }
    void Move()
    {

    }
    void Transition()
    {

    }
    void JumpAttack()
    {
        Debug.Log("Boss is jumping");
        //if(player == null) return;
        isGrounded = false;
        //Vector2 targetPosition = player.transform.position;
        Vector2 targetPosition = (Vector2)player.transform.position + new Vector2(Random.Range(-1f, 1f), 0);
        Vector2 jumpDirection = (targetPosition - (Vector2)transform.position).normalized;
        //_rigidbody.linearVelocity = new Vector2(targetPosition.x, jumpAmount);
        _rigidbody.linearVelocity = new Vector2(jumpDirection.x * 5f, jumpAmount);
        Debug.Log(isGrounded);
        
       //yield return new WaitForSeconds(1f);
    }
    IEnumerator JumpLoop()
    {
        while(true)
        {
            yield return new WaitUntil(() => isGrounded==true);
            yield return new WaitForSeconds(jumpCooldown);
        }
    }
    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            _rigidbody.linearVelocity = Vector2.zero;
            Debug.Log(isGrounded);
        }
        if (hitInfo.collider.CompareTag("Player"))
        {
            PlayerHealth playerHP = hitInfo.collider.GetComponentInChildren<PlayerHealth>();
            if (playerHP != null)
            {
                playerHP.LoseHealth(damageDealt);
            }else
            {
                Debug.LogError("Error: Null Exception in Enemy.cs");
            }
        }
    }
}
