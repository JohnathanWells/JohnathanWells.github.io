using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpControl))]
public class FrogMovement : MonoBehaviour
{
    public JumpControl jump;
    public float jumpForce;
    public float forwardForce;
    public float airForce;
    public float maxSpeed;
    private Rigidbody2D player;
    private Vector2 contactNormal;
    private bool facingRight;

    enum FrogState
    {
        GROUND,
        HOPPING,
        LANDED
    }
    private FrogState state;

    private delegate void StateFunction();
    private StateFunction[] stateProcesses;

    void MapStateFunctions()
    {
        stateProcesses = new StateFunction[] {
            () => Ground(),
            () => Hopping(),
            () => Landed()
        };
    }

    // Use this for initialization
    void Start()
    {
        facingRight = true;
        player = GetComponent<Rigidbody2D>();
        MapStateFunctions();
        ChangeState(FrogState.GROUND);
    }

    void ChangeState(FrogState newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stateProcesses[(int)state]();

        if ((Input.GetAxisRaw("Horizontal") > 0 && player.velocity.x < 0) ||
                (Input.GetAxisRaw("Horizontal") < 0 && player.velocity.x > 0))
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
    }

    void Ground()
    {
        float moveForce = Input.GetAxisRaw("Horizontal") * forwardForce;
        Vector2 force = new Vector2(moveForce, jumpForce);

        force = Input.GetAxisRaw("Horizontal") != 0 ? force : Vector2.zero;

        player.AddForce(force, ForceMode2D.Impulse);
        ChangeState(FrogState.HOPPING);
    }

    void Hopping()
    {
        if(jump.isGrounded())
        {
            ChangeState(FrogState.LANDED);
        }
        else if(Mathf.Abs(player.velocity.x) <= maxSpeed)
        {
            Vector2 force = new Vector2(airForce, 0) * Input.GetAxisRaw("Horizontal");
            player.AddForce(force);
        }
    }

    void Landed()
    {
        ChangeState(FrogState.GROUND);
    }

    void Orient()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, contactNormal) - 90f);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
    }

    void Flip()
    {
        Vector3 angle = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0).eulerAngles;
        angle.y = facingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(angle);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        contactNormal = c.contacts[0].normal;
    }
}
