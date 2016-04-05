using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent (typeof (ParticleSystem))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (SphereCollider))]


// Requirement Unity 5.3
public class FireBall : SpellBase {





	private ParticleSystem particles;
//	private float maxParticleRadius;
//	private float maxParticleSize;

	private Rigidbody rigidBody;
	private SphereCollider collider;
//	private float maxSize;



//	private float sizeGrowRate;
//	private float particleSizeGrowRate;
//	private float particleRadiusGrowRate;


//	public float sizeGrowRateAfterRelease = 0.1f;
//	public float particleSizeGrowRateAfterRelease = 0.1f;
//	public float particleRadiusGrowRateAfterRelease = 0.1f;

	private Vector3 scaleGrowRateBeforeRelease;
	private Vector3 maxScaleBeforeRelease;

	public Vector3 Noise = Vector3.zero;
	public float Damping = 0.3f;
	Quaternion direction;


	private Vector3 initialScale;

	void Awake () {

		particles = gameObject.GetComponent<ParticleSystem>();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		collider = gameObject.GetComponent<SphereCollider> ();

		scaleGrowRateBeforeRelease = transform.localScale / maxChargeTimeInSeconds;
		maxScaleBeforeRelease =  new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		transform.localScale = new Vector3 (0, 0, 0);

		direction = Quaternion.LookRotation (this.transform.forward * 1000);

		transform.Rotate (new Vector3 (Random.Range (-Noise.x, Noise.x), Random.Range (-Noise.y, Noise.y), Random.Range (-Noise.z, Noise.z)));


	}
	// delta time in seconds.
	public void Grow(float deltaTime){

		//If components are found
		if (particles && rigidBody && collider) {

			float x = transform.localScale.x;
			float y = transform.localScale.y;
			float z = transform.localScale.z;
			//grow size of the particles
			if (x < maxScaleBeforeRelease.x)
				x += scaleGrowRateBeforeRelease.x * deltaTime;

			if (y < maxScaleBeforeRelease.y)
				y += scaleGrowRateBeforeRelease.y * deltaTime;

			if (z < maxScaleBeforeRelease.z)
				z += scaleGrowRateBeforeRelease.z * deltaTime;

			transform.localScale = new Vector3 (x, y, z);
			syncScale = transform.localScale;

			Debug.Log (transform.localScale);

		}
			
	}
		
	public void Release(Vector3 dir){

		base.Release (dir);
		gameObject.transform.forward = dir;
		initialScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);

		maxScaleBeforeRelease = Vector3.Scale (transform.localScale, maxScale);
		scaleGrowRateBeforeRelease = Vector3.Scale(transform.localScale, scaleGrowRate);

//		maxParticleSize = particles.startSize * maxGrowRatioAfterRelease;
//		maxParticleRadius = particles.startSpeed * maxGrowRatioAfterRelease;
//		maxSize = collider.radius * maxGrowRatioAfterRelease;
//
//		particleSizeGrowRate = particleSizeGrowRateAfterRelease;
//		particleRadiusGrowRate = particleRadiusGrowRateAfterRelease;
//		sizeGrowRate = sizeGrowRateAfterRelease;



	}

	 protected void Update () {
		base.Update ();

		if (IsReleased)
			Grow (Time.deltaTime);
	}

	void LateUpdate ()
	{	
		if (IsReleased) {
			//this.transform.rotation = Quaternion.Lerp (this.transform.rotation, direction, Damping);
			this.transform.position += this.transform.forward * Speed * Time.deltaTime;
		}
	}
		

	void OnCollisionEnter(Collision collision){

		//Debug.Log ("Destroyed");
		Destroy (gameObject);
	
	}
		

	public Vector3 InitialScale(){

		return initialScale;

	}
		
	public Vector3 FinalScale(){

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