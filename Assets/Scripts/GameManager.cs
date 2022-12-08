using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float backVol;

    public int GameMode; // 0-���丮���, 1-���Ѹ��, (2-���丮����)
    public int CurrentStage; //1, 2, 3, 4, 5
    public int[] canOpenStoryStage; //���丮��� �� �� �ִ���
    public int[] canOpenInfinityStage; //���Ѹ�� �� �� �ִ���

    public int bestScore;
    public bool startOpening;
    public bool canFly;

    public static GameManager Instance
    {
        get
        { // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ�
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ� ����
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // ���� ��ȯ�Ǵ��� ����� �ν���Ʈ�� �ı����� �ʵ���
        DontDestroyOnLoad(gameObject);
    }


    public void Load()
    {
        //�����Ͱ� �������� ������ return
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
