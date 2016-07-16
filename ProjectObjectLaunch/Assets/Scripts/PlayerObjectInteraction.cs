using UnityEngine;
using System.Collections;

public class PlayerObjectInteraction : MonoBehaviour {

	public GameObject rightObjectPosition;

	public GameObject leftObjectPosition;

	public GameObject centerObjectPosition;

	public GameObject cam;

	string usableTag = "Oggetto Usabile";

	GameObject[] objects = {null,null};

	GameObject[] stackedObjects = new GameObject[10];

	bool[] areObjects = {false,false};

	int stackQty = 0;

	int stackSide = 0;

	void Update () {

		bool leftDown = Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.JoystickButton4);

		bool rightDown = Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.JoystickButton5);

		if (leftDown || rightDown) {

			Debug.Log (1);

			int clickSide = leftDown ? 0 : 1;

			int otherSide = leftDown ? 1 : 0;

			bool thisHandOccupied = areObjects [clickSide];

			if (thisHandOccupied) {

				Debug.Log (2);

				ObjectProperties thisObjProperties = objects [clickSide].GetComponent<ObjectProperties> ();

				bool isThisObjStackable = thisObjProperties.stackable;

				if (isThisObjStackable) {

					Debug.Log (3);

					RaycastHit hit = new RaycastHit ();

					bool objClicked = Physics.Raycast (cam.transform.position, cam.transform.forward, out hit);

					bool isObjUsable = false;

					bool notTooFar = false;

					if (objClicked) {

						isObjUsable = hit.collider.gameObject.tag == usableTag;	

						notTooFar = hit.distance < 2;

					}

					bool otherHandFree = !areObjects [otherSide];

					if (objClicked && isObjUsable && notTooFar) {

						if (otherHandFree) {

							bool notReachedMaxStackValue = stackQty < thisObjProperties.maxStack;

							if (notReachedMaxStackValue) {

								ObjectProperties clickedObjProperties = hit.collider.gameObject.GetComponent<ObjectProperties> ();

								bool sameObjType = thisObjProperties.getName () == clickedObjProperties.getName ();

								if (sameObjType) {

									AddToStack (clickSide, hit.collider.gameObject);

								} else {

									//TODO Error Message Not Same Object

								}

							} else {

								//TODO Error Message Max Stack Reached

							}

						} else {

							//TODO Error Message Other Hand Occupied

						}

					} else {

						Debug.Log (4);

						bool moreThanOneInStack = stackQty > 0;

						bool stackIsNotThisSide = clickSide == stackSide;

						if (moreThanOneInStack && stackIsNotThisSide) {

							if (otherHandFree) {

								UseStack (clickSide);

							} else {

								//TODO Error Message Other Hand Occupied

							}

						} else {

							UseObject (clickSide);

						}

					}

				} else {

					UseObject (clickSide);

				}

			} else {

				RaycastHit hit = new RaycastHit ();

				bool objClicked = Physics.Raycast (cam.transform.position, cam.transform.forward, out hit);

				bool isObjUsable = false;

				bool notTooFar = false;

				if (objClicked) {

					isObjUsable = hit.collider.gameObject.tag == usableTag;	

					notTooFar = hit.distance < 2;

				}

				if (objClicked && isObjUsable && notTooFar) {

					ObjectProperties clickedObjProperties = hit.collider.gameObject.GetComponent<ObjectProperties> ();

					if (clickedObjProperties.isTwoHands) {

						bool otherHandFree = !areObjects [otherSide];

						if (otherHandFree) {

							PickObjectTwoHands (hit.collider.gameObject);

						} else {

							//TODO Error Message Too Heavy To Pick With One Hand

						}

					} else {

						PickObjectOneHand (clickSide, hit.collider.gameObject);

					}

				} else {

					//TODO Hit With Hands

				}

			}

		}
	
	}

	void PickObjectTwoHands (GameObject obj){

		GameObject parentObject = centerObjectPosition;

		areObjects [0] = true;

		areObjects [1] = true;

		objects[0] = objects[1] = obj;

		Rigidbody rb = obj.GetComponent<Rigidbody> ();

		rb.isKinematic = true;

		rb.detectCollisions = false;

		obj.transform.SetParent (parentObject.transform);

		obj.transform.localPosition = new Vector3 (0, 0, 0);

		obj.transform.localRotation = obj.GetComponent<ObjectProperties>().getHandRotation ();

	}

	void PickObjectOneHand (int side, GameObject obj){

		GameObject parentObject = (side == 0) ? leftObjectPosition : rightObjectPosition;

		areObjects[side] = true;

		objects[side] = obj;

		Rigidbody rb = obj.GetComponent<Rigidbody> ();

		rb.isKinematic = true;

		rb.detectCollisions = false;

		obj.transform.SetParent (parentObject.transform);

		obj.transform.localPosition = new Vector3 (0, 0, 0);

		obj.transform.localRotation = obj.GetComponent<ObjectProperties>().getHandRotation ();

	}

	void AddToStack(int side, GameObject obj){

		stackSide = side;

		ObjectProperties objProperties = obj.GetComponent<ObjectProperties> ();

		GameObject parentObject = (side == 0) ? leftObjectPosition : rightObjectPosition;

		stackedObjects [stackQty] = obj;

		Rigidbody rb = obj.GetComponent<Rigidbody> ();

		rb.isKinematic = true;

		rb.detectCollisions = false;

		obj.transform.SetParent (parentObject.transform);

		obj.transform.localPosition = objProperties.stackedOverPosition*(stackQty+1);

		obj.transform.localRotation = obj.GetComponent<ObjectProperties>().getHandRotation ();

		stackQty++;

	}

	void UseObject (int side){

		bool isTwoHands = objects [side].GetComponent<ObjectProperties> ().isTwoHands;

		bool launch = Input.GetButton ("Launch Trigger");

		if (isTwoHands) {

			if (launch) {

				objects [side].transform.parent = null;

				areObjects[0] = areObjects[1] = false;

				Rigidbody rb = objects [side].GetComponent<Rigidbody> ();

				rb.isKinematic = false;

				rb.detectCollisions = true;

				rb.AddForce (cam.transform.forward * 2000);

			} else {

				//TODO Hit

			}

		} else {

			if (launch) {

				objects [side].transform.parent = null;

				areObjects[side] = false;

				Rigidbody rb = objects [side].GetComponent<Rigidbody> ();

				rb.isKinematic = false;

				rb.detectCollisions = true;

				RaycastHit hit = new RaycastHit ();

				if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

					rb.AddForce ((hit.point-objects [side].transform.position).normalized * 1500);

				} else {

					rb.AddForce (cam.transform.forward * 2000);
				
				}

				rb.AddForce (GetComponent<PlayerMovement>().movement*20,ForceMode.VelocityChange);

			} else {

				//TODO Hit

			}

		}

	}

	void UseStack(int side){

		bool launch = Input.GetButton ("Launch Trigger");

		if (launch) {

			stackQty--;

			GameObject obj = stackedObjects [stackQty];

			obj.transform.parent = null;

			Rigidbody rb = obj.GetComponent<Rigidbody> ();

			rb.isKinematic = false;

			rb.detectCollisions = true;

			RaycastHit hit = new RaycastHit ();

			if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

				rb.AddForce ((hit.point - obj.transform.position).normalized * 1500);

			} else {

				rb.AddForce (cam.transform.forward * 2000);

			}

			rb.AddForce (GetComponent<PlayerMovement> ().movement * 20, ForceMode.VelocityChange);

		}

	}

}
