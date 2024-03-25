using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideosButton videoButtonPrefab;
    [SerializeField] private Transform videoContainer;

    [SerializeField] private ListOfVideos videos;
    public VideoPlayer videoPlayer;
    [SerializeField] private GameObject panelSelectorVideo;
    private void Start()
    {
        uploadVideos();
    }
    private void Update()
    {
        isPlaying();
    }
    private void uploadVideos()
    {
        for (int i = 0;i<videos.videos.Length;i++)
        {
           VideosButton video= Instantiate(videoButtonPrefab, videoContainer);
            video.ConfigureVideo(videos.videos[i]);

        }
    } 
    
    private void isPlaying()
    {
        if (videoPlayer.isPlaying)
        {

            panelSelectorVideo.SetActive(false);
        }
        else
        {

            panelSelectorVideo.SetActive(true);
        }
    }
}
