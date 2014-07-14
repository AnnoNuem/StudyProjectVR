using UnityEngine;
using System;
using System.Collections;
// this contains the serial port communication
using System.IO.Ports;


public class CommunicationToFeelSpace : MonoBehaviour {
	


	private SerialPort sp;


	// Use this for initialization
	void Start () {
	
	sp = new SerialPort( "COM4"
		                    , 9600
		                    , Parity.None
		                    , 8
		                    , StopBits.One);
	
		// Open the port for communications
		sp.Open();
		
		// Write a string
		sp.Write("Hello World");
		
		// Write a set of bytes
		sp.Write(new byte[] {0xAA, 0x01}, 0, 2);
		// Close the port
		sp.Close();
	
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	
	
	
	}
}
