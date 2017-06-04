using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Abilities : MonoBehaviour {
	public GameObject Q_object;
	public GameObject W_object;
	public GameObject R_object;
	//Debuff_Manager debuffs;

	public Player_Stats stat_script;

	public Texture2D target_mouse;
	public CursorMode cursor = CursorMode.Auto;
	Vector3 Q_pos;

	public float time_Q_cast = 0;
	public float time_W_cast = 0;
	public float time_E_cast = 0;
	public float time_R_cast = 0;
	public float Q_CD = 4;
	public float W_CD = 6;
	public float E_CD = 6;
	public float R_CD = 4;

	public float Q_cost = 5;
	public float W_cost = 10;
	public float E_cost = 10;
	public float R_cost = 15;

	public bool W_selection = false;
	public bool E_selection = false;

	public bool Q_on_CD = false;
	public bool W_on_CD = false;
	public bool E_on_CD = false;
	public bool R_on_CD = false;

	public float remaining_Q_CD = 0;
	public float remaining_W_CD = 0;
	public float remaining_E_CD = 0;
	public float remaining_R_CD = 0;

	public GameObject temp;

	float temp_x = 0;
	float temp_z = 0;

	// Use this for initialization
	void Start () {
		//target_mouse = Resources.Load ("ret");
		//debuffs = this.gameObject.GetComponent<Debuff_Manager> ();
		stat_script = this.gameObject.GetComponent<Player_Stats> ();
	}



	// Update is called once per frame
	void Update () {
		//Q ABILITY*******************************************************************************************************************************
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (Q_on_CD == false) {
				if (stat_script.mana >= Q_cost) {
					time_Q_cast = Time.time;
			
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit)) {
						temp_x = hit.point.x;
						temp_z = hit.point.z;
					}

					float dir_x = temp_x - transform.position.x;
					float dir_z = temp_z - transform.position.z;

					Vector3 Q_dir = new Vector3 (dir_x, 0, dir_z);

					Q_dir.Normalize ();

					Q_pos = this.transform.position + (Q_dir * 2);

					temp = Instantiate (Q_object, Q_pos, this.transform.rotation);

					temp.GetComponent<Mystic_Shot> ().Create (temp_x, temp_z, this.transform.rotation);
					Q_on_CD = true;
					stat_script.mana -= Q_cost;
					stat_script.UpdateManaBar ();
				} 
				else {
					Debug.Log ("NOT ENOUGH MANA TO CAST Q");
				}
			} 
			else {
				Debug.Log ("REMAINING Q CD: " + remaining_Q_CD);
			}
		}
		if (Q_on_CD == true) {
			if (Time.time >= time_Q_cast + Q_CD)
				Q_on_CD = false;
		}

		//W ABILITY*******************************************************************************************************************************
		if (Input.GetKeyDown (KeyCode.W)) {
			if (W_on_CD == false) {
				if (stat_script.mana >= W_cost) {
					//change mouse cursor to "targetting_cursor"
					W_selection = true;
					Debug.Log ("W PRESSED");
				} 
				else
					Debug.Log ("NOT ENOUGH MANA TO CAST W");
			}
			else
				Debug.Log ("REMAINING W CD: " + remaining_W_CD);
		}

		if(Input.GetMouseButtonDown(0))	{
			if(W_selection == true)	{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.tag == "Blue_Champ")
					{
						Debug.Log ("PUTTING SHIELD ON " + hit.collider.gameObject.name);
						//********************APPLY THE SHIELD BUFF HERE***************************************
						//create the shield object
						GameObject s = Instantiate(W_object, this.gameObject.transform);
						s.transform.localPosition = Vector3.zero;
						//hit.collider.gameObject
						W_on_CD = true;
						time_W_cast = Time.time;
						stat_script.mana -= W_cost;
						stat_script.UpdateManaBar ();
					}
					W_selection = false;
				}
			}
		}
		if (W_on_CD == true) {
			if (Time.time >= time_W_cast + W_CD)
				W_on_CD = false;
		}	
		//E ABILITY*******************************************************************************************************************************
		if (Input.GetKeyDown (KeyCode.E)) {
			if (E_on_CD == false) {
				if (stat_script.mana >= W_cost) {
					//change mouse cursor to "targetting_cursor"
					E_selection = true;
				}
				else
					Debug.Log ("NOT ENOUGH MANA TO CAST E");
			}
			else
				Debug.Log ("REMAINING E CD: " + remaining_E_CD);
		}

		if(Input.GetMouseButtonDown(0))	{
			if(E_selection == true)	{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.tag == "Red_Champ" || hit.collider.gameObject.tag == "Red_Minion")
					{
						//in this case, Malefic Visions' ID # is 0
						hit.collider.gameObject.GetComponent<Debuff_Manager>().ApplyDebuffToSelf(0, this.gameObject);
						E_on_CD = true;
						time_E_cast = Time.time;
						stat_script.mana -= E_cost;
						stat_script.UpdateManaBar ();
					}
					E_selection = false;
				}
			}
		}
		if (E_on_CD == true) {
			if (Time.time >= time_E_cast + E_CD)
				E_on_CD = false;
		}

		//R ABILITY*******************************************************************************************************************************
		if (Input.GetKeyDown (KeyCode.R)) {
			if (R_on_CD == false) {
				if (stat_script.mana >= R_cost) {
					time_R_cast = Time.time;
					temp = Instantiate (R_object, this.transform.position, this.transform.rotation);
					//temp.transform.parent = this.gameObject.transform;
					R_on_CD = true;
					stat_script.mana -= R_cost;
					stat_script.UpdateManaBar ();
				} 
				else
					Debug.Log ("NOT ENOUGH MANA TO CAST R");
			} 
			else {
				Debug.Log ("REMAINING R CD: " + remaining_R_CD);
			}
		}
		if (R_on_CD == true) {
			if (Time.time >= time_R_cast + R_CD)
				R_on_CD = false;
		}

		//update CD
		remaining_Q_CD = time_Q_cast + Q_CD - Time.time;
		remaining_W_CD = time_W_cast + W_CD - Time.time;
		remaining_E_CD = time_E_cast + E_CD - Time.time;
		remaining_R_CD = time_R_cast + R_CD - Time.time;
	}
}
