using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    WorldManager worldMan;
    Transform player;


    float startTimer;

    NewPlayer p;


    // Use this for initialization
    void Start()
    {
        worldMan = GameObject.FindGameObjectWithTag("worldMan").GetComponent<WorldManager>();
        player = GameObject.Find("newPlayer").transform;

        p = player.GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!p.dontDeactivate)
        {
            if (startTimer < 2)
                startTimer += Time.deltaTime;
            else
            {
                if (Vector3.Distance(player.transform.position, transform.position) > worldMan.activationDist)
                {
                    worldMan.deactivateObj.Add(gameObject);
                    gameObject.SetActive(false);
                    //            Debug.Log("deactivated");
                }
            }
        }


    }
}
