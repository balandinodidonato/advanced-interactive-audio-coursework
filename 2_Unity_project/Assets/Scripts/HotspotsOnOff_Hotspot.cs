/*      
 *      JUPITER ARTLAND VR   
 *      
 *      Author: Rosie Jack
 *      Date: 09.07.21
 *      Last modified: 24.08.21
 *      
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
 *      HOTSPOTS ON OFF _ HOTSPOT
 *          1. Determines how the hot spot reacts to a users gaze.
 *          2. Determines whether or not the cursor is visible.
 *          3. Calls a delay before a timer selection begins.
 *          4. Makes an icon bigger when selected & revert back to original 
 *                  size if unselected again.
 *          5. Detects if the hotspots are active and if they are and the
 *                  turn on/off button is being looked at.
 *          6. Turns the hotspots on/off
 * 
 */

public class HotspotsOnOff_Hotspot : MonoBehaviour
{
    private Button hotspot;

    [SerializeField]
    private Button secondHotspot;

    private bool _isOver = false;

    private InteractiveItem interactiveItem;

    // added to try and hide the cursor except when over a hotspot
    public bool useFillImage = true;

    [SerializeField] private Image cursorImage;

    //sets the fill image for the cursor
    [SerializeField] private Image cursorFillImage;

    [SerializeField] private bool hideOnStart = true;

    public float selectionTime = 2.5f;

    private Coroutine selectionFill;

    private bool cursorFilled;

    private bool activeCursorSelection;

    //added to try and make all hotspots disappear
    public GameObject[] AllHotspotsInScene;

    private static bool appearanceOn = true;
    private static bool appearanceOff = false;


    /*
     *          AWAKE
     *      Called when the program starts, fetches
     *      the external components needed to make the 
     *      script function.
     * 
     */
    private void Awake()
    {
        //used to make sure that the component has a button attached to it
        hotspot = GetComponent<Button>();

        //Makes sure that the Interactive Item component is attached
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
     *       to the external Interactive Item script
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
        //sets the cursor bool to false
        cursorFilled = false;
        //makes the cursor appear
        Show();
        //sets the timer to 0
        float timer = 0.0f;
        //sets the cursor fill back to none
        cursorFillImage.fillAmount = 0.0f;


        //this while statement fills the cursor timer
        while (timer < selectionTime)
        {
            float fill = timer / selectionTime;
            cursorFillImage.fillAmount = fill;
            timer += Time.deltaTime;

            yield return null;
        }

        //once the timer is full the cursor should be filled in
        cursorFillImage.fillAmount = 1.0f;

        //the cursor is no longer active
        activeCursorSelection = false;

        //lets the program know that the cursor is filled
        cursorFilled = true;

        //irrelevant line below
        hotspot.onClick.Invoke();

        //if the hotspots are on they are turned off, if they are
        //off they turn on
        if (appearanceOn == false && appearanceOff == true)
        {
            MakeReappear();
        }
        else if (appearanceOn == true && appearanceOff == false)
        {
            MakeDisappear();
        }

        //hides the cursor
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

    /*
     *          MAKE DISAPPEAR
     *      Makes all the hotspots in the list disappear while
     *      at the same time changing the colour to be darker so 
     *      the user is able to see that the button is selected
     *      
     */

    public void MakeDisappear()
    {
        for (int i = 0; i < AllHotspotsInScene.Length; i++)
        {
            AllHotspotsInScene[i].SetActive(false);
        }
        appearanceOn = false;
        appearanceOff = true;
        hotspot.GetComponent<Image>().color = new Color32(164, 164, 164, 255);
        secondHotspot.GetComponent<Image>().color = new Color32(164, 164, 164, 255);
    }


    /*
     *          MAKE REAPPEAR
     *      Makes all the hotspots in the list reppear while
     *      at the same time changing the colour to be lighter again so 
     *      the user is able to see that the button has been de-selected
     *      
     */

    public void MakeReappear()
    {
        for (int i = 0; i < AllHotspotsInScene.Length; i++)
        {
            AllHotspotsInScene[i].SetActive(true);
        }
        hotspot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        secondHotspot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        appearanceOn = true;
        appearanceOff = false;
    }
    

}
