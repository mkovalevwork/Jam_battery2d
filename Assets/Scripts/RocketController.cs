using UnityEngine;

public class RocketController : MonoBehaviour
{
    private GameObject player;
    public float speed;
    float flySpeed;
    Vector2 targetPos;

    void Start()
    {
        player = PlayerManager.instance.player;
        targetPos = player.transform.position;
    }

    void Update()
    {
        flySpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, flySpeed);
    }


}
