using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_Projectile : MonoBehaviour {
	public GameObject target;
	public float damage = 0;
	public float speed = 0.1f;

	public bool red;


	void OnTriggerEnter(Collider col)	{
		/*
		//blue bullet
		if (red == false) {
			if (col.gameObject.tag == "Red_Champ") {
				col.gameObject.GetComponent<Stats> ().TakeDamage (damage);
				Destroy (this.gameObject);
			} else if (col.gameObject.tag == "Red_Turret") {
				col.gameObject.GetComponent<Turret_AI> ().TakeDamage (damage);
				Destroy (this.gameObject);
			}
		}
		//red bullet
		else {
			if (col.gameObject.tag == "Blue_Champ") {
				col.gameObject.GetComponent<Stats> ().TakeDamage (damage);
				Destroy (this.gameObject);
			} else if (col.gameObject.tag == "Blue_Turret") {
				col.gameObject.GetComponent<Turret_AI> ().TakeDamage (damage);
				Destroy (this.gameObject);
			}
		}
		*/
		if (col.gameObject == target) {
			if(col.gameObject.tag.Contains("Turret"))
				col.gameObject.GetComponent<Turret_AI> ().TakeDamage (damage);
			else
				col.gameObject.GetComponent<Stats> ().TakeDamage (damage);

			Destroy (this.gameObject);
		}
	}

	public void Create(GameObject t, float d)	{
		target = t;
		damage = d;
	}

	void Path()	{
		
	}
		
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, speed);
		} 
		else
			Destroy (this.gameObject);

	}
}
