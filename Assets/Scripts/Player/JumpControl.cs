using UnityEngine;
using System.Collections;

public class JumpControl : MonoBehaviour {
    public float force;

    private Rigidbody2D body;

    private bool jump;
    private bool doubleJump;
    public bool canDoubleJump;

    private bool grounded;

	void Start () 
    {
        body = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        if (Input.GetButtonDown("Jump") && Time.timeScale > 0f)
        {
            if (grounded) {
                jump = true;
            } else if (doubleJump && canDoubleJump) {
                jump = true;
                doubleJump = false;
            }
        }
	}

    void FixedUpdate()
    {
        if (jump) {
            body.AddForce(transform.up * force, ForceMode2D.Impulse);
            jump = false;
        }
    }

    public void SetGrounded(bool flag) {
        grounded = flag;
        doubleJump = flag || doubleJump;
    }

    public bool isGrounded()
    {
        return grounded;
    }
}
