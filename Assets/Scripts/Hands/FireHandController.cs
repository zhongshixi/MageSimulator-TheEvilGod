using UnityEngine;
using System.Collections;



// Created by Zhongshi Xi
[RequireComponent (typeof (HandController))]
[RequireComponent (typeof(MageSpellControl))]
public class FireHandController : MageHandController {


	// prefab of fire ball
	public FireBall fireBallPrefab;

	// spawning location in y axis of fire ball relative to the hand that holds it.
	public float fireBallSpawnLocation = 0.15f;

	// The minum angle fingers need to bend to be considered as clenching. range from -90 deg to 180 deg relative to
	// the palm surface. -90 degree represents the hand is fully clenched, 180 degree represents all fingers are streached straight.
	[Range(-90.0f, 180.0f)]
	public float clenchingAngle = 15.0f;

	// the tolerance angle of palm to be considered as facing downwards relative to the local y axis.
	[Range (0.0f, 20.0f)]
	public float palmFacingDownAngle = 15.0f;

	// the time in seconds required to cast another fire ball after the previous bal is released.
	public float castingReloadTime = 1.0f;

	//public bool castingDirection 

	private FireBall currentFireBall;

	private float currCastTime;

	private float castStartTime;

	protected void Start(){

		base.Start ();

		castStartTime = 0.0f;
		currCastTime = castStartTime;

	}

	//Check if the palm facing upwards, all fingers are bent and if leap motion 
	//device meets the condidence level. If three conditions are met, then it is considered ready to cast fire ball. 
	bool IsReadyToCastFireBall(HandModel hand){

		return HandRecog.IsPalmFacingUpwards (hand, palmFacingDownAngle,  -1 * gameObject.transform.up) && HandRecog.IsHandClenchingStrict (hand, clenchingAngle) && hand.GetLeapHand().Confidence >= confidenceLevel;

	}

	// get the spawning location of fire ball.
	Vector3 GetFireBallSpawnPosition(HandModel hand, float distance){

		Vector3 palmPos = hand.GetPalmPosition ();

		Vector3 palmDir = hand.GetPalmNormal ();

		return palmPos + Vector3.Scale(palmDir,new Vector3(distance,distance,distance));
	}


	void CastFireBall(HandModel hand){

		if (currCastTime < castingReloadTime) {
			
			return;

		}else if (!IsCastingStarted && hand.GetLeapHand().Confidence >= confidenceLevel && !spellControl.SpellCasting()) {
			
				spellControl.SpellCasting ();

				IsCastingStarted = true;

				currentFireBall = GameObject.Instantiate (fireBallPrefab, GetFireBallSpawnPosition (hand, fireBallSpawnLocation), hand.gameObject.transform.rotation) as FireBall;

		}

	}

	void ReleaseFireBall(HandModel hand){


		if (currentFireBall && IsCastingStarted && !HandRecog.IsHandClenchingNonStrict (hand, clenchingAngle) && hand.GetLeapHand().Confidence > confidenceLevel) {

			currentFireBall.Release (hand.GetPalmDirection ());
			IsCastingStarted = false;
			currentFireBall = null;

			spellControl.ReleaseCastingControl ();

			currCastTime = castStartTime;


		}


	}


	void UpdateFireBall(HandModel hand){

		if (currentFireBall ) {

			currentFireBall.transform.position = GetFireBallSpawnPosition (hand, fireBallSpawnLocation);
			currentFireBall.transform.forward = hand.GetPalmDirection ();
			if (IsCastingStarted && HandRecog.IsPalmFacingUpwards(hand,palmFacingDownAngle, -1 * gameObject.transform.up)) {

				currentFireBall.Grow (Time.deltaTime);
			}
		}

	}



	// Update is called once per frame
	protected void Update () {

		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		currCastTime = currCastTime + Time.deltaTime;

		if (rightHand) {

			if (IsReadyToCastFireBall (rightHand))
				CastFireBall (rightHand);
			 else 
				ReleaseFireBall (rightHand);

			UpdateFireBall (rightHand);

		} else if (leftHand) {

			if (IsReadyToCastFireBall (leftHand))
				CastFireBall (leftHand);
			else
				ReleaseFireBall (leftHand);

			UpdateFireBall (leftHand);

		} else {

			if(currentFireBall)
				Destroy (currentFireBall.gameObject);
			
			IsCastingStarted = false;
			spellControl.ReleaseCastingControl ();

		}
	}
}
