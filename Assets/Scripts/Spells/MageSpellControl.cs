using UnityEngine;
using System.Collections;

public class MageSpellControl : MonoBehaviour {

	// Use this for initialization
	private bool IsSpellCasting = false;

	void Start () {


	}

	public void GrabCastingControl(){
		IsSpellCasting = true;
	}

	public bool SpellCasting() {
		return IsSpellCasting;

	}

	public void ReleaseCastingControl(){

		IsSpellCasting = false;
	}
}