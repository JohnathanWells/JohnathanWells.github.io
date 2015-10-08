using UnityEngine;
using System.Collections;

public class IcemanController : MonoBehaviour {
    private float health;

	void Start () {
        health = 100;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Fireball(Clone)")
        {
            health -= 50f;
            if (health <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
