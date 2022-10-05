/**
 * @file        Player_Walk
 * @author      Shimizu_Yosuke
 * @date        2022/10/5
 * @brief       プレイヤーの移動を制御する
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{

    /** @brief プレイヤーの移動速度*/
    [Header("プレイヤーの移動スピード")]
    [SerializeField] float fPlayerWalk = 2.0f;

    void Awake()
    {
        Application.targetFrameRate = 60; //60FPSに設定
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**WASDでプレイヤーを動かします**/
    }
}
