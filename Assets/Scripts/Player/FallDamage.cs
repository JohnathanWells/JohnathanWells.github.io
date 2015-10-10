using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpControl))]
public class FallDamage : MonoBehaviour
{
    public JumpControl jump;
    public float maxDownTime;
    private float fallingVelocity;
    private bool falling;
    private bool lethalFall;
    private bool didLethalFall;
    private Rigidbody2D player;
    private float time;
    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        falling = !jump.isGrounded();
        lethalFall = falling && player.velocity.y < -10f;

        if(didLethalFall)
        {
            time += Time.deltaTime;
        }

        if(time > 1)
        {
            didLethalFall = false;
            time = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(lethalFall)
        {
            didLethalFall = true;
            player.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
        }
    }

    public bool getLethalFall()
    {
        return didLethalFall;
    }
}
