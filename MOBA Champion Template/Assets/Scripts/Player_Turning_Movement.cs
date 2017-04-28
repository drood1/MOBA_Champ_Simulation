using UnityEngine;
using System.Collections;

public class Player_Turning_Movement : MonoBehaviour {

    public float speed = 1;

    public float RotateSpeed = 1;

    Rigidbody this_rb;
    // Use this for initialization
    void Start () {
        this_rb = this.gameObject.GetComponent<Rigidbody>();
	}
		

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
		
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * 0.1f * speed);
		else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -0.1f * speed);
    }
}
