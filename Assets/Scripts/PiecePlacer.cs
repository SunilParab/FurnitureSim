using UnityEngine;
using UnityEngine.InputSystem;

public class PiecePlacer : MonoBehaviour
{

    public FurniturePiece curPiece;
    InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Attack");
    }

    public Vector3 FindPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit info;

        Physics.Raycast(ray, out info, 1000);

        return info.point;
    }

    void Update()
    {

        if (curPiece != null)
        {
            Vector3 newPos = FindPos();
            newPos += new Vector3(0, curPiece.halfHeight, 0);
            curPiece.transform.position = newPos;
        }

        if (interactAction.IsPressed()) {
            curPiece = null;
        }

    }

}
