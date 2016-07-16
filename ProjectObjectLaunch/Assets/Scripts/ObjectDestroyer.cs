using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour {

	void OnCollisionEnter(Collision col){

		Destroy (col.collider.gameObject);

	}

}
