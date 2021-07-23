using UnityEngine;

public class Existuisher : MonoBehaviour
{    
    public float amount;
    public float costPerFixedUpdate;
    public ParticleSystem system;
    private bool hasExti = false;

    void FixedUpdate()
    {
        TurnOn();
        hasExti = GetComponent<PlayerCombat>().hasExtinguisher;
    }
    
    void TurnOn()
    {
        if (Input.GetMouseButton(1) && amount > 0 && hasExti)
        {
            Debug.Log("Mouse pressed");
            system.Play(true);            
            amount -= costPerFixedUpdate;
            if (amount == 0)
            {
                GetComponent<PlayerCombat>().hasExtinguisher = false;
            }
        }

       
        else
        {
            system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Debug.Log("Mouse notpressed");           
        }
    }
}
