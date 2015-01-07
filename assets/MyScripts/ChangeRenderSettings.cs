// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : razial
// Created          : 12-29-2014
//
// Last Modified By : razial
// Last Modified On : 01-02-2015
// ***********************************************************************
// <copyright file="ChangeRenderSettings.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// Class ChangeRenderSettings.
/// </summary>
public class ChangeRenderSettings : MonoBehaviour
{

    /// <summary>
    /// The fog color normal
    /// </summary>
	Color fogColorNormal = Color.black;
    /// <summary>
    /// The fog color easy
    /// </summary>
	Color fogColorEasy = new Color (0.0F / 255, 13F / 255 , 2F / 255);
    /// <summary>
    /// The fog color hard
    /// </summary>
	Color fogColorHard = new Color (15F / 255, 0.0F / 255, 0.0F / 255);
    /// <summary>
    /// The ambient light color normal
    /// </summary>
	Color ambientLightColorNormal = new Color (82F / 255, 82F / 255, 82F / 255);
    /// <summary>
    /// The ambient light color easy
    /// </summary>
	Color ambientLightColorEasy = new Color (61F / 255, 145F / 255, 81F / 255);
    /// <summary>
    /// The ambient light color hard
    /// </summary>
	Color ambientLightColorHard = new Color (145F / 255, 61F / 255, 61F / 255);

    /// <summary>
    /// The cam1
    /// </summary>
	Camera cam1;
    /// <summary>
    /// The cam2
    /// </summary>
	Camera cam2;

		// Use this for initialization
    /// <summary>
    /// Starts this instance.
    /// </summary>
		void Start ()
		{
        cam1 = GameObject.Find("RightEyeAnchor").camera;
        cam2 = GameObject.Find("LeftEyeAnchor").camera;
		}
	
		// Update is called once per frame
        /// <summary>
        /// Updates this instance.
        /// </summary>
		void Update ()
		{
	
		}

        /// <summary>
        /// Switches the easy.
        /// </summary>
		public void switchEasy ()
		{
		RenderSettings.ambientLight = ambientLightColorEasy;
		RenderSettings.fogColor = fogColorEasy;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorEasy;
		cam2.backgroundColor = fogColorEasy;

		
		}

        /// <summary>
        /// Switches the hard.
        /// </summary>
		public void switchHard ()
		{
		RenderSettings.ambientLight = ambientLightColorHard;
		RenderSettings.fogColor = fogColorHard;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorHard;
		cam2.backgroundColor = fogColorHard;

	}

        /// <summary>
        /// Switches the normal.
        /// </summary>
		public void switchNormal ()
		{
		RenderSettings.ambientLight = ambientLightColorNormal;
		RenderSettings.fogColor = fogColorNormal;
		RenderSettings.fogDensity = 0.02f;
		RenderSettings.fog = true;
		RenderSettings.fogMode = FogMode.ExponentialSquared;
		cam1.backgroundColor = fogColorNormal;
		cam2.backgroundColor = fogColorNormal;

	}
	
}