using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Damage : MonoBehaviour {
	public bool red;

	//Script for storing player's stats (in this case, AP)
	public Stats stats;

	public GameObject caster;

	//Enemy's script for dealing damage
	public Stats enemy_stats;

	public float base_damage = 10;
	public float AP_scaling = 0.5f;

	public float AP = 1;

	float raw_damage = 0;

	void OnTriggerEnter(Collider col)	{
		if ((red == false && (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion"))) {
			Debug.Log ("BLUE CUBES HITTING RED CHAMP");
			enemy_stats = col.gameObject.GetComponent<Stats> ();

			//deal damage to the enemy
			float raw_damage = base_damage + (AP_scaling * AP);

			//Debug.Log ("RAW DAMAGE: " + raw_damage);

			//factor in enemy's defenses for damage
			float total_damage = raw_damage * (100 / (100 + enemy_stats.MR - stats.magic_pen));

			//Debug.Log ("TOTAL DAMAGE: " + total_damage);

			enemy_stats.TakeDamage (total_damage);

			//if I want this not not be able to hit multiple enemies
			//Destroy(this.gameObject);
		} 
		else if (red == true && (col.gameObject.tag == "Blue_Champ" || col.gameObject.tag == "Blue_Minion")) {
			//Debug.Log ("RED CUBES HITTING BLUE CHAMP");
			enemy_stats = col.gameObject.GetComponent<Stats> ();

			//deal damage to the enemy
			raw_damage = base_damage + (AP_scaling * AP);

			//Debug.Log ("RAW DAMAGE: " + raw_damage);

			//factor in enemy's defenses for damage
			//Debug.Log("RAW DAMAGE: " + raw_damage);
			//Debug.Log("ENEMY MR: " + enemy_stats.MR);
			//Debug.Log("MAGIC PEN: " + stats.magic_pen);

			float total_damage = raw_damage * (100 / (100 + enemy_stats.MR - stats.magic_pen));

			//Debug.Log ("TOTAL DAMAGE: " + total_damage);

			enemy_stats.TakeDamage (total_damage);

			//if I want this not not be able to hit multiple enemies
			//Destroy(this.gameObject);


		}
	}

	// Use this for initialization
	void Start () {
		//AP = this.gameObject.GetComponentInParent<Talon_R_Cubes> ().AP;
		red = this.gameObject.GetComponentInParent<Q_Center_Despawn>().red;
		caster = this.gameObject.GetComponentInParent<Q_Center_Despawn> ().caster;
		stats = caster.GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
