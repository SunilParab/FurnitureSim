using UnityEngine;
using System.Collections.Generic;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    List<FurniturePiece> pieces;

    bool CheckPieces()
    {
        return true; //Check for necessary pieces (like a table that needs a monitor stand)
    }

    bool CheckStable()
    {
        return true;
    }

    public virtual bool CheckBuilt()
    {
        return CheckPieces() && CheckStable();
    }

    public void Merge(Furniture other)
    {
        
    }

}
