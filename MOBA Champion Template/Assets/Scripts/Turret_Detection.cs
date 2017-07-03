using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Detection : MonoBehaviour {
	public bool red;
	public Turret_AI t;

	public List<GameObject> targets;

	//public GameObject tar;

	void OnTriggerEnter(Collider col)	{
		if (red == true) { // red bullet
			if (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion") {
				Debug.Log (col.gameObject.name + " ADDED TO TARGET LIST");
				targets.Add (col.gameObject);
				t.setTarget (targets[0]);
				//tar = col.gameObject;
			}
		}
		else //red == false (blue bullet)
		{
			if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
				Debug.Log (col.gameObject.name + " ADDED TO TARGET LIST");
				targets.Add (col.gameObject);
				t.setTarget (targets[0]);
				//tar = col.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider col)	{
		if (red == true) { // red bullet
			if (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion") {
				Debug.Log (col.gameObject.name + " REMOVED FROM TARGET LIST");
				targets.Remove (col.gameObject);
			}
		} 
		else {
			if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
				Debug.Log (col.gameObject.name + " REMOVED FROM TARGET LIST");
				targets.Remove (col.gameObject);
			}	
		}
		if (targets.Count == 0)
			t.cancelFire ();
	}

	// Use this for initialization
	void Start () {
		t = this.gameObject.GetComponentInParent<Turret_AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
