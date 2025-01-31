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
        Vector3 rotation = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;//Gets the angle in degrees
        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);//returns a rotation based on the line above.
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
