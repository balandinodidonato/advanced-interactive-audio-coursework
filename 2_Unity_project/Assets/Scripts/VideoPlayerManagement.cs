/*      
 *      JUPITER ARTLAND VR 
 *      
 *      Author: Rosie Jack
 *      Date: 12.02.21
 *      Last modified: 12.02.21
 *      
 *      Inspiration, guidance and some parts of code from Jupiter Artland VR Experience
 *      code from the author Graeme White.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]

/*
 * VIDEO PLAYER MANAGEMENT
 * 
 * allows the video player to play videos held in an array
 * 
 */
public class VideoPlayerManagement : MonoBehaviour
{
    // creates the array to hold the video clips - can be altered in Unity Editor
    public VideoClip[] videos = new VideoClip[16];

    [SerializeField]
    private VideoPlayer _videoPlayer;

    //start obtains the video player component

    void Start()
    {
        _videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }

    /*
     * play video
     * 
     * plays video in specific array slot.
     * 
     * checks to make sure there are enough videos to fill all slots in an array.
     * method fetches video from slot and plays it.
     * 
     */

    public void PlayVideo(int indexNumber)
    {

        //checks to make sure there are not too many videos in the array
        if (indexNumber >= videos.Length)
        {
            Debug.LogErrorFormat("Error! Too many videos!", indexNumber, videos.Length);

            return;
        }

        //checks to make sure there are not too few videos in the array
        else if(indexNumber < 0)
        {
            Debug.LogErrorFormat("Error! Number entered must be above 0", indexNumber);

            return;
        }

        //obtains the video at the selected number in the array
        _videoPlayer.clip = videos[indexNumber];
        //plays the video
        _videoPlayer.Play();
    }
}
