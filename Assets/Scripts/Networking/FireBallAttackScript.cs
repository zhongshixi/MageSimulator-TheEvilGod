using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FireBallAttackScript : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		GameObject hitObj = collision.collider.gameObject;
		RunnerHealth runnerHealth = hitObj.GetComponent<RunnerHealth> ();
		if (runnerHealth) {
<<<<<<< HEAD
			runnerHealth.ApplyDamage (100);
=======
			runnerHealth.CmdApplyDamage (100);
>>>>>>> master
		}
	}
}
