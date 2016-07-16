using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	float maxSpeed = 0.10f;

	public Vector3 movement;

	public int stamina = 100;

	int startTimeJump = 10;

	int timeJump = 0;

	void FixedUpdate () {

		bool runButton = Input.GetButton ("Run");

		bool canRun = stamina > 0;

		movement = Input.GetAxisRaw ("Lateral") * transform.right * 5 + Input.GetAxisRaw ("Straight") * transform.forward * 5;

		movement = Vector3.ClampMagnitude (movement, maxSpeed*(isGrounded () ? 50 : 30))*Time.fixedDeltaTime;

		bool running = movement.magnitude > 0 && runButton && canRun;

		movement *= running ? 1.5f : 1;

		stamina += running ? -1 : ((stamina < 100 && !runButton) ? 1 : 0);

		GetComponent<Rigidbody> ().MovePosition (transform.position + movement);

		timeJump--;

		if (Input.GetButtonDown ("Jump") && isGrounded() && timeJump < 0 && GetComponent<Rigidbody>().velocity.y < 1) {
			
			GetComponent<Rigidbody> ().AddForce (transform.up*10000);

			timeJump = startTimeJump;

		}

	}

	bool isGrounded(){

		Collider[] colliders = Physics.OverlapSphere (new Vector3 (transform.position.x, transform.position.y , transform.position.z), 0.1f);

		return colliders.Length > 1;

	}

}
