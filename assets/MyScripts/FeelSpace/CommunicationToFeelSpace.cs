using UnityEngine;
using System;
using System.Collections;
// this contains the serial port communication
using System.IO.Ports;


public class CommunicationToFeelSpace : MonoBehaviour {
	


	private SerialPort sp;
	public Transform character ;
	byte lsb;
	byte msb;

	// Use this for initialization
	void Start () {
	
		character = GameObject.Find ("ForwardDirection").transform;


				sp = new SerialPort ("COM9"
		                    , 9600	
		                    , Parity.None
		                    , 8
		                    , StopBits.One);
		sp.Open ();
		InvokeRepeating("ChangeBeltSignal", 0.1F, 0.1F);

		}
	// Update is called once per frame
	void ChangeBeltSignal () {

						lsb = (byte)(character.transform.eulerAngles.y % 256);
						msb = (byte)Math.Floor (character.transform.eulerAngles.y / 256);
						sp.Write (new byte[] {0xAA, 0x04, lsb , msb , 0xFF}, 0, 5);
						Debug.Log ("penis");
				
	}

	void OnApplicationQuit() {

		sp.Write (new byte[] {0xAA, 0x01}, 0, 2);
		sp.Close();
	}

}


