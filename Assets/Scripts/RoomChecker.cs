using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RoomChecker : MonoBehaviour
{
    [SerializeField]
    List<Furniture> furnitureList;

    [SerializeField]
    List<SpecialPiece> specialPieces;

    public GameObject winner;
    public GameObject loser;

    public GameObject checking;

    public Vector3 size;
    InputAction acceptAction;

    bool activated = false;

    void Start()
    {
        size = transform.localScale;
        size.y = 80;
        acceptAction = InputSystem.actions.FindAction("Test");
    }

    void Update()
    {
        if (acceptAction.triggered && !activated)
        {
            activated = true;
            CheckRoom();
        }
    }

    public void CheckRoom()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + new Vector3(0, size.y / 2, 0), size / 2, transform.rotation, LayerMask.GetMask("Furniture"));

        for (int i = 0; i < hits.Length; i++)
        {
            FurniturePiece curPiece = hits[i].GetComponent<FurniturePiece>();
            if (curPiece.tempParent == null)
            {
                curPiece.AttachSelf();
            }

            if (!furnitureList.Contains(curPiece.tempParent))
            {
                curPiece.tempParent.Activate();
                furnitureList.Add(curPiece.tempParent);
            }

        }

        checking.SetActive(true);
        Invoke(nameof(CheckWin),5);
    }

    bool CheckWin()
    {
        checking.SetActive(false);
        bool output = true;

        for (int i = 0; i < furnitureList.Count; i++)
        {
            output = output && furnitureList[i].CheckBuilt();

        }

        for (int i = 0; i < specialPieces.Count; i++)
        {
            output = output && specialPieces[i].CheckValid();

        }

        if (output)
        {
            winner.SetActive(true);
        }
        else
        {
            loser.SetActive(true);
        }

        return output;
    }

}
