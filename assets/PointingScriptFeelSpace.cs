using UnityEngine;
using System.Collections;

public class PointingScriptFeelSpace : MonoBehaviour {


	int timeForPointing = 8;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScriptFeelSpace.state == ManagerScriptFeelSpace.states.pointing)
		{
			//stuff for pointing, waiting for keys computing angles etc goes here
			//transit to walking (new trial) if finisched.

			//ManagerScript.switchState(ManagerScript.states.walking);
		}
	}

	public void NewPointing () {
				Invoke ("toLongPoint", timeForPointing);
		}

	void toLongPoint(){
		ManagerScriptFeelSpace.abortTrial ();
		Debug.Log ("To long for pointing");
		((GuiScriptFeelspace)(GameObject.Find ("GuiHelper").GetComponent ("GuiScriptFeelspace"))).toSlowPoint ();
	}

}	
