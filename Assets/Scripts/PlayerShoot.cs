using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 10f;
    public Transform firingPoint;
    public string lookFor;
    public GameObject bulletPrefab;

    float timeUntilFire;
    PlayerMovement pm;

    private void Start() {
        pm = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && timeUntilFire < Time.time) {
            Shoot();
            timeUntilFire = Time.time + fireRate;
        }
    }

    private void Shoot() {
        /*float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical"); */
        //.locate(lookFor);
        /*float angle = Mathf.Atan2(my, mx) * Mathf.Rad2Deg;
        //Debug.Log(firingPoint.transform);
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle + 5.0f)));
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle - 5.0f)));
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle + 10.0f)));
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle - 10.0f))); */
    }
}
