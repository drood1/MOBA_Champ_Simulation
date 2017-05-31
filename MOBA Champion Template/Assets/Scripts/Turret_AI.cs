using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_AI : MonoBehaviour {
	public GameObject target;
	public GameObject turret_bullet;
	public GameObject player;

	public float health = 20;
	public float armor = 10;


	public float range = 10;
	public float base_damage = 10;
	public float actual_damage;
	public float multiplier = 1.25f;

	public bool shooting = false;


	// Use this for initialization
	void Start () {
		actual_damage = base_damage;
	}


	public void TakeDamage(float amount)	{
		float final_amount = amount;

		health -= final_amount;
		if (health <= 0) {
			Debug.Log ("TURRET DIED");
			Destroy (this.gameObject);
		} 
		else
			Debug.Log (this.gameObject.name + " took " + amount + " damage!");

	}

	void Fire()	{
		if (Vector3.Distance (this.gameObject.transform.position, target.transform.position) <= range) {
			//create a turret_bullet object
			GameObject b = Instantiate(turret_bullet, this.transform.position, Quaternion.identity);
			b.GetComponent<Enemy_Bullet> ().setInfo (target, actual_damage);

			actual_damage = actual_damage * multiplier;
			Invoke ("Fire", 2);
		}
		else {
			Debug.Log ("PLAYER EXITED RANGE");
			actual_damage = base_damage;
			target = null;
			CancelInvoke ();
			shooting = false;
		}
	}

	void setTarget(GameObject t)	{
		target = t;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Player") == null) {
			target = null;
			CancelInvoke ();
			shooting = false;
		}

		if (Vector3.Distance (player.transform.position, this.transform.position) <= range && shooting == false) {
			Debug.Log ("PLAYER ENTERED RANGE");
			target = player;
			Fire ();
			shooting = true;
		}

	}
}
