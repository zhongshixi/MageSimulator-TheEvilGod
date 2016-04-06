using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AnimationScript : NetworkBehaviour{
	public float runningThreshold;
	public Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		float speed = Vector3.Magnitude (vel);
		anim.SetFloat ("Speed", speed);
	
	}
}
