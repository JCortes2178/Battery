using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float meleeDistance;
    public float speed;
    [Header("Player Tracking")]
    public GameObject player;
    public float distance;
    private Rigidbody2D _rigidbody;
    [Header("Jump Mechanics")]
    public float jumpAmount = 10f;
    public float jumpCooldown = 3f;
    public float damageDealt = 10f;
    public bool isGrounded = false;
    [Header("Stagger Mechanics")]
    public int hitsUntilStaggered = 20;
    public int hitCounter = 0;
    public float staggerTime = 3f;
    public bool isStaggered = false;
    [Header("Spawning Enemy Attack")]
    public GameObject minionType;
    public Transform spawnPoint;
    [Header("Shooting Player")]
    [SerializeField] protected float timer;
    public int fireRate = 3;
    public Transform firePoint;
    public GameObject enemyBulletPrefab;
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
        Collider2D bossCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);
    }
    void Update()
    {
        Vector3 rotation = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;//Gets the angle in degrees
        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);//returns a rotation based on the line above.
        timer += Time.deltaTime;
        distance = Vector2.Distance(transform.position, player.transform.position);//might not need
        if (isGrounded == true && !isStaggered)
        {
            JumpAttack();
        }
        if (timer > fireRate)
        {
            timer = 0;
            Shoot();
        }
        
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
    void TakeDamage(int damage)
    {
        hitCounter++;
        if(hitCounter>= hitsUntilStaggered)
        {
            Staggered ();
        }
    }
    void JumpAttack()
    {
        Debug.Log("Boss is jumping");
        isGrounded = false;
        Vector2 targetPosition = (Vector2)player.transform.position + new Vector2(Random.Range(-1f, 1f), 0);//maybe remove the randomness element
        Vector2 jumpDirection = (targetPosition - (Vector2)transform.position).normalized;
        _rigidbody.linearVelocity = new Vector2(jumpDirection.x * 5f, jumpAmount);
        Debug.Log(isGrounded);
    }
    protected void Shoot()
    {

        //Vector2 direction = player.transform.position - transform.position;
        Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
    }
    void Staggered()
    {
        isStaggered = true;
        _rigidbody.linearVelocity = Vector2.zero;
        //time staggered
        //WaitForSeconds(staggerTime);
        //maybe bonus damage?
        hitCounter = 0;
        isStaggered = false;//boss is no longer staggered
        SpawnAttack();
    }

    void SpawnAttack()
    {
        Instantiate(minionType, spawnPoint.position, Quaternion.identity);
    }
    IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => isGrounded == true);
            yield return new WaitForSeconds(jumpCooldown);
        }
    }
    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.collider.CompareTag("Ground"))/*|| hitInfo.gameObject.layer == groundLayer*/
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
            }
            else
            {
                Debug.LogError("Error: Null Exception in Enemy.cs");
            }
        }
    }
}
