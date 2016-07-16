using UnityEngine;
using System.Collections;

public class ZombieDamage : MonoBehaviour {

	int lifePoints = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (lifePoints < 0) {

			GetComponent<ZombieMovement> ().player.GetComponent<DrawCrosshair> ().ZombieKilled ();

			Destroy (gameObject);

		}
	
	}

	void OnCollisionEnter(Collision col){

		if (col.collider.transform.tag == "Oggetto Usabile") {

			if (col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude > 1.5f) {

				lifePoints -= Mathf.RoundToInt (col.relativeVelocity.magnitude * (Mathf.Pow (col.gameObject.GetComponent<Rigidbody> ().mass / 2, 2)) / 5);

			}
			
		} else {

			float verticalSpeed = col.relativeVelocity.y;

			lifePoints -= Mathf.RoundToInt (verticalSpeed>8 ? verticalSpeed : 0);

		}

	}

}
