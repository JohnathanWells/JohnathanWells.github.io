using UnityEngine;
using System.Collections;

public class DestroyOutsideBounds : MonoBehaviour {
    public GameObject boundary;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == boundary)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
