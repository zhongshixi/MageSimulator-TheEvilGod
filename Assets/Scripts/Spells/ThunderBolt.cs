using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class ThunderBolt : SpellBase {

//	private ElectricityLine3D electricity;

	private Transform from;
	private Transform to;

	private Vector3 releaseDirection;
	private float releaseSpeed;

	private SphereCollider collider;

	void Awake(){
		//electricity = GetComponent<ElectricityLine3D> ();

	}

	void Start () {

		collider = gameObject.AddComponent<SphereCollider> ();
		collider.radius = 0;
		collider.center = Vector3.zero;

	}	

	void Update () {
		
		if (IsReleased) {

//			Vector3 distanceTraveled= releaseDirection * releaseSpeed * Time.deltaTime;
//
//			Vector3[] array = electricity.GetPoints ();
//			array [0] = array [0] + distanceTraveled;
//			array [1] = array [1] + distanceTraveled;
//
//			electricity.SetPoints (array);
//			collider.transform.LookAt (array [0]);
//			collider.radius = (array [1] - array [0]).magnitude/2.0f;
//			gameObject.transform.position = Math3dExt.MidPosition (array[0], array[1]);

		
		}

	}

	public void SetPosition(Vector3 pole1, Vector3 pole2){

		Vector3[] array = new Vector3[2];
		array [0] = pole1;
		array [1] = pole2;

		//electricity.SetPoints (array);

		gameObject.transform.position = Math3dExt.MidPosition (pole1, pole2);

	}

	public void Release(Vector3 dir, float speed){
		
		releaseDirection = dir;
		releaseSpeed = speed;

		gameObject.AddComponent<TimedDestroy>();
		IsReleased = true;



	}

	void OnCollisionEnter(Collision other)
	{


	}
}
