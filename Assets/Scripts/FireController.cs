using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private ExtinguishingController player;
    

    private void Update()
    {
       if(player.animation) ani.SetTrigger("Destroy");
    }
}
