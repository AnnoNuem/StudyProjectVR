using UnityEngine;
using System.Collections;

public class TestOculusRotation : MonoBehaviour {
	GameObject displaytext ;
	GameObject direction;
	float test;
	// Use this for initialization
	void Start () {
	displaytext = GameObject.Find ("Displaytext");
	direction = GameObject.Find ("ForwardDirection");

	}

	// Update is called once per frame
	void Update () {
		test = direction.transform.eulerAngles.y;
		displaytext.GetComponent<TextMesh> ().text = test.ToString();
	}
}
