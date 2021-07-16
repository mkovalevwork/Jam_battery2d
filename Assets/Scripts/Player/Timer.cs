using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [HideInInspector] public bool canStart;
    public float time;
    private float cooldown = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.R)) StartCoroutine(TimeTick());
    }

    private IEnumerator TimeTick()
    {
        if (cooldown <= 0)
        {
            cooldown = time;
            Debug.Log("Time Out");
            canStart = true;
        }
        else
        {
            cooldown -= Time.fixedDeltaTime;
            canStart = false;
        }
        yield return null;
    }
}
