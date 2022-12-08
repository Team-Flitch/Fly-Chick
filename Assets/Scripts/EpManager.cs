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

        //���� ���� �ε����� ���� �ʿ� �����Ǵ� ���Ѹ�� �� ����
        int curStage = GameManager.instance.CurrentStage;
        GameManager.instance.canOpenStoryStage[curStage] = 1; // ���丮��� ���� ��������
        GameManager.instance.canOpenInfinityStage[curStage - 1] = 1; // ���Ѹ�� ���� ��������

        //����
        GameManager.instance.Save();
    }

    void GoNextStage()
    {
        StartCoroutine(FadeScene());
    }

    IEnumerator FadeScene() // �� ������
    {
        Fade.gameObject.SetActive(true);
        Fade.DOFade(1f, 0.7f);
        GameManager.instance.CurrentStage++;
        yield return new WaitForSeconds(0.7f);
        //Fade.gameObject.SetActive(false);
        SceneManager.LoadScene(nextScene);
    }
}