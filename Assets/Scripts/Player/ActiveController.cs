using UnityEngine;

public class ActiveController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    private bool isActive;
    private GameObject player;
    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKey(KeyCode.E) && !isActive)
        {
            box.isTrigger = false;
            isActive = true;
            GetComponent<Movement>().enabled = true;
            player = col.gameObject;
            col.transform.parent = transform;
            col.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            box.isTrigger = true;
            isActive = false;
            GetComponent<Movement>().enabled = false;
            player.SetActive(true);
            player.transform.localPosition = Vector2.right * 1.5f * transform.localScale;
            player.transform.parent = null;
        }
    }
}