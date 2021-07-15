using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour
{
    public float distanceToPlayer;
    public float lookRange;
    

    Transform target;
    GameObject player;
    NavMeshAgent agent;

    CharacterStats myStats;



    void Start()
    {
        //Setup NuvMesh Properties
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;


        //player = GameObject.FindGameObjectWithTag("Player");
        player = PlayerManager.instance.player;
        target = player.transform;

        myStats = GetComponent<CharacterStats>();
    }
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= lookRange)
        {
            agent.SetDestination(target.position);

            if (distanceToPlayer <= agent.stoppingDistance)
            {
                /*CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
                if (playerCombat != null)
                {
                    playerCombat.Attack(player.GetComponent<PlayerStats>());
                    Debug.Log(myStats.currentHealth);
                }*/
                //attack the player
                //facetarget? brackeys making ai
                
            }
        }



    

    void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRange);

        }
    }
}   
