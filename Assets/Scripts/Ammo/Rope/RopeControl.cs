using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeControl : MonoBehaviour {
    public float speedBoostFactor;
    public float climbSpeed;

    private GameObject player;
    private SpriteRenderer playerRenderer;
    public HookshotControl hookshot;
    public GameObject hook;

    private LineRenderer line;
    private DistanceJoint2D rope;
    public float minLength;
    public float maxLength;

    private bool attached;
    private bool boostEnabled;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
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
        playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
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
    }

    void RotateObjectTowardsRope()
    {
        Vector2 jointDirection = player.transform.position - hook.transform.position;
        Quaternion rotation = Quaternion.FromToRotation(Vector2.right, -jointDirection);
        playerRenderer.gameObject.transform.rotation = rotation;
    }

    void DrawRope()
    {
        line.SetPosition(0, hookshot.transform.position);
        line.SetPosition(1, hook.transform.position);
    }
}
