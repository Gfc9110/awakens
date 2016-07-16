using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour {

	public string objName = "";

	public bool stackable = false;

	public bool isTwoHands = false;

	public int maxStack = 0;

	public Vector3 handRotation = new Vector3 ();

	public Vector3 stackedOverPosition = new Vector3 ();

	Quaternion qHandRotation = new Quaternion ();

	Rigidbody rb;

	float mass = 0f;

	void Start(){

		qHandRotation.eulerAngles = handRotation;

		rb = GetComponent<Rigidbody> ();

		mass = rb.mass;

		maxStack--;

	}

	public Quaternion getHandRotation(){

		return qHandRotation;	
	}

	public string getName(){

		return objName;
	}

	public Rigidbody getRigidbody(){

		return rb;
	}

	public float getMass(){

		return mass;
	}

}
