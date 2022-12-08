using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PauseUI;
    public AudioSource click;

    private bool paused = false;

    public void Pause() // �Ͻ�����
    {
        paused = true;
        GameManager.instance.canFly = false;
        click.Play();
    }

    public void Resume() // �簳
    {
        paused = false;
        GameManager.instance.canFly = true;
        click.Play();
    }

    public void StopGame() // ���� ������
    {
        paused = false;
        SceneManager.LoadScene("MainMenu");
        click.Play();
    }

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if(paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if(!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
