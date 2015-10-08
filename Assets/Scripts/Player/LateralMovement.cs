using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpControl))]
public class LateralMovement : MonoBehaviour
{
    public float speed;
    public float force;
    public HookshotControl hookshotControl;
    public SpriteRenderer characterSprite;
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
        else if (hookshotControl.IsHooked())
        {
            Vector2 pivotPoint = hookshotControl.HookPoint();
            if (horizontal > 0 && pivotPoint.x >= transform.position.x || horizontal < 0 && pivotPoint.x <= transform.position.x)
            {
                Vector2 lateralForce = Vector3.Cross((Vector3)pivotPoint - transform.position, Vector3.forward).normalized;
                lateralForce *= horizontal * force / (player.velocity.magnitude + 1f);
                player.AddForce(lateralForce);
            }
        }
        else if (Mathf.Abs(player.velocity.magnitude) < speed)
        {
            Vector2 lateralForce = Vector2.right * horizontal * force / (player.velocity.magnitude + 1f);
            player.AddForce(lateralForce);
        }
    }

    void Orient(float horizontal)
    {
        if (jump.isGrounded() && horizontal != 0)
        {
            characterSprite.transform.rotation = Quaternion.FromToRotation(Vector2.right, horizontal * Vector2.right);
        }
    }
}
