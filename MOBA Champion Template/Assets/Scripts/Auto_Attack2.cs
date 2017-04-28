using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Attack2 : MonoBehaviour {
	public GameObject target;
	public GameObject projectile;

	public GameObject click_indicator;

	public Right_Click_Movement move_script;

	public bool shooting = false;

	public float AA_range = 10;

	public bool AA_on_CD = false;
	public float attack_speed = 1;

	public float time_of_attack = 0;
	public float next_AA_time = 0;

	public bool in_anim = false;

	public float damage = 10;

	public float AA_timer = 1f; //how many seconds long the AA animation is


	Vector3 dir;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		move_script = this.gameObject.GetComponent<Right_Click_Movement> ();
		rb = this.gameObject.GetComponent<Rigidbody>();
	}

	void AA_Anim()	{
		if (shooting == true) {
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

	void PathToTarget(GameObject t)	{
		Debug.Log ("PATHING");
		dir = this.gameObject.transform.position - t.transform.position;

		//have the player facing towards the target
		float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		Vector3 temp = new Vector3(0, angle + 180f, 0);

		angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		temp = new Vector3(0, angle + 180f, 0);

		transform.rotation = Quaternion.Euler(temp);

		//move "forward" (towards target)
		rb.MovePosition(transform.position + transform.forward * Time.deltaTime * move_script.move_speed);

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
					Instantiate (click_indicator, hit.collider.transform.position, Quaternion.identity);
					Debug.Log ("TARGET IS " + hit.collider.gameObject.name);
					target = hit.collider.gameObject;
					shooting = true;
				}

			}

		}
		if (shooting == true && target != null) {
			//check if target is in range
			if (Vector3.Distance (this.transform.position, target.transform.position) > AA_range) {
				//target is too far away, path to them
				move_script.move_speed = move_script.backup_MS;
				PathToTarget(target);
			} 
			//target is in range
			else {
				//root the player to "play" the auto animation
				move_script.move_speed = 0;
				//calll the function when animation is over to see if the player didn't cancel the AA
				Invoke ("AA_Anim", AA_timer);
			}

		}

		//reseting AA Cooldown
		if (AA_on_CD == true) {
			if (Time.time > next_AA_time)
				AA_on_CD = false;

		}

	}
}
