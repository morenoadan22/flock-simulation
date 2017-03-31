using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private UnityFlockController flockController;

    public Vector3 targetPosition;

    public Target targetPrefab;
    private Target targetObject;

    public enum FlockMode
    {
        LAZY,
        CIRCLE,
        FOLLOW
    }

    public static FlockMode currentFlockMode;

    private void Awake()
    {
        if( !instance )
        {
            instance = this;
        }
        else if( instance != this )
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        flockController = GetComponent<UnityFlockController>();
        targetObject = Instantiate(targetPrefab, transform.position, transform.rotation) as Target;

        InitSimulation(FlockMode.LAZY);
    }    

    public void onClick(string buttonTag)
    {
        targetObject.MoveToNewPosition();
        FlockMode flockFromTag = (FlockMode)System.Enum.Parse(typeof(FlockMode), buttonTag, true);
        InitSimulation(flockFromTag);
    }

    void InitSimulation(FlockMode flockMode)
    {
        if (flockMode == currentFlockMode) return;

        currentFlockMode = flockMode;
        
        switch( flockMode )
        {
            case FlockMode.LAZY:
                Debug.Log("Lazy Flock Mode");
                targetObject.MoveToNewPosition();             
                break;
            case FlockMode.FOLLOW:
                Debug.Log("Follow Flock Mode");
                break;
            case FlockMode.CIRCLE:
                Debug.Log("Circle Flock Mode");
                targetObject.transform.position = flockController.flockCenter;
                break;
        }       
    }

    private void Update()
    {
        FlockTowardsTarget();

        if ( currentFlockMode == FlockMode.LAZY )
        {            
            if (Vector3.Distance(targetObject.transform.position, flockController.flockCenter) <= 10.0f)
            {
                Debug.Log("Flock Reached Target");
                targetObject.MoveToNewPosition();
            }
        }
        else if( currentFlockMode == FlockMode.CIRCLE)
        {

        }
        else if( currentFlockMode == FlockMode.FOLLOW)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    targetObject.transform.position = hit.point;                    
                }
            }
        }
    }

    private void FlockTowardsTarget()
    {
        targetPosition = targetObject.transform.position;
        flockController.MoveTowardsTarget(targetObject.transform);
    }
}
