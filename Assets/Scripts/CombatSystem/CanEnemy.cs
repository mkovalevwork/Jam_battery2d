using UnityEngine;
using UnityEngine.AI;

public class CanEnemy : MonoBehaviour
{
    //Enemy STATS
    //Enemy MOVEMENT
    //Enemy States: Patroling/Chasing/Attacking
    //Enemy TakeDamage
    //Enemy Destroy


    //blue - SightRange Visualize
    //green - AttackRange Visualize

    //CHOSE ENEMY TYPE

    [SerializeField] private bool canEnemy;
    [SerializeField] private bool toasterEnemy;

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
    public GameObject projectile;


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

        if (canEnemy)
        {
            //Choose state
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        if (toasterEnemy)
        {
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
            if (alreadyAttacked)
            {
                Patrolling();
            }
        }
        
        
        
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

        if (!alreadyAttacked && canEnemy)
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
        if (!alreadyAttacked && toasterEnemy)
        {
            //Attack code here
            Debug.Log("attack done");
            Instantiate(projectile,transform.position,Quaternion.identity);
            
            //attack Animation to add/sound///////////////////////////

            // Play attack sound ƒÀﬂ «¿œ”— ¿ –¿ ≈“€
            //PlaySound(attackSoundPath);

            //End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }

    }

    void ResetAttack()
    {
        //timer for attacks
        alreadyAttacked = false;
        agent.SetDestination(transform.position);
        Debug.Log("TIMERDONE");
        

    }

    void Patrolling()
    {
        if (Vector2.Distance(transform.position,player.transform.position) <= attackRange)
        {
            
            
            var heading = player.transform.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            agent.SetDestination(transform.position - direction);
            //Debug.Log("run away");


        }
        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            Debug.Log(" go to player");
            
            var heading = player.transform.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            agent.SetDestination(transform.position + direction);
            //Debug.Log(direction);
            //go to player
        }
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
