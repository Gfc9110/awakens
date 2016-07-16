using UnityEngine;
using System.Collections;

public class DrawCrosshair : MonoBehaviour {

	public Texture crosshair;

	public Texture barBackground;

	public Texture lifeBar;

	public Texture staminaBar;

	int zombieKilled = 0;



	void Start(){

		Cursor.lockState = CursorLockMode.Locked;

		Cursor.visible = false;

	}

	void OnGUI(){

		int screenWidth = Screen.width;

		int screenHeight = Screen.height;

		int lifePoints = GetComponent<PlayerDamage> ().lifePoints;

		int stamina = GetComponent<PlayerMovement> ().stamina;

		GUI.DrawTexture (new Rect((screenWidth/2)-32,(screenHeight/2)-32,64,64),crosshair);

		GUI.Label (new Rect (20, 20, 100, 20), "Vita : " + lifePoints.ToString() );



		GUI.DrawTexture (new Rect(screenWidth-250, screenHeight-70,200,20),barBackground);

		GUI.DrawTexture (new Rect((screenWidth-250)+(200-(lifePoints*2)), screenHeight-70,lifePoints*2,20),lifeBar);



		GUI.DrawTexture (new Rect(screenWidth-250, screenHeight-140,200,20),barBackground);

		GUI.DrawTexture (new Rect((screenWidth-250)+(200-(stamina*2)), screenHeight-140,stamina*2,20),staminaBar);

		GUI.Label (new Rect (20, 40, 100, 20), "Stamina : " + GetComponent<PlayerMovement> ().stamina.ToString() );

		GUI.Label (new Rect (20, 60, 100, 20), "Uccisioni : " + zombieKilled.ToString() );

	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape)) {

			Cursor.lockState = CursorLockMode.None;

			Cursor.visible = true;

		}

	}

	public void ZombieKilled(){

		zombieKilled++;

	}

}
