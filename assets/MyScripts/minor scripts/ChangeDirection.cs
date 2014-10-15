//using UnityEngine;
//using System.Collections;
//
//public class ChangeDirection : MonoBehaviour {
//
//	
//	
//
//	
//	Quaternion OrientationOffset = Quaternion.identity;
//	
//	private float   YRotation = 0.0f;
//
//	
//	public virtual void UpdatePlayerForwardDirTransform()
//	{
//		{
//			Quaternion q = Quaternion.identity;
//			transform.rotation = q * GameObject.Find(OVRCameraController).transform.rotation;
//		}
//	}
//	
//	/// <summary>
//	/// Initializes the inputs.
//	/// </summary>
//	public void InitializeInputs()
//	{
//		// Get our start direction
//		OrientationOffset = transform.rotation;
//		// Make sure to set y rotation to 0 degrees
//		YRotation = 0.0f;
//	}
//	
//	/// <summary>
//	/// Sets the cameras.
//	/// </summary>
//	public void SetCameras()
//	{
//		
//		GameObject.Find(OVRCameraController).SetOrientationOffset(OrientationOffset);
//		GameObject.Find(OVRCameraController).SetYRotation(YRotation);
//		
//	}
//	void Start () {
//		
//		SetCameras ();
//			InitializeInputs();
//		
//	}
//	void Update () {
//	
//		UpdatePlayerForwardDirTransform ();
//
//	}
//}
