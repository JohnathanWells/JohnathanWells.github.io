using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpControl))]
public class LateralMovement : MonoBehaviour
{
    public float speed;
    public float force;
    private JumpControl jump;
    private Rigidbody2D player;
    private Vector2 contactNormal;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        jump = GetComponent<JumpControl>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Move(horizontal);
        Orient(horizontal);
    }

    void Move(float horizontal)
    {
        if (jump.isGrounded())
        {
            Vector2 lateralVelocity = transform.right * horizontal * speed;
            Vector2 velocity = player.velocity;
            velocity.x = lateralVelocity.x;
            player.velocity = velocity;
        }
        else if(Mathf.Abs(player.velocity.magnitude) < speed)
        {
            Vector2 lateralForce = Vector2.right * horizontal * force / (player.velocity.magnitude + 1f);
            player.AddForce(lateralForce);
        }
    }

    void Orient(float horizontal)
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, contactNormal) - 90f);

        if (!jump.isGrounded())
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Environment"))
        {
            contactNormal = c.contacts[0].normal;
        }
    }
}
