using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    Vector2 joystickInput;

    public float turnSpeed = 4.0f;
    public Transform player;

    public float height = 1f;
    public float distance = 2f;

    private Vector3 offsetX;
    private Vector3 startPos;
    //private Vector3 offsetY;

    
    public float smoothSpeed = 0.125f; // Speed of camera movement


    void Start()
    {
        startPos = transform.position;
        offsetX = new Vector3(0, height, -distance);
        //offsetY = new Vector3(0, 0, distance);
    }

    void LateUpdate()
    {
        //if (Mathf.Abs(joystickInput.x) > 0.05)
        //{
        //    offsetX = Quaternion.AngleAxis(joystickInput.x * turnSpeed, Vector3.up) * offsetX;
        //    //offsetY = Quaternion.AngleAxis(joystickInput.y * turnSpeed, Vector3.right) * offsetY;
        //    transform.position = player.position + offsetX;
        //    transform.LookAt(player.position);
        //}
        //else
        //{
        //    offsetX = new Vector3(0, height, -distance);
        //    transform.position = player.position + offsetX;
        //    transform.LookAt(player.position);
        //}

        offsetX = Quaternion.AngleAxis(joystickInput.x * turnSpeed, Vector3.up) * offsetX;
        //offsetY = Quaternion.AngleAxis(joystickInput.y * turnSpeed, Vector3.right) * offsetY;
        transform.position = player.position + offsetX;
        transform.LookAt(player.position);
    }

    public void GetXvalue(InputAction.CallbackContext ctx)
    {
        joystickInput = ctx.ReadValue<Vector2>();
    }
}
