using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    internal FlockController controller;
    public Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {
		if( controller)
        {
            Vector3 relativePos = steer() * Time.deltaTime;

            if( relativePos != Vector3.zero)
            {
                rigidBody.velocity = relativePos;
            }

            //enforce minimum and maximum speeds for the boids
            float speed = rigidBody.velocity.magnitude;
            if( speed > controller.maxVelocity)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * controller.maxVelocity;
            }
            else if( speed < controller.minVelocity)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * controller.minVelocity;
            }
        }
	}

    private Vector3 steer()
    {
        Vector3 center = controller.flockCenter - transform.localPosition;
        Vector3 velocity = controller.target.localPosition - transform.localPosition;
        Vector3 follow = controller.target.localPosition - transform.localPosition;
        Vector3 separation = Vector3.zero;

        foreach( Flock flock in controller.flockList)
        {
            if( flock != this)
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;

                separation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
        randomize.Normalize();

        return (controller.centerWeight * center +
            controller.velocityWeight * velocity +
            controller.separationWeight * separation +
            controller.followWeight * follow +
            controller.randomSizeWeight * randomize);
    }
}
