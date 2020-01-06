using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーキャラクター操作スクリプト

    [SerializeField]
    Rigidbody rigid;

    public float speed;

    Vector3 tapStartPos;
    Vector3 nowTapPos;
    Vector3 dif;

    float radian;

    [SerializeField]
    Animator anim;

    [SerializeField]
    GameData gameData;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private GameObject miniMapCamera;

    private void Start()
    {
        //rigid = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveControll();
    }


    private void FixedUpdate()
    {
        Move();
        MoveMiniMapCamera();
    }

    private void MoveControll()
    {
        //移動
        if (Input.GetMouseButtonDown(0))
        {
            //始点座標を代入
            tapStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButton(0))
        {
            //押している最中に今の座標を代入
            nowTapPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            dif = nowTapPos - tapStartPos;
            //スワイプ量によってSpeedを変化させる.この時、絶対値にする。
            speed = dif.x * dif.y;
            speed /= 100;
            speed = Mathf.Abs(speed);
            //最高速度
            if (speed > 5)
            {
                speed = 5;
            }
            //最低速度
            if (speed < 3)
            {
                speed = 3;
            }

            //もしspeedが0より上であれば、アニメーションさせる
            if (speed > 0)
            {
                anim.SetBool("Running", true);
            }
            //回転する角度計算
            radian = Mathf.Atan2(dif.x, dif.y) * Mathf.Rad2Deg;
        }
        else
        {
            //rotebool = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            speed = 0;
            anim.SetBool("Running", false);
        }
    }

    private void Move()
    {
        var f = transform.forward * speed;
        f.y = -3;

        rigid.velocity = f;
        rigid.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, radian, 0), 10);
    }

    Vector3 miniMapCameraPos;
    private void MoveMiniMapCamera()
    {
        miniMapCameraPos = this.transform.position;
        miniMapCameraPos.y = 20;
        miniMapCamera.transform.position = miniMapCameraPos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Present")
        {
            soundManager.PlaySE(Define.SE.Decide);

            var makePos = other.gameObject.transform.position;
            makePos += new Vector3(0, 1, 0);
            var ef = Instantiate(gameData.heartEffect, makePos, Quaternion.identity);
            Destroy(ef, 1.0f);

            Destroy(other.gameObject);
        }
    }
}
