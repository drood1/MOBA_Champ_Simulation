using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
    public Transform center;

    public float speed = 1;

	public float inner_shrink_speed = 1;
	public float outer_shrink_speed = 1.15f;

	public bool clockwise = true;

	public float distance = 0f;

	float inner_min = 2.675581f;
	float outer_min = 4f;
	float inner_max = 6.5f;
	float outer_max = 9.5f;


	// Use this for initialization
	void Start () {
		//center = GameObject.Find ("Center");
		center = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {

		distance = Vector3.Distance (this.transform.position, center.transform.position);


		//shrink
		if (Input.GetKey (KeyCode.Q)) {
			if (clockwise == true) {
				if (distance > inner_min)
					transform.position = Vector3.MoveTowards (transform.position, center.transform.position, inner_shrink_speed * Time.deltaTime);
			} 
			else if (clockwise == false) {
				if (distance > outer_min)
					transform.position = Vector3.MoveTowards (transform.position, center.transform.position,outer_shrink_speed * Time.deltaTime);
			}
		}

		//expand
		if(Input.GetKey(KeyCode.E))	{
			if (clockwise == true) {
				if (distance < inner_max)
					transform.position = Vector3.MoveTowards (transform.position, center.transform.position, inner_shrink_speed * Time.deltaTime * -1);
			} 
			else if (clockwise == false) {
				if (distance < outer_max)
					transform.position = Vector3.MoveTowards (transform.position, center.transform.position,outer_shrink_speed * Time.deltaTime * -1);
			}

		}


		if(clockwise == true)
        	transform.RotateAround(center.position, Vector3.up, 20 * Time.deltaTime * speed);
		else
			transform.RotateAround(center.position, Vector3.up, 20 * Time.deltaTime * speed * -1);
	}
}
