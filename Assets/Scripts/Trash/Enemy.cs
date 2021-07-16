using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // функция получения урона, смерти
    // Заготовки для анимаций получения урона и смерти

    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt anumation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        // die animation
        animator.SetBool("IsDead", true);

        // disable enemy
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        this.enabled = false;
    }
}
