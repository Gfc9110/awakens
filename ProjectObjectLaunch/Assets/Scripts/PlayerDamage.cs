using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	public int lifePoints = 100;

	int startingHitTime = 15;

	int hitTime = 15;
	
	// Update is called once per frame
	void Update () {

		if (hitTime-- <= 0) {

			hitTime = startingHitTime;

			Vector3 center = new Vector3 (transform.position.x, transform.position.y + 0.85f, transform.position.z);

			Collider[] colliders = Physics.OverlapSphere (center, 0.85f);

			int damage = 0;

			foreach (Collider col in colliders){

				if (col.gameObject.tag == "Zombie")
					damage += Random.Range (1,3);

			}

			lifePoints -= damage;

		}

		//Debug.Log (lifePoints);

		if (lifePoints <= 0)
			Application.Quit ();
	
	}

	void OnCollisionEnter(Collision col){

		float verticalSpeed = col.relativeVelocity.y;

		lifePoints -= Mathf.RoundToInt (verticalSpeed>8 ? verticalSpeed : 0);

	}

}
