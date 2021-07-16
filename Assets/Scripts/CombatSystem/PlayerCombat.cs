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
    public int health;
    [SerializeField] private int currentHealth;


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

        // Damage them
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<GlobalEnemy>().TakeDamage(attackDamage);
            
        }
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

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}