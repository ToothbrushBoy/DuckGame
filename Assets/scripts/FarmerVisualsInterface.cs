using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerVisualsInterface : MonoBehaviour
{
    public enum Jacket
    {
        BASE, // not intended for use
        RIFLE,
        SHOTGUN
    }

    public Transform muzzle;
    public GunUpdate gun;
    private Jacket jacket;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public void setAimTarget(Transform t)
    {
        gun.SetAimTarget(t);
    }

    public Transform GetMuzzle()
    {
        return muzzle;
    }

    public void SetJacket(Jacket j)
    {
        jacket = j;
        skinnedMeshRenderer.material = skinnedMeshRenderer.materials[(int)j];
    }

    private void Start()
    {
        SetJacket(Jacket.RIFLE);
    }
}
