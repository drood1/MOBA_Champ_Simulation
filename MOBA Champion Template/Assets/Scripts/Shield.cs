using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public float duration = 4f;

	// Use this for initialization
	void Start () {
		Debug.Log ("SHIELD CREATED");

		Invoke ("DestroySelf", duration);
	}

	void DestroySelf()	{
		Debug.Log ("SHIELD EXPIRED");
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {


	}
}
