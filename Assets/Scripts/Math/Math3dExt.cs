using UnityEngine;
using System.Collections;
using System;



public class Math3dExt : MonoBehaviour {



	// calculate the mid point between two vectors
	public static Vector3 MidPosition(Vector3 vec1, Vector3 vec2){

		Vector3 sum = vec1 + vec2;

		Vector3 mid = new Vector3 (sum.x / 2, sum.y / 2, sum.z/2);

		return mid;

	}

	// calculate the distance between two vectors.
	public static float Distance(Vector3 vec1, Vector3 vec2){

		Vector3 diff = vec1 - vec2;

		return diff.magnitude;

	}

	// calculate the direction from source vector to destination vector
	public static Vector3 Direction(Vector3 src, Vector3 dest){

		Vector3 diff = dest - src;

		return diff.normalized;

	}

	// Determine if two vectors are equal on each axis. The tolerance vector serves
	// as a container of maximum acceptable absolute error of the difference of values on each axis.
	public static bool VectorEqual(Vector3 vec1, Vector3 vec2, Vector3 tolerance){

		Vector3 diff = vec1 - vec2;

		if (Math.Abs (diff.x) > Math.Abs (tolerance.x))
			return false;

		if (Math.Abs (diff.y) > Math.Abs (tolerance.y))
			return false;

		if (Math.Abs (diff.z) > Math.Abs (tolerance.z))
			return false;

		return true;
	}

	// Determine if two vectors are equal on each axis. The tolerance value serves
	// as maximum acceptable absolute error of the difference of values on each axis.
	public static bool VectorEqual(Vector3 vec1, Vector3 vec2, float toleranceValue){

		return VectorEqual (vec1, vec2, new Vector3 (toleranceValue, toleranceValue, toleranceValue));

	}

	// Equivalent of VectorEqual(Vector3 vec1, Vector3 vec2, 0,01f)
	public static bool VectorEqual(Vector3 vec1, Vector3 vec2){

		return VectorEqual (vec1, vec2, 0.01f);
	}


	// Create a plane normal represented by Vector3 (A,B,C) corresponding to  Ax + By + Cz + D = 0
	public static Vector3 CreatePlaneNormal(Vector3 point1, Vector3 point2, Vector3 point3){

		Vector3 p1p2 = point2 - point1;
		Vector3 p1p3 = point3 - point1;

		Vector3 normal = Vector3.Cross (p1p2, p1p3);

		return new Vector3 (normal.x, normal.y, normal.z);

	}
	// Create a plane represented by Vector4 (A,B,C,D) corresponding to  Ax + By + Cz + D = 0.
	public static Vector4 CreatePlane(Vector3 point1, Vector3 point2, Vector3 point3){

	
		Vector3 p1p2 = point2 - point1;
		Vector3 p1p3 = point3 - point1;

		Vector3 normal = Vector3.Cross (p1p2, p1p3);

		float d = -1 * Vector3.Dot (normal, point3);

		return new Vector4 (normal.x, normal.y, normal.z, d);
	}





}
