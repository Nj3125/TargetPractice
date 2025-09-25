using UnityEngine;

public class GunAimAtMouse : MonoBehaviour
{
    public Camera mainCamera;
    public float planeDistance = 10f; // distance from camera along forward

    void Update()
    {
        // Ray from mouse
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Plane perpendicular to camera at fixed distance
        Plane plane = new Plane(mainCamera.transform.forward, mainCamera.transform.position + mainCamera.transform.forward * planeDistance);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            // Rotate gun to look at this point
            Vector3 direction = hitPoint - transform.position;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }
}
