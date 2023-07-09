using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SET "duck" TO THE DUCK'S TRANSFORM
// USE "SetJacket(FarmerVisualsInterface.Jacket.-----) WHERE ----- IS THE TYPE OF FARMER. (currently only RIFLE and SHOTGUN)" 

public class FarmerVisualsInterface : MonoBehaviour
{
    public enum Jacket
    {
        BASE, // not intended for use
        RIFLE,
        SHOTGUN,
        SMG
    }

    public Transform duck;

    public Transform muzzle;
    public GunUpdate gun;
    [SerializeField] public Jacket jacket;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Transform aimTarget;
    private float diff, timer = 0f;

    public void setAimTarget(Transform t)
    {
        gun.SetAimTarget(t);
        aimTarget = t;
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
        SetJacket(jacket);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            timer = 0f;
            if (duck != null) aimTarget.position = new Vector3(duck.position.x, duck.position.y, duck.position.z);
        }
        diff = Mathf.Min(Mathf.Max(aimTarget.position.x - transform.position.x, -10f), 10f);
        transform.rotation = Quaternion.Euler(0f, diff * 7, 0f);
    }
}
