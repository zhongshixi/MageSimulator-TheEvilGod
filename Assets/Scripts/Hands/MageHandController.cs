using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent (typeof (HandController))]
[RequireComponent (typeof(MageSpellControl))]
public class MageHandController : NetworkBehaviour {


	// how accurate the leap motion is supposed to be. 0.0f means completely inaccurate, 1.0f means completely accurate.
	[Range(0.0f, 1.0f)]
	public float confidenceLevel;

	protected HandController handController;

	protected bool IsCastingStarted;

	protected MageSpellControl spellControl;

	protected void Awake () {

		handController = GetComponent<HandController> ();
		spellControl = GetComponent<MageSpellControl> ();

	}

	protected void Spawn(GameObject obj){
		NetworkServer.Spawn (obj);
	}

	protected void Start(){
	
	}

	protected void Update(){

	}
		
}