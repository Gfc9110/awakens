using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {

	public GameObject player;
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 movement = player.transform.position - transform.position;

		movement.y = 0;

		movement = Vector3.ClampMagnitude (movement,0.06f)*Time.fixedDeltaTime*(60 + Random.Range (-10f, 10f));

		GetComponent<Rigidbody> ().MovePosition (transform.position + movement);
	
	}

}
