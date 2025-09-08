using UnityEngine;

public class SpecialPiece : MonoBehaviour
{
    public FurniturePiece pieceRef;

    public virtual bool CheckValid()
    {
        return pieceRef.CheckBuilt();
    }
}
