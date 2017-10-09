using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret_AI : MonoBehaviour {
	public GameObject target;
	public GameObject turret_bullet;

	public bool red;

	public float max_health = 20;
	public float health = 20;
	public float armor = 10;

	public float time_between_shots = 2f;
	public bool shot_ready = true;

	public float range = 10;
	public float base_damage = 10;
	public float actual_damage;
	public float multiplier = 1.25f;

	public Image hp_bar;

	// Use this for initialization
	void Start () {
		actual_damage = base_damage;
		health = max_health;
		hp_bar = transform.Find ("Turret_HP/HP_Bar").GetComponent<Image> ();
	}


	public void TakeDamage(float amount)	{
		float final_amount = amount;

		health -= final_amount;
		if (health <= 0) {
			Debug.Log ("TURRET DIED");
			Destroy (this.gameObject);
		} 
		else
			//Debug.Log (this.gameObject.name + " took " + amount + " damage!");
		
		hp_bar.fillAmount = health/max_health;
	}

	void resetBool()	{
		//Debug.Log ("RESETTING BOOL");
		shot_ready = true;
		Fire ();
	}

	void Fire()	{
		if (target != null && shot_ready == true) {
			//create a turret_bullet object
			GameObject b = Instantiate (turret_bullet, this.transform.position, Quaternion.identity);
			b.GetComponent<Turret_Bullet> ().setInfo (target, actual_damage);

			actual_damage = actual_damage * multiplier;
			shot_ready = false;
			Invoke ("resetBool", time_between_shots);
		}
	}

	public void setTarget(GameObject t)	{
		target = t;
		Fire ();
	}

	public void cancelFire()	{
		target = null;
		//CancelInvoke ();
		actual_damage = base_damage;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
