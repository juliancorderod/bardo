using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootStarMan : MonoBehaviour {

	public GameObject shootStar;

	float randomNum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		randomNum = Random.Range(0,100);

		if(randomNum > 98){

			Instantiate(shootStar,new Vector3(Random.Range(-300,300),200, Random.Range(-300,300)),Quaternion.identity);
		}

	}
}
