using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public Transform muzzle;
    public float range = 100f;
    public float fireRate = 0.5f;
    public LayerMask targetMask;

    private float nextFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, targetMask))
        {
            Debug.Log("Hit: " + hit.collider.name);
            ScoreManager.Instance.AddScore(1);
            Destroy(hit.collider.gameObject);
        }
        GetComponent<RecoilGun>().Recoil();
        Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red, 100f);
    }
}
