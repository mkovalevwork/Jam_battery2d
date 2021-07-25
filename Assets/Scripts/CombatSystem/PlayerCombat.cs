using System.Collections;
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
    bool alreadyAttacked; //for cooldown damage to enemys

    bool canTakeDamage = true;//for hit from fire

    [SerializeField] bool ableToPickUpExtinguisher = false;
    public bool hasExtinguisher = false;
    [SerializeField] private GameObject nearExtinguisher;
    [SerializeField] bool readyToKillFire = false;

    //ui
    public GameObject gameOverScreenHandler;
    public HealthBar healthBar;
    public GameObject extinguisherIcon;

    bool isDead = false;


    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        if (hasExtinguisher)
        {
            extinguisherIcon.SetActive(true);
        }
        if (!hasExtinguisher)
        {
            extinguisherIcon.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentHealth > 0)
            {
                Attack();
            }
            
        }

        if (ableToPickUpExtinguisher)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                PickUpExtinguisher();
            }
        }

        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Health<=0");
            if (isDead == false)
            {
                Debug.Log("load isDeadChanger");
                isDeadChanger();
            }
            //Invoke(nameof(DestroyPlayer), 4f);
            //death Animation to add/sound///////////////////////////
        }
    }
     void isDeadChanger()
    {
        Debug.Log("Start isDeadChanger");
        isDead = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/player_death");

        gameOverScreenHandler.GetComponent<gameOverScreen>().ShowEndScreen();
        animator.SetTrigger("isDead");
        gameObject.GetComponent<Movement>().enabled = false;
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

            
            animator.SetBool("ReadyToAttack", false);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }

    void ResetAttack()
    {
        //timer for attacks
        alreadyAttacked = false;
        animator.SetBool("ReadyToAttack", true);
    }

    public void TakeDamage(int damage)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/player_damage");

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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

    #region ChargeMechanic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Charger"))
        {
            if (collision.GetComponent<ChargerController>().hasPower == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Environment/Charger/charger");
                collision.GetComponent<Animator>().SetBool("Using", true);
                ReCharging();
                GetComponent<Movement>().onStun = true;
                collision.GetComponent<ChargerController>().hasPower = false;
                collision.GetComponent<Animator>().SetBool("IsEmpty", true);
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
    #endregion

    
    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Exit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ToBeContinued");
        }
        //for foreobstacle
        if (trigger.gameObject.tag == "FireObstacle")
        {
            if (canTakeDamage)
            {
                StartCoroutine(WaitForSeconds());
                currentHealth = (currentHealth - trigger.gameObject.GetComponent<FireObstacleController>().DamageOfFireCollision);
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/player_damage");
            }
            
        }
        if (trigger.gameObject.tag == "FireObstaclTrigger")
        {
            readyToKillFire = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hasExtinguisher)
                {
                    Destroy(trigger.gameObject);
                    readyToKillFire = false;
                    hasExtinguisher = false;
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Extinguisher/extinguisher_dash");
                }
                
            }
        }
        //for Extinguisher
        if (trigger.gameObject.tag == "Extinguisher")
        {
            ableToPickUpExtinguisher = true;
            nearExtinguisher = trigger.gameObject;
        }
        else
        {
            ableToPickUpExtinguisher = false;
            nearExtinguisher = null;
        }
    }

    /*private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Extinguisher")
        {
            ableToPickUpExtinguisher = false;
            nearExtinguisher = null;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ToBeContinued");
        }
    }

    IEnumerator WaitForSeconds()
    {
        canTakeDamage = false;
        yield return new WaitForSecondsRealtime(3);
        canTakeDamage = true;
    }
       
    void PickUpExtinguisher()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/Extinguisher/extinguisher_up");

        Destroy(nearExtinguisher);
        ableToPickUpExtinguisher = false;
        hasExtinguisher = true;
    }

   
}
