using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	public Player_Stats stats;

	public float shield_total = 100;
	public float remaining_shields;

	public float duration = 4f;

	// Use this for initialization
	void Start () {
		remaining_shields = shield_total;

		Debug.Log ("SHIELD CREATED");
		stats = transform.parent.GetComponent<Player_Stats> ();

		Invoke ("DestroySelf", duration);
	}

	public void DestroySelf()	{
		Debug.Log ("SHIELD EXPIRED");
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {


	}
}
