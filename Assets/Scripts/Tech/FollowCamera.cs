using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    Vector2 joystickInput;

    public float turnSpeed = 4.0f;
    public Transform player;

    public float height = 1f;
    public float distance = 2f;
    public float lookAtHeight = 2f;

    private Vector3 offsetX;
    private Vector3 offsetY;


    void Start()
    {
        offsetX = new Vector3(0, 0, -distance);

        RaycastHit hit;
        int layerMask = 1 << 6;
        if (Physics.Raycast(transform.position, -transform.up, out hit, layerMask))
        {
            float terrainHeight = hit.point.y; // Adjust this value based on your terrain height
            offsetY = new Vector3(0, height + terrainHeight, 0);
        }
    }

    void LateUpdate()
    {
        // Keep the camera above the terrain
        int layerMask = 1 << 6;

        RaycastHit hit;
        RaycastHit hitCam;
 
        if (Physics.Raycast(player.position, -transform.up, out hit, layerMask))
        {
            //Debug.Log(hit.point.y);
            //Physics.Raycast(transform.position, -transform.up, out hitCam, layerMask);

            //if (hitCam.distance < height)
            //{
            //    float terrainHeight = hitCam.point.y + 1f;
            //    offsetY = new Vector3(0, terrainHeight + height, 0);
            //}
            //else
            //{
            //    float terrainHeight = hit.point.y;
            //    offsetY = new Vector3(0, terrainHeight + height, 0);
            //}

            float terrainHeight = hit.point.y;
            offsetY = new Vector3(0, terrainHeight + height, 0);
        }


        offsetX = Quaternion.AngleAxis(joystickInput.x * turnSpeed * Time.deltaTime, Vector3.up) * offsetX;

        Vector3 newPos = new Vector3(player.position.x + offsetX.x, offsetY.y, player.position.z + offsetX.z);

        transform.position = newPos;
        transform.LookAt(player.position + new Vector3(0, lookAtHeight, 0));
    }

    public void GetXvalue(InputAction.CallbackContext ctx)
    {
        joystickInput = ctx.ReadValue<Vector2>();
    }
}
