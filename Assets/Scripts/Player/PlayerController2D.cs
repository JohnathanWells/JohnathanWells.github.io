using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {

    public float maxSpeed;
    public float jumpHeight;
    public float rotationSpeed;

    public LayerMask validSurfaces;

    private GameObject lastSpawn;

    /*private Quaternion[] allRotations = new Quaternion[4] { 
		Quaternion.Euler(0, 0, 0),
		Quaternion.Euler(0, 0, 180),
		Quaternion.Euler(0, 0, 270),
		Quaternion.Euler(0, 0, 90)
	};*/
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale > 0f)
        {
            //updateMove();
        }
	}

    void FixedUpdate()
    {
        //Physics2D.gravity = GameManager.getGravity();
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, allRotations[GameManager.getState()], rotationSpeed * Time.deltaTime);
    }

    void updateMove()
    {
        /*if (GameManager.getState() == 0 || GameManager.getState() == 1)
        {
            if (Input.GetKey(GameManager.findKeyWithName(GameManager.keybinds, "Move Left").key) || Input.GetKey(GameManager.findKeyWithName(GameManager.keybinds, "Move Right").key))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            if (Input.GetKey(GameManager.findKeyWithName(GameManager.keybinds, "Move Up").key) || Input.GetKey(GameManager.findKeyWithName(GameManager.keybinds, "Move Down").key))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Input.GetAxis("Vertical") * maxSpeed);
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.CompareTag("Win Condition"))
        {
            c.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        if (c.CompareTag("Respawn"))
        {
            lastSpawn = c.gameObject;
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
    }
}
