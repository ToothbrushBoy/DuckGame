using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpdate : MonoBehaviour
{
    public Transform posTarget;
    public Transform aimTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = posTarget.position;
        if (aimTarget != null)
        {
            transform.LookAt(aimTarget.position);
        }
    }

    public void SetAimTarget(Transform t)
    {
        aimTarget = t;
    }
}
