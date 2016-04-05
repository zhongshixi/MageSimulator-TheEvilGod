using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MultiplayerRoleStarter : NetworkManager {

	[SerializeField] GameObject GodCharacter;
	[SerializeField] GameObject ManCharacter;

	// Use this for initialization
	void Start () {
		ClientScene.RegisterPrefab (GodCharacter);
		ClientScene.RegisterPrefab (ManCharacter);
		
	}


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		if (numPlayers == 0) {
			//assign God Character
			Vector3 godStartPos = GameObject.Find("GodStart").transform.position;
			GameObject player = (GameObject)Instantiate (GodCharacter);
			player.transform.position = godStartPos;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		} else {
			//assign Man Character
			Vector3 manStartPos = GameObject.Find("ManStart").transform.position;
			GameObject player = (GameObject)Instantiate (ManCharacter);
			player.transform.position = manStartPos;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
