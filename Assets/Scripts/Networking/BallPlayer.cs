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
	private float initialCameraHeight;
	private float spaceBarTimer = 0.0f;
	private float spaceBarCooldown = 0.25f;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer)
			return;

		GameObject.Find ("Main Camera").SetActive (false);
		SkinnedMeshRenderer[] skinnedModel = GetComponentsInChildren<SkinnedMeshRenderer> ();
		foreach (SkinnedMeshRenderer r in skinnedModel) {
			r.enabled = false;
		}

		MeshRenderer[] meshedModel = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer r in meshedModel) {
			r.enabled = false;
		}
		playerCamera.enabled = true;
		GetComponent<AudioListener> ().enabled = true;
		initialCameraHeight = playerCamera.transform.position.y - transform.position.y;

		UnityEngine.UI.Text helpMessage = GameObject.Find ("helpText").GetComponent<UnityEngine.UI.Text> ();
		helpMessage.text = "Objective: Avoid fireballs to stay alive until time runs out";
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!isLocalPlayer)
			return;

		if (IsGrounded ()) {
			if (IsMoving ()) {
				PlayFootStepSound ();

				//bob camera
				playerCamera.transform.position = 
					new Vector3 (transform.position.x, 
						transform.position.y + initialCameraHeight + bobHeight * Mathf.Sin (((footstepCooldown - footstepTimer) / footstepCooldown) * Mathf.PI), 
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

		if (Input.GetKey (KeyCode.Space) && spaceBarTimer<=0) {
			CmdMove (RunDirection.jump);
			spaceBarTimer = spaceBarCooldown;
		}

		if (spaceBarTimer > 0)
			spaceBarTimer -= Time.deltaTime;
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