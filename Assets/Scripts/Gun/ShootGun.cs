using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public Transform muzzle;
    public float range = 100f;
    public float fireRate = 0.5f;
    public LineRenderer laserLine;
    public float laserDuration = 0.1f;
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
        Vector3 endPos = muzzle.position + muzzle.forward * range;

        if (Physics.Raycast(ray, out hit, range, targetMask))
        {
            Debug.Log("Hit: " + hit.collider.name);
            ScoreManager.Instance.AddScore(1);
            Destroy(hit.collider.gameObject);
        }
        GetComponent<RecoilGun>().Recoil();
        Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red, 100f);
        StartCoroutine(FireLaser(endPos));
    }

    System.Collections.IEnumerator FireLaser(Vector3 endPos)
    {
        laserLine.SetPosition(0, muzzle.position);
        laserLine.SetPosition(1, endPos);
        laserLine.enabled = true;

        yield return new WaitForSeconds(laserDuration);

        laserLine.enabled = false;
    }
}
