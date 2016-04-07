using UnityEngine;
using System.Collections;

[RequireComponent (typeof(IGrowable))]
public class Trajectory : MonoBehaviour {


	private LineRenderer lineRenderer;

	public GameObject landMark;

	public int numberOfLineSegment;
	void Awake(){

	}

	// Use this for initialization
	void Start () {

		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetWidth (0.01f, 0.1f);
		lineRenderer.SetColors (Color.red, Color.red);
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetVertexCount (numberOfLineSegment);


	}

	void DrawLine(float distance){

		//Debug.Log (distance);
		lineRenderer.SetVertexCount (numberOfLineSegment);
		int i = 0;
		float lengthOfSegment = distance / numberOfLineSegment;

		while (i < numberOfLineSegment) {
			Vector3 pos = transform.position + i * transform.forward*lengthOfSegment;
			lineRenderer.SetPosition(i, pos);
			i++;
		}

	}

	void FixedUpdate(){

		//lineRenderer.SetVertexCount (0);
		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {

			//Debug.Log ("hit");
			DrawLine (hit.distance);

		} else {

			DrawLine (200.0f);
		}


	}

	// Update is called once per frame
	void Update () {



	}
}