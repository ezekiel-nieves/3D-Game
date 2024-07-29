using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public GameObject player;
    public float speedUpDown = 3.2f;
    private PlayerMovementAdvanced playerMovement;
    private Rigidbody playerRigidbody;
    public AudioSource sound;

    private bool isClimbing = false;
    private bool nearLadder = false;

    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovementAdvanced>();
        playerRigidbody = player.GetComponent<Rigidbody>();

        if (sound != null)
        {
            sound.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ladder") && gameObject.CompareTag("Reach"))
        {
            Debug.Log("Cursor entered ladder trigger.");
            nearLadder = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ladder") && gameObject.CompareTag("Reach"))
        {
            Debug.Log("Cursor exited ladder trigger.");
            nearLadder = false;
            if (isClimbing)
            {
                StopClimbing();
            }
        }
    }

    void Update()
    {
        if (nearLadder && Input.GetKeyDown(KeyCode.E))
        {
            if (!isClimbing)
            {
                StartClimbing();
            }
            else
            {
                StopClimbing();
            }
        }

        if (isClimbing)
        {
            HandleClimbing();
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        playerMovement.enabled = false;
        playerRigidbody.useGravity = false; // Disable gravity while on ladder

        Debug.Log("Climb ladder by pressing W/S, press E again to exit.");
    }

    private void StopClimbing()
    {
        isClimbing = false;
        playerMovement.enabled = true;
        playerRigidbody.useGravity = true; // Enable gravity after leaving ladder

        if (sound != null && sound.isPlaying)
        {
            sound.Stop();
        }

        Debug.Log("Exited ladder climb mode.");
    }

    private void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 climbDirection = Vector3.up * verticalInput;
            playerRigidbody.MovePosition(player.transform.position + climbDirection * (speedUpDown * Time.deltaTime));

            if (sound != null && !sound.isPlaying)
            {
                sound.loop = true;
                sound.Play();
            }
        }
        else
        {
            if (sound != null && sound.isPlaying)
            {
                sound.Stop();
            }
        }
    }
}
