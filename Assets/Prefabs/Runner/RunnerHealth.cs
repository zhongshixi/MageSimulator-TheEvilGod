using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RunnerHealth : NetworkBehaviour {

	[SyncVar]
	private int health = 100;

	private bool isDead;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !isDead)
			Death ();
		
	}

	[Command]
	public void CmdApplyDamage(int damage){
		health -= damage;
		if (health <= 0)
			Death ();
	}

	public void Death(){
		isDead = true;
		GetComponent<CapsuleCollider> ().enabled = false;
		GetComponent<Rigidbody> ().useGravity = false;
		GameObject messageObj = GameObject.Find ("RunnerText");
		SkinnedMeshRenderer[] skinnedModel = GetComponentsInChildren<SkinnedMeshRenderer> ();
		foreach (SkinnedMeshRenderer r in skinnedModel) {
			r.enabled = false;
		}

		MeshRenderer[] meshedModel = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer r in meshedModel) {
			r.enabled = false;
		}

		GameTimer gameTimer = GameObject.Find ("GameTimer").GetComponent<GameTimer> ();
		gameTimer.UpdateMenCount ();

		if (isLocalPlayer) {
			UnityEngine.UI.Text message = messageObj.GetComponent<UnityEngine.UI.Text> ();
			message.text = "You are dead.";
		}
	}

	public bool IsDead(){
		return isDead;
	}


}
