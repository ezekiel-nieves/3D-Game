using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // If you're using the UI Text component

public class Doors : MonoBehaviour
{
    // Objects
    public Animator Door;
    public GameObject OpenText;
    public AudioSource DoorSound;

    // Variables
    public bool inReach;
    private bool isOpen;
    private bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        isOpen = false;
        isAnimating = false;
        OpenText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UpdateOpenText();
            OpenText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            OpenText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E) && !isAnimating)
        {
            if (!isOpen)
            {
                StartCoroutine(AnimateDoor(true));
            }
            else
            {
                StartCoroutine(AnimateDoor(false));
            }
        }
    }

    private IEnumerator AnimateDoor(bool opening)
    {
        isAnimating = true;

        if (opening)
        {
            DoorOpens();
        }
        else
        {
            DoorCloses();
        }

        // Get the length of the current animation
        AnimatorStateInfo animationState = Door.GetCurrentAnimatorStateInfo(0);
        float animationLength = animationState.length;

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationLength);

        isAnimating = false;
    }

    private void DoorOpens()
    {
        Debug.Log("The door opened!");
        Door.SetBool("isOpen", true);
        DoorSound.Play();
        isOpen = true;
        UpdateOpenText();
    }

    private void DoorCloses()
    {
        Debug.Log("The door closed!");
        Door.SetBool("isOpen", false);
        DoorSound.Play();
        isOpen = false;
        UpdateOpenText();
    }

    private void UpdateOpenText()
    {
        if (isOpen)
        {
            OpenText.GetComponent<Text>().text = "Press E to Close";
        }
        else
        {
            OpenText.GetComponent<Text>().text = "Press E to Open";
        }
    }
}
