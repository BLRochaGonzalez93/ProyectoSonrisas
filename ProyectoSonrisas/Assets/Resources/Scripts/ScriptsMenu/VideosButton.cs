using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideosButton : MonoBehaviour
{
    [SerializeField] private Image videoImage;
    [SerializeField] private TextMeshProUGUI videoName;

    private VideoPlayer videoPlayer;   

    public VideoMuseum videoLoaded {  get; private set; }
    private void Start()
    {
        videoPlayer= FindAnyObjectByType<VideoPlayer>();
    }
    public void ConfigureVideo(VideoMuseum video)
    {
        videoLoaded = video;
        videoImage.sprite= videoLoaded.imageVideo;
        videoName.text= videoLoaded.nameVideo;
    }
    public void playVideo()
    {   
        videoPlayer.clip=videoLoaded.videoClip;
        Debug.Log("reproduciendo video");
        videoPlayer.Play();
    }
}
