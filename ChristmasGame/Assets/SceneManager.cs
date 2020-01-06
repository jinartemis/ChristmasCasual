using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public enum GameState
    {
        Title,
        Main,
        Result,
    }

    [SerializeField]
    private Button noAdsButton;

    [SerializeField]
    private Button settingButton;

    [SerializeField]
    private GameObject settingPanel;

    [SerializeField]
    private Button restoreButton;

    [SerializeField]
    private Button closeSettingButton;

    [SerializeField]
    private Button likeButton;

    [SerializeField]
    private Button storeButton;

    [SerializeField]
    private GameObject storePanel;

    [SerializeField]
    private Button closeStoreButton;

    [SerializeField]
    private GameObject shopScrollViewContentObject;

    [SerializeField]
    private Button rankingButton;

    [SerializeField]
    private Button tapButton;

    //タイトルパネル
    [SerializeField]
    private GameObject titlePanel;

    //メインゲームパネル
    [SerializeField]
    private GameObject mainPanel;

    //リザルトパネル
    [SerializeField]
    private GameObject resultPanel;

    //ゲーム中に生成するオブジェクトの親
    [SerializeField]
    private GameObject mainObjects;

    [SerializeField]
    private GameObject rankingPanel;

    //ランキング表示スクロールビューコンテンツ
    [SerializeField]
    private GameObject rankingContentObject;

    //各プレイヤーのランキングパネル
    [SerializeField]
    private GameObject rankObject;

    [SerializeField]
    private Button closeRankingPanelButton;

    [Header("0:プレイヤー, 1:他プレイヤー")]
    [SerializeField]
    private Sprite[] rankSprite;

    [SerializeField]
    private Button goResultButton;

    [SerializeField]
    private SoundManager sound;

    public enum WhichChara
    {
        Player,
        Other,
    }

    [SerializeField]
    private Button tapToRetryButton;

    [SerializeField]
    private Button resultLikeButton;

    [SerializeField]
    private Button resultRankingButton;

    [SerializeField]
    private Button homeButton;

    [SerializeField]
    private GameManager gm;

    private void Awake()
    {
        noAdsButton.onClick.AddListener(() => noAds());
        settingButton.onClick.AddListener(() => ShowSettingPanel(true));
        restoreButton.onClick.AddListener(() => Restore());
        closeSettingButton.onClick.AddListener(() => ShowSettingPanel(false));
        likeButton.onClick.AddListener(() => Like());
        storeButton.onClick.AddListener(() => ShowStorePanel(true));
        closeStoreButton.onClick.AddListener(() => ShowStorePanel(false));

        tapButton.onClick.AddListener(() => ChangeScene(GameState.Main));

        rankingButton.onClick.AddListener(() => ShowRank(true));
        closeRankingPanelButton.onClick.AddListener(() => ShowRank(false));

        goResultButton.onClick.AddListener(() =>ChangeScene(GameState.Result));

        tapToRetryButton.onClick.AddListener(() => ChangeScene(GameState.Main));
        resultLikeButton.onClick.AddListener(() => Like());
        resultRankingButton.onClick.AddListener(() => ShowRank(true));
        homeButton.onClick.AddListener(() => ChangeScene(GameState.Title));

        int bought = PlayerPrefs.GetInt("NoAds", 0);
        if(bought == 1)
        {
            //広告非表示課金済み
            noAdsButton.interactable = false;
        }


    }

    private void Start()
    {
        ChangeScene(GameState.Title);
    }

    private void Update()
    {
        
    }

    public void ChangeScene(GameState state)
    {
        Debug.Log("シーン切り替え");
        switch (state)
        {
            case GameState.Title:
                titlePanel.SetActive(true);
                mainPanel.SetActive(false);
                resultPanel.SetActive(false);

                foreach (Transform obj in mainObjects.transform)
                {
                    GameObject.DestroyImmediate(obj.gameObject);
                }

                sound.PlayBGM(Define.BGM.Title);

                break;

            case GameState.Main:
                sound.PlaySE(Define.SE.Decide);
                titlePanel.SetActive(false);
                mainPanel.SetActive(true);
                resultPanel.SetActive(false);

                
                sound.PlayBGM(Define.BGM.Game);

                gm.InitGame();
                break;

            case GameState.Result:
                sound.PlaySE(Define.SE.Decide);
                titlePanel.SetActive(false);
                mainPanel.SetActive(false);
                resultPanel.SetActive(true);
                break;
        }
    }


    //広告非表示
    void noAds()
    {
        sound.PlaySE(Define.SE.Decide);
        Debug.Log("広告非表示");
        PlayerPrefs.SetInt("NoAds", 1);
        noAdsButton.interactable = false;
    }

    //設定パネル表示
    void ShowSettingPanel(bool open)
    {
        settingPanel.SetActive(open);
        if (open)
        {
            sound.PlaySE(Define.SE.Select);
        }
        else
        {
            sound.PlaySE(Define.SE.Cancel);
        }
    }

    //リストア
    void Restore()
    {
        sound.PlaySE(Define.SE.Select);
        Debug.Log("リストア");
    }


    //追加要素購入パネル表示
    void ShowStorePanel(bool open)
    {
        storePanel.SetActive(open);
        if (open)
        {
            sound.PlaySE(Define.SE.Decide);
        }
        else
        {
            sound.PlaySE(Define.SE.Cancel);
        }
    }

    //ランキング表示
    void ShowRank(bool open)
    {
        rankingPanel.SetActive(open);

        if (open)
        {
            sound.PlaySE(Define.SE.Select);
            //ランキング作成


            int playerRank = 2;

            for(int i = 0; i < 100; i++)
            {
               string info = "1st PlayerName";
               var rankerObj = Instantiate(rankObject, rankingContentObject.transform);
                if(i == playerRank-1)
                {
                    info = "player";
                    rankerObj.GetComponent<Image>().sprite = rankSprite[(int)WhichChara.Player];
                }
                else
                {
                    info = "other";
                    rankerObj.GetComponent<Image>().sprite = rankSprite[(int)WhichChara.Other];
                }
                rankerObj.transform.Find("Text").GetComponent<Text>().text = info;
            }

        }        
        else
        {
            sound.PlaySE(Define.SE.Cancel);
            //ランキング消去
            foreach (Transform obj in rankingContentObject.transform)
            {
                GameObject.DestroyImmediate(obj.gameObject);
            }
        }
    }


    void Like()
    {
        //ストアＵＲＬへ
#if UNITY_EDITOR
        Debug.Log("Unity Editor");

#elif UNITY_IPHONE
    //https://itunes.apple.com/en/app/id[アプリのApple ID]?mt=8
#else
    //https://play.google.com/store/apps/details?id=<package_name>
#endif
    }

}
