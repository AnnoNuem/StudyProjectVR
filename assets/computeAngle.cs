using UnityEngine;
using System.Collections;



public class computeAngle : MonoBehaviour {

	double angleBetween = 0.0;
	Transform target;

	Vector2 targetDirection;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		targetDirection = target.position - transform.position;
		angleBetween = Vector3.Angle (transform.forward, targetDirection);
		Debug.Log(angleBetween);
	}
}
