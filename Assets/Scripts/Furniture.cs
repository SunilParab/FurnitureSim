using UnityEngine;
using System.Collections.Generic;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    public List<FurniturePiece> pieces = new List<FurniturePiece>();

    public bool active;

    bool CheckPieces()
    {
        return true; //Check for necessary pieces (like a table that needs a monitor stand)
        //No longer used
    }

    bool CheckStable()
    {
        return GetComponent<Rigidbody>().linearVelocity.magnitude < 1;
    }

    public virtual bool CheckBuilt()
    {
        return CheckPieces() && CheckStable();
    }

    public void Activate()
    {
        active = true;
        gameObject.AddComponent<Rigidbody>();
    }

    public void Merge(Furniture other)
    {
        for (int i = 0; i < other.pieces.Count; i++)
        {
            AddPiece(other.pieces[i]);
        }
        Destroy(other.gameObject);
    }

    void AddPiece(FurniturePiece newPiece)
    {
        newPiece.transform.SetParent(transform, true);
        newPiece.tempParent = this;
        newPiece.moveable = false;
        Debug.Log(pieces.Count);
        pieces.Add(newPiece);
    }

}
