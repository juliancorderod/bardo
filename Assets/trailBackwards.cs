using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailBackwards : MonoBehaviour
{

    TrailRenderer tr;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[tr.positionCount];
        tr.GetPositions(positions);

        for (int i = 0; i < positions.Length; i++)
            positions[i].z += Time.deltaTime;

        //        tr.SetPositions(positions);
    }
}
