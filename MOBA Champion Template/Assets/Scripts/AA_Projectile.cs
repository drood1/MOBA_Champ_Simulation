using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_Projectile : MonoBehaviour {
	public GameObject target;
	public float damage = 0;
	public float speed = 1;


	void OnTriggerEnter(Collider col)	{
		if (col.gameObject.tag == "Enemy")	{
			col.gameObject.GetComponent<Enemy_Stats> ().TakeDamage (damage);
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
		} else
			Destroy (this.gameObject);

	}
}
