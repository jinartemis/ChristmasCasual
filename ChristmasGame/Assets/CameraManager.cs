using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField]
    private GameObject playerObject;

    //プレイヤーとカメラの距離
    Vector3 dif;
   
    void Start()
    {
        dif = this.transform.position - playerObject.transform.position;
    }

    void Update()
    {
        this.transform.position = playerObject.transform.position + dif;
    }
}
