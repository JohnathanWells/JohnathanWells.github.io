using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Rope
{
    public float climbSpeed;
    public float boostSpeed;

    public float minLength;
    public float maxLength;
}

[System.Serializable]
public struct RopeSpring
{
    public Vector2 anchorOffset;
    public float dampingRatio;
    public float frequency;
}

public class RopeControl : MonoBehaviour {
    private GameObject player;
    private SpriteRenderer playerRenderer;

    public HookshotControl hookshot;
    public GameObject hook;

    public Rope ropeProperties;
    public RopeSpring springProperties;

    public GameObject springPrefab;

    private LineRenderer line;
    private DistanceJoint2D rope;
    private SpringJoint2D spring;

    private bool attached;
    private bool boostEnabled;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
        attached = false;
        boostEnabled = false;
	}

	void Update () {
        if (attached) {
            boostEnabled = Input.GetButton("Fire2");
        } else if (Vector3.Distance(hookshot.transform.position, hook.transform.position) > ropeProperties.maxLength) {
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
        float vertical = boostEnabled ? ropeProperties.boostSpeed : Input.GetAxis("Vertical");
        float distance = vertical * ropeProperties.climbSpeed * Time.fixedDeltaTime;
        rope.distance = Mathf.Clamp(rope.distance - distance, 
                                    ropeProperties.minLength, 
                                    ropeProperties.maxLength);
    }

    public void AttachRope()
    {
        MakeRope();
        line.SetVertexCount(3);
        attached = true;
        DrawRope();
    }

    private void MakeRope()
    {
        float initialDistance = Vector2.Distance(player.transform.position, hook.transform.position);

        // Adding spring joint first. The rope will attach to it
        spring = (SpringJoint2D)player.AddComponent<SpringJoint2D>();
        spring.distance = 0f;
        spring.dampingRatio = springProperties.dampingRatio;
        spring.frequency = springProperties.frequency;
        RotateObjectTowardsRope();

        // Rope Joint 
        Vector2 jointPosition = (Vector2)spring.transform.position + spring.anchor;
        GameObject ropeObj = (GameObject)(GameObject.Instantiate(springPrefab, jointPosition, Quaternion.identity));
        ropeObj.transform.parent = player.transform;

        rope = ropeObj.GetComponent<DistanceJoint2D>();
        rope.connectedBody = hook.GetComponent<Rigidbody2D>();
        rope.distance = Mathf.Clamp(initialDistance, ropeProperties.minLength, ropeProperties.maxLength);
        rope.maxDistanceOnly = true;

        spring.connectedBody = rope.GetComponent<Rigidbody2D>();
    }

    public void DetachRope()
    {
        if (attached)
        {
            playerRenderer.gameObject.transform.rotation = Quaternion.identity;
            DestroyObject(rope);
            DestroyObject(spring);
        }
        line.SetVertexCount(2);
        attached = false;
    }

    void RotateObjectTowardsRope()
    {
        Transform spriteTransform = playerRenderer.gameObject.transform;

        Vector2 jointDirection = hook.transform.position - spriteTransform.position;
        spriteTransform.rotation = Quaternion.FromToRotation(Vector2.right, jointDirection);

        spring.anchor = playerRenderer.transform.localPosition + spriteTransform.rotation * springProperties.anchorOffset;
    }

    void DrawRope()
    {
        if (attached)
        {
            line.SetPosition(0, spring.transform.position + (Vector3)spring.anchor);
            line.SetPosition(1, rope.transform.position);
            line.SetPosition(2, hook.transform.position);
        }
        else
        {
            line.SetPosition(0, player.transform.position 
                              + playerRenderer.transform.localPosition 
                              + playerRenderer.transform.rotation * springProperties.anchorOffset);
            line.SetPosition(1, hook.transform.position);
        }
    }
}
