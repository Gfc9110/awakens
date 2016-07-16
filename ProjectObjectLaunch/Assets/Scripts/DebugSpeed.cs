using UnityEngine;
using System.Collections;

public class DebugSpeed : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () {

		Debug.Log (GetComponent<Rigidbody> ().velocity.magnitude);
		
	}
}
