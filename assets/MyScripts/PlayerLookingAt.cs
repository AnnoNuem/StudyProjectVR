// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : razial
// Created          : 12-29-2014
//
// Last Modified By : razial
// Last Modified On : 01-07-2015
// ***********************************************************************
// <copyright file="PlayerLookingAt.cs" company="INLUSIO">
//     Copyright (c) INLUSIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using UnityEngine;
using URandom;
using System.Collections;

/// <summary>
/// Class PlayerLookingAt.
/// </summary>
public class PlayerLookingAt : MonoBehaviour
{
    /// <summary>
    /// The camera transform
    /// </summary>
    Transform cameraTransform = null;
    /// <summary>
    /// The pos_blue
    /// </summary>
    Vector3 pos_blue;
    /// <summary>
    /// The pos_new
    /// </summary>
    Vector3 pos_new;
    /// <summary>
    /// The ray direction
    /// </summary>
    Vector3 rayDirection;
    /// <summary>
    /// The number of spheres to reach
    /// </summary>
    int numberOfSpheresToReach = 2;
    /// <summary>
    /// The number of spheres reached
    /// </summary>
    int numberOfSpheresReached = 0;

    // time a user has to reach the next blue sphere
    /// <summary>
    /// The time to get to blue sphere
    /// </summary>
    int timeToGetToBlueSphere = 8; // was 20, fixed it =(
    
    // for looking at check
    /// <summary>
    /// The center rect
    /// </summary>
    private Rect centerRect;

    // lets force playber to look 1 second at the blue light
    /// <summary>
    /// The timer
    /// </summary>
    private float timer = 0.0f;
    /// <summary>
    /// The displaytext
    /// </summary>
    GameObject displaytext;
    /// <summary>
    /// The spawn distance
    /// </summary>
    float spawnDistance = 40.0f; // How far away to spawn
    /// <summary>
    /// The move distance
    /// </summary>
    double moveDistance = 15.0;   // How close can the character get
    /// <summary>
    /// The hiding
    /// </summary>
    private bool hiding = false; // for inner logic
    /// <summary>
    /// The urand
    /// </summary>
    private UnityRandom urand;
    /// <summary>
    /// The how high object respawns
    /// </summary>
    int HowHighObjectRespawns = 4;  // so the object will respawn on the same hight

    /// <summary>
    /// The left
    /// </summary>
    bool left = false;
    /// <summary>
    /// The degree of spawn
    /// </summary>
    float DegreeOfSpawn = 0 ;
    /// <summary>
    /// The how often turned left
    /// </summary>
    int HowOftenTurnedLeft = 0;
    /// <summary>
    /// The how often turned right
    /// </summary>
    int HowOftenTurnedRight = 0;
    /// <summary>
    /// The counter for missed trials
    /// </summary>
    int	CounterForMissedTrials = 0;
    /// <summary>
    /// The ovr cam d
    /// </summary>
    private Transform OVRCamD;

    // Single-dimensional array
    /// <summary>
    /// The numbers
    /// </summary>
    public static int[] numbers = new int[9001];

    /// <summary>
    /// The randvalue
    /// </summary>
    float randvalue = 0;
    /// <summary>
    /// The time when respawned
    /// </summary>
private  string TimeWhenRespawned;
/// <summary>
/// The time when reached
/// </summary>
private  string TimeWhenReached;


/// <summary>
/// Awakes this instance.
/// </summary>
    void Awake ()
    {
        cameraTransform = GameObject.FindWithTag("OVRcam").transform;
        displaytext = GameObject.Find("Displaytext");
    }



    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start ()
    {

        renderer.enabled = false;
        urand = new UnityRandom((int)System.DateTime.Now.Ticks);
        float[] shufflebag = { 1, 2, 3, 4, 5, 6, };
        ShuffleBagCollection<float> thebag = urand.ShuffleBag(shufflebag);
        int myInt = 0;
        while (myInt < 9000)
        {
            myInt++;
            randvalue = thebag.Next();
            numbers [myInt] = (int)(randvalue);
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    void Update ()
    {
        float length = 10.0f;
        RaycastHit hit;
        rayDirection = cameraTransform.TransformDirection(Vector3.forward);
        Vector3 rayStart = cameraTransform.position + rayDirection;      // Start the ray away from the player to avoid hitting itself
        Debug.DrawRay(rayStart, rayDirection * length, Color.green);

        if (ManagerScript.getState() == ManagerScript.states.walking)
        {

            if (Physics.Raycast(rayStart, rayDirection, out hit, length) && hit.collider.tag == "blueball")
            {
                timer += Time.deltaTime;
                Debug.Log("option 1");
            } else
            {
                timer = 0.0f;
            }

            if (!hiding && Vector3.Distance(cameraTransform.position, transform.position) < moveDistance && timer > 0.5)
            {
                Debug.Log("option 3");
                HideAndMove();
            }

        } //else
        //{
        //    renderer.enabled = false;
        //}

    }

    /// <summary>
    /// News the trial.
    /// </summary>
    public void newTrial ()
    {

        OVRManager.display.RecenterPose();
        DegreeOfSpawn = 0;
        // r = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
        rayDirection = cameraTransform.TransformDirection(Vector3.forward);
        pos_blue.x = cameraTransform.position.x + spawnDistance * rayDirection.x;
        pos_blue.y = (float)HowHighObjectRespawns;
        pos_blue.z = cameraTransform.position.z + spawnDistance * rayDirection.z;
        transform.position = pos_blue;

        renderer.enabled = true;
        TimeWhenRespawned = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");

        hiding = false;
        timer = 0;

        Invoke("toLong", timeToGetToBlueSphere);


    }

    /// <summary>
    /// Hides the and move.
    /// </summary>
    void HideAndMove ()
    {

        if (numberOfSpheresReached != numberOfSpheresToReach)
        {

            numberOfSpheresReached++; // first time it gets 1

            // user reached blue sphere in time
            CancelInvoke("toLong");

            hiding = true;
            renderer.enabled = false;

            // if we reached the number of spheres point back

            if (numberOfSpheresReached == numberOfSpheresToReach)
            {
                point();

            }
            else { 

            // spanning random at 30 60 90 degrees left or right
            switch ((numbers [ManagerScript.realTrialNumber]))
            {

            // the jidder should be around 5 to 15 degree in total, so we dont have so many conditions
            // lets try it with 10 degree in total
                case 1:
                    left = false;
                    //DegreeOfSpawn = 90;
                    DegreeOfSpawn = urand.Range(85, 95, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
                case 2:
                    left = false;
                    //DegreeOfSpawn = 60 ;
                    DegreeOfSpawn = urand.Range(55, 65, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
                case 3:
                    left = false;
                    //DegreeOfSpawn = 30 ;
                    DegreeOfSpawn = urand.Range(25, 35, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
                case 4:
                    left = true;
                    DegreeOfSpawn = urand.Range(85, 95, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
                case 5:
                    left = true;
                    DegreeOfSpawn = urand.Range(55, 65, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
                case 6:
                    left = true;
                    DegreeOfSpawn = urand.Range(25, 35, UnityRandom.Normalization.STDNORMAL, 1.0f);
                    break;
            }

            // here depending on the conditon, we rotate the spehre and move it forward
            if (left)
            {
                ManagerScript.CurrentOrientation = 0;
  
                transform.Rotate(0, 360 - DegreeOfSpawn, 0, Space.Self);

                transform.localPosition += transform.forward * (float)spawnDistance;
                displaytext.GetComponent<TextMesh>().text = "<--";
                Invoke("clearGUItext", 0.5f);
                HowOftenTurnedLeft++;

            } else //right
            {
                ManagerScript.CurrentOrientation = 1;
                //BAAAAD	transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (float)(DegreeOfSpawn), transform.eulerAngles.z); // NOOOT WORKING 
                transform.Rotate(0, DegreeOfSpawn, 0, Space.Self);

                transform.localPosition += transform.forward * (float)spawnDistance;
                displaytext.GetComponent<TextMesh>().text = "-->";
                Invoke("clearGUItext", 0.5f);
                HowOftenTurnedRight++;
            }

            renderer.enabled = true;

            // call function if user takes to long to get to blue sphere
            Invoke("toLong", timeToGetToBlueSphere);

            hiding = false;
   
            ManagerScript.generatedAngle = DegreeOfSpawn;

            TimeWhenReached = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");

            ((testofsql)(GameObject.Find("OVRPlayerController").GetComponent("testofsql"))).CreateWaypoint("NULL", DegreeOfSpawn.ToString(), TimeWhenRespawned, transform.position.ToString(), transform.rotation.ToString(), numberOfSpheresReached.ToString(), testofsql.CURRENT_TRIAL_ID.ToString(), "1", TimeWhenReached);

            TimeWhenRespawned = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff;");
            }
        }
        else
        {
            hiding = true;
            renderer.enabled = false;

        }
    }

    /// <summary>
    /// To the long.
    /// </summary>
    void toLong ()
    {
        //Add parameters
        recordData.recordDataParameters(0, "999");
        displaytext.GetComponent<TextMesh>().text = "Time's up for this trial!\nNew Trial";
        Invoke("clearGUItext", 1f);
        // count how often we miss
        CounterForMissedTrials++;

        //('Waypoints_id','DegreeOfRespawn','TimeWhenRespawned','GlobalCoordinats','TransformRotation','NumberInTrial','Trial_id','reached')
        ((testofsql)(GameObject.Find("OVRPlayerController").GetComponent("testofsql"))).CreateWaypoint("NULL", "999", "0", transform.position.ToString(), transform.rotation.ToString(), numberOfSpheresReached.ToString(), testofsql.CURRENT_TRIAL_ID.ToString(), "0", "NULL");

        ManagerScript.abortTrial();

    }

    /// <summary>
    /// Points this instance.
    /// </summary>
    void point ()
    {
        CancelInvoke("toLong");
        ManagerScript.switchState(ManagerScript.states.pointing);
    }

    /// <summary>
    /// Clears the gu itext.
    /// </summary>
    void clearGUItext ()
    {
        displaytext.GetComponent<TextMesh>().text = "";
    }

}

