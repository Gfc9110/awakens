using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public GameObject player;

	public GameObject zombiePrefab;

	public int spawnTime = 100;

	int remainingTime = 0;

	void Start () {

		remainingTime = Random.Range (0, spawnTime);
	
	}
	
	// Update is called once per frame
	void Update () {

		remainingTime--;

		if (remainingTime < 1) {

			remainingTime = spawnTime;

			GameObject z = (GameObject) Instantiate (zombiePrefab, transform.position, Quaternion.identity);

			z.GetComponent<ZombieMovement> ().player = player;

		}
	
	}
}
