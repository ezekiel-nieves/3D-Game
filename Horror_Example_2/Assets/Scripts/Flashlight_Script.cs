using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight_Script : MonoBehaviour
{

    // Two objects that are set publically
    public AudioSource FlickSwitch;
    public GameObject Flashlight;

    private bool on;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        Debug.Log("Is the flashlight on? " + on);
        if (FlickSwitch != null)
        {
            FlickSwitch.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Method Call
            ToggleLight();
        }
    }

    // A method that simply disables or enables the light object if the user presses F
    private void ToggleLight()
    {
        if (!on)
        {
            Debug.Log("The flashlight is on!");
            on= true;
            Flashlight.SetActive(true);
            FlickSwitch.Play();
        }

        else if (on)
        {
            Debug.Log("The flashlight is off!");
            on = false;
            Flashlight.SetActive(false);
            FlickSwitch.Play();
        }
    }
}
