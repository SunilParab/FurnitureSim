using UnityEngine;

public class TableTop : SpecialPiece
{
    public override bool CheckValid()
    {
        return Quaternion.Angle(transform.rotation, Quaternion.identity) < 10 && base.CheckValid();
    }
}
