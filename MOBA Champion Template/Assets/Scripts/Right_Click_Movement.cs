using UnityEngine;
using System.Collections;

public class Right_Click_Movement : MonoBehaviour {
	public Auto_Attack aa_script;

	public GameObject click_indicator;

    Vector3 dir;
    public Vector3 target_location;
    public float target_distance;
	public float max_distance = 3f;
    public Rigidbody rb;

    public float move_speed = 16f;
	//keeps a "backup" variable for base movespeed to use when movespeed-altering effects expire
	public float backup_MS;

    public float turn_speed = 4f;

    //Initialization
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
		aa_script = this.gameObject.GetComponent<Auto_Attack> ();
		backup_MS = move_speed;
    }


    void Path()
    {
		//get the direction from point A (player's position) to point B (target location)
        dir = this.gameObject.transform.position - target_location;

		//update target location (to check when player is "close enough")
        //target_distance = Vector3.Distance(this.transform.position, target_location);

		//move toward the target location using the player's rigidbody component
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * move_speed);

        //update target_distance
        target_distance = Vector3.Distance(this.transform.position, target_location);
    }

	//standard rotate function to make character face in the direction that they're moving
	//(dir gotten from Path() function)
    void Rotate_Func()
    {
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Vector3 temp = new Vector3(0, angle + 180f, 0);

        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        temp = new Vector3(0, angle + 180f, 0);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler (temp), 4f);
        transform.rotation = Quaternion.Euler(temp);
        //GetComponent<Rigidbody>().velocity = dir.normalized * turn_speed;
    }

    public void setTarget(GameObject g)
    {
        if (g.name != "Floor")
            setTargetLocation(g.gameObject.transform.position);
    }


    public void setTargetLocation(Vector3 l)
    {
		//when moving, turn off auto-attack
		aa_script.shooting = false;
        target_location = l;
		move_speed = backup_MS;
        Path();
    }

    // Update is called once per frame
    void Update () {

        Rotate_Func();

		//Keep pathing to the target location until it gets close enough
		if (target_distance > max_distance)
        {
            Path();
        }


		//Upon right clicking,
        if (Input.GetMouseButton(1))
        {
			//Create a Raycast to get the mouse's location on the game's "floor" and mark that as the target location for pathing
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
				if (hit.collider.gameObject.name == "Floor") {
					//create a brief "click_indicator" object at the selected location to give player visual feedback
					//Note: click_indicator object deletes itself after a half second
					Instantiate (click_indicator, hit.point, Quaternion.identity);
					setTargetLocation (hit.point);
				}
            }

        }

		//Press Esc to quit the game
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();

    }
}
