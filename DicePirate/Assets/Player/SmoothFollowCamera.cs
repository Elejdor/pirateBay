using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour {
	public Transform target;
	private Vector3 targetOffset = Vector3.zero;

	// Use this for initialization
	void Start () {
		targetOffset = target.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
