using UnityEngine;
using System.Collections;

public class HitSplat : MonoBehaviour
{
    public float lethalVelocity;
    public Transform frogSprite;
    private Rigidbody2D player;
    private bool lethalHit;
    private Vector3 frogScale;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        frogScale = frogSprite.localScale;
    }

    void FixedUpdate()
    {
        if (player.velocity.y <= -lethalVelocity)
        {
            lethalHit = true;
        }
        else
        {
            lethalHit = false;
        }
    }

    IEnumerator Splat(Vector3 splat, float lerpTime)
    {
        float t = 0;
        while (t < lerpTime)
        {
            frogSprite.localScale = Vector2.Lerp(frogSprite.localScale, splat, t);
            t += Time.deltaTime;
        }
        t = 0;
        while (t < lerpTime)
        {
            frogSprite.localScale = Vector2.Lerp(frogSprite.localScale, frogScale, t);
            t += Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (lethalHit)
        {
            float splatFactor = 1.6f;
            Vector2 destinSplat = new Vector2(frogSprite.localScale.x * splatFactor, frogSprite.localScale.y / splatFactor);
            StartCoroutine(Splat(destinSplat, 0.2f));
        }
    }
}
