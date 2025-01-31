using Unity.Mathematics;
using UnityEngine;

public class EnemyArtillery : Enemy
{
    public GameObject player;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    [SerializeField] private int fireRate;
    [SerializeField] private float timer;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            timer = 0;
            Shoot();
        }
    }
    void Shoot()
    {
        
        //Vector2 direction = player.transform.position - transform.position;
        Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
    }
}
