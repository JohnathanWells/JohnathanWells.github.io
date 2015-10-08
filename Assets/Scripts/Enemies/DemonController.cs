using UnityEngine;
using System.Collections;

public class DemonController : MonoBehaviour {

    private float health;

	void Start () {
        health = 100;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (other.name == "IceShard(Clone)")
            {
                health -= 25f;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
