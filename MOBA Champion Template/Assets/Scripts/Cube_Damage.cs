using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Damage : MonoBehaviour {

	//Script for storing player's stats (in this case, AP)
	public Stats stats;

	public GameObject caster;

	//Enemy's script for daeling damage
	public Enemy_Stats enemy_stats;

	public float base_damage = 10;
	public float AP_scaling = 0.5f;

	public float AP = 1;


	void OnTriggerEnter(Collider col)	{
		if (col.gameObject.tag == "Red_Champ" || col.gameObject.tag == "Red_Minion") {
			enemy_stats = col.gameObject.GetComponent<Enemy_Stats> ();

			//deal damage to the enemy
			float raw_damage = base_damage + (AP_scaling * AP);

			Debug.Log ("RAW DAMAGE: " + raw_damage);

			//factor in enemy's defenses for damage
			float total_damage = raw_damage * (100 / (100 + enemy_stats.MR - stats.magic_pen));

			Debug.Log ("TOTAL DAMAGE: " + total_damage);

			enemy_stats.TakeDamage(total_damage);

			//if I want this not not be able to hit multiple enemies
			//Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		caster = this.gameObject.GetComponent<Talon_R_Cubes> ().caster.gameObject;
		stats = caster.GetComponent<Stats>();
		AP =  stats.AP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
