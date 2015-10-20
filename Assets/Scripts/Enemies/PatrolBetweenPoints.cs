using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class PatrolBetweenPoints : MonoBehaviour {
    public float xMin;
    public float xMax;

    public float patrolSpeed;

    private Rigidbody2D body;
    private Vector2 velocity;
    bool headingRight;

	void Start () 
    {
        velocity = new Vector2(patrolSpeed, 0);
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
        headingRight = Random.value > 0.5f;
        Flip();
        Vector3 pos = transform.localPosition;
        pos.x = (xMax + xMin) / 2.0f;
        transform.localPosition = pos;
	}

    void FixedUpdate()
    {
        if((headingRight && transform.localPosition.x > xMax) 
        || (!headingRight && transform.localPosition.x < xMin)) 
            Flip();

        body.velocity = transform.rotation*velocity;
    }

    void Flip()
    {
        headingRight = !headingRight;
        transform.rotation = Quaternion.Euler(0f, headingRight ? 0f : 180f, 0f);
    }
}
