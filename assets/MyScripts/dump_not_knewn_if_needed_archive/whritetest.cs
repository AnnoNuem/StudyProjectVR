using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


public class whritetest : MonoBehaviour {

	private int lauf =0;
	private StreamWriter sw ;

	// Use this for initialization
	void Start () {
	
				sw = new StreamWriter ("CDriveDirs.txt");

				sw.WriteLine ("test.txt");
				
		}
	void OnApplicationQuit() {

		sw.Close ();

	}

	
	
	// Update is called once per frame
	void Update () {
	

	

			lauf++ ;
			sw.WriteLine(lauf); 
			


	}
}