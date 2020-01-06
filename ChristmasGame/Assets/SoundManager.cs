using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Button bgmButton;

    [SerializeField]
    private Image bgmImage;

    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Button seButton;

    [SerializeField]
    private Image seImage;

    [SerializeField]
    private Slider seSlider;

    [Header("0;on,  1;off")]
    [SerializeField]
    private Sprite[] images;


    [SerializeField]
    private GameObject bgmObject;
    private AudioSource[] bgm;


    [SerializeField]
    private GameObject seObject;
    private AudioSource se;

    [Header("0select, 1decide, 2cancel, 3start, 4clear, 5gameover, 6error")]
    [SerializeField]

    private AudioClip[] clip;

    public enum OnOff
    {
        On,
        Off
    }

    float bgmVolume;
    float seVolume;


    private void Awake()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGM", 1.0f);
        seVolume = PlayerPrefs.GetFloat("SE", 1.0f);

        bgmSlider.value = bgmVolume;
        seSlider.value = seVolume;

        bgmSlider.onValueChanged.AddListener(delegate { bgmVolumeChange(); });
        seSlider.onValueChanged.AddListener(delegate { seVolumeChange(); });

        bgmButton.onClick.AddListener(() => bgmValueReset());
        seButton.onClick.AddListener(() => seValueReset());




        bgm = bgmObject.GetComponents<AudioSource>();
        se = seObject.GetComponent<AudioSource>();

        Debug.Log(se);

        foreach (AudioSource b in bgm)
        {
            b.volume = bgmVolume;
        }


        se.volume = seVolume;

    }

    void bgmVolumeChange()
    {
        bgmVolume = bgmSlider.value;

        foreach (AudioSource b in bgm)
        {
            b.volume = bgmVolume;
        }

        if (bgmVolume == 0)
        {
            bgmImage.sprite = images[(int)OnOff.Off];
        }
        else
        {
            bgmImage.sprite = images[(int)OnOff.On];
        }
    }

    void seVolumeChange()
    {
        seVolume = seSlider.value;

        se.volume = seVolume;


        if (seVolume == 0)
        {
            seImage.sprite = images[(int)OnOff.Off];
        }
        else
        {
            seImage.sprite = images[(int)OnOff.On];
        }
    }


    void bgmValueReset()
    {
        if(bgmSlider.value == 0)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1.0f);
        }
        else
        {
            bgmSlider.value = 0;
        }
    }

    void seValueReset()
    {
        if (seSlider.value == 0)
        {
            seSlider.value = PlayerPrefs.GetFloat("SE", 1.0f);
        }
        else
        {
            seSlider.value = 0;
        }
    }



    public void PlaySE(Define.SE s)
    {
        se.PlayOneShot(clip[(int)s]);
    }

    public void PlayBGM(Define.BGM b)
    {

        if(bgm[(int)b].isPlaying == false)
        {
            foreach (var bb in bgm)
            {
                bb.Stop();
            }
            bgm[(int)b].Play();
        }


    }

}
