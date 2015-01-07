// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : razial
// Created          : 12-29-2014
//
// Last Modified By : razial
// Last Modified On : 12-29-2014
// ***********************************************************************
// <copyright file="FollowPlayer.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// Class FollowPlayer. Intended for the Stars
/// 
/// Without this, one could just attach the stars to the player. But than they rotate with him, which is unwanted. 
/// 
/// </summary>
public class FollowPlayer : MonoBehaviour {

    /// <summary>
    /// The character
    /// </summary>
	private Transform character;
	// Use this for initialization
    /// <summary>
    /// Starts this instance.
    /// </summary>
	void Start () {
		character = GameObject.Find ("OVRPlayerController").transform;
	}
	
	// Update is called once per frame
    /// <summary>
    /// Updates this instance.
    /// </summary>
	void Update () {
		this.transform.position = character.position;	
	}
}
