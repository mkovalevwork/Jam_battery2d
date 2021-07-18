using UnityEngine;

public class RotationToMouse : MonoBehaviour
{
    public Camera playerCamera;

    private Vector3 rotateDirection;
    private float rotateAngle;

    void Update()
    {
        // Define mouse position.
        rotateDirection = Input.mousePosition - playerCamera.WorldToScreenPoint(transform.position);

        // Calculate rotating angle.
        rotateAngle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;

        // Rotate the object
        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
        
        
    }
}
