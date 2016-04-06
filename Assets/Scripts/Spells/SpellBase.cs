using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpellBase : NetworkBehaviour, IGrowable {

	[SyncVar]
	protected Vector3 syncScale;

	[SyncVar]
	protected Vector3 syncVelocity;

	[SyncVar]
	protected Vector3 syncDirection;

	protected bool IsReleased = false;

	public float maxChargeTimeInSeconds = 3;

	public float Speed;

	public Vector3 scaleGrowRate = new Vector3 (5.0f, 5.0f, 5.0f);
	public Vector3 maxScale = new Vector3 (100.0f,100.0f,100.0f);


	protected void Start () {


	}
		

	public void Update () {
		//Debug.Log ("Updating Scale");
		NetworkIdentity identity  = GetComponent<NetworkIdentity>();
		if (identity) {

			if (identity.isClient) {
				Debug.Log ("Client");
				transform.localScale = syncScale;
				if (GetComponent<Rigidbody> ())
					GetComponent<Rigidbody> ().velocity = syncVelocity;
				transform.forward = syncDirection;

			} else {

				syncDirection = transform.forward;
			}
		}
	}

		
	public void Release(Vector3 dir){
		IsReleased = true;

	}

	virtual public Vector3 InitialScale(){
		return this.transform.localScale;

	}
		
	virtual public Vector3 FinalScale(){
		return this.transform.localScale;
	}
}

