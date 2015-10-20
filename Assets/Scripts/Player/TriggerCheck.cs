using UnityEngine;
using System.Collections;

public class TriggerCheck : MonoBehaviour {
    public HookshotControl hookshot;

    private GameObject lastSpawn;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Win Condition"))
        {
            c.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if (c.CompareTag("Respawn"))
        {
            lastSpawn = c.gameObject;
        }
        if (c.CompareTag("Enemy") || (c.CompareTag("Water") && !hookshot.IsHooked()))
        {
            Respawn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = lastSpawn.transform.position;
        if (hookshot.IsHooked())
        {
            hookshot.CancelHook();
        }
    }
}
