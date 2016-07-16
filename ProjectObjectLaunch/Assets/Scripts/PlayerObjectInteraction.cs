using UnityEngine;
using System.Collections;

public class PlayerObjectInteraction : MonoBehaviour {

	public GameObject rightObjectPosition;

	public GameObject leftObjectPosition;

	public GameObject cam;

	string usableTag = "Oggetto Usabile";

	GameObject[] objects = {null,null};

	bool[] areObjects = {false,false};

	int stackQty = 0;

	bool stacking = false;

	string stackType = "";

	void FixedUpdate () {

		bool leftDown = Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.JoystickButton4);

		bool rightDown = Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.JoystickButton5);

		if (leftDown || rightDown) {

			int clickSide = leftDown ? 0 : 1;

			int otherSide = leftDown ? 1 : 0;

			bool thisHandOccupied = areObjects [clickSide];

			if (thisHandOccupied) {

				ObjectProperties thisObjProperties = objects [clickSide].GetComponent<ObjectProperties> ();

				bool isThisObjStackable = thisObjProperties.stackable;

				if (isThisObjStackable) {

					RaycastHit hit = new RaycastHit ();

					bool objClicked = Physics.Raycast (cam.transform.position, cam.transform.forward, out hit);

					bool isObjUsable = hit.collider.gameObject.tag == usableTag;

					bool otherHandFree = areObjects [otherSide];

					if (objClicked && isObjUsable) {

						if (otherHandFree) {

							bool notReachedMaxStackValue = stackQty < thisObjProperties.maxStack;

							if (notReachedMaxStackValue) {

								ObjectProperties clickedObjProperties = hit.collider.gameObject.GetComponent<ObjectProperties> ();

								bool sameObjType = thisObjProperties.getName () == clickedObjProperties.getName ();

								if (sameObjType) {

									//TODO Add to stack

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

						bool moreThanOneInStack = stackQty > 1;

						if (moreThanOneInStack) {

							if (otherHandFree) {

								//TODO Hit or Launch one from stack

							} else {

								//TODO Error Message Other Hand Occupied

							}

						}

					}

				} else {

					//TODO Hit or Launch

				}

			} else {

				RaycastHit hit = new RaycastHit ();

				bool objClicked = Physics.Raycast (cam.transform.position, cam.transform.forward, out hit);

				bool isObjUsable = hit.collider.gameObject.tag == usableTag;

				if (objClicked && isObjUsable) {

					ObjectProperties clickedObjProperties = hit.collider.gameObject.GetComponent<ObjectProperties> ();

					if (clickedObjProperties.isTwoHands) {

						bool otherHandFree = areObjects [otherSide];

						if (otherHandFree) {

							//TODO Pick Object WIth Two Hands

						} else {

							//TODO Error Message Too Heavy To Pick With One Hand

						}

					} else {

						//TODO Pick Object

					}

				} else {

					//TODO Hit With Hands

				}

			}

		}





		/*

		bool leftDown = Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.JoystickButton4);

		bool rightDown = Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.JoystickButton5);

		if (leftDown || rightDown) {

			int n = leftDown ? 0 : 1;

			RaycastHit hit = new RaycastHit ();

			bool castHit = Physics.Raycast (cam.transform.position, cam.transform.forward, out hit);

			bool pickableObject = hit.collider.gameObject.tag == usableTag;

			if (castHit && pickableObject) {

				ObjectProperties pickObjPro = gameObject.GetComponent<ObjectProperties>();

				Rigidbody rb = pickObjPro.getRigidbody ();

				bool pickObjStakle = pickObjPro.stackable;

				if (stacking) {

					if (pickObjPro.getName () == stackType && stackQty < pickObjPro.maxStack) {

						stackQty++;

						//instantiate type on left hand to show qty

					}

				} else {



				}

			}

		}*/

























		/*RaycastHit hit = new RaycastHit ();

		for (int i = 0; i < 2; i++) {

			bool leftMouse = i == 0;

			bool mouseClicked = Input.GetMouseButtonDown (i);

			bool joystickClicked = Input.GetKeyDown (leftMouse ? KeyCode.JoystickButton4 : KeyCode.JoystickButton5);

			if ((mouseClicked || joystickClicked)) {

				if (!areObjects [i]) {

					if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

						float distance = hit.distance;

						if (distance < 2.5f) {

							string objectTag = hit.collider.gameObject.tag;

							if (objectTag == usableTag) {

								areObjects [i] = true;

								objects [i] = hit.collider.gameObject;

								Rigidbody rb = objects [i].GetComponent<Rigidbody> ();

								rb.isKinematic = true;

								rb.detectCollisions = false;

								objects [i].transform.SetParent ((leftMouse ? leftObjectPosition : rightObjectPosition).transform);

								objects [i].transform.localPosition = new Vector3 (0, 0, 0);

								objects [i].transform.localRotation = objects [i].GetComponent<ObjectProperties> ().getHandRotation (); //Quaternion.identity;
						
							}

						}
						
					}

				} else {

					objects[i].transform.parent = null;

					areObjects[i] = false;

					Rigidbody rb = objects[i].GetComponent<Rigidbody> ();

					rb.isKinematic = false;

					rb.detectCollisions = true;

					if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

						rb.AddForce ((hit.point-objects[i].transform.position).normalized * 1500);

					} else {

						rb.AddForce (cam.transform.forward * 2000);
					}

					rb.AddForce (GetComponent<PlayerMovement>().movement*20,ForceMode.VelocityChange);

				}

			}

		}

		/*RaycastHit hit = new RaycastHit ();

		if (Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.JoystickButton5)) {

			if (!isRightObject) {

				if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward),out hit)) {

					if (hit.distance < 2.5f) {

						Debug.Log ("Ray Touched");

						Collider[] colliders = Physics.OverlapSphere (hit.point, 0.2f);

						Debug.Log (colliders.Length);

						GameObject oggetto = null;

						foreach (Collider c in colliders) {


							if (c.gameObject.tag == "Oggetto Usabile") {

								RaycastHit hit2 = new RaycastHit ();

								Physics.Raycast( new Ray(hit.point+hit.normal*0.1f,c.transform.position-(hit.point+hit.normal*0.1f)), out hit2);

								if (hit2.collider.gameObject == c.gameObject) {

									oggetto = c.gameObject;

									break;

								}

							}

						}

						if (oggetto) {

							isRightObject = true;

							rightObject = oggetto;

							Rigidbody rb = rightObject.GetComponent<Rigidbody> ();

							rb.isKinematic = true;

							rb.detectCollisions = false;

							rightObject.transform.SetParent (rightObjectPosition.transform);

							rightObject.transform.localPosition = new Vector3 (0, 0, 0);

							rightObject.transform.localRotation = Quaternion.identity;

						}

					}

				}

			} else {
					
				if (isRightObject) {

					rightObject.transform.parent = null;

					isRightObject = false;

					Rigidbody rb = rightObject.GetComponent<Rigidbody> ();

					rb.isKinematic = false;

					rb.detectCollisions = true;

					if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

						rb.AddForce ((hit.point-rightObject.transform.position).normalized * 1500);

					} else {

						rb.AddForce (cam.transform.forward * 2000);
					}

				}

			}

		}

		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.JoystickButton4)) {

			if (!isLeftObject) {

				if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward),out hit)) {

					if (hit.distance < 2.5f) {

						Debug.Log ("Ray Touched");

						Collider[] colliders = Physics.OverlapSphere (hit.point, 0.2f);

						Debug.Log (colliders.Length);

						GameObject oggetto = null;

						foreach (Collider c in colliders) {


							if (c.gameObject.tag == "Oggetto Usabile") {

								RaycastHit hit2 = new RaycastHit ();

								Physics.Raycast( new Ray(hit.point+hit.normal*0.1f,c.transform.position-(hit.point+hit.normal*0.1f)), out hit2);

								if (hit2.collider.gameObject == c.gameObject) {

									oggetto = c.gameObject;

									break;

								}

							}

						}

						if (oggetto) {

							isLeftObject = true;

							leftObject = oggetto;

							Rigidbody rb = leftObject.GetComponent<Rigidbody> ();

							rb.isKinematic = true;

							rb.detectCollisions = false;

							leftObject.transform.SetParent (leftObjectPosition.transform);

							leftObject.transform.localPosition = new Vector3 (0, 0, 0);

							leftObject.transform.localRotation = Quaternion.identity;

						}

					}

				}

			} else {

				if (isLeftObject) {

					leftObject.transform.parent = null;

					isLeftObject = false;

					Rigidbody rb = leftObject.GetComponent<Rigidbody> ();

					rb.isKinematic = false;

					rb.detectCollisions = true;

					if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out hit)) {

						rb.AddForce ((hit.point-leftObject.transform.position).normalized * 1500);

					} else {

						rb.AddForce (cam.transform.forward * 2000);
					}

				}

			}

		}*/
	
	}

}
