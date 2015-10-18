using UnityEngine;
using System.Collections;

public class loadLevelButton : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        Application.LoadLevel(levelToLoad);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
