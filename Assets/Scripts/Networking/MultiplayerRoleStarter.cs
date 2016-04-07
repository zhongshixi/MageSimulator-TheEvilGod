using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityCustomAssets;

public class MultiplayerRoleStarter : NetworkManager {

	[SerializeField] GameObject GodCharacter;
	[SerializeField] GameObject ManCharacter;
	[SerializeField] GameObject Indicator;

	private UnityEngine.UI.Text ipMessage;
	private UnityEngine.UI.Text helpMessage;

	// Use this for initialization
	void Start() {
		ClientScene.RegisterPrefab (GodCharacter);
		ClientScene.RegisterPrefab (ManCharacter);
		ClientScene.RegisterPrefab (Indicator);
	}




	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		GameTimer gameTimer = GameObject.Find ("GameTimer").GetComponent<GameTimer> ();


		ipMessage = GameObject.Find ("ipText").GetComponent<UnityEngine.UI.Text> ();
		helpMessage = GameObject.Find ("helpText").GetComponent<UnityEngine.UI.Text> ();
		if (numPlayers == 0) {
			//assign God Character
			GameObject godStart = GameObject.Find("GodStart");
			Vector3 godStartPos = godStart.transform.position;
			Vector3 godStartRot = godStart.transform.eulerAngles;
			GameObject player = (GameObject)Instantiate (GodCharacter);
			player.transform.position = godStartPos;
			player.transform.eulerAngles = godStartRot;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
			ipMessage.text = "Your IP Address: " + Network.player.ipAddress;

		} else {
			//assign Man Character
			Vector3 manStartPos = GameObject.Find("ManStart").transform.position;
			GameObject player = (GameObject)Instantiate (ManCharacter);
			GameObject indicator = (GameObject)Instantiate (Indicator);
			player.transform.position = manStartPos;
			indicator.transform.position = manStartPos;
			indicator.GetComponent<CustomSmoothFollow> ().target = player.transform;
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

			gameTimer.PlayerAdded ();
			if (gameTimer.IsGameStarted ()) {
				player.GetComponent<RunnerHealth> ().ApplyDamage (100);
			}
		}
	}

	public void SetIPText(string text){
		ipMessage = GameObject.Find ("ipText").GetComponent<UnityEngine.UI.Text> ();
		ipMessage.text = text;
	}

	public void SetHelpText(string text){
		helpMessage = GameObject.Find ("helpText").GetComponent<UnityEngine.UI.Text> ();
		helpMessage.text = text;
	}



	

}
