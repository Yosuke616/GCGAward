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

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /**WASDでプレイヤーを動かします**/
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0,0,fPlayerWalk);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= new Vector3(fPlayerWalk, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= new Vector3(0, 0, fPlayerWalk);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(fPlayerWalk, 0, 0);
        }
    }
}
