using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Detection : MonoBehaviour {
	public bool red; //true = "this is a red turret". false = "this is a blue turret"
	public Turret_AI t;	//Turret_AI script that is attached to the turret this script is attached to

	//list of any targets that are in range
	public List<GameObject> targets;

	void OnTriggerEnter(Collider col)	{
		//if this turret is red and a blue champion or minion enters range, add it to the list of possible targets
		if (red == true) { 
			if (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion") {
				Debug.Log (col.gameObject.name + " ADDED TO TARGET LIST");
				targets.Add (col.gameObject);
				t.setTarget (targets[0]);
			}
		}
		else //red == false (blue turret)
		{
			if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
				Debug.Log (col.gameObject.name + " ADDED TO TARGET LIST");
				targets.Add (col.gameObject);
				t.setTarget (targets[0]);
			}
		}
	}

	void OnTriggerExit(Collider col)	{
		//if this turret is red and the object that left range is a blue champ or minion, remove it from the list of possible targets
		if (red == true) {
			if (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion") {
				Debug.Log (col.gameObject.name + " REMOVED FROM TARGET LIST");
				targets.Remove (col.gameObject);
			}
		} 
		//if this turret is blue and the object that left range is a red champ or minion, remove it from the list of possible targets
		else {
			if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
				Debug.Log (col.gameObject.name + " REMOVED FROM TARGET LIST");
				targets.Remove (col.gameObject);
			}	
		}

		//if there are no more targets in range, stop firing
		if (targets.Count == 0)
			t.cancelFire ();
		//otherwise, acquire new target
		else
			t.setTarget (targets[0]);
	}

	// Use this for initialization
	void Start () {
		t = this.gameObject.GetComponentInParent<Turret_AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
