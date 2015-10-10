using UnityEngine;
using System.Collections;

public class AnimateFrog : MonoBehaviour
{
    public JumpControl jump;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", jump.isGrounded());
        anim.SetBool("Moving", Input.GetAxisRaw("Horizontal") != 0);
    }
}
