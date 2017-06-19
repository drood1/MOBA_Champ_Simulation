using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Detection : MonoBehaviour {
	public bool red;
	public Turret_AI t;

	public GameObject tar;

	void OnTriggerEnter(Collider col)	{
		if (red == true) {
			if (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion") {
				Debug.Log ("TARGET SET TO " + col.gameObject.name);
				t.setTarget (col.gameObject);
				tar = col.gameObject;
			}
		}
		else {
			if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
				t.setTarget (col.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider col)	{
		if (col.gameObject == tar) {
			Debug.Log (tar.name + " HAS LEFT RANGE");
			t.cancelFire ();
		}
	}

	// Use this for initialization
	void Start () {
		t = this.gameObject.GetComponentInParent<Turret_AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
