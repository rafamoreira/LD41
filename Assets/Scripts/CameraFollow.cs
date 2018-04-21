using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
	// Use this for initialization
	void Start ()
    {
        if (target == null)
        {
            Debug.Log("Camera needs a target");
        }
	}

	// Update is called once per frame
	void LateUpdate ()
    {
        MoveToTarget();
	}

    void MoveToTarget()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }
}
