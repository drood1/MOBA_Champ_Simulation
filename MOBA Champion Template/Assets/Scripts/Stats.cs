using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
	public Player_Abilities abilities;
	public Shield current_shield;

	public int level = 1;

	public float max_health = 100;
	public float health;

	public float max_mana = 100;
	public float mana;

	public float AD = 75;

	public float AP = 100;

	public float armor = 80;
	public float MR = 50;

	public float armor_pen = 0;
	public float magic_pen = 20;

	public float CDR = 0;

	public Image hp_bar;
	public Image mana_bar;

	// Use this for initialization
	void Start () {
		hp_bar = transform.Find ("UI_Canvas/Player_HP_Bar").GetComponent<Image> ();
		mana_bar = transform.Find ("UI_Canvas/Player_Mana_Bar").GetComponent<Image> ();
		abilities = this.gameObject.GetComponent<Player_Abilities> ();
		health = max_health;
		mana = max_mana;
	}

	public void UpdateManaBar()	{
		mana_bar.fillAmount = mana/max_mana;
	}

	//should add an "index" argument (0 = physical, 1 = magic, 2 = true) in the future
	public void TakeDamage(float amount)	{
		Debug.Log ("AMOUNT: " + amount);
		float final_amount = amount;
		//calculate mitigation

		/*
		//*******************************DOUBLE CHECK THESE CALCULATIONS WHEN IMPLEMENTING*******************************
		//physical damage reduced by armor
		if(index == 0)  	{
			final_amount = amount * (1 - (100 / (armor + 100)));
		}
		//magic damage reduced by MR
		if(index == 1)	{
			final_amount = amount * (1 - (100 / (MR + 100)));
		}
		*/

		if (this.transform.Find ("Shield(Clone)") != null) {
			current_shield = this.transform.Find ("Shield(Clone)").GetComponent<Shield> ();
			if (current_shield.remaining_shields > final_amount) {
				current_shield.remaining_shields -= final_amount;
				//Debug.Log ("Took " + final_amount + " damage! Remaining shields: " + current_shield.remaining_shields);
			} 
			else {
				health -= (final_amount - current_shield.remaining_shields);
				//Debug.Log ("Player took " + (final_amount - current_shield.remaining_shields) + " damage!");
				current_shield.DestroySelf ();
			}
		} 
		else {
			health -= final_amount;
			//Debug.Log ("NO SHIELD, " + this.name + " took " + final_amount + " damage!");
		}

		//Debug.Log ("DONE WITH SHIELD CHECKS");

		hp_bar.fillAmount = health/max_health;

		if (health <= 0) {
			if (this.gameObject.tag.Contains ("Champ"))
				SceneManager.LoadScene ("main");

			Debug.Log (this.name + " DIED");
			Destroy(transform.Find("Player_Canvas"));
			Destroy (this.gameObject);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
