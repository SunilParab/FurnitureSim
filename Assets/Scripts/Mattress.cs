using UnityEngine;

public class Mattress : SpecialPiece
{

    public BoxCollider sleepSpace;
    public Vector3 sleepSpaceSize;
    void Start()
    {
        sleepSpaceSize = sleepSpace.bounds.size / 2;
        sleepSpace.enabled = false;
    }

    public override bool CheckValid()
    {
        Collider[] hits = Physics.OverlapBox(sleepSpace.bounds.center, sleepSpaceSize, transform.rotation, LayerMask.GetMask("Furniture"));

        return hits.Length == 1 && base.CheckValid();
    }

}