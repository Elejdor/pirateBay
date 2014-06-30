using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float rotationSpeed = 100;
	public float velocity = 0;
	private float maxSpeed = 5;
	public float Leap = 0;
	public GameObject shipModel;

	private bool useDicepl = false;
	// Use this for initialization
	void Start () {
		velocity = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (!useDicepl) 
		{
			if (Input.GetKey (KeyCode.A))
			{
				Leap = -1;
			}
			else if (Input.GetKey (KeyCode.D))
			{
				Leap = 1;
			}
			else 
			{
				Leap = 0;
			}
		}

		shipModel.transform.rotation = Quaternion.Euler(new Vector3(shipModel.transform.eulerAngles.x, shipModel.transform.eulerAngles.y, 0));
		shipModel.transform.Rotate(new Vector3(0.0f, Leap * rotationSpeed * Time.deltaTime, 0));
		shipModel.transform.rotation = Quaternion.Euler(new Vector3(shipModel.transform.eulerAngles.x, shipModel.transform.eulerAngles.y, -Leap*10.0f));
		transform.Translate(shipModel.transform.forward*velocity*Time.deltaTime);
	}
}
