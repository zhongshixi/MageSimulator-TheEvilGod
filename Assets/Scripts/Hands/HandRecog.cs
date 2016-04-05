using UnityEngine;
using System.Collections;
using System;
using Leap;

//Author Zhongshi Xi
//Github: https://github.com/xzs424
//Email: xizhongshiwise@gmail.com.
// HandRecog provides methods of detecting gestures of a hand.
public class HandRecog : MonoBehaviour {



	public static bool IsPalmFacingUpwards(HandModel hand, float theta, Vector3 up ){

		Vector3 dir = hand.GetPalmNormal ();

		float angle = Vector3.Angle (dir, up);

		if (Math.Abs (angle) <= theta)
			return true;

		return false;

	}

	public static bool IsPalmFacingDownwards(HandModel hand, float theta, Vector3 down){
		Vector3 dir = hand.GetPalmNormal ();

		float angle = Vector3.Angle (dir, down);

		if (Math.Abs (angle) <= theta)
			return true;

		return false;

	}

	public Vector3 MiddleBetweenPalms(HandModel handOne, HandModel handTwo){

		return Math3dExt.MidPosition (handOne.GetPalmPosition (), handTwo.GetPalmPosition ());

	}

	public static float DistanceBetweenPalms(HandModel handOne, HandModel handTwo){

		Vector3 handOnePalmPos = handOne.GetPalmPosition();
		Vector3 handTwoPalmPos = handTwo.GetPalmPosition ();

		return Math3dExt.Distance (handOnePalmPos, handTwoPalmPos);

	}

	// Calculate direction from one palm to another.
	public static Vector3 DirectionBetweenPalm(HandModel fromHand, HandModel toHand){

		Vector3 fromHandPalmPos = fromHand.GetPalmPosition ();
		Vector3 toHandPalmPos = toHand.GetPalmPosition ();

		return Math3dExt.Direction (fromHandPalmPos, toHandPalmPos);

	}


	// Check if the hand is clenching by certain amount of angle. Strict clenching means every finger must
	// bend by that certain amount fo angle.
	public static bool IsHandClenchingStrict(HandModel hand, float minAngle){

		bool a = HandRecog.IsIndexFingerTipBent (hand, minAngle);
		bool b = HandRecog.IsMiddleFingerTipBent (hand, minAngle);
		bool c = HandRecog.IsRingFingerTipBent (hand, minAngle);
		bool d = HandRecog.IsLittleFingerTipBent (hand, minAngle);

		bool t = HandRecog.IsThumbTipBent (hand, minAngle);

		return a && b && c && t && d;

	}

	// Check if the hand is clenching by certain amount of angle. Non Strict clenching means only index finger,
	// moddle ringer and ring finger need to bend by that certain amount of angle.
	public static bool IsHandClenchingNonStrict(HandModel hand, float minAngle){


		bool a = HandRecog.IsIndexFingerTipBent (hand, minAngle);
		bool b = HandRecog.IsMiddleFingerTipBent (hand, minAngle);
		bool c = HandRecog.IsRingFingerTipBent (hand, minAngle);

		return a && b && c;

	}

	// Check if a specific part of finger bends by and within a range of angles.
	public static bool IsFingerBentWithinAngle(HandModel hand, int fingerIndex, int boneIndex, float minAngle, float maxAngle){

		FingerModel finger = hand.fingers [fingerIndex];

		Vector3 fingerDir = finger.GetBoneDirection (boneIndex);

		Vector3 palmNormal = hand.GetPalmNormal ();

		Vector3 palmDir = hand.GetPalmDirection ();

		if (fingerIndex == 0) {

			float angle = Math3d.SignedVectorAngle (palmDir, fingerDir, palmNormal);

			if (angle >= minAngle && angle <= maxAngle)
				return true;


		} else if (fingerIndex > 0) {

			Vector3 projPlane = Vector3.Cross(palmNormal, palmDir).normalized;
			Vector3 projVector = Math3d.ProjectVectorOnPlane(projPlane,fingerDir).normalized;


			float angle = Math3d.SignedVectorAngle(palmNormal,projVector,projPlane);

			if (angle >= minAngle && angle <= maxAngle)
				return true;
		}


		return false;

	}


	// Calculate the angle between a finger direction and a  direcitonal vector.
	public static float AngleBetweenFingerAndVector(HandModel hand,int fingerIndex, int boneIndex, Vector3 referenceVector,Vector3 projectPlaneNormal){

		FingerModel finger = hand.fingers [fingerIndex];
		Vector3 fingerVector = finger.GetBoneDirection (boneIndex);

		Vector3 fingerVectorProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, fingerVector).normalized;
		Vector3 referenceVectorProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, referenceVector).normalized;

		float angle = Math3d.SignedVectorAngle (referenceVectorProj, fingerVectorProj, projectPlaneNormal);


		return angle;

	}

	// Calculate the angle between tip directions of two different fingers.
	public static float AngleBetweenFingerTips(HandModel hand, int fingerIndexOne, int fingerIndexTwo, Vector3 projectPlaneNormal){

		FingerModel fingerOne = hand.fingers [fingerIndexOne];
		FingerModel fingerTwo = hand.fingers [fingerIndexTwo];

		Vector3 fingerOneDir = fingerOne.GetBoneDirection (3);
		Vector3 fingerTwoDir = fingerTwo.GetBoneDirection (3);

		Vector3 fingerOneDirProj = Math3d.ProjectVectorOnPlane (projectPlaneNormal, fingerOneDir).normalized;
		Vector3 fingerTwoDirProj = Math3d.ProjectVectorOnPlane(projectPlaneNormal,fingerTwoDir).normalized;

		return Math3d.SignedVectorAngle ( fingerOneDirProj, fingerTwoDirProj, projectPlaneNormal);

	}

	// calculate the angle between tip directions of two different fingers projected on the plane that is perpendicular to the palm surface.
	public static float AngleBetweenFingerTipsVertical(HandModel hand, int fingerIndexOne, int fingerIndexTwo){
		Vector3 palmVector = hand.GetPalmDirection ();
		Vector3 palmNormal = hand.GetPalmNormal ();
		Vector3 crossVect = Vector3.Cross (palmVector, palmNormal).normalized;
		return AngleBetweenFingerTips (hand, fingerIndexOne, fingerIndexTwo, crossVect);

	}

	// calculate the angle between tip directions of two different fingers projected on the palm surface.
	public static float AngleBetweenFingerTipsHorizontal(HandModel hand, int fingerIndexOne, int fingerIndexTwo){

		float angle = AngleBetweenFingerTips(hand,fingerIndexOne, fingerIndexTwo,hand.GetPalmNormal());

		if (hand.GetLeapHand ().IsRight)
			return -angle;

		return angle;

	}

	public static float AngleBetweenFingerTipAndPalmDirection(HandModel hand, int fingerIndex){

		return AngleBetweenFingerAndPalmDirection (hand, fingerIndex, 3);
	}


	public static float AngleBetweenFingerAndPalmDirection(HandModel hand, int fingerIndex, int boneIndex){

		Vector3 palmVector = hand.GetPalmDirection ();
		Vector3 palmNormal = hand.GetPalmNormal ();
		Vector3 crossVect = Vector3.Cross (palmVector, palmNormal).normalized;

		float angle =  AngleBetweenFingerAndVector (hand, fingerIndex, boneIndex, palmVector, crossVect);

		return angle;
	
	}


	public static float AngleBetweenPalmsNormals(HandModel leftHand, HandModel rightHand, Vector3 projectPlane){

		Vector3 leftPalmNorm = leftHand.GetPalmNormal ();
		Vector3 rightPalmNorm = rightHand.GetPalmNormal ();

		Vector3 leftPalmNormProj = Math3d.ProjectVectorOnPlane (projectPlane,leftPalmNorm).normalized;
		Vector3 rightPalmNormProj = Math3d.ProjectVectorOnPlane (projectPlane,rightPalmNorm).normalized;

		float angle = Math3d.SignedVectorAngle (leftPalmNormProj, rightPalmNormProj,projectPlane);

		return angle;

	}

	public static bool PalmsFacingEachOther(HandController handController, HandModel leftHand, HandModel rightHand, float minAngle){

		float angle = AngleBetweenPalmsNormals (leftHand, rightHand,
			handController.transform.forward);

		if (Math.Abs (angle) >= minAngle) {
			return true;
		}

		return false;
	}


	public static float AngleBetweenPalmsDirections(HandModel leftHand, HandModel rightHand, Vector3 projectPlane){

		Vector3 leftPalmDir = leftHand.GetPalmDirection ();
		Vector3 rightPalmDir = rightHand.GetPalmDirection ();

		Vector3 leftPalmDirProj = Math3d.ProjectVectorOnPlane (projectPlane, leftPalmDir).normalized;
		Vector3 rightPalmDirProj = Math3d.ProjectVectorOnPlane (projectPlane, rightPalmDir).normalized;

		float angle = Math3d.SignedVectorAngle (leftPalmDirProj, rightPalmDirProj, projectPlane);

		return angle;

	}

	public static HandModel FindFirstLeftHand(HandModel [] hands){

		for (int i = 0; i <hands.Length; i++) {

			HandModel hand = hands[i];
			if (hand.GetLeapHand().IsLeft){

				return hand;
			}

		}

		return null;

	}

	public static HandModel FindFirstRightHand(HandModel [] hands){

		for (int i = 0; i <hands.Length; i++) {

			HandModel hand = hands[i];
			if (hand.GetLeapHand().IsRight){

				return hand;
			}

		}

		return null;

	}


	private static bool IsFingerBent(HandModel hand, int fingerIndex, int boneIndex, float theta){

		float angle = AngleBetweenFingerAndPalmDirection (hand, fingerIndex, boneIndex);
		if (angle >= theta)
			return true;
		else
			return false;

	}

	private static bool IsFingerBent(HandModel hand, int fingerIndex, int  boneIndex, float minTheta, float maxTheta){

		float angle = AngleBetweenFingerAndPalmDirection (hand, fingerIndex, boneIndex);
		if (angle >=minTheta && angle <= maxTheta)
			return true;
		else
			return false;

	}

	private static bool IsFingerTipBent(HandModel hand, int fingerIndex, float theta){

		return IsFingerBent (hand, fingerIndex, 3,theta);
	}

	public static bool IsThumbTipBent(HandModel hand, float theta){


		return IsFingerTipBent (hand, 0, theta);
	}

	public static bool IsIndexFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 1, theta);
	}

	public static bool IsMiddleFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 2, theta);
	}

	public static bool IsRingFingerTipBent(HandModel hand, float theta){

		return IsFingerTipBent (hand, 3, theta);

	}

	public static bool IsLittleFingerTipBent(HandModel hand, float theta){
		return IsFingerTipBent (hand, 4, theta);
	}
}