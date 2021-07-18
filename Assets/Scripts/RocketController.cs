using UnityEngine;

public class RocketController : MonoBehaviour
{
    private GameObject player;
    public float speed;
    float flySpeed;
    Vector2 targetPos;
    public int damage;
    public float radiusOfDamage;

    void Start()
    {
        player = PlayerManager.instance.player;
        targetPos = player.transform.position;
    }

    void Update()
    {
        flySpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, flySpeed);
        if (transform.position.x == targetPos.x || transform.position.y == targetPos.y)
        {
            if (Vector2.Distance(transform.position,player.transform.position) < radiusOfDamage)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }


}
