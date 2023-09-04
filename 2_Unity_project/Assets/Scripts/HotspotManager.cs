/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 09.02.21
 *      Last modified: 09.02.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * HOTSPOT MANAGER
 * 
 * managers the linking of hotspots in a scene
 * and the scenes that they are required to load
 * 
 */
public class HotspotManager : MonoBehaviour
{
    public Button[] hotspotsInScene;

    [SerializeField]
    private GameManager theManager;

    /*
     * START
     * 
     * method invokes the link hotspots to scenes method
     * 
     */

    void Start()
    {
        linkHotspotsToScenes();
    }

    /*
     * LINK HOTSPOTS TO SCENE METHOD
     * 
     */

    public void linkHotspotsToScenes()
    {
        if(hotspotsInScene.Length != theManager.scenesToLoad.Length)
        {
            Debug.Log("The number of hotspots does not match the number of scenes to load");

            return;
        }

        else
        {
            for(int i = 0; i < hotspotsInScene.Length; i++)
            {
                string sceneName = theManager.scenesToLoad[i];

                hotspotsInScene[i].onClick.AddListener(() => theManager.sceneSelection(sceneName));
            }
        }
    }


}
