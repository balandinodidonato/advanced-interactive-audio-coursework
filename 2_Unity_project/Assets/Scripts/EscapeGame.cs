/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 05.02.21
 *      Last modified: 05.02.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that allows a user to escape the app when on PC
 * DEVELOPMENT PUPOSES ONLY
 */

public class EscapeGame : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
