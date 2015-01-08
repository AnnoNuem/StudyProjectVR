using UnityEngine;
using System.Collections;

public class PauseCoroutine : MonoBehaviour
{

    static public PauseCoroutine instance; //the instance of our class that will do the work
    
    void Awake ()
    { //called when an instance awakes in the game
        instance = this; //set our static reference to our newly initialized instance
    }
    
    IEnumerator PerformCoroutine ()
    { //the coroutine that runs on our monobehaviour instance
        while (true)
        {
            yield return new WaitForSeconds(2f);
        }
    }
    
    static public void DoCoroutine ()
    {
        instance.StartCoroutine("PerformCoroutine"); //this will launch the coroutine on our instance
    }
}