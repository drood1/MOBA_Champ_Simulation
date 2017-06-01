using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	public Player_Stats stats;

	public float duration = 4f;

	// Use this for initialization
	void Start () {
		Debug.Log ("SHIELD CREATED");
		stats = transform.parent.GetComponent<Player_Stats> ();

		Invoke ("DestroySelf", duration);
	}

	public void DestroySelf()	{
		Debug.Log ("SHIELD EXPIRED");
		stats.shields = 0;
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {


	}
}
