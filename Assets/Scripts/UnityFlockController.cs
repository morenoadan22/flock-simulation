using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFlockController : MonoBehaviour {
    public Vector3 offset;
    public Vector3 bound;
    public float speed = 100.0f;
    public GameObject target;
    
    public Vector3 initialPosition;
    public Vector3 nextMovementPoint;
    private GameObject targetInstance;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        CalculateNextMovementPoint();        
        targetInstance = Instantiate(target, nextMovementPoint, Quaternion.identity) as GameObject;        
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 1.0f * Time.deltaTime);
        targetInstance.transform.position = nextMovementPoint;

        if (Vector3.Distance(nextMovementPoint, transform.position) <= 10.0f)
        {
            CalculateNextMovementPoint();
        }
        if ( GameManager.currentFlockMode == GameManager.FlockMode.LAZY)
        {          

        }
                
	}

    void CalculateNextMovementPoint()
    {
        float posX = Random.Range(initialPosition.x - bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

        nextMovementPoint = initialPosition + new Vector3(posX, posY, posZ);

        Debug.Log("New Target Position: " + nextMovementPoint);
    }
}
