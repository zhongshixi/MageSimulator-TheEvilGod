using UnityEngine;
using System.Collections;

public class MageThunderHandController : MageHandController {
	/*

	public float palmDirMinAngle = 120.0f;
	public float handClenchingMinAngle = 15.0f;

	//public ThunderBolt thunderBoltPrefab;

	//private ThunderBolt currentThunderBolt;

	//private ThunderBolt [] bolts;


	protected void Start () {

		base.Start ();

		//bolts = new ThunderBolt[5];
	}

	bool IsReadyToCastBolts(HandModel leftHand, HandModel rightHand){

		return HandRecog.PalmsFacingEachOther (handController, leftHand, rightHand, palmDirMinAngle) &&
			HandRecog.IsHandClenchingStrict (leftHand, handClenchingMinAngle) && HandRecog.IsHandClenchingStrict (rightHand, handClenchingMinAngle);
	}

	bool IsReadyToRelaseBolts(HandModel leftHand, HandModel rightHand){

		int counter = 0;

		if (!HandRecog.IsIndexFingerTipBent (leftHand, handClenchingMinAngle)) {

			counter++;
		}

		if (!HandRecog.IsMiddleFingerTipBent (leftHand, handClenchingMinAngle)) {

			counter++;
		}

		if (!HandRecog.IsRingFingerTipBent (leftHand, handClenchingMinAngle)) {

			counter++;
		}

		if (!HandRecog.IsIndexFingerTipBent (rightHand, handClenchingMinAngle)) {

			counter ++;
		}

		if (!HandRecog.IsMiddleFingerTipBent (rightHand, handClenchingMinAngle)) {

			counter ++;
		}

		if (!HandRecog.IsRingFingerTipBent(rightHand, handClenchingMinAngle)){

			counter++;
		}

		if (counter >= 3)
			return true;

		return false;

	}

	void CastBolts(HandModel leftHand, HandModel rightHand ){


		if (!IsCastingStarted && !spellControl.SpellCasting()) {

			Vector3 middle = leftHand.GetPalmPosition () + rightHand.GetPalmPosition ();

			currentThunderBolt = GameObject.Instantiate (thunderBoltPrefab, middle, gameObject.transform.rotation) as ThunderBolt;
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());

			IsCastingStarted = true;
			spellControl.GrabCastingControl ();
		}



	}

	void UpdateBolts(HandModel leftHand, HandModel rightHand){

		if (currentThunderBolt && IsCastingStarted)
			currentThunderBolt.SetPosition (leftHand.GetPalmPosition (), rightHand.GetPalmPosition ());
	}

	void ReleaseBolts(HandModel leftHand, HandModel rightHand){

		if (currentThunderBolt ) {
			currentThunderBolt.Release ((leftHand.GetPalmDirection () + rightHand.GetPalmDirection ()).normalized, 5.0f);
			IsCastingStarted = false;
			spellControl.ReleaseCastingControl ();
			currentThunderBolt = null;
		}
	}

	void CancelCasting(){	

		if (currentThunderBolt)
			Destroy (currentThunderBolt.gameObject);

		IsCastingStarted = false;
		spellControl.ReleaseCastingControl ();

	}

	// Update is called once per frame
	protected void Update () {

		HandModel[] hands = handController.GetAllGraphicsHands ();
		HandModel rightHand = HandRecog.FindFirstRightHand (hands);
		HandModel leftHand = HandRecog.FindFirstLeftHand (hands);

		if (rightHand && leftHand) {

			if (IsReadyToCastBolts (leftHand, rightHand)) {
				CastBolts (leftHand, rightHand);

			}



			if (IsReadyToRelaseBolts (leftHand, rightHand))
				ReleaseBolts (leftHand, rightHand);

			UpdateBolts (leftHand, rightHand);

		} else {

			CancelCasting ();

		}

	}
*/

}