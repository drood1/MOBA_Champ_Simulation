using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Manager : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}


	public void ApplyDebuffToSelf(int debuff_id, GameObject caster)	{
		//identify the proper debuff based off debuff_id
		if (debuff_id == 0) {
			Debug.Log ("MALEFIC VISIONS APPLIED");
			this.gameObject.AddComponent<MaleficVisions>();
		}
		//this.gameObject.AddComponent<DebuffApplied>();
		return;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
