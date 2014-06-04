using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

public class randomSpawning : MonoBehaviour {
	
	void FixedUpdate () {
		Poisson pTest = new Poisson(5);
		Debug.Log("Random values-->"+ pTest.Sample());
	}

}


