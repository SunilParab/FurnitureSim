using UnityEngine;
using System.Collections.Generic;

public class FurniturePiece : MonoBehaviour
{

    public Vector3 size;

    List<Furniture> parentPieces = new List<Furniture>();

    GameObject tempParent;

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
        Collider[] hits = Physics.OverlapBox(transform.position, size);
        for (int i = 0; i < hits.Length; i++) {
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
        Debug.Log("Place");
    }

    public void Pickup()
    {
        Debug.Log("Pickup");
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
        if (other.tempParent != null)
        {
            if (tempParent == null)
            {
                transform.SetParent(other.tempParent.transform, true);
                tempParent = other.tempParent;
                moveable = false;
            }
            else
            {

            }
        }
        else if (tempParent != null)
        {
            if (other.tempParent == null)
            {
                other.transform.SetParent(tempParent.transform, true);
                other.tempParent = tempParent;
                other.moveable = false;
            }
            else
            {

            }
        }
        else
        {
            GameObject parent = new GameObject("TestParent");
            parent.AddComponent<Furniture>();


            transform.SetParent(parent.transform, true);
            other.transform.SetParent(parent.transform, true);

            tempParent = parent;
            other.tempParent = parent;

            moveable = false;
            other.moveable = false;
        }
    }

}
