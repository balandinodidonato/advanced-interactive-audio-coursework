/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 29.01.21
 *      Last modified: 26.02.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CURSOR GAZE ATTRIBUTES
 * 
 * Manages the rotation and scale of the cursor
 */

public class CursorGazeAttributes : MonoBehaviour
{
    //cursor transformation
    [SerializeField]
    private Transform cursorTransform;

    //allows the camera to be selected in the Unity Editor
    [SerializeField]
    private Transform theCamera;

    private Vector3 originalScale;

    //used to set orginal rotation
    private Quaternion originalRotation;



    //Awake - obtains the orignal scale and rotation of the cursor

    private void Awake()
    {
        //the scale of the cursor is set - currently 
        //small so does not distract the viewer
        originalScale = new Vector3(0.1f, 0.1f, 0.1f);
        

        //original rotation is stored
        originalRotation = cursorTransform.localRotation;
    }

    /*  SET POSITION
     *  
     * sets the position of the cursor when an interactive HAS NOT 
     * been collided with
     */

    public void SetPosition()
    {
        //sets the cursor
        cursorTransform.position = theCamera.position + theCamera.forward;

        //uses the cursor scale previously set to adjust the cursor
        cursorTransform.localScale = originalScale;

        //set cursor rotation
        cursorTransform.localRotation = originalRotation;
    }

    /*
     * SET POSITION
     * sets the position of the cursor when an interactive HAS
     * been collided with
     */

    public void SetPosition(RaycastHit aCollision)
    {
        //sets cursor position based on collision point
        cursorTransform.position = aCollision.point;

        //sets scale of cursor based on collision point
        cursorTransform.localScale = originalScale * aCollision.distance;

        //set cursor rotation
        cursorTransform.localRotation = originalRotation;
    }
}
