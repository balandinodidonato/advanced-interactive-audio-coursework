/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 29.01.21
 *      Last modified: 08.02.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


/*
 *  GAZE RAYCASTER
 * Allows the users gaze to act as the input for 
 * interactive item activation
 */

public class GazeRaycaster : MonoBehaviour
{
    public event Action<RaycastHit> raycastHit;

    [SerializeField]
    private Transform theCamera;

    [SerializeField]
    private CursorGazeAttributes cursor;

    //this float determines the max distance the camera looks for a hotspot
    [SerializeField]
    private float rayLength = 200.0f;

    public InteractiveItem currentInteractiveItem;

    private InteractiveItem previousInteractiveItem;
   

    private void FixedUpdate()
    {
        EyeRaycasting();
    }

    private void EyeRaycasting()
    {

        Ray ray = new Ray(theCamera.position, theCamera.forward);

        RaycastHit aCollision;

        if (Physics.Raycast(ray, out aCollision, rayLength))
        {
            InteractiveItem tempInteractiveItem = aCollision.collider.GetComponent<InteractiveItem>();

            currentInteractiveItem = tempInteractiveItem;

            if ((tempInteractiveItem == true) && (tempInteractiveItem != previousInteractiveItem))
            {
                tempInteractiveItem.Over();
            }

            if (tempInteractiveItem != previousInteractiveItem)
            {
                DeactiveLastInteractiveItem();
            }

            previousInteractiveItem = tempInteractiveItem;

            cursor.SetPosition(aCollision);

            if (raycastHit != null)
            {
                raycastHit(aCollision);
            }

        }

        else
        {
            DeactiveLastInteractiveItem();
            currentInteractiveItem = null;
            cursor.SetPosition();
        }
    }

        private void DeactiveLastInteractiveItem()
        {
            if (previousInteractiveItem == null)
            {
                return;
            }

            previousInteractiveItem.Out();
            previousInteractiveItem = null;
        }

}
