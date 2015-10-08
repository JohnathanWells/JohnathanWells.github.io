using UnityEngine;
using System.Collections;

public class SpikeSpawner : MonoBehaviour {
    public GameObject spikePrefab;
    public float spawnDelay;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Random.value * nextSpawnTime + Time.time;
    }

	void Update () {
        if (nextSpawnTime > Time.time)
        {
            Instantiate(spikePrefab, transform.position, transform.rotation);
        }
	}
}
