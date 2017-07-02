using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_Center_Despawn : MonoBehaviour {

	public GameObject caster;

	public bool red;

	// Use this for initialization
	void Start () {
		
	}

	public void SetCaster(GameObject p)	{
		caster = p;
		red = p.GetComponent<Player_Abilities> ().red;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position;

		if (transform.childCount == 0)
			Destroy (this.gameObject);
	}
}
