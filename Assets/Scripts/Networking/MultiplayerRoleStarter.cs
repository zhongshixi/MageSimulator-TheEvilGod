using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityCustomAssets;

public class MultiplayerRoleStarter : NetworkManager {

	[SerializeField] GameObject GodCharacter;
	[SerializeField] GameObject ManCharacter;
	[SerializeField] GameObject Indicator;

	// Use this for initialization
	void Start () {
		ClientScene.RegisterPrefab (GodCharacter);
		ClientScene.RegisterPrefab (ManCharacter);
		ClientScene.RegisterPrefab (Indicator);
	}


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		if (numPlayers == 0) {
			//assign God Character
			GameObject godStart = GameObject.Find("GodStart");
			Vector3 godStartPos = godStart.transform.position;
			Vector3 godStartRot = godStart.transform.eulerAngles;
			GameObject player = (GameObject)Instantiate (GodCharacter);
			player.transform.position = godStartPos;
			player.transform.eulerAngles = godStartRot;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		} else {
			//assign Man Character
			Vector3 manStartPos = GameObject.Find("ManStart").transform.position;
			GameObject player = (GameObject)Instantiate (ManCharacter);
			GameObject indicator = (GameObject)Instantiate (Indicator);
			player.transform.position = manStartPos;
			indicator.transform.position = manStartPos;
			indicator.GetComponent<CustomSmoothFollow> ().target = player.transform;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
