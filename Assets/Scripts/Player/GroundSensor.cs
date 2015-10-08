using UnityEngine;
using System.Collections;

public class GroundSensor : MonoBehaviour {
    public JumpControl player;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Environment"))
        {
            player.SetGrounded(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Environment"))
        {
            player.SetGrounded(false);
        }
    }
}
