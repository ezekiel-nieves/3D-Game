using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Sensitivity")]

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    [Header("Bobbing")]

    public float crouchBobFrequency = 5f; // Lower frequency for crouching
    public float crouchBobHorizontalAmplitude = 0.03f; // Smaller amplitude for crouching
    public float crouchBobVerticalAmplitude = 0.03f; // Smaller amplitude for crouching

    public float bobFrequency = 10f;
    public float bobHorizontalAmplitude = 0.05f;
    public float bobVerticalAmplitude = 0.05f;

    public float runningBobFrequency = 20f; // Higher frequency for running
    public float runningBobHorizontalAmplitude = 0.1f; // Larger amplitude for running
    public float runningBobVerticalAmplitude = 0.1f; // Larger amplitude for running

    private float bobTimer = 0f;

    public bool enableHeadBobbing = true;
    private Vector3 startPos;

    // Reference to the player object
    public GameObject player;
    private PlayerMovementAdvanced playerMovement;



    // Start is called before the first frame update
    // Locks and makes cursor invisible
    void Start()
    {
        startPos = transform.localPosition;

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovementAdvanced>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // mouse input check!

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensX;


        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate 

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // head bops

        // Head bobbing
        if (enableHeadBobbing && playerMovement != null && playerMovement.state != PlayerMovementAdvanced.MovementState.air)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (playerMovement != null && (playerMovement.state == PlayerMovementAdvanced.MovementState.sprinting))
                {
                    // Use running head bobbing parameters
                    bobTimer += Time.deltaTime * runningBobFrequency;
                    float bobHorizontal = Mathf.Sin(bobTimer) * runningBobHorizontalAmplitude;
                    float bobVertical = Mathf.Cos(bobTimer * 2) * runningBobVerticalAmplitude;
                    transform.localPosition = startPos + new Vector3(bobHorizontal, bobVertical, 0);
                }
                else if (playerMovement.state == PlayerMovementAdvanced.MovementState.crouching)
                {
                    // Use crouching head bobbing parameters
                    bobTimer += Time.deltaTime * crouchBobFrequency;
                    float bobHorizontal = Mathf.Sin(bobTimer) * crouchBobHorizontalAmplitude;
                    float bobVertical = Mathf.Cos(bobTimer * 2) * crouchBobVerticalAmplitude;
                    transform.localPosition = startPos + new Vector3(bobHorizontal, bobVertical, 0);
                }
                else
                {
                    // Use walking head bobbing parameters
                    bobTimer += Time.deltaTime * bobFrequency;
                    float bobHorizontal = Mathf.Sin(bobTimer) * bobHorizontalAmplitude;
                    float bobVertical = Mathf.Cos(bobTimer * 2) * bobVerticalAmplitude;
                    transform.localPosition = startPos + new Vector3(bobHorizontal, bobVertical, 0);
                }
            }
            else
            {
                bobTimer = 0f;
                transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * bobFrequency);
            }
        }
        else
        {
            transform.localPosition = startPos;
        }
    }
}
