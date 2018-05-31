using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFlyScript : MonoBehaviour {

	float resetTimer;
	bool resetNow = false;

	public float timeToResetAvg;

	// Use this for initialization
	void Start () {

		transform.rotation = Random.rotation;

	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.forward * Time.deltaTime * 0.4f);

		resetTimer += Time.deltaTime;

		if (resetTimer >= Random.Range(timeToResetAvg - 5f,timeToResetAvg + 5f)){
			resetNow = true;
			resetTimer = 0f;
		}

		if(resetNow){
			transform.rotation = Random.rotation;
			resetNow = false;
		}
	}
}
