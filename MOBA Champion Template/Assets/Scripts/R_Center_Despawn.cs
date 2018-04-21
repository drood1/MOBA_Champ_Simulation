using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Center_Despawn : MonoBehaviour {
	//This script serves as "clean up" to delete the invisible
	//center object of the R prefab once all the children are deleted
	public GameObject caster;

	public bool red;

	// Use this for initialization
	void Start () {
		
	}

	public void SetCaster(GameObject p)	{
		caster = p;
		if(p.GetComponent<Player_Abilities> () != null)
			red = p.GetComponent<Player_Abilities> ().red;
		else if(p.GetComponent<Player_Abilities_2> () != null)
			red = p.GetComponent<Player_Abilities_2> ().red;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position;

		if (transform.childCount == 0)
			Destroy (this.gameObject);
	}
}
