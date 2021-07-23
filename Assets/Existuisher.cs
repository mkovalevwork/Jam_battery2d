using UnityEngine;

public class Existuisher : MonoBehaviour
{    
    public float amount;
    public float costPerFixedUpdate;
    public ParticleSystem system;
    private bool hasExti = false;

    public string extinguisherSound;
    public string extinguisherEmptySound;

    [FMODUnity.EventRef]
    private FMOD.Studio.EventInstance eventInstance;

    private void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(extinguisherSound);
    }

    void FixedUpdate()
    {
        TurnOn();
        hasExti = GetComponent<PlayerCombat>().hasExtinguisher;
    }
    
    void TurnOn()
    {
        if (Input.GetMouseButton(1) && amount > 0 && hasExti)
        {
            // Set event parameter. Transform to "from 0 to 1" value.
            eventInstance.setParameterByName("extinguisher_charge", amount / 1000);

            // Play fmod event.
            eventInstance.start();

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
            // Stop fmod event.
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Debug.Log("Mouse notpressed");           
        }
    }
}
