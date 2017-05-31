using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Bar_Pos : MonoBehaviour {
	public GameObject g;

	Vector3 pos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pos = new Vector3 (g.transform.position.x, 2, g.transform.position.z);
		this.transform.position = pos;
	}
}
