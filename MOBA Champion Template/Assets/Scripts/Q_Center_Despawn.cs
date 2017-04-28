using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_Center_Despawn : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position;

		if (transform.childCount == 0)
			Destroy (this.gameObject);
	}
}
