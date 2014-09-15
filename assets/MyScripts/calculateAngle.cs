using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class calculateAngle : MonoBehaviour {
	public Transform target;

	//FixedUpdate is used for rigid bodies and is executed independently of the configuration of system i.e. runs on regular set of intervals
	void FixedUpdate () {
		Vector2 targetVector = new Vector2 (target.position.x, target.position.z); 
		Vector2 transformVector = new Vector2 (transform.position.x, transform.position.z);
		Vector2 forwardVector = new Vector2 (transform.forward.x, transform.forward.z);

		Vector2 targetDir = targetVector - transformVector;

		float angle = Vector2.Angle (targetDir, forwardVector);

		//Debug.Log ("Angle --->" + angle);
		
	}
	
}