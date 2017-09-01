﻿using System.Collections;
using UnityEngine;

public class xRoutineTest : MonoBehaviour {

	void Start () {
        //create a routine, and chain actions on it
        xRoutine routine = xRoutine.Create()
            .Append(RoutineMethod1())
            .WaitForSeconds(0.25f)
            .Append(RoutineMethod2)
            .WaitForSeconds(0.25f);
        //You can decide to add stuff later.
        routine.WaitForSecondsRealtime(0.25f)
            //You can also add multiple methods to execute. They will still run one after the other
            .Append(RoutineMethod1(), RoutineMethod1())
            .Append(RoutineMethod2, RoutineMethod2)
            .WaitForFixedUpdate()
            //You can also add yield instructions like this
            .Append(new WaitForSecondsRealtime(0.2f))
            .Append(new WaitForSeconds(0.3f))
            .Append(() => { Debug.Log("Execute code on the fly!"); })
            .Append(() => { Debug.Log("Press Enter to continue..."); })
            .WaitForKeyDown(KeyCode.Return)
            .Append(() => { Debug.Log("Thanks!"); })
            .Append(() => { Debug.Log("Left click to continue..."); })
            .WaitForMouseDown(0)
            .Append(() => { Debug.Log("Thanks again!"); })
            .Append(() => { Debug.Log(string.Format("xRoutine is still Running: {0}", routine.IsRunning)); })
            .Append(() => { Debug.Log("We are going to stop now."); })
            //I will stop it here
            //I can call Stop() from anywhere else for immediate interruption.
            .Append(() => { routine.Stop(); })
            .Append(() => { Debug.Log("This will not be executed"); });

        //We can even start another routine, and wait for the previous one to finish.
        xRoutine.Create()
            .WaitForXRoutine(routine)
            .Append(() => { Debug.Log("Routine just finished executing."); })
            .Append(() => { Debug.Log(string.Format("xRoutine is still Running: {0}", routine.IsRunning)); });

        //Note: if you call WaitForXRoutine on itself, it will running a task of checking if that task is done. Which will never be.
        //In other words, calling WaitForXRoutine on itself blocks the routine, until Stop() is called somewhere else.
    }

    //This is an enumerator method, that we can use with coroutines as usual
    IEnumerator RoutineMethod1()
    {
        Debug.Log("Routine Method 1");
        yield return new WaitForSeconds(0.1f);
        yield return null;
    }
    //this is just a method returning void, that we can also use with coroutines
    void RoutineMethod2()
    {
        Debug.Log("Routine Method 2");
    }
}