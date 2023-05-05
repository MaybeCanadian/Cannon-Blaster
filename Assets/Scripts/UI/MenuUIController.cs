using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.OutSideInit();

        ClipDatatBase.OutSideInit();

        AudioManager.StopAllChannel(PLaybackChannelList.Music);

        AudioClip track = ClipDatatBase.GetTrack(TrackList.Fearless);

        AudioManager.PlaySound2D(track, PLaybackChannelList.Music, 1.0f, true);
    }
    public void OnPlayButtonPress()
    {
        SceneChanger.GoToGameFromMain();
    }
    public void OnOptionsButtonPress()
    {
        SceneChanger.GoToOptionsFromMain();
    }
    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
