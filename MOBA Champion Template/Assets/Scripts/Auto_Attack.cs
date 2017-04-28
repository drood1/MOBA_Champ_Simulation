using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Attack : MonoBehaviour {
	public GameObject target;
	public GameObject projectile;

	public bool shooting = false;

	public float AA_range = 10;

	public bool attacking = true;
	public bool AA_on_CD = false;
	public float attack_speed = 1;

	public float time_of_attack = 0;
	public float next_AA_time = 0;

	public bool in_anim = false;
	public float anim_duration;

	public float damage = 10;

	// Use this for initialization
	void Start () {
		
	}

	public void AA_Anim()	{


	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.tag == "Enemy") {
					Debug.Log ("TARGET IS " + hit.collider.gameObject.name);
					target = hit.collider.gameObject;
					shooting = true;
				}

			}

		}
		if (shooting == true && target != null) {
			//check if target is in range
			if (Vector3.Distance (this.transform.position, target.transform.position) > AA_range) {
				Debug.Log ("Too far away");
				//target is too far away, path to them
			} 
			//target is in range
			else {
				//tracking what time an autoattack was fired and calculating when the next will fire
				if (attacking == true) {
					if (AA_on_CD == false) {
						GameObject temp = Instantiate (projectile, this.transform.position, Quaternion.identity);
						temp.GetComponent<AA_Projectile> ().Create (target, damage);
						Debug.Log ("Attack at: " + Time.time);
						time_of_attack = Time.time;
						next_AA_time = time_of_attack + (1 / attack_speed);
						AA_on_CD = true;
					}
				}
			}

		}

		//reseting AA Cooldown
		if (AA_on_CD == true) {
			if (Time.time > next_AA_time)
				AA_on_CD = false;

		}

	}
}
