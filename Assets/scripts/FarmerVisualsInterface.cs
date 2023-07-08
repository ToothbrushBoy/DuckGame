using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerVisualsInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform muzzle;
    public GunUpdate gun;

    public void setAimTarget(Transform t)
    {
        gun.SetAimTarget(t);
    }

    public Transform GetMuzzle()
    {
        return muzzle;
    }
}
