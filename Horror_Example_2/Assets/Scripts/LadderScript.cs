using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public GameObject player;
    bool inside = false;
    bool isClimbing = false;
    public float speedUpDown = 3.2f;
    private PlayerMovementAdvanced playerMovement;
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovementAdvanced>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("Player entered ladder trigger.");
            inside = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            Debug.Log("Player exited ladder trigger.");
            playerMovement.enabled = true;
            inside = false;
            isClimbing = false; // Ensure climbing mode is off when leaving ladder
            playerRigidbody.useGravity = true; // Enable gravity after leaving ladder
        }
    }

    void Update()
    {
        if (inside)
        {
            if (!isClimbing && Input.GetKeyDown(KeyCode.E))
            {
                playerMovement.enabled = false;
                playerRigidbody.useGravity = false; // Disable gravity while on ladder

                isClimbing = true;
                Debug.Log("Climb ladder by pressing W/S, press E again to exit.");
            }
            else if (isClimbing && Input.GetKeyDown(KeyCode.E))
            {
                isClimbing = false;
                playerMovement.enabled = true;
                Debug.Log("Exited ladder climb mode.");
            }

            if (isClimbing && (Input.GetKey("w") || Input.GetKey("s")))
            {
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 climbDirection = Vector3.up * verticalInput;
                playerRigidbody.MovePosition(player.transform.position + climbDirection * (speedUpDown * Time.deltaTime));
            }
        }
    }
}
