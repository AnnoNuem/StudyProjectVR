using UnityEngine;
using System.Collections;

public class bla : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string formatForMySql = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
		Debug.Log (System.DateTime.Now);
		Debug.Log (formatForMySql);
		Debug.Log (" INSERT INTO Session (Subject_ID, DataTime, SessionID) VALUES (" 
						+ System.DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss:ffff;") + ");");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
