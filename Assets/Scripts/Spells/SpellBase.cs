using UnityEngine;
using System.Collections;

public class SpellBase : MonoBehaviour, IGrowable {


	protected bool IsReleased = false;

	public float maxChargeTimeInSeconds = 3;

	public float Speed = 5;

	public Vector3 scaleGrowRate = new Vector3 (5.0f, 5.0f, 5.0f);
	public Vector3 maxScale = new Vector3 (100.0f,100.0f,100.0f);

	protected void Start () {


	}
		
	// Update is called once per frame
	public void Update () {


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

