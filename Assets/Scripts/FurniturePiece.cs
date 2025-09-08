using UnityEngine;
using System.Collections.Generic;

public class FurniturePiece : MonoBehaviour
{

    public Vector3 size;

    List<Furniture> parentPieces = new List<Furniture>();

    public Furniture tempParent;

    public bool attachable = true;
    public bool moveable = true;
    Collider myCollider;


    void Start()
    {
        myCollider = GetComponent<Collider>();
        size = transform.localScale;
    }

    void AttachPieces()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, size / 2);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject == gameObject) { continue; }
            if (hits[i].gameObject.CompareTag("Piece"))
            {
                Attach(hits[i].GetComponent<FurniturePiece>());
            }
        }
    }

    public void Place()
    {
        myCollider.enabled = true;
        AttachPieces();
    }

    public void Pickup()
    {
        myCollider.enabled = false;
    }

    /*void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.gameObject);
        if (other.collider.gameObject.CompareTag("Piece"))
        {
            Attach(other.collider.GetComponent<FurniturePiece>());
        }
    }*/

    void Attach(FurniturePiece other)
    {
        if (tempParent != null && tempParent == other.tempParent) {
            return;
        }

        if (other.tempParent != null)
        {
            if (tempParent == null)
            {
                transform.SetParent(other.tempParent.transform, true);
                tempParent = other.tempParent;
                moveable = false;
                other.tempParent.pieces.Add(this);
            }
            else
            {
                tempParent.Merge(other.tempParent);
            }
        }
        else if (tempParent != null)
        {
            if (other.tempParent == null)
            {
                other.transform.SetParent(tempParent.transform, true);
                other.tempParent = tempParent;
                other.moveable = false;
                tempParent.pieces.Add(other);
            }
            else
            {
                tempParent.Merge(other.tempParent);
            }
        }
        else
        {
            GameObject parent = new GameObject("TestParent");
            Furniture parentFurniture = parent.AddComponent<Furniture>();
            //parent.layer = LayerMask.NameToLayer("Furniture");
            parentFurniture.pieces.Add(this);
            parentFurniture.pieces.Add(other);

            transform.SetParent(parent.transform, true);
            other.transform.SetParent(parent.transform, true);

            tempParent = parentFurniture;
            other.tempParent = parentFurniture;

            moveable = false;
            other.moveable = false;
        }
    }

    public void AttachSelf()
    {
        GameObject parent = new GameObject("TestParent");
        Furniture parentFurniture = parent.AddComponent<Furniture>();
        //parent.layer = LayerMask.NameToLayer("Furniture");
        parentFurniture.pieces.Add(this);

        transform.SetParent(parent.transform, true);

        tempParent = parentFurniture;

        moveable = false;
    }

    public bool CheckBuilt()
    {
        if (tempParent == null)
        {
            return false;
        }
        else
        {
            return tempParent.active;
        }
    }

}
