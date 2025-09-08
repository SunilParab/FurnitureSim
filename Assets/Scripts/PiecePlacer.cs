using UnityEngine;
using UnityEngine.InputSystem;

public class PiecePlacer : MonoBehaviour
{

    public FurniturePiece curPiece;
    InputAction interactAction;
    InputAction shiftAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Attack");
        shiftAction = InputSystem.actions.FindAction("Sprint");
    }

    public Vector3 FindPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit info;

        Physics.Raycast(ray, out info, 1000);

        return info.point;
    }

    public Vector3 FindPosHeightless()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit info;

        Physics.Raycast(ray, out info, 1000, LayerMask.GetMask("Ground"));

        return info.point;
    }

    void Update()
    {

        if (curPiece != null)
        {
            Vector3 newPos;

            if (shiftAction.IsPressed())
            {
                newPos = FindPosHeightless();
                curPiece.transform.position = new Vector3(newPos.x,curPiece.transform.position.y,newPos.z);;
            }
            else
            {
                newPos = FindPos();
                newPos += new Vector3(0, curPiece.size.y / 2, 0);
                curPiece.transform.position = newPos;
            }
        }

        if (interactAction.triggered) {
            if (curPiece != null)
            {
                curPiece.Place();
                curPiece = null;
            }
            else
            {
                curPiece = Pickup();
                if (curPiece != null)
                {
                    curPiece.Pickup();
                }
            }
        }

    }

    public FurniturePiece Pickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit info;

        Physics.Raycast(ray, out info, 1000);

        if (info.collider.gameObject.CompareTag("Piece"))
        {
            FurniturePiece reference = info.collider.GetComponent<FurniturePiece>();

            if (reference.moveable) {
                return reference;
            } else {
                return null;
            }
        }
        else
        {
            return null;
        }

        

    }

}
