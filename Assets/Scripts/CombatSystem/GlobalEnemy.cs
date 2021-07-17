using UnityEngine;
using UnityEngine.AI;

public class GlobalEnemy : MonoBehaviour
{
    //Enemy STATS
    //Enemy MOVEMENT
    //Enemy States: Patroling/Chasing/Attacking
    //Enemy TakeDamage
    //Enemy Destroy


    //blue - SightRange Visualize
    //green - AttackRange Visualize

    //STATS
    public int health;
    [SerializeField]private int currentHealth;
    public int damage;
    public float timeBetweenAttacks;
    public float sightRange;
    public float attackRange;

    //Path of sound event in Fmod
    public string detectSoundPath;
    public string damageSoundPath;
    public string attackSoundPath;
    public string deathSoundPath;


    //States
    private NavMeshAgent agent;
    //attack    
    bool alreadyAttacked;


    //Utility

    private GameObject player;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    public LayerMask playerLayer;


    void Start()
    {
        //STATS
        currentHealth = health;

        //States
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Utility
        player = PlayerManager.instance.player;
    }

    void Update()
    {
        //Check if player insideSightRange
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        //Choose state
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }


    void ChasePlayer()
    {
        //Chase State
        agent.SetDestination(player.transform.position);
        //death Animation to add/sound///////////////////////////
    }

    void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //Turn on if needed
        //transform.LookAt(player.transform);

        if (!alreadyAttacked)
        {
            //Attack code here
            Debug.Log("attack done");
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
            //attack Animation to add/sound///////////////////////////

            // Play attack sound
            PlaySound(attackSoundPath);

            //End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }

    void ResetAttack()
    {
        //timer for attacks
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            PlaySound(damageSoundPath);
        }

        if (currentHealth <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
            //death Animation to add/sound///////////////////////////
            PlaySound(deathSoundPath);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);

    }



    private void OnDrawGizmosSelected()
    {
        //visualize attack and sight range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
