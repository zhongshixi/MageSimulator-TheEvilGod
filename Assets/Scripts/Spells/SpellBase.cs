using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpellBase : NetworkBehaviour, IGrowable {


	public AudioSource staticSoundPrefab;

	public AudioSource releasingSoundPrefab;

	public float maxChargeTimeInSeconds = 3;

	public GameObject spawnOnHit;

	public float Speed;

	public Vector3 scaleGrowRate = new Vector3 (5.0f, 5.0f, 5.0f);
	public Vector3 maxScale = new Vector3 (100.0f,100.0f,100.0f);


	[SyncVar]
	protected Vector3 syncScale;

	[SyncVar]
	protected Vector3 syncVelocity;

	[SyncVar]
	protected Vector3 syncDirection;

	protected bool IsReleased = false;

	protected AudioSource staticSound;
	protected AudioSource releasingSound;


	protected void Start () {

		if (staticSoundPrefab) {

			staticSound = GameObject.Instantiate (staticSoundPrefab, transform.position, transform.rotation) as AudioSource;
			staticSound.transform.parent = gameObject.transform;
			staticSound.Play ();
		}


	}

	protected void Update () {

		transform.localScale = syncScale;
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().velocity = syncVelocity;

	}

	[ClientRpc]
	protected void RpcRelease(){

		if (releasingSoundPrefab) {
			releasingSound = GameObject.Instantiate (releasingSoundPrefab, transform.position, transform.rotation) as AudioSource;
			//releasingSound.transform.parent = gameObject.transform;
			releasingSound.Play ();
		}

	}


	public void Release(Vector3 dir){

		IsReleased = true;
		RpcRelease ();

	}

	protected void SpawnOnHit(GameObject obj, Vector3 position){

		GameObject g = GameObject.Instantiate (obj, position, Quaternion.identity) as GameObject;
		g.transform.localScale = transform.localScale / 10.0f;

	}


	protected void OnCollisionEnter(Collision collision){

		if (spawnOnHit)
			SpawnOnHit (spawnOnHit, collision.contacts [0].point);
		

	}


	virtual public Vector3 InitialScale(){
		return this.transform.localScale;

	}

	virtual public Vector3 FinalScale(){
		return this.transform.localScale;
	}
}
