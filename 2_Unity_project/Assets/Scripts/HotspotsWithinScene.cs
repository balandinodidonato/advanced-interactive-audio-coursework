/*      
 *      JUPITER ARTLAND VR   
 *      
 *      Author: Rosie Jack
 *      Date: 16.06.21
 *      Last modified: 16.06.21
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
 *          (SPECIFIC TO HOTSPOTS WITHIN A SCENE INSTEAD OF THE MENU)
 *          1. Determines how the hot spot reacts to a users gaze.
 *          2. Determines whether or not the cursor is visible.
 *          3. Calls a delay before a timer selection begins.
 *          4. Makes an icon bigger when selected & revert back to original 
 *                  size if unselected again.
 * 
 */

public class HotspotsWithinScene : MonoBehaviour
{
    

    private Button hotspot;

    private bool _isOver = false;

    private InteractiveItem interactiveItem;

    // added to try and hide the cursor except when over a hotspot
    public bool useFillImage = true;
    
    [SerializeField] private Image cursorImage;

    //sets the fill image for the cursor
    [SerializeField] private Image cursorFillImage;

    [SerializeField] private bool hideOnStart = true;

    public float selectionTime = 4.0f;

    private Coroutine selectionFill;

    private bool cursorFilled;

    private bool activeCursorSelection;

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

        //hides the cursor again if the hotspot is only glanced at and not hovered over
        cursorImage.gameObject.SetActive(false);
        cursorFillImage.fillAmount = 0.0f;


    }
}
