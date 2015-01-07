using UnityEngine;

    public class DebugPlayer : MonoBehaviour
    {

        Transform StartPointCoordinates;
        private UnityRandom urand;
        float responceTime;
        float reactAfter;
        float temp1;
        bool counter = false;
        Transform BlueBallPosition;
        bool defetablemassage = true;
        // Use this for initialization

        // real rinnning or just quick jumst to end
        public	bool RealPlayerOrNot = false;


        void Start ()
        {

            StartPointCoordinates = GameObject.Find("StartPoint").transform;
            urand = new UnityRandom((int)System.DateTime.Now.Ticks);
      
        }

        void Update ()
        {



            if (ManagerScript.getState() != ManagerScript.states.startScreen && ManagerScript.getState() != ManagerScript.states.end)
            {


                // We need to unpause the game
                if (ManagerScript.getState() == ManagerScript.states.pause || ManagerScript.getState() == ManagerScript.states.blockover)
                {
                    Invoke("Unpause", 0.5f);

                }
                else
                {


                    // move player forward
                    if (ManagerScript.getState() != ManagerScript.states.pointing && ManagerScript.getState() == ManagerScript.states.walking)
                    {

                        BlueBallPosition = GameObject.Find("BlueBallGLow").transform;

                        temp1 = SpawnLookRed.GetSpeedMoveScale();

                        transform.position = Vector3.MoveTowards(transform.position, BlueBallPosition.position, (float)(temp1 * Time.deltaTime * 1f));
                        transform.LookAt(BlueBallPosition); // lets allways face the blue ball 


                    }

                    if (((SpawnLookRed)(GameObject.Find("RedBallGlow").GetComponent("SpawnLookRed"))).GetYellowState() == SpawnLookRed.yellowSphereStates.defeatable && defetablemassage)
                    {
                        defetablemassage = false;
                        GenrataTimeForDebugPlayerResponceDeley();

                    }

                    if (((SpawnLookRed)(GameObject.Find("RedBallGlow").GetComponent("SpawnLookRed"))).GetYellowState() != SpawnLookRed.yellowSphereStates.defeatable)
                    {
                        defetablemassage = true;
                    }


                    // if a sphere is in the defeatable mode, generate a random time with a function (due to some fucked up shit, the yellow spehre will do it ...)
                    if (counter && Time.time > reactAfter)
                    {

                        counter = false;

                        ((SpawnLookRed)(GameObject.Find("RedBallGlow").GetComponent("SpawnLookRed"))).FakePress = true;
                        Invoke("unpush", 0.3f);
                        // this will "push " the button
                    }

                    // lets point 
                    if (ManagerScript.getState() == ManagerScript.states.pointing)
                    {



                        transform.LookAt(StartPointCoordinates);

                        int temp2 = UnityEngine.Random.Range(1, 2);

                        switch (temp2)
                        {


                            // the jidder should be around 5 to 15 degree in total, so we dont have so many conditions
                            // lets try it with 10 degree in total
                            case 1:

                                transform.Rotate(0, 90, 0, Space.Self);
                                break;
                            case 2:

                                transform.Rotate(0, 270, 0, Space.Self);


                                break;
                        }

                        ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).PointFakeButton = true;

                        Invoke("unpush", 0.2f);
                    }


                }



            }
        }

        private static void Unpause ()
        {
            ((Pause)(GameObject.Find("OVRCameraController").GetComponent("Pause"))).FakePauseButton = true;
        }

        void unpush ()
        {

            ((PointingScript)(GameObject.Find("helperObject").GetComponent("PointingScript"))).PointFakeButton = false;
            ((SpawnLookRed)(GameObject.Find("RedBallGlow").GetComponent("SpawnLookRed"))).FakePress = false;



        }

        public void GenrataTimeForDebugPlayerResponceDeley ()
        {

            //generate here the time
            responceTime = (float)urand.Range(10, 60, UnityRandom.Normalization.STDNORMAL, 10.0f);

            responceTime = responceTime / 100;
            counter = true;
            reactAfter = Time.time + responceTime;
            Debug.Log(responceTime);

        }



    }


