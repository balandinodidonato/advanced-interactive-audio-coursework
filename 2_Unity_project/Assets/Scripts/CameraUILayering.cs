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
using System;
using UnityEngine;

//This code organises the Camera UI

public class CameraUILayering : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas_;

    private void Awake()
    {
        canvas_.enabled = true;

        canvas_.sortingOrder = Int16.MaxValue;

        Canvas.ForceUpdateCanvases();
    }
}
