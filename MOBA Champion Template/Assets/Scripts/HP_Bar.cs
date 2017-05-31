using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Bar : MonoBehaviour {
	public GameObject g;
	public float max_hp;
	public float current_hp;

	// Use this for initialization
	void Start () {
		max_hp = g.GetComponent<Player_Stats> ().max_health;
		current_hp = max_hp;
	}

	// Update is called once per frame
	void Update () {
		//change X scale based on current_hp/max_hp
		current_hp = g.GetComponent<Player_Stats>().health;
		this.transform.localScale = new Vector3((current_hp/max_hp), 1, 1);
	}
}
