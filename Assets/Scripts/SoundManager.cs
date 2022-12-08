using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider Volume;
    public AudioSource bgm_Mainmenu, bgm_Play, bgm_Ending;


    private void Start()
    {       
        GameManager.instance.backVol = PlayerPrefs.GetFloat("backvol", 1f);
        Volume.value = GameManager.instance.backVol;
        bgm_Mainmenu.volume = Volume.value;
        bgm_Play.volume = Volume.value;
        bgm_Ending.volume = Volume.value;
    }

    void Update()
    {
        SoundSlider();
    }

    void SoundSlider()
    {
        bgm_Mainmenu.volume = Volume.value;
        bgm_Play.volume = Volume.value;
        bgm_Ending.volume = Volume.value;

        GameManager.instance.backVol = Volume.value;
        PlayerPrefs.SetFloat("backvol", GameManager.instance.backVol);
    }
}
