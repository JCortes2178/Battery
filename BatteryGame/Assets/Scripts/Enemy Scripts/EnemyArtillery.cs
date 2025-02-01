using Unity.Mathematics;
using UnityEngine;

public class EnemyArtillery : Enemy
{
    public GameObject player;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    [SerializeField] protected int fireRate;
    [SerializeField] protected float timer;


    void Update()
    {
        Vector3 rotation = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;//Gets the angle in degrees
        firePoint.rotation = Quaternion.Euler(0, 0, rotZ);//returns a rotation based on the line above.
        timer += Time.deltaTime;
        //create aggrodistance
        if (timer > fireRate)
        {
            timer = 0;
            Shoot();
        }
    }
    protected void Shoot()
    {

        //Vector2 direction = player.transform.position - transform.position;
        Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
    }
}
