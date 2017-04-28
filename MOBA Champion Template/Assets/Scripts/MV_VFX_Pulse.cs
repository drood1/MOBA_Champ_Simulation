using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MV_VFX_Pulse : MonoBehaviour {
	public float max_size = 0.6f;

	public float min_size = 0.2f;

	public bool shrinking = false;

	Vector3 increment = new Vector3(0.0075f, 0.0075f, 0.0075f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > max_size)
			shrinking = true;
		if (transform.localScale.x < min_size)
			shrinking = false;


		if (shrinking == true) {
			//Debug.Log ("A");
			transform.localScale -= increment;
		}
		else {
			transform.localScale += increment;
			//Debug.Log ("B");
		}

	}
}
