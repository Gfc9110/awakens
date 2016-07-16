using UnityEngine;
using System.Collections;

public class PlayerHorizontalLook : MonoBehaviour {
	
	void FixedUpdate () {

		transform.rotation = Quaternion.AngleAxis ((Input.GetAxis ("Horizontal")*2*Time.fixedDeltaTime*60)+transform.eulerAngles.y, transform.up);
	
	}
}
