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
    [Header("プレイヤーのジャンプの大きさ")]
    [SerializeField] float fJumpPower = 6.0f;

    //プレイヤーの座標を設定するための変数
    private Rigidbody rb;
    private CapsuleCollider cp;

    //ジャンプ二度出来ないようにするフラグ
    //trueでジャンプ出来ない　falseでジャンプできる状態
    private bool bJumpFlg;
    //地面と接触しているかの判定
    private bool bCollGround;

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";


    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;       
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cp = this.GetComponent<CapsuleCollider>();

        bCollGround = true;
        bJumpFlg = false;
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /**WASDでプレイヤーを動かします**/
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0,0, fPlayerWalk);
            
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-fPlayerWalk, 0, 0);
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -fPlayerWalk);

            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(fPlayerWalk, 0, 0);

            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if(!Input.anyKey){
            // RunからWaitに遷移する
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isJump,false);
        }


        // Wait or Run からJumpに切り替える処理
        // スペースキーを押下している
        if (Input.GetKey(KeyCode.Space) && !bJumpFlg && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            // Wait or RunからJumpに遷移する
            this.animator.SetBool(key_isJump, true);
            bJumpFlg = true;
            //プレイヤーに上ベクトルの力を加える
            rb.AddForce(0,fJumpPower,0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            Debug.Log("Rosalina");
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                bJumpFlg = false;
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

}
