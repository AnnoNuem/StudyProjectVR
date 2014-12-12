//using UnityEngine;
//using System.Collections;
//
//public class crosshairtest : MonoBehaviour 
//{
//	
//	public Texture2D m_crosshairTexture;
//	// Rect for crosshair size and position
//	private Rect m_crosshairRect;  
//	
//	// bool to turn crosshair on and off
//	private bool m_bIsCrosshairVisible = true;
//	
//	void Awake () 
//	{
//		m_crosshairRect = new Rect((Screen.width - m_crosshairTexture.width) / 2, 
//		                           (Screen.height - m_crosshairTexture.height) / 2, 
//		                           m_crosshairTexture.width, 
//		                           m_crosshairTexture.height);
//	}
//	
//	void OnGUI () 
//	{
//		if (ManagerScript.getState () == ManagerScript.states.walking || ManagerScript.getState () == ManagerScript.states.pointing) {
//			if (m_bIsCrosshairVisible)
//				GUI.DrawTexture (m_crosshairRect, m_crosshairTexture);
//		}
//	}
//}