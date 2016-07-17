using UnityEngine;
using System.Collections;

public class GrassCreator : MonoBehaviour {

	public Vector2 chunkSize = new Vector2 (1,1);

	public GameObject grassPiece;

	public GameObject player;

	public float minGrassSize = 0.8f;

	public float maxGrassSize = 1.2f;

	public float grassPerMeter = 100;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < (grassPerMeter * chunkSize.x * chunkSize.y); i++) {

			GameObject grass = (GameObject) Instantiate (grassPiece, new Vector3(Random.Range (-chunkSize.x/2,chunkSize.x/2)+transform.position.x,transform.position.y,Random.Range (-chunkSize.y/2,chunkSize.y/2)+transform.position.z), Quaternion.Euler (-90,0,0));

			float scaleFactor = Random.Range (minGrassSize, maxGrassSize);

			grass.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);

			grass.GetComponent<DrawDistance> ().player = player;

		}
	
	}
}
