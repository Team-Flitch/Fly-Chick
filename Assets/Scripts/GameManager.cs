using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float backVol;

    public int GameMode; // 0-스토리모드, 1-무한모드, (2-스토리실행)
    public int CurrentStage; //1, 2, 3, 4, 5
    public int[] canOpenStoryStage; //스토리모드 열 수 있는지
    public int[] canOpenInfinityStage; //무한모드 열 수 있는지

    public int bestScore;
    public bool startOpening;
    public bool canFly;

    public static GameManager Instance
    {
        get
        { // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }
            return instance;
        }
    }

    public void Awake()
    {
        //canOpenStoryStage = new int[5] { 1, 0, 0, 0, 0 };
        //canOpenInfinityStage = new int[5] { 0, 0, 0, 0, 0 };

        //Load();

        if (instance == null)
        {
            instance = this;
            canOpenStoryStage = new int[5] { 1, 0, 0, 0, 0 };
            canOpenInfinityStage = new int[5] { 0, 0, 0, 0, 0 };

            Load();
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스 삭제
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 씬이 전환되더라도 선언된 인스턴트가 파괴되지 않도록
        DontDestroyOnLoad(gameObject);
    }


    public void Load()
    {
        //데이터가 존재하지 않으면 return
        if (!PlayerPrefs.HasKey("canOpenStoryStage0")) return;

        for (int i = 0; i < 5; i++)
        {
            canOpenStoryStage[i] = PlayerPrefs.GetInt("canOpenStoryStage" + i);
            canOpenInfinityStage[i] = PlayerPrefs.GetInt("canOpenInfinityStage" + i);
        }
        backVol= PlayerPrefs.GetFloat("backVolume");
        bestScore=PlayerPrefs.GetInt("bestscore");

        Debug.Log("=====Loading=====");
        Debug.Log("backVol : "+backVol);
        Debug.Log("bestScore : " + bestScore);
    }

    public void Save()
    {
        Debug.Log("=====Saving=====");
        Debug.Log("backVol : " + backVol);
        Debug.Log("bestScore : " + bestScore);

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("canOpenStoryStage" + i, canOpenStoryStage[i]);
            PlayerPrefs.SetInt("canOpenInfinityStage" + i, canOpenInfinityStage[i]);
        }

        PlayerPrefs.SetFloat("backVolume", backVol);
        PlayerPrefs.SetInt("bestscore", bestScore);
    }
}
