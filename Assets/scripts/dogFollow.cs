using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogFollow : MonoBehaviour
{

    public Transform player;
    public float minDist, lerpVal;
    CharacterController control;

    // Use this for initialization
    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dogDist = Vector3.Distance(player.position, transform.position);

        if (dogDist > minDist || dogDist < 15)
        {

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, lerpVal),
                                             transform.position.y, Mathf.Lerp(transform.position.z, player.position.z, lerpVal));

        }

        control.Move(transform.up * -Time.deltaTime * 10f);
    }
}
