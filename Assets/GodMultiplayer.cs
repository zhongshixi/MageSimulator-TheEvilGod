using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GodMultiplayer : NetworkBehaviour {

	[SerializeField] Camera playerCamera;

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			return;

		GameObject.Find ("Main Camera").SetActive (false);
		playerCamera.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
