using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : NetworkBehaviour {

	[SerializeField] Camera playerCamera;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion currentRotation;
	Quaternion originalRotation;
	void Update ()
	{
		if (!isLocalPlayer)
			return;
		// Read the mouse input axis
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationX = ClampAngle (rotationX, minimumX, maximumX);
		rotationY = ClampAngle (rotationY, minimumY, maximumY);
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
		currentRotation = originalRotation * xQuaternion;
		CmdRotateObject (currentRotation);
		playerCamera.transform.rotation = currentRotation * yQuaternion;
	}

	[Command]
	public void CmdRotateObject(Quaternion rotation){
		GetComponent<Rigidbody>().rotation = rotation;
	}
	[Command]
	public void CmdVerticalCameraMovement(Quaternion altitude){
		playerCamera.transform.rotation = altitude;
	}

	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;

	}



	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle <= -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
}