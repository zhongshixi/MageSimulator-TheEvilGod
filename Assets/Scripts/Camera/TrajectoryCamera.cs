using UnityEngine;
using System.Collections;

public class TrajectoryCamera : MonoBehaviour {

	[SerializeField] Camera trajectoryCamera;
	float cameraDistanceTravelled;
	float distanceFromPlayer = 100.0F;
	float mapSize = 180.0F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		//Added By Jason, we interpolate between fields of view for zoom. This is not used, can be cleaned up
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
////			Vector3 ray = hit.transform.position - transform.position;
//			trajectoryCamera.fieldOfView = Mathf.Lerp(trajectoryCamera.fieldOfView,mapSize/hit.distance,Time.deltaTime);
//			//trajectoryCamera.transform.position = hit.transform.position + 5.0F*ray.normalized;

		} else {
			
		}
	
	}
}
