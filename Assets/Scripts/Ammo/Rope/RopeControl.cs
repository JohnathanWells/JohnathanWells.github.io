using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeControl : MonoBehaviour {
    public float speedBoostFactor;
    public float climbSpeed;

    private GameObject player;
    private Rigidbody2D playerBody;
    private SpriteRenderer playerRenderer;
    public HookshotControl hookshot;
    public GameObject hook;

    private LineRenderer line;
    private DistanceJoint2D rope;
    public Vector2 tetherOffset;
    public float minLength;
    public float maxLength;

    private bool attached;
    private bool boostEnabled;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
        playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
        attached = false;
        boostEnabled = false;
	}

	void Update () {
        if (attached) {
            boostEnabled = Input.GetButton("Fire2");
        } else if (Vector3.Distance(hookshot.transform.position, hook.transform.position) > maxLength) {
            hookshot.RetractRope();
        }
    }

    void FixedUpdate()
    {
        if (attached) {
            ControlRope();
            RotateObjectTowardsRope();
        }
        DrawRope();
    }

    void ControlRope()
    {
        float vertical = boostEnabled ? speedBoostFactor : Input.GetAxis("Vertical");
        float distance = vertical * climbSpeed * Time.fixedDeltaTime;
        rope.distance = Mathf.Clamp(rope.distance - distance, minLength, maxLength);
    }

    public void AttachRope()
    {
        MakeRope();
        attached = true;
    }

    private void MakeRope()
    {
        float initialDistance = Vector2.Distance(player.transform.position, hook.transform.position);
        rope = (DistanceJoint2D)player.AddComponent<DistanceJoint2D>();
        rope.connectedBody = hook.GetComponent<Rigidbody2D>();
        rope.distance = Mathf.Clamp(initialDistance, minLength, maxLength);
        rope.maxDistanceOnly = true;
    }

    public void DetachRope()
    {
        if (attached)
        {
            playerRenderer.gameObject.transform.rotation = Quaternion.identity;
            DestroyObject(rope);
        }
        attached = false;
    }

    void RotateObjectTowardsRope()
    {
        Transform spriteTransform = playerRenderer.gameObject.transform;

        Vector2 jointDirection = hook.transform.position - spriteTransform.position;
        spriteTransform.rotation = Quaternion.FromToRotation(Vector2.right, jointDirection);
        spriteTransform.Rotate(jointDirection, playerBody.velocity.x < 0f ? 180f : 0f, Space.World);

        rope.anchor = playerRenderer.transform.localPosition + spriteTransform.rotation * tetherOffset;
    }

    void DrawRope()
    {
        if (rope)
            line.SetPosition(0, rope.transform.position + (Vector3)rope.anchor);
        else
            line.SetPosition(0, player.transform.position + playerRenderer.transform.localPosition + playerRenderer.transform.rotation * tetherOffset);
        line.SetPosition(1, hook.transform.position);
    }
}
