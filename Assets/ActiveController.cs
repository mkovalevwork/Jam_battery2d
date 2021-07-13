using UnityEngine;

public class ActiveController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    void OnTriggerEnter2D(Collider2D col)
    {
        box.isTrigger = false;
        GetComponent<Movement>().enabled = true;
        Destroy(col.gameObject);
    }
}