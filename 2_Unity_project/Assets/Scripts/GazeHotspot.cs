/*      
 *      JUPITER ARTLAND VR   
 *      
 *      Author: Rosie Jack
 *      Date: 02.06.21
 *      Last modified: 24.08.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *  Require Component ensures that external scripts required
 *  to make the code function are always present. - Makes sure 
 *  that the program will never be disfunctional due to 
 *  missing scripts.
 */
[RequireComponent(typeof(InteractiveItem))]
[RequireComponent(typeof(BoxCollider))]

/*
 *      GAZE HOTSPOT
 *          1. Determines how the hot spot reacts to a users gaze.
 *          2. Determines whether or not the cursor is visible.
 *          3. Calls a delay before a timer selection begins.
 *          4. Makes an icon bigger when selected & revert back to original 
 *                  size if unselected again.
 * 
 */

public class GazeHotspot : MonoBehaviour
{
    private Button hotspot;

    private bool _isOver = false;

    private InteractiveItem interactiveItem;

    public bool useFillImage = true;
    // addded to try and hide the cursor except when over a hotspot
    [SerializeField] private Image cursorImage;

    //sets the fill image for the cursor
    [SerializeField] private Image cursorFillImage;

    [SerializeField] private bool hideOnStart = true;

    public float selectionTime = 3.0f;

    private Coroutine selectionFill;

    private bool cursorFilled;

    private bool activeCursorSelection;
    //makes a list of game objects that can be adjusted in the inspector.
    //public GameObject[] adjacentScenes;

    /*
     *          AWAKE
     *      Called when the program starts, fetches
     *      the external components needed to make the 
     *      script function.
     * 
     */
    private void Awake()
    {
        hotspot = GetComponent<Button>();

        interactiveItem = GetComponent<InteractiveItem>();

    }

    /*
     *          START   
     *      initialises the cursor fill image amount to 0 
     *      and hides cursor if told to hide on start
     */
    private void Start()
    {
        cursorFillImage.fillAmount = 0.0f;

        if (hideOnStart == true)
        {
            Hide();
        }

    }

    /*
     *          HIDE  
     *      Makes the cursor invisible until it is over a hotspot
     * 
     */
    private void Hide()
    {
        cursorImage.gameObject.SetActive(false); //sets the cursor to be hidden when the program starts
        cursorFillImage.gameObject.SetActive(false);
        activeCursorSelection = false;
        cursorFillImage.fillAmount = 0.0f;
    }

    /*
     *          SHOW 
     *      Makes the cursor appear when over a hotspot
     * 
     */

    public void Show()
    {
        cursorImage.gameObject.SetActive(true); //shows the cursor when over a hotspot
        cursorFillImage.gameObject.SetActive(true);
        activeCursorSelection = true;
    }


    /*
     *          ON ENABLE
     *       Connects Handle Over and Handle Out
     *       to the external interactive Item script
     *       to ensure functionality.
     *       
     */
    private void OnEnable()
    {
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
    }


    /*
     *          FILL SELECTION CURSOR
     *      
     *      method for filling the selector fill image
     * 
     */

    private IEnumerator FillSelectionCursor()
    {

        cursorFilled = false;
        Show();
        float timer = 0.0f;

        cursorFillImage.fillAmount = 0.0f;

        while (timer < selectionTime)
        {
            float fill = timer / selectionTime;
            cursorFillImage.fillAmount = fill;
            timer += Time.deltaTime;

            yield return null;
        }

        cursorFillImage.fillAmount = 1.0f;

        activeCursorSelection = false;

        cursorFilled = true;

        hotspot.onClick.Invoke();

        Hide();

    }

    /*
     *          HANDLE OVER 
     * 
     *      used when the users gazes over a hotspot
     */

    private void HandleOver()
    {
        _isOver = true;

        Debug.Log("Start Fill Coroutine");
        selectionFill = StartCoroutine(FillSelectionCursor());

        //makes the selected icon bigger - a visual representation of which 
        //icon has been selected
        IconScaleIncrease();
    }


    /*
     *          HANDLE OUT
     * 
     *      used when the users gaze is not on a hotspot
     */

    private void HandleOut()
    {
        _isOver = false;

        if (selectionFill != null)
        {
            Debug.Log("Stop Fill Coroutine");
            StopCoroutine(selectionFill);
        }

        //makes the selected icon revert back to the original scale 
        //Shows that the icon is no longer selected.
        IconScaleDecrease();

        //hides the cursor again if the hotspot is only glanced at and not hovered over
        cursorImage.gameObject.SetActive(false); 
        cursorFillImage.fillAmount = 0.0f;


    }

    /*
     *          ICON SCALE INCREASE
     *      increases the scale of the selected icon
     *      Makes the icon double in size.
     * 
     */

    private void IconScaleIncrease()
    {
        //makes the icon bigger on all axis
        gameObject.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        Debug.Log("Game object is called: " + gameObject.name);
        Debug.Log("Increase Image Size");
        //changes the position of the gameobject so it is bigger.
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 10.0f);
        //makes the two adjacent icons disappear to make the experience look 
        //less busy for the viewer.
        //makeAdjacentDisappear();
    }

    /*
     *          ICON SCALE DECREASE    
     *      Decreases the scale of the selected icon
     *      Reverts the icon back to the original scale.
     * 
     */

    private void IconScaleDecrease()
    {
        Debug.Log("Decrease Image Size");
        //scales the icon back to its original state.
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //makes the icon return to its original position.
       // gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 10.0f);
        //makes the two adjacent icons reappear.
       // makeAdjacentReappear();
    }

    

    /*
     *          MAKE ADJACENT DISAPPEAR
     *      Collects the game objects that have been input into the 
     *      inspector view as being the two next to the selected 
     *      icon. Having established which scenes they are, the
     *      game object icons are deactivated.
     * 
     */
     /*
    private void makeAdjacentDisappear()
    {
        GameObject firstSideScene;
        GameObject secondSideScene;
        firstSideScene = adjacentScenes[0];
        secondSideScene = adjacentScenes[1];
        Debug.Log("Adjacent Scenes are: " + firstSideScene + " & " + secondSideScene);
        //deactivates the adjacent scene icons.
        adjacentScenes[0].SetActive(false);
        adjacentScenes[1].SetActive(false);
    }

        */

    /*
     *          MAKE ADJACENT REAPPEAR
     *      Collects the game objects that have been input into the 
     *      inspector view as being the two next to the selected 
     *      icon. Having established which scenes they are, the
     *      game object icons are reactivated.
     * 
     */

        /*
    private void makeAdjacentReappear()
    {
        GameObject oneSideScene;
        GameObject twoSideScene;
        oneSideScene = adjacentScenes[0];
        twoSideScene = adjacentScenes[1];
        //reactivates the adjacent scene icons.
        adjacentScenes[0].SetActive(true);
        adjacentScenes[1].SetActive(true);

    }

    */
}
