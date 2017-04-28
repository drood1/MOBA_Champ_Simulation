using UnityEngine;
using System.Collections;

public class Auto_Shrink_Expand : MonoBehaviour {


	public Transform center;

	//expanding when false, shrinking when true
	public bool shrinking = false;

	public float distance;

	public float max_dist = 10f;

	public float min_dist = 2f;

	public float shrink_speed = 5f;

	public bool destroy_on_max_dist = false;


	// Use this for initialization
	void Start () {
		center = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {

		distance = Vector3.Distance (this.transform.position, center.position);

		if(shrinking == true)
			transform.position = Vector3.MoveTowards (transform.position, center.transform.position, shrink_speed * Time.deltaTime);
		else
			transform.position = Vector3.MoveTowards (transform.position, center.transform.position, shrink_speed * Time.deltaTime * -1);



		if (distance > max_dist && shrinking == false)
			shrinking = true;
		if (distance < min_dist && shrinking == true)
			shrinking = false;


		if (destroy_on_max_dist && distance > max_dist) {
			Destroy (this.gameObject);
		}
	}
}
