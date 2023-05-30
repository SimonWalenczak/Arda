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
    private Vector3 offsetY;


    void Start()
    {
        offsetX = new Vector3(0, 0, -distance);
        offsetY = new Vector3(0, height, 0);
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

        // Keep the camera above the terrain
        int layerMask = 1 << 6;

        RaycastHit hit;
        //Debug.DrawRay(player.position, -transform.up, Color.yellow, 1);

        if (Physics.Raycast(transform.position, -transform.up, out hit, layerMask))
        {
            float terrainHeight = hit.point.y; // Adjust this value based on your terrain height
            offsetY = new Vector3(0, height + terrainHeight, 0);

            //if (transform.position.y < terrainHeight)
            //{
            //    transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
            //}
        }

        offsetX = Quaternion.AngleAxis(joystickInput.x * turnSpeed, Vector3.up) * offsetX;

        Vector3 newPos = new Vector3(player.position.x + offsetX.x, offsetY.y, player.position.z + offsetX.z);


        transform.position = newPos;
        transform.LookAt(player.position);




    }

    public void GetXvalue(InputAction.CallbackContext ctx)
    {
        joystickInput = ctx.ReadValue<Vector2>();
    }
}
