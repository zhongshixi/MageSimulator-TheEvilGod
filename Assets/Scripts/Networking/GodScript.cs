using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GodScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			return;
		
		UnityEngine.UI.Text helpMessage = GameObject.Find ("helpText").GetComponent<UnityEngine.UI.Text> ();
		helpMessage.text = "Objective: Destroy all men on the ground below before time runs out";
<<<<<<< HEAD
=======
		GetComponent<AudioListener> ().enabled = false;


>>>>>>> master
	}
	
	// Update is called once per frame
	void Update () {
		


	
	}
}
