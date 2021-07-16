using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private Timer time;
    [SerializeField] private float rangeAttack;
    private GameObject clone;
    private GameObject enemy;
    
    
    private void Start()
    {
        StartCoroutine(IRayDraw());
    }

    private IEnumerator IRayDraw()
    {
        while (true)
        {
            if(time.canStart && Input.GetKey(KeyCode.R))
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy");
                Vector2 dir = enemy.transform.position - transform.position;
                Debug.DrawRay(transform.position, dir, Color.yellow);
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir, rangeAttack);

                if (hit2D.transform.name != null)
                {
                    Debug.Log("Damaged: " + hit2D.transform.name);
                }
            }

            yield return null;
        }
    }
}
