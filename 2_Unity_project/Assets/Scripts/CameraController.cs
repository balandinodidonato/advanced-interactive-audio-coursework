/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 29.01.21
 *      Last modified: 29.01.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitive = 5.0f;

    //these max and min values are only required when using Mouse control
    public float minYRotate = -180.0f;
    public float maxYRotate = 180.0f;

    //rotation variables
    private float rotateX = 0.0f;
    private float rotateY = 0.0f;

    //camera
    private Camera theCamera;


    /*  AWAKE METHOD
     * Called when screen is awoken.
     * Obtains camera component, locks the cursor to the centre of the
     * screen and hides the cursor.
     * 
     * N.B. When testing in Unity, ESC key is pressed to show the 
     * cursor so the game can be paused or stopped.
     */

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        theCamera = Camera.main;
    }

    // Update called before the first frame loads
    void Update()
    {
        //determine x axis rotation from mouse input
        rotateX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitive;

        //determine y axis rotation from mouse input
        rotateY += Input.GetAxis("Mouse Y") * mouseSensitive;

        //clamp method used to determine Y axis rotation value
        rotateY = Mathf.Clamp(rotateY, minYRotate, maxYRotate);

        //moves the camera
        theCamera.transform.localEulerAngles = new Vector3(-rotateY, rotateX, 0);
    }
}
