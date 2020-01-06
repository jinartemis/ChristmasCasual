using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainObjects;

    [SerializeField]
    GameData gameData;

    [SerializeField]
    PlayerController pController;

    [SerializeField]
    Animator anim;

    [SerializeField]
    private Text timeText;


    [SerializeField]
    float defaultTimeLimit;

    float time;
    int second;


    float delta;
    [SerializeField]
    private float makeSpan = 2.0f;

    public bool isPlaying;
    [SerializeField]
    SceneManager sceneManager;

    //ゲーム開始時の処理
    public void InitGame()
    {
        Debug.Log("ゲーム初期化");

        foreach (Transform obj in mainObjects.transform)
        {
            GameObject.DestroyImmediate(obj.gameObject);
        }

        pController.enabled = true;

        time = defaultTimeLimit;

        isPlaying = true;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if(isPlaying == false)
        {
            return;
        }

        delta += Time.deltaTime;
        if(delta > makeSpan)
        {
            delta = 0;

            int rnd = Random.Range(0, gameData.presentPrefab.Length);
            var makePos = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(10.0f, 15.0f), Random.Range(-5.0f, 5.0f));
            var present = Instantiate(gameData.presentPrefab[rnd], mainObjects.transform);
            present.transform.position = makePos;
        }


        time -= Time.deltaTime;

        if(time <= 0)
        {
            //時間切れ
            isPlaying = false;

            anim.SetBool("Running", false);

            sceneManager.ChangeScene(SceneManager.GameState.Result);
            pController.speed = 0;
            pController.enabled = false;
        }


        second = Mathf.FloorToInt(time);
        timeText.text = "0:" + second.ToString("D2");
    }

   
}
