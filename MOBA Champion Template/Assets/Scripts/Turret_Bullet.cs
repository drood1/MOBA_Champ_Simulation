using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Bullet : MonoBehaviour {
	public GameObject target;

	public bool red;
	public float damage = 10;
	public float move_speed = 1f;


	void OnTriggerEnter(Collider col)	{
		if (col.gameObject.tag.Contains("Red") && red == false) {
			col.gameObject.GetComponent<Stats> ().TakeDamage (damage);
			Destroy (this.gameObject);
		}
		else if(col.gameObject.tag.Contains("Blue") && red == true) 	{
			col.gameObject.GetComponent<Stats> ().TakeDamage (damage);
			Destroy (this.gameObject);
		}
	}


	// Use this for initialization
	void Start () {
		
	}
		

	public void setInfo(GameObject t, float d)	{
		target = t;
		damage = d;
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, (move_speed * Time.deltaTime));
	}
}
