using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] InGameManager inGameManager;
    Rigidbody2D rig;
    Animator chick_Anim;
    public string obstacleName1;
    public string obstacleName2;
    public string obstacleName3;
    public string upObstacleName1;
    public string upObstacleName2;
    public AudioSource bird;
    public AudioSource die;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        chick_Anim = GetComponent<Animator>();

        rig.AddForce(Vector3.up * 270);

        obstacleName1 = inGameManager.obstacle[0].name + "(Clone)";
        if (inGameManager.obstacle.Length >= 2)
            obstacleName2 = inGameManager.obstacle[1].name + "(Clone)";
        if (inGameManager.obstacle.Length >= 3)
            obstacleName3 = inGameManager.obstacle[2].name + "(Clone)";
        if (inGameManager.upObstacle.Length >= 1)
            upObstacleName1 = inGameManager.upObstacle[0].name + "(Clone)";
        if (inGameManager.upObstacle.Length >= 2)
            upObstacleName2 = inGameManager.upObstacle[1].name + "(Clone)";
    }

    void Update()
    {
        chick_Anim.SetFloat("Velocity", rig.velocity.y);

        //범위에 따른 게임오버
        if (transform.localPosition.y <= -500f || transform.localPosition.x <= -650f)
        {
            GameOver();
        }

        //죽은상태면 반응x
        if (inGameManager.isdead) return;

        //고개
        if (rig.velocity.y > 0)
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.localRotation.z, 10f, rig.velocity.y / 8f));
        else transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.localRotation.z, -10f, -rig.velocity.y / 8f));

        //터치
        if (GameManager.instance.canFly)
        {
            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                rig.velocity = Vector3.zero;
                rig.AddForce(Vector3.up * 270);
                bird.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == obstacleName1 || col.gameObject.name == obstacleName2 || col.gameObject.name == obstacleName3
            || col.gameObject.name == upObstacleName1 || col.gameObject.name == upObstacleName2 || col.gameObject.name == "DeadLine")
            GameOver();
    }

    public void StopAnim()
    {
        rig.simulated = false;
        chick_Anim.enabled = false;
    }

    void GameOver()
    {
        rig.velocity = Vector3.zero;
        StopAnim();
        inGameManager.StopAnim();
        inGameManager.GameOver();
    }
}
