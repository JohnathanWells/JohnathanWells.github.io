using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class RopeProperties
{
    public GameObject tetherObject;
    public GameObject connectedObject;
    public GameObject parentObject; // For organization in the editor

    public float minLength;
    public float maxLength;

    public Vector2 connectionOffset;

    public Rigidbody2D connectedBody {
        get { 
            return connectedObject.GetComponent<Rigidbody2D>(); 
        } 
    }

    public Vector3 connectedPosition {
        get {
            return connectedObject.transform.position;
        }
    }

    public Rigidbody2D tetherBody {
        get {
            return tetherObject.GetComponent<Rigidbody2D>();
        }
    }

    public Vector3 tetherPosition {
        get {
            return tetherObject.transform.position;
        }
    }
}
