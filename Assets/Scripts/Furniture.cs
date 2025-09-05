using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    List<FurniturePiece> pieces;


    InputAction acceptAction;
    void Start()
    {
        acceptAction = InputSystem.actions.FindAction("Test");
    }

    void Update()
    {
        if (acceptAction.triggered)
        {
            Debug.Log("yaya");
            CheckBuilt();
        }
    }

    bool CheckPieces()
    {
        return true; //Check for necessary pieces (like a table that needs a monitor stand)
    }

    bool CheckStable()
    {
        gameObject.AddComponent<Rigidbody>();

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
