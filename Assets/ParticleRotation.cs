using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    private Vector2 dir2D;
    void Update()
    {
        dir2D = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.up = dir2D.normalized;
    }
}
