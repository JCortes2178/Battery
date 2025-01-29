using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    private Vector3 mousePosition;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //converts the screen-space mouse position to world coordinates.
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        //Shooting code.
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
