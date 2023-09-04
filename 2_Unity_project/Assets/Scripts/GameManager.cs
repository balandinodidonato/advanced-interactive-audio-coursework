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
using UnityEngine.SceneManagement;

/*
 * Game Manager
 * 
 * Manages the application.
 * Keeps track of the scenes and which ones need loaded
 * 
 */

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    Scene scene;

    [Header("Scene Management")]

    public string[] scenesToLoad;

    [Header("Current scene")]
    public string activeScene;

 

    /*
     * AWAKE
     * ensures that there will only be one instance of the game
     * manager
     */

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene();

        activeScene = scene.buildIndex + "-" + scene.name;

    }

    public void sceneSelection(string selectedScene)
    {
        SceneManager.LoadScene(selectedScene);

        activeScene = selectedScene;
    }
}
