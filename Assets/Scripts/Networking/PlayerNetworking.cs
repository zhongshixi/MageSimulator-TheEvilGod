using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNetworking : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	[SerializeField] AudioListener FPSAudioListener;


	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GameObject.Find ("Main Camera").SetActive (false);
			GetComponent<CharacterController> ().enabled = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
			FPSCharacterCam.enabled = true;
			FPSAudioListener.enabled = true;
			GetComponent<AudioSource> ().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
