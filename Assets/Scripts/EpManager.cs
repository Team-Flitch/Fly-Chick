using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EpManager : MonoBehaviour
{
    [SerializeField] Button goBtn;
    [SerializeField] string nextScene;
    [SerializeField] GameObject stageClearPanel;
    [SerializeField] Image Fade;
    public AudioSource clear;

    void Start()
    {
        goBtn.onClick.AddListener(GoNextStage);

        stageClearPanel.SetActive(true);
        clear.Play();

        //다음 씬의 인덱스와 현재 맵에 대응되는 무한모드 맵 열기
        int curStage = GameManager.instance.CurrentStage;
        GameManager.instance.canOpenStoryStage[curStage] = 1; // 스토리모드 다음 스테이지
        GameManager.instance.canOpenInfinityStage[curStage - 1] = 1; // 무한모드 현재 스테이지

        //저장
        GameManager.instance.Save();
    }

    void GoNextStage()
    {
        StartCoroutine(FadeScene());
    }

    IEnumerator FadeScene() // 씬 나가기
    {
        Fade.gameObject.SetActive(true);
        Fade.DOFade(1f, 0.7f);
        GameManager.instance.CurrentStage++;
        yield return new WaitForSeconds(0.7f);
        //Fade.gameObject.SetActive(false);
        SceneManager.LoadScene(nextScene);
    }
}