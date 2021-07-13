using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speedX, speedY;
    [SerializeField] private float speed;
    private Vector2 vel;

    
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
    }
}
