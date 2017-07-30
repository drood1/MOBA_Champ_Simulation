using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour {

	//Debuff_Manager debuffs;

	public float health = 100;

	public float armor = 50;

	public float MR = 40;

	public void TakeDamage(float amount)	{
		//Debug.Log (this.gameObject.name + " took " + amount + " damage!");
		health -= amount;
		if (health <= 0)
			Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		//debuffs = this.gameObject.GetComponent<Debuff_Manager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
