using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public enum FlockMode
    {
        LAZY,
        CIRCLE,
        FOLLOW
    }

    public static FlockMode currentFlockMode = 0;

    private void Awake()
    {
        if( !instance )
        {
            instance = this;
        }
        else if( instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitSimulation(currentFlockMode);
    }


    public void onClick(string buttonTag)
    {
        FlockMode flockFromTag = (FlockMode)System.Enum.Parse(typeof(FlockMode), buttonTag, true);
        InitSimulation(flockFromTag);
    }

    public void InitSimulation(FlockMode flockMode)
    {
        if (flockMode == currentFlockMode) return;

        currentFlockMode = flockMode;
        
        switch( flockMode)
        {
            case FlockMode.LAZY:
                Debug.Log("Lazy Flock Mode");                
                break;
            case FlockMode.FOLLOW:
                Debug.Log("Follow Flock Mode");
                break;
            case FlockMode.CIRCLE:
                Debug.Log("Circle Flock Mode");
                break;
        }

        
    }

    
}
