using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Attack : MonoBehaviour {
	public GameObject target;
	public GameObject projectile;

	//indicates what team this object is on (true = red team, false = blue team)
	public bool red;

	public GameObject click_indicator;

	public Right_Click_Movement move_script;

	//indicates if the character's current command is to lock on to a target and auto-attack it
	public bool shooting = false;

	public float AA_range = 10;

	public bool AA_on_CD = false;
	public float attack_speed = 1;

	public float time_of_attack = 0;
	public float next_AA_time = 0;

	public bool in_anim = false;

	public float damage = 10;

	//how many seconds long the AA animation is
	public float AA_timer = 1f;

	//indicator position variables
	Vector3 indicator_pos;
	float ix;
	float iz;

	Vector3 dir;
	public Rigidbody rb;

	//Initialization
	void Start () {
		move_script = this.gameObject.GetComponent<Right_Click_Movement> ();
		rb = this.gameObject.GetComponent<Rigidbody>();

		//if this is a player object, figure out what team they are on based on what variable is set in their Abilities script
		if(this.gameObject.GetComponent<Player_Abilities> () != null)
			red = this.gameObject.GetComponent<Player_Abilities> ().red;
		else if(this.gameObject.GetComponent<Player_Abilities_2> () != null)
			red = this.gameObject.GetComponent<Player_Abilities_2> ().red;
	}

	//
	void AA_Anim()	{
		//if the player is currently in the "state" to AA and their AA is off cooldown
		if (shooting == true) {
			if (AA_on_CD == false) {
				//create the AA projectile and launch it toward the target
				GameObject temp = Instantiate (projectile, this.transform.position, Quaternion.identity);
				temp.GetComponent<AA_Projectile> ().Create (target, damage);
				//Debug.Log ("Attack at: " + Time.time);

				//put AA on cooldown and process when it will come off cooldown next
				time_of_attack = Time.time;
				next_AA_time = time_of_attack + (1 / attack_speed);
				AA_on_CD = true;
			}
		}
	}

	void PathToTarget(GameObject t)	{
		//get the direction from point A (player's position) to point B (target location)
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

		//Upon right clicking
		if (Input.GetMouseButton(1))
		{
			//Create a Raycast to get the mouse's location in the game
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				//if the player is on the blue team and right clicks an object belonging to the red team
				if (red == false && hit.collider.gameObject.tag.Contains("Red")) {
					ix = hit.collider.transform.position.x;
					iz = hit.collider.transform.position.z;
					indicator_pos = new Vector3 (ix, 0.1f, iz);

					//create a brief "click_indicator" object at the bottom of the target to give player visual feedback
					//Note: click_indicator object deletes itself after a half second
					Instantiate (click_indicator, indicator_pos, Quaternion.identity);

					//Debug.Log ("TARGET IS " + hit.collider.gameObject.name);

					target = hit.collider.gameObject;
					shooting = true;
				}
				//if the palyer is on the red team and right clicks on an object belonging to the blue team
				else if (red == true && hit.collider.gameObject.tag.Contains("Blue")) {
					ix = hit.collider.transform.position.x;
					iz = hit.collider.transform.position.z;
					indicator_pos = new Vector3 (ix, 0.1f, iz);

					//create a brief "click_indicator" object at the bottom of the target to give player visual feedback
					//Note: click_indicator object deletes itself after a half second
					Instantiate (click_indicator, indicator_pos, Quaternion.identity);

					//Debug.Log ("TARGET IS " + hit.collider.gameObject.name);

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
				//AA_timer indicates how long it takes for the character to perform their AA animation before the projectile is created and launched
				Invoke ("AA_Anim", AA_timer);
			}

		}

		//reseting AA Cooldown based on attack speed and when the lasts attack was fired (calculated in AA_Anim())
		if (AA_on_CD == true) {
			if (Time.time > next_AA_time)
				AA_on_CD = false;

		}

	}
}
