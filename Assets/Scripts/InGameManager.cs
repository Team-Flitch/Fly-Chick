using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class InGameManager : MonoBehaviour
{
    [Header("점수")]
    [SerializeField] public Text bestScore_txt;
    [SerializeField] public Text currentScore_txt;

    [Header("장애물")]
    public GameObject[] obstacle;
    [SerializeField] GameObject[] obstacles = new GameObject[5];
    [SerializeField] RectTransform parent;
    int idx = 0;

    [Header("장애물2")]
    public GameObject[] upObstacle;
    [SerializeField] GameObject[] upObstacles = new GameObject[2];
    [SerializeField] RectTransform parent2;
    int idx2 = 0;
    bool up;

    [Header("애니메이션")]
    [SerializeField] Animator bg_Anim;    //배경 움직임
    [SerializeField] Animator floor_Anim; //바닥 움직임
    [SerializeField] GameObject endEp;    //에피소드

    [Header("시간")]
    [SerializeField] float stageTime;  //현재 경과 시간
    [SerializeField] float desTime;    //목표시간
    float nextTime;
    float nextTime2;
    float speed = 12f;

    [Header("UI")]
    [SerializeField] Player player;
    [SerializeField] Text time;
    [SerializeField] Text stage_txt;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject[] gameOverUI; //0이 스토리모드. 1이 무한모드
    [SerializeField] Button home_btn;
    [SerializeField] Button retry_btn;
    [SerializeField] Image Fade;

    [Header("Sound")]
    [SerializeField]AudioSource bg_audio;
    public AudioSource die;
    public AudioSource click;


    public bool isdead = false;
    public GameObject opening;

    void Start()
    {
        Debug.Log("게임모드는 " + GameManager.instance.GameMode);
        //사운드
        bg_audio.volume = GameManager.instance.backVol;
        bg_audio.Play();

        // 시간 측정
        StartCoroutine("Timer");

        // 버튼 등록
        home_btn.onClick.AddListener(GoMainMenu);
        retry_btn.onClick.AddListener(Retry);

        GameManager.instance.canFly = true;


        // 모드 별 노출할 UI
        if (GameManager.instance.GameMode == 0) // 스토리모드
        {
            if (GameManager.instance.startOpening==true && GameManager.instance.CurrentStage == 1) // 오프닝 실행
                StartCoroutine("Opening");
            stage_txt.text = "Stage " + GameManager.instance.CurrentStage;
            time.gameObject.SetActive(false);
        }
        else if (GameManager.instance.GameMode == 1) // 무한모드
        {
            stage_txt.text = "";
            time.gameObject.SetActive(true);
        }
    }

    IEnumerator FadeScene() // 씬 들어오기
    {
        if (!Fade.gameObject.activeInHierarchy)
            Fade.gameObject.SetActive(true);
        Fade.DOFade(0f, 0.7f);
        yield return new WaitForSeconds(0.7f);
        Fade.gameObject.SetActive(false);
    }

    IEnumerator Opening()
    {
        StartCoroutine(FadeScene());
        StopCoroutine("Timer");
        GameManager.instance.canFly = false;
        player.gameObject.SetActive(false);
        opening.SetActive(true);
        yield return new WaitForSeconds(28.5f);
        SkipOpening();
    }

    public void SkipOpening()
    {
        GameManager.instance.canFly = true;
        GameManager.instance.startOpening = false;
        player.gameObject.SetActive(true);        
        StopCoroutine("Opening");
        opening.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (!isdead)
        {
            //무한모드이면 초 보여주기
            if (GameManager.instance.GameMode == 1)
                time.text = ((int)(stageTime/10)).ToString();

            MakeObstacles();
        }
    }

    void MakeObstacles()
    {
        if (Time.timeScale == 0 || endEp.gameObject.activeInHierarchy)
            return;

        if (stageTime>nextTime)
        {
            nextTime = stageTime + 4f;
            int obsIdx = Random.Range(0, obstacle.Length);
            obstacles[idx] = Instantiate(obstacle[obsIdx], Vector3.zero, Quaternion.identity);
            obstacles[idx].transform.SetParent(parent);
            obstacles[idx].transform.localScale = Vector3.one;
            // 스테이지 별로 다른 난이도
            if (GameManager.instance.CurrentStage == 1)
            {
                obstacles[idx].transform.localPosition = new Vector3(840f, Random.Range(-500f, -350f), 0f);
            }
            else if (GameManager.instance.CurrentStage == 4)
            {
                obstacles[idx].transform.localPosition = new Vector3(840f, Random.Range(-700f, -400f), 0f);
            }
            else if (GameManager.instance.CurrentStage == 5)
            {
                obstacles[idx].transform.localPosition = new Vector3(840f, Random.Range(0f, 110f), 0f);
            }
            else
            {
                obstacles[idx].transform.localPosition = new Vector3(840f, Random.Range(-650f, -500f), 0f);
            }
            if (++idx == 5) idx = 0;
        }

        if (obstacles[0])
        {
            obstacles[0].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            if (obstacles[0].transform.localPosition.x < -700f)
            {
                Destroy(obstacles[0]);
            }
        }
        if (obstacles[1])
        {
            obstacles[1].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            if (obstacles[1].transform.localPosition.x < -700f)
            {
                Destroy(obstacles[1]);
            }
        }
        if (obstacles[2])
        {
            obstacles[2].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            if (obstacles[2].transform.localPosition.x < -700f)
            {
                Destroy(obstacles[2]);
            }
        }
        if (obstacles[3])
        {
            obstacles[3].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            if (obstacles[3].transform.localPosition.x < -700f)
            {
                Destroy(obstacles[3]);
            }
        }
        if (obstacles[4])
        {
            obstacles[4].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            if (obstacles[4].transform.localPosition.x < -700f)
            {
                Destroy(obstacles[4]);
            }
        }

        if ((GameManager.instance.CurrentStage == 2 || GameManager.instance.CurrentStage == 3) && stageTime > nextTime2)
        {
            nextTime2 = stageTime + 20f;
            int upIdx = Random.Range(0, upObstacle.Length);
            upObstacles[idx2] = Instantiate(upObstacle[upIdx], Vector3.zero, Quaternion.identity);
            upObstacles[idx2].transform.SetParent(parent);
            upObstacles[idx2].transform.localScale = Vector3.one;
            // 스테이지 별로 다른 난이도
            if (GameManager.instance.CurrentStage == 2)
            {
                upObstacles[idx2].transform.localPosition = new Vector3(840f, Random.Range(300f, 700f), 0f);
            }
            else if (GameManager.instance.CurrentStage == 3)
            {
                upObstacles[idx2].transform.localPosition = new Vector3(840f, Random.Range(400f, 730f), 0f);
            }
            if (++idx2 == 2) idx2 = 0;
        }

        if (upObstacles[0])
        {
            if (GameManager.instance.CurrentStage == 2)
                upObstacles[0].transform.Translate(-0.3f*speed*Time.deltaTime, 0, 0);
            else if (GameManager.instance.CurrentStage == 3)
            {
                if (upObstacles[0].transform.localPosition.y > 700f)
                {
                    up = false;
                }
                else if (upObstacles[0].transform.localPosition.y < 400f)
                {
                    up = true;
                }

                if (up)
                    upObstacles[0].transform.Translate(-0.2f * speed * Time.deltaTime, 0.1f * speed * Time.deltaTime, 0);
                else
                    upObstacles[0].transform.Translate(-0.2f * speed * Time.deltaTime, -0.1f * speed * Time.deltaTime, 0);
            }
            if (upObstacles[0].transform.localPosition.x < -700f)
            {
                Destroy(upObstacles[0]);
            }
        }
        if (upObstacles[1])
        {
            if (GameManager.instance.CurrentStage == 2)
                upObstacles[1].transform.Translate(-0.3f * speed * Time.deltaTime, 0, 0);
            else if (GameManager.instance.CurrentStage == 3)
            {
                if (upObstacles[1].transform.localPosition.y > 700f)
                {
                    up = false;
                }
                else if (upObstacles[1].transform.localPosition.y < 400f)
                {
                    up = true;
                }

                if (up)
                    upObstacles[1].transform.Translate(-0.2f * speed * Time.deltaTime, 0.1f * speed * Time.deltaTime, 0);
                else
                    upObstacles[1].transform.Translate(-0.2f * speed * Time.deltaTime, -0.1f * speed * Time.deltaTime, 0);
            }
            if (upObstacles[1].transform.localPosition.x < -700f)
            {
                Destroy(upObstacles[1]);
            }
        }
    }

    public void StopAnim()
    {
        bg_Anim.enabled = false;
        floor_Anim.enabled = false;
    }

    public void GameOver()
    {
        isdead = true;
        GameManager.instance.canFly = false;
        GameOverPanel.SetActive(true);
        die.Play();

        if (GameManager.instance.GameMode == 0)
        {
            //스토리모드
            StopAllCoroutines();
            gameOverUI[0].SetActive(true);
            gameOverUI[1].SetActive(false);
        }
        else
        {
            //무한모드
            StopAllCoroutines();
            // 현재 점수
            int score = (int)(stageTime / 10);
            currentScore_txt.text = score.ToString();
            // 최고점수
            if (score > GameManager.instance.bestScore) // 최고 점수 갱신
            {
                GameManager.instance.bestScore = score;
            }
            
            bestScore_txt.text = GameManager.instance.bestScore.ToString();

            gameOverUI[0].SetActive(false);
            gameOverUI[1].SetActive(true);
        }

        GameManager.instance.Save();
    }

    public void GoMainMenu()
    {
        //click.Play();
        //StartCoroutine(FadeToMain());
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeToMain()
    {
        Fade.gameObject.SetActive(true);
        Fade.DOFade(1f, 0.7f);
        yield return new WaitForSeconds(0.7f);
        Fade.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    void Retry()
    {
        //click.Play();
        //StartCoroutine(Delay());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Delay()
    {
        yield return null;
        if (!click.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else StartCoroutine(Delay());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        stageTime++;

        //스테이지 모드이면 클리어 초 세기
        if (GameManager.instance.GameMode==0 && stageTime/10 >= desTime)
        {
            GameManager.instance.canFly = false;
            StopAnim();
            player.StopAnim();
            endEp.SetActive(true);
            StopCoroutine("Timer");
        }
        StartCoroutine(Timer());
    }


    public void ClearStoryMode()
    {
        GameManager.instance.canOpenInfinityStage[4] = 1;
        //Debug.Log(GameManager.instance.canOpenInfinityStage[4]);
    }
}
