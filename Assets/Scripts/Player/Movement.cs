using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speedX, speedY;
    [SerializeField] private float speed;
    private Vector2 vel;
    private bool isSoundPlayed;
    public float soundDelay;

    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)) speedX = speed; 
        if (Input.GetKey(KeyCode.S)) speedX = -speed; 
        
        if (Input.GetKey(KeyCode.D)) speedY = speed; 
        if (Input.GetKey(KeyCode.A)) speedY = -speed;

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            speedX = 0;
        }
        
        if(!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            speedY = 0;
        }

        vel = new Vector2(speedY, speedX);
        
        player.Translate(vel);

        if (speedX != 0 || speedY != 0)
        {
            if (!isSoundPlayed)
            {
                PlayFootstepsSound();
                isSoundPlayed = true;

                Invoke(nameof(ResetAttack), soundDelay);
            }
        }
        {

        }
    }

    // Воспроизведение звука шагов.
    void PlayFootstepsSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/player_footsteps");
    }

    void ResetAttack()
    {
        //timer for sound
        isSoundPlayed = false;
    }
}
