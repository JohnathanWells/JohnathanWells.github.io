using UnityEngine;
using System.Collections;

public class AimAtMouse : MonoBehaviour {
    private bool aimingEnabled;

    void Start()
    {
        aimingEnabled = true;
    }

    void Update()
    {
        if (!aimingEnabled) {
            return;
        }

        if(Time.timeScale != 0f)
        {
            // Figure out angle from horizontal
            Vector3 pivotPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 coords = Input.mousePosition;
            Vector3 direction = coords - pivotPos;
            if (direction.x < 0)
            {
                direction.x = -direction.x;
                Vector3 angles = Quaternion.FromToRotation(Vector3.right, direction).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, 180f, angles.z);
            }
            else
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
            }
        }
    }

    public void ToggleAiming(bool flag)
    {
        aimingEnabled = flag;
    }
}
