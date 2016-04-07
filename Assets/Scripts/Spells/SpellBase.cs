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


	protected void FixedUpdate () {
		Debug.Log ("isReleased: " + IsReleased); 

			
		//Debug.Log ("syncScale" + syncScale.x + ", " + syncScale.y + ", " + syncScale.z);
		//transform.localScale = syncScale;
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().velocity = syncVelocity;

	}

	[ClientCallback]
	void Update(){
		if (!NetworkServer.active)
			transform.localScale = new Vector3 (50.0f,50.0f,50.0f);
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
		g.transform.localScale = transform.localScale/2;

	}


	protected void OnCollisionEnter(Collision collision){

		if (spawnOnHit)
			SpawnOnHit (spawnOnHit, collision.contacts [0].point);
		

	}


	virtual public Vector3 InitialScale(){
		return this.transform.localScale;

	}

	virtual public Vector3 FinalScale(){

		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {

			float distance = hit.distance;

			float travelTime = distance / Speed;

			Vector3 growth = scaleGrowRate * travelTime;

			Vector3 finalScale = transform.localScale + growth;

			float x, y, z = 0.0f;

			if (finalScale.x >= maxScale.x)
				x = maxScale.x;
			else
				x = finalScale.x;

			if (finalScale.y >= maxScale.y)
				y = maxScale.y;
			else
				y = finalScale.y;

			if (finalScale.z >= maxScale.z)
				z = maxScale.z;
			else
				z = finalScale.z;

			return new Vector3 (x, y, z);

		}

		return transform.localScale;

	}
}
