using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Canvas pauseScreen;

    private float oldTime;

    // Use this for initialization
    void Start()
    {
        pauseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.enabled = !pauseScreen.enabled;
            if (pauseScreen.enabled)
            {
                oldTime = Time.timeScale;
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = oldTime;
            }
        }
    }

}
