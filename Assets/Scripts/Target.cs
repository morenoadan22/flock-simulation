using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    public Vector3 bound;
    public float speed = 100.0f;

    private Vector3 initialPosition;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;                
	}
	
	public void MoveToNewPosition()
    {
        float posX = Random.Range(initialPosition.x - bound.x, initialPosition.x + bound.x);
        float posY = Random.Range(initialPosition.y - bound.y, initialPosition.y + bound.y);
        float posZ = Random.Range(initialPosition.z - bound.z, initialPosition.z + bound.z);

        transform.position = new Vector3(posX, posY, posZ);
    }
}
