using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BallPlayer : NetworkBehaviour {

	public enum RunDirection
	{
		forward,
		backward,
		left,
		right,
		jump
	}

	[SerializeField] Camera playerCamera;

	public AudioClip[] footstepClips;
	public AudioClip jumpClip;
	public AudioClip landClip;

	public float movementSpeed;
	public float jumpStrength;
	private float footstepTimer = 0.0f;
	public float footstepCooldown;
	public float bobHeight;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			return;

		GameObject.Find ("Main Camera").SetActive (false);
		playerCamera.enabled = true;
		GetComponent<AudioListener> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		if (IsGrounded ()) {
			if (IsMoving ()) {
				PlayFootStepSound ();

				//bob camera
				playerCamera.transform.position = 
					new Vector3 (transform.position.x, 
						transform.position.y + bobHeight * Mathf.Sin (((footstepCooldown - footstepTimer) / footstepCooldown) * Mathf.PI), 
						transform.position.z);
			}
		}

		playerCamera.transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, 0, 0);

		if (Input.GetKey (KeyCode.W)) {
			CmdMove (RunDirection.forward);
		}

		if (Input.GetKey (KeyCode.A)) {
			CmdMove (RunDirection.left);
		}

		if (Input.GetKey (KeyCode.D)) {
			CmdMove (RunDirection.right);
		}

		if (Input.GetKey (KeyCode.S)) {
			CmdMove (RunDirection.backward);
		}

		if (Input.GetKey (KeyCode.Space)) {
			CmdMove (RunDirection.jump);
		}
	}



	bool IsGrounded(){
		return Physics.Raycast (transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f);
	}

	bool IsMoving(){
		return Vector3.Magnitude (GetComponent<Rigidbody> ().velocity) > 0.1f;
	}

	void PlayFootStepSound(){
		if (footstepTimer <= 0) {
			int x = Random.Range (0, footstepClips.Length);
			GetComponent<AudioSource> ().PlayOneShot (footstepClips [x]);
			footstepTimer = footstepCooldown;
		} else {
			footstepTimer -= Time.deltaTime;
		}
	}

	[Command]
	void CmdMove(RunDirection direction){
		switch (direction) {
		case RunDirection.forward:
			GetComponent<Rigidbody> ().AddForce (transform.forward * movementSpeed);
			break;

		case RunDirection.backward:
			GetComponent<Rigidbody> ().AddForce (-1*transform.forward * movementSpeed);
			break;

		case RunDirection.left:
			GetComponent<Rigidbody> ().AddForce (-1*transform.right * movementSpeed);
			break;

		case RunDirection.right:
			GetComponent<Rigidbody> ().AddForce (transform.right * movementSpeed);
			break;

		case RunDirection.jump:
			if (IsGrounded()) {
				GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpStrength);
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource> ().PlayOneShot (jumpClip);
			}
			break;
		}
	}
}