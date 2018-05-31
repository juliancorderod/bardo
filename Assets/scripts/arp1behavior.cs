using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arp1behavior : MonoBehaviour {

    public Transform playerT;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (Vector3.Distance(transform.position, playerT.position) < 10f)
        //    transform.position = Vector3.MoveTowards(transform.position, playerT.position, 1f);
        //else
            transform.RotateAround(playerT.position, Vector3.up * 0.1f, 1f);

		
	}
}
