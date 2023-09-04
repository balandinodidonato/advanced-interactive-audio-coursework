/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 08.02.21
 *      Last modified: 02.04.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


/*
 * INTERACTIVE ITEM
 * 
 * manages the actions for iteractive item based 
 * on user inputs
 */
public class InteractiveItem : MonoBehaviour
{
    public event Action OnOver;

    public event Action OnOut;

    protected bool _isOver;

    [SerializeField] private GameObject hotspot;

    /*
     * OVER
     * 
     * called when the gaze is over the interactive
     * item
     */

    public void Over()
    {
        _isOver = true;

        if (OnOver != null)
        {
            OnOver();
        }
    }

    /*
     * OUT
     * called when gaze has left the interactive item
     * 
     */

    public void Out()
    {
        _isOver = false;

        if(OnOut != null)
        {
            OnOut();
        }
    }

}
