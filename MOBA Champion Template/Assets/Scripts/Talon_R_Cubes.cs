using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talon_R_Cubes : MonoBehaviour {
	public Transform center;
	public Transform caster = null;

	public float distance;
	public float dist_to_player;

	public float max_dist = 8f;

	public float min_dist = 1f;

	public float speed = 5f;

	public bool shrinking = false;

	// Use this for initialization
	void Start () {
		center = gameObject.transform.parent;
	}

	void Shrink()	{
		shrinking = true;
		//speed = speed * 1.25f;
	}

	public void SetCaster (GameObject p)
	{
		caster = p.transform;
	}

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (this.transform.position, center.position);
		dist_to_player = Vector3.Distance (this.transform.position, caster.position);

		if (shrinking == false) {
			if (distance < max_dist) {
				transform.position = Vector3.MoveTowards (transform.position, center.transform.position, speed * Time.deltaTime * -1);
			} 
			else {
				Invoke ("Shrink", 2.5f);
			}
		} 
		else {
			transform.position = Vector3.MoveTowards (transform.position, caster.transform.position, speed * Time.deltaTime);
			speed++;

			if (dist_to_player < min_dist)
				Destroy (this.gameObject);
		}
	}



}
