using UnityEngine;
using System.Collections.Generic;

class RopeLink
{
    public GameObject gameObject;
    public Transform transform;
    public SpringJoint2D joint;

    public Rigidbody2D body {
        get {
            return gameObject.GetComponent<Rigidbody2D>();
        }
    }

    public float length {
        get {
            return joint.distance;
        }
    }

    public RopeLink(Object prefab, Vector2 position, Rigidbody2D linkedBody)
    {
        gameObject = (GameObject)GameObject.Instantiate(prefab);
        transform = gameObject.transform;
        transform.position = position;

        joint = gameObject.GetComponent<SpringJoint2D>();
        joint.connectedBody = linkedBody;
        joint.distance = Vector2.Distance(position, linkedBody.transform.position);

        SetupCollider();
    }

    void SetupCollider()
    {
        BoxCollider2D bCollider = gameObject.GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(0.06f, joint.distance);
        bCollider.size = size;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, joint.connectedBody.transform.position - transform.position);
        Physics2D.IgnoreCollision(bCollider, joint.connectedBody.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(bCollider, joint.connectedBody.GetComponent<CircleCollider2D>());
    }
}
