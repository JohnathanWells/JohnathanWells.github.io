using UnityEngine;
using System.Collections;

public class PatrolBetweenPoints : MonoBehaviour {
    public float xMin;
    public float xMax;

    public float patrolSpeed;

    bool headingRight;

	void Start () 
    {
        headingRight = Random.value > 0.5f;
        Flip();
        Vector3 pos = transform.localPosition;
        pos.x = (xMax + xMin) / 2.0f;
        transform.localPosition = pos;
	}
	
	void Update () 
    {
        if((headingRight && transform.localPosition.x > xMax) 
        || (!headingRight && transform.localPosition.x < xMin)) 
        {
            Flip();
        }

        // Translating relative to current transform makes sure that creature is always walking in the way it's rotated.
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime, transform);
	}

    void Flip()
    {
        headingRight = !headingRight;
        transform.rotation = Quaternion.Euler(0f, headingRight ? 0f : 180f, 0f);
    }
}
