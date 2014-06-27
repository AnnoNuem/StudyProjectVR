using UnityEngine;
using System.Collections;

public class PointingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.state == ManagerScript.states.pointing)
		{
			//stuff for pointing, waiting for keys computing angles etc goes here
			//transit to walking (new trial) if finisched.

			//ManagerScript.switchState(ManagerScript.states.walking);
		}
	}
}
