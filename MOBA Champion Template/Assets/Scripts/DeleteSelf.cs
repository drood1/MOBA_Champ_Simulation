using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {
	public float timer = 0.5f;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", timer);
	}

	void DestroySelf()	{
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
