using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpellBase : NetworkBehaviour, IGrowable {

	[SyncVar]
	protected Vector3 syncScale;

	protected bool IsReleased = false;

	public float maxChargeTimeInSeconds = 3;

	public float Speed = 5;

	public Vector3 scaleGrowRate = new Vector3 (5.0f, 5.0f, 5.0f);
	public Vector3 maxScale = new Vector3 (100.0f,100.0f,100.0f);

	protected void Start () {


	}
		
	// Update is called once per frame
	[ClientCallback]
	public void Update () {
		//Debug.Log ("Updating Scale");
		transform.localScale = syncScale;
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

