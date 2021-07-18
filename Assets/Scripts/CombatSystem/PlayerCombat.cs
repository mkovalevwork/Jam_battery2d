using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange;
    public int attackDamage = 40;
    public int attackCoast;
    public int health;
    public float timeBetweenAttacks;
    [SerializeField] private int currentHealth;
    bool alreadyAttacked;


    //test sound
    //public AudioSource hitSound;

    void Start()
    {
        currentHealth = health;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
    }

    
    void Attack()
    {
        // play an attack animation
        animator.SetTrigger("Attack");


        // detect enemys in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        if (!alreadyAttacked)
        {
            // Damage them
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<CanEnemy>().TakeDamage(attackDamage);
            }

            // Play attack sound.
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/shock_attack");

            alreadyAttacked = true;

            currentHealth -= attackCoast;

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

        if (currentHealth <= 0)
        {
            Invoke(nameof(DestroyPlayer), 0.5f);
            //death Animation to add/sound///////////////////////////
        }
    }

    void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Charger"))
        {
            if (collision.GetComponent<ChargerController>().hasPower == true)
            {
                ReCharging();
                GetComponent<Movement>().onStun = true;
                collision.GetComponent<ChargerController>().hasPower = false;
                Invoke(nameof(ResetStun), collision.GetComponent<ChargerController>().chargeStunTimer);
            }
            
        }
    }
    void ReCharging()
    {
        currentHealth = health;
    }

    void ResetStun()
    {
        GetComponent<Movement>().onStun = false;
    }
}
