﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour {
	public float max_health = 100;
	public float health = 100;
	public float shields = 0;

	public float AD = 75;

	public float AP = 100;

	public float armor = 80;
	public float MR = 50;

	public float armor_pen = 0;
	public float magic_pen = 20;

	public float CDR = 0;

	public Image hp_bar;


	// Use this for initialization
	void Start () {
		hp_bar = transform.Find ("Player_Canvas/Player_HP_Bar").GetComponent<Image> ();
	}

	//should add an "index" argument (0 = physical, 1 = magic, 2 = true) in the future
	public void TakeDamage(float amount)	{
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

		if (shields > 0) {
			if (final_amount < shields) {
				shields -= final_amount;
				Debug.Log ("Player took " + final_amount + " damage! Remaining shields: " + shields);
			}
			else {
				health -= (final_amount - shields);
				shields = 0;
				Destroy(transform.Find("Player/Shield"));
			}
		}
		else
			Debug.Log ("Player took " + final_amount + " damage!");

		health -= final_amount;
		hp_bar.fillAmount = health/max_health;

		if (health <= 0) {
			Debug.Log ("PLAYER DIED");
			Destroy(GameObject.Find("Player_Health_Bar"));
			Destroy (this.gameObject);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
