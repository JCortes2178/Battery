using Unity.VisualScripting;
using UnityEngine;

public class EnemyFlyingGoomba : EnemyAI
{
    //public GameObject player;
    //public float speed;
    //public float distance;
    //private float aggroDistance;
    void Update()
    {
        this.Move();
    }
    new void Move()
    {
        //maybe move these into the if statement below
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position; //Necessary to get enemy to rotate to the player
        direction.Normalize(); //Necessary to get enemy to rotate to the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Necessary to get enemy to rotate to the player

        if (distance < aggroDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle); //Kept code in case we need enemy to rotate to player
        }
    }
}
