using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    WorldManager worldMan;
    Transform player;


    // Use this for initialization
    void Start()
    {
        worldMan = GameObject.FindGameObjectWithTag("worldMan").GetComponent<WorldManager>();
        player = GameObject.Find("newPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) > worldMan.activationDist)
        {
            worldMan.deactivateObj.Add(gameObject);
            gameObject.SetActive(false);
        }


    }
}
