using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 500;
    float verticalRotation;

    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    const float radiusDefault = 5;
    [SerializeField]
    float radius;

    public float maxUp;
    public float maxDown;

    float startX;
    float startY;
    float startZ;

    float spinOffset;
    InputAction secondaryAction;

    void Start()
    {

        secondaryAction = InputSystem.actions.FindAction("Secondary");

        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
        startZ = transform.localPosition.z;
    }


    // Update is called once per frame
    void Update()
    {

        if (secondaryAction.IsPressed())
        {
            //Get vertical
            verticalRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, maxUp, maxDown);
        }
        else
        {
            spinOffset = 0;
        }

        //Set vertical angle and full position
        transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        transform.localPosition = new Vector3(startX, startY + radius * Mathf.Sin(verticalRotation * Mathf.PI / 180), startZ - radius * Mathf.Cos(verticalRotation * Mathf.PI / 180));


        //Spin around
        transform.RotateAround(gameObject.transform.parent.transform.position,gameObject.transform.parent.transform.up,spinOffset);

        //Stay out of ground
        if (transform.position.y < 0.1f) {
            transform.Translate(new Vector3(0,-transform.position.y+0.1f,0),Space.World);
        }
    }

    public void SetRadius(float newRadius) {
        radius = newRadius;
    }


}