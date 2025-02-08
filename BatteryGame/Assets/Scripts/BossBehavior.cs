using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public enum BossState { idle, move, attack, stagger, dead }
public class BossBehavior : Enemy
{
    BossState currentState = BossState.idle;
    GameObject player;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float timer;
    void Update()
    {
        switch (currentState)
        {
            case BossState.idle:
                Idling();
                break;
            case BossState.move:
                Moving();
                break;
            case BossState.attack:
                Attacking();
                break;
            case BossState.dead:
                Dead();
                break;
        }
    }

    void Idling()
    {
        //Does nothing except checking aggro range.
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= aggroDistance)
        {
            ChangeState(BossState.attack);
        }
    }
    void Attacking()
    {
        timer += Time.deltaTime;
        if (timer >= attackCooldown)
        {
            timer = 0;
            //boss attack move
        }
    }
    void Moving()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void Staggered()
    {

    }
    void Dead()
    {
        Destroy(gameObject);
    }
    void ChangeState(BossState newState)
    {
        //if (currentState == newState) return; //this could prevent redundant state changing but I need to test out if it happens in the first place
        currentState = newState;

    }
}
