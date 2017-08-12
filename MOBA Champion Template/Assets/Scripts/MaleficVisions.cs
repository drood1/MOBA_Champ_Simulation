using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleficVisions : MonoBehaviour {
	public GameObject caster;
	public GameObject vfx;
	public Stats stats;
	public float total_duration = 10;
	public float time_applied;
	public float remaining_duration;

	public float damage_per_tick = 10;

	public float time_between_ticks = 1;

	float tick_timer = 0;


	// Use this for initialization
	void Start () {
		stats = this.gameObject.GetComponent<Stats> ();
		time_applied = Time.time;
		remaining_duration = total_duration;
		//Debug.Log (this.gameObject.name + " has received Malefic Visions at " + time_applied);

		//MV_VFX_prefab = (GameObject)Resources.Load ("Resources/Prefabs/Malefic_Visions_VFX");

		vfx = Instantiate(Resources.Load("Malefic_Visions_VFX", typeof(GameObject))) as GameObject;
		vfx.transform.parent = this.gameObject.transform;
		vfx.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		//at 60 FPS, deal damage every 1 second
		if(tick_timer / 60 >= time_between_ticks) {
			stats.TakeDamage (damage_per_tick);
			//Debug.Log ("TICK DAMAGE AT " + Time.time);
			tick_timer = 0;
		}

		tick_timer++;

		//Debug.Log (tick_timer);

		if (Time.time > time_applied + total_duration) {
			Destroy (vfx);
			Destroy (this);
		}

		remaining_duration = (time_applied + total_duration) - Time.time;
	}
}
