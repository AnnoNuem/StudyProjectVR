using UnityEngine;
using System.Collections;
using URandom;
public class testofpoisson : MonoBehaviour
{
	private UnityRandom urand;

		
	void Start ()
	{
		urand = new UnityRandom(9000);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (urand.Exponential (5.0f));
	}
}

