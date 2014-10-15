using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private Transform character;
	// Use this for initialization
	void Start () {
		character = GameObject.Find ("OVRPlayerController").transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = character.position;	
	}
}
