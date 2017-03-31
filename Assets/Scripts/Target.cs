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

    public void MoveToPoint(Vector3 position)
    {
        float xMin = initialPosition.x - bound.x;
        float xMax = initialPosition.x + bound.x;
        position.x = Mathf.Clamp(position.x, xMin, xMax);

        float yMin = initialPosition.y - bound.y;
        float yMax = initialPosition.y + bound.y;
        position.y = Mathf.Clamp(position.y, yMin, yMax);

        float zMin = initialPosition.z - bound.z;
        float zMax = initialPosition.z + bound.z;
        position.z = Mathf.Clamp(position.z, yMin, zMax);

        transform.position = position;
    }
}
