using UnityEngine;
using System.Collections;

public class DrawDistance : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float distance = Mathf.Abs ((new Vector2(player.transform.position.x,player.transform.position.z)-new Vector2(transform.position.x,transform.position.z)).sqrMagnitude);

		GetComponent<MeshRenderer> ().enabled = distance < 400;
	
	}
}
