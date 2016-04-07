using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class StartupGUI : MonoBehaviour {
	private string address = "localhost";
<<<<<<< HEAD
	private bool menuGUI = true;
	private bool gameGUI = false;
=======
>>>>>>> master
	private NetworkManager manager;
	// Use this for initialization

	void Awake(){
		manager = GetComponent<NetworkManager> ();
	}

	void OnGUI () {
<<<<<<< HEAD
		if (menuGUI)
			MenuGUI ();
		else if (gameGUI)
			GameGUI ();
=======
		if (NetworkServer.active || NetworkClient.active)
			GameGUI ();
		else 
			MenuGUI ();
>>>>>>> master
	}

	void MenuGUI(){
		GUI.color = Color.white;
		GUIStyle titleStyle = new GUIStyle ();
		titleStyle.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 0.9f);
		titleStyle.fontSize = 30;
		titleStyle.fontStyle = FontStyle.Bold;
		titleStyle.alignment = TextAnchor.MiddleCenter;

		GUIStyle labelStyle = new GUIStyle ();
		labelStyle.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 0.9f);
		labelStyle.fontSize = 15;
		labelStyle.fontStyle = FontStyle.Normal;
		labelStyle.alignment = TextAnchor.MiddleCenter;

		address = GUI.TextField (new Rect (Screen.width * 0.50f - 100 / 2 + 55, Screen.height * 0.60f - 25 / 2, 100, 25), address);

		GUI.Label (new Rect (Screen.width * 0.50f - 100 / 2, Screen.height * 0.40f - 50 / 2, 100, 50), "Dualism", titleStyle);

		if (GUI.Button (new Rect (Screen.width * 0.50f - 100 / 2, Screen.height * 0.50f - 25 / 2, 100, 25), "Host Game"))
			OnStartupHost ();

		if (GUI.Button (new Rect (Screen.width * 0.50f - 100 / 2 - 55, Screen.height * 0.60f - 25 / 2, 100, 25), "Join Game"))
			OnJoinWorld ();

	
	}

	void GameGUI(){
<<<<<<< HEAD
		if (NetworkServer.active || NetworkClient.active) {
			if (GUI.Button (new Rect (Screen.width * 0.10f - 100 / 2, Screen.height * 0.10f - 25 / 2, 100, 25), "Exit Game"))
				ExitGame ();
		}
=======
		if (GUI.Button (new Rect (Screen.width * 0.10f - 100 / 2, Screen.height * 0.10f - 25 / 2, 100, 25), "Exit Game"))
			ExitGame ();
	
>>>>>>> master
	}


	public void OnStartupHost(){
		manager.networkPort = 7777;
		manager.StartHost ();
<<<<<<< HEAD
		menuGUI = false;
		gameGUI = true;
=======
>>>>>>> master
	}

	public void OnJoinWorld(){
		manager.networkAddress = address;
		manager.networkPort = 7777;
		manager.StartClient ();
<<<<<<< HEAD
		menuGUI = false;
		gameGUI = true;
=======
>>>>>>> master
	}

	public void ExitGame(){
		manager.StopHost ();
<<<<<<< HEAD
		menuGUI = true;
		gameGUI = false;
=======
>>>>>>> master
	}



}
