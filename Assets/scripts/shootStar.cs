using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootStar : MonoBehaviour {

	Vector3 initPos, randPos;
	public float secAlive;
	float timer = 0;

	// Use this for initialization
	void Start () {
		
		initPos = transform.position;
		randPos = new Vector3(Random.Range(-300,300),150,Random.Range(-300,300));
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

		transform.position = Vector3.Slerp(initPos,randPos,timer/secAlive);
		
	}
}
