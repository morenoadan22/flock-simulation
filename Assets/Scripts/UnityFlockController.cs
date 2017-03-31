using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFlockController : MonoBehaviour {
    public Vector3 offset;
    public Vector3 bound;
    public float speed = 100.0f;
    public int flockSize = 5;
    
    private Vector3 initialPosition;        
    public UnityFlock prefab;
    
    public Vector3 flockCenter;
    internal Vector3 flockVelocity;
    private Vector3 nextMovementPoint;
            
    public ArrayList flockList = new ArrayList();    

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;               
               
        for (int i = 0; i < flockSize; i++)
        {
            UnityFlock flock = Instantiate(prefab, transform.position, transform.rotation) as UnityFlock;
            flock.transform.parent = transform;
            flock.controller = this;
            flockList.Add(flock);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach (UnityFlock flock in flockList)
        {
            center += flock.transform.localPosition;
            velocity += flock.rigidBody.velocity;
        }

        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;

        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), 1.0f * Time.deltaTime);
        
        if( Vector3.Distance(nextMovementPoint, transform.position) <= 10.0f)
        {
            //CalculateNextMovementPoint();
        }                     
	}

    void CalculateNextMovementPoint()
    {
        float posX = Random.Range(initialPosition.x - bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

        nextMovementPoint = initialPosition + new Vector3(posX, posY, posZ);        
    }


    public void MoveTowardsTarget(Transform targetTransform)
    {
        nextMovementPoint = targetTransform.position;
    }
}
