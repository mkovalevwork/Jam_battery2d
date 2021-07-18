using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ExtinguishingController : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    
    // Active range
    [SerializeField] private float range;

    // Event when active can Start
    [SerializeField] private UnityEvent canActive;

    // Signal for Fire Controller
    [HideInInspector] public bool animation;
    
    void Start()
    {
        canActive = new UnityEvent();
        canActive.AddListener(Animate);
        StartCoroutine(IRayDraw());
    }

    private IEnumerator IRayDraw()
    {
        while (true)
        {
            GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
            foreach (GameObject fire in fires)
            {
                Vector2 dir = fire.transform.position - transform.position;
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir);

                if (dir.magnitude <= range && Input.GetKeyDown(KeyCode.E))
                {
                    canActive.Invoke();
                }
            }
            yield return null;
        }
    }

    void Animate()
    {
        particle.SetActive(true);
        animation = true;
        Invoke("ParticleActiveFalse", 1f);
    }

    void ParticleActiveFalse()
    {
        particle.SetActive(false);
    }
}
