using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Google.Play.Review;

public class StageManager : MonoBehaviour
{
    #region ����
    public UnityEngine.UI.Extensions.HorizontalScrollSnap HorizontalScrollSnap;
    [Header("======")]
    public GameObject Canvas_Main; // ���θ޴� ȭ��
    public GameObject Canvas_Select; // �������� ���� ȭ��
    public GameObject Setting_btn;
    public GameObject Rule; // ���� ��� �г�
    public GameObject Setting; // ���� ��� �г�
    public Image Backbtn;
    public Image Black; // ������ ������ ����
    public Text Title_txt; // �������� ���� Ÿ��Ʋ
    public GameObject Credit; // ������ ũ����
    public Image Credit_Img; // ������ ũ���� �̹���
    public GameObject Credit_txt; // ������ ũ���� �̹���
    public Image[] Fade;
    [Header("======")]
    public GameObject[] Stage; // �������� (1~5)
    public GameObject[] Locked; // �������� ��� �̹���
    public Image[] Lock; // �ڹ��� �̹���
    public Image UnLocked; // ��� ����
    public AudioSource click; // ��ưŬ�� ȿ����

    // Create instance of ReviewManager
    private ReviewManager _reviewManager;
    #endregion

    void Start()
    {
        StartCoroutine(FadeScene(1));
    }

    public void StartBtnClick() // ���� ����
    {
        click.Play();
        // ��� �Ǻ�
        if (GameManager.instance.GameMode == 0)
            Debug.Log("���丮���");
        else if (GameManager.instance.GameMode == 1)
            Debug.Log("���Ѹ��");

        // �������� �Ǻ�
        if (Stage[0].activeInHierarchy == true) // �������� 1
        {
            if (Locked[0].activeInHierarchy == true)
            {
                Debug.Log("��������1 ���� �ȵ�");
                StartCoroutine(UnlockWarning(0));
            }
            else
            {
                Debug.Log("��������1 ���� ����");
                GameManager.instance.CurrentStage = 1;
                GameManager.instance.startOpening = true;
                StartCoroutine(FadeScene(0));
                SceneManager.LoadScene("Stage1");
            }
        }
        else if (Stage[1].activeInHierarchy == true) // �������� 2
        {
            if (Locked[1].activeInHierarchy == true)
            {
                Debug.Log("��������2 ���� �ȵ�");
                StartCoroutine(UnlockWarning(1));
            }
            else
            {
                Debug.Log("��������2 ���� ����");
                GameManager.instance.CurrentStage = 2;
                StartCoroutine(FadeScene(0));
                SceneManager.LoadScene("Stage2");
            }
        }
        else if (Stage[2].activeInHierarchy == true)
        {
            if (Locked[2].activeInHierarchy == true) // �������� 3
            {
                Debug.Log("��������3 ���� �ȵ�");
                StartCoroutine(UnlockWarning(2));
            }
            else
            {
                Debug.Log("��������3 ���� ����");
                GameManager.instance.CurrentStage = 3;
                StartCoroutine(FadeScene(0));
                SceneManager.LoadScene("Stage3");
            }
        }
        else if (Stage[3].activeInHierarchy == true) // �������� 4
        {
            if (Locked[3].activeInHierarchy == true)
            {
                Debug.Log("��������4 ���� �ȵ�");
                StartCoroutine(UnlockWarning(3));
            }
            else
            {
                Debug.Log("��������4 ���� ����");
                GameManager.instance.CurrentStage = 4;
                StartCoroutine(FadeScene(0));
                SceneManager.LoadScene("Stage4");
            }
        }
        else if (Stage[4].activeInHierarchy == true) // �������� 5
        {
            if (Locked[4].activeInHierarchy == true)
            {
                Debug.Log("��������5 ���� �ȵ�");
                StartCoroutine(UnlockWarning(4));
            }
            else
            {
                Debug.Log("��������5 ���� ����");
                GameManager.instance.CurrentStage = 5;
                StartCoroutine(FadeScene(0));
                SceneManager.LoadScene("Stage5");
            }
        }

    }

    #region DOTWEEN
    IEnumerator FadeScene(int val) // int val - 0: �� ������ / 1: �� ������
    {
        if (val == 0)
        {
            Fade[1].gameObject.SetActive(true);
            Fade[0].gameObject.SetActive(true);
            Fade[1].DOFade(1f, 0.7f);
            yield return new WaitForSeconds(0.7f);
            Fade[0].DOFade(1f, 0f);
        }
        else
        {
            if (!Fade[0].gameObject.activeInHierarchy)
                Fade[0].gameObject.SetActive(true);
            if (!Fade[1].gameObject.activeInHierarchy)
                Fade[1].gameObject.SetActive(true);
            Fade[0].DOFade(0f, 0.7f);
            Fade[1].DOFade(0f, 0f);
            yield return new WaitForSeconds(0.7f);
            Fade[1].gameObject.SetActive(false);
            Fade[0].gameObject.SetActive(false);
        }
    }

    IEnumerator BlackFade(int val)
    {
        if (val == 0)
        {
            Black.DOFade(0.0f, 0.3f);
            yield return new WaitForSeconds(0.3f);
            Black.gameObject.SetActive(false);

        }
        else if (val == 1)
        {
            Black.gameObject.SetActive(true);
            Black.DOFade(0.6f, 0.3f);
        }

    }

    IEnumerator Backbtn_Fade(int val)
    {
        if (val == 0)
        {
            Backbtn.DOFade(0.0f, 0.3f);
            yield return new WaitForSeconds(0.3f);
            Backbtn.gameObject.SetActive(false);

        }
        else if (val == 1)
        {
            Backbtn.gameObject.SetActive(true);
            Backbtn.DOFade(1f, 0.3f);
        }

    }

    IEnumerator UnlockWarning(int i)
    {
        Lock[i].transform.DOShakePosition(0.5f, new Vector3(10, 10, 0));

        UnLocked.transform.DOScale(new Vector3(1f, 1f, 0), 0.1f);
        UnLocked.DOFade(1f, 0.1f);
        yield return new WaitForSeconds(0.7f);
        UnLocked.DOFade(0f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        UnLocked.transform.DOScale(new Vector3(0f, 0f, 0), 0f);
    }

    #endregion


    #region ���θ޴�����
    public void ToMainMenu() // �������� ���� -> ���θ޴� 
    {
        StartCoroutine(BlackFade(0));
        Canvas_Main.SetActive(true);
        Setting_btn.SetActive(true);
        Canvas_Select.SetActive(false);
        click.Play();

        for (int i = 0; i < 4; i++)
            HorizontalScrollSnap.PreviousScreen();
    }

    public void ToSelectMode(int mode) // ���θ޴� -> �������� ���� 
    {
        GameManager.instance.GameMode = mode;

        StartCoroutine(BlackFade(1));
        Canvas_Main.SetActive(false);
        Setting_btn.SetActive(false);
        Canvas_Select.SetActive(true);
        click.Play();

        if (GameManager.instance.GameMode == 0)
        {
            Title_txt.text = "���丮 ���\n�������� ����";
            for (int i = 0; i < 5; i++)
            {
                if (GameManager.instance.canOpenStoryStage[i] == 1)
                    Locked[i].SetActive(false);
                else
                    Locked[i].SetActive(true);
            }
        }
        else if (GameManager.instance.GameMode == 1)
        {
            Title_txt.text = "���� ���\n�������� ����";
            for (int i = 0; i < 5; i++)
            {
                if (GameManager.instance.canOpenInfinityStage[i] == 1)
                    Locked[i].SetActive(false);
                else
                    Locked[i].SetActive(true);
            }
        }
    }

    public void HowToPlay() // ���� ��� 
    {
        StartCoroutine(BlackFade(1));
        StartCoroutine(Backbtn_Fade(1));
        Canvas_Main.SetActive(false);
        Setting_btn.SetActive(false);
        Rule.SetActive(true);
        click.Play();
    }

    public void BackToMainMenu() // ���ӹ�� -> ���θ޴�
    {
        StartCoroutine(BlackFade(0));
        StartCoroutine(Backbtn_Fade(0));
        Rule.SetActive(false);
        Canvas_Main.SetActive(true);
        Setting_btn.SetActive(true);
        click.Play();
    }

    public void SettingPanel(int val) // ���� �г� // 0-���� 1-����
    {
        StartCoroutine(BlackFade(val));

        if (val == 0)
            Setting.SetActive(false);
        else
            Setting.SetActive(true);
        click.Play();
    }

    public void Backbtn_ctrl()
    {
        if (Rule.activeInHierarchy == true)
            BackToMainMenu();
        else
            DevCredit_Btn(0);
    }

    #endregion


    #region ����-����&ũ����
    public void WriteReview_Btn() // ����-���� �ۼ��ϱ� ��ư
    {
        StartCoroutine(ShowReview());
    }

    IEnumerator ShowReview()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        PlayReviewInfo _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }

    public void DevCredit_Btn(int val) // ����- ������ ũ���� ��ư
    {
        StartCoroutine(ShowCredit(val));
        StartCoroutine(Backbtn_Fade(val));
    }

    IEnumerator ShowCredit(int val)
    {
        if (val == 0) // ������
        {
            Credit_Img.DOFade(0.0f, 0.3f);
            yield return new WaitForSeconds(0.05f);
            Credit_txt.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            Credit.gameObject.SetActive(false);
        }
        else if (val == 1) // ������
        {
            Credit.gameObject.SetActive(true);
            Credit_txt.gameObject.SetActive(true);
            Credit_Img.DOFade(1f, 0.25f);
        }
    }
    #endregion
}