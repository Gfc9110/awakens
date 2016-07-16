using UnityEngine;
using System.Collections;

public class CameraVerticalLook : MonoBehaviour {
	
	void FixedUpdate () {

		float angle = transform.localRotation.eulerAngles.x;
		angle -= Input.GetAxis ("Vertical")*2*Time.fixedDeltaTime*60;
		if (angle > 180)
			angle -= 360;
		transform.localRotation = Quaternion.Euler (Mathf.Clamp (angle, -90, 90),0,0);
	
	}
}
