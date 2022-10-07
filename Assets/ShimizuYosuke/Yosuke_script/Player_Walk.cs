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
    [Header("プレイヤーのステータス")]
    [SerializeField] float fPlayerWalk = 2.0f;
    [SerializeField] float fJumpPower = 6.0f;
    [SerializeField] private float PlayerRot = 1.0f;


    //プレイヤーの座標を設定するための変数
    private Rigidbody rb;

    //ジャンプ二度出来ないようにするフラグ
    //trueでジャンプ出来ない　falseでジャンプできる状態
    private bool bJumpFlg;

    //回転し始めた時の角度の保存
    private Vector3 fPivot;
    //どれくらい回転しているかを保存する
    private float leftRot;
    private float rightRot;
    private float backRot;

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private float applySpeed = 0.2f;       // 回転の適用速度

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;       
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        bJumpFlg = false;
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        ///**WASDでプレイヤーを動かします**/
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //移動
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // WaitからRunに遷移する
        //    this.animator.SetBool(key_isRun, true);

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //カメラの正面方向との計算をする
        //    Vector3 camera = Camera.main.transform.TransformDirection(Vector3.forward);
        //    Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);

        //    Debug.Log(camera);

        //    //左回転させる
        //    leftRot += 0.5f;

        //    Debug.Log(this.transform.forward);

        //    //移動
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;
        //    // WaitからRunに遷移する
        //    this.animator.SetBool(key_isRun, true);

        //    if (leftRot <= 1.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, -45, 0));
        //    }

        //}
        //else {
        //    leftRot = 0.0f;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    //回転変数をプラスしていく
        //    backRot += 0.5f;

        //    //移動
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // WaitからRunに遷移する
        //    this.animator.SetBool(key_isRun, true);

        //    if (backRot <= 2.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, -45, 0));
        //    }
        //}
        //else {
        //    backRot = 0.0f;
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    //回転させていく
        //    rightRot += 0.5f;

        //    //移動
        //    this.transform.position += transform.forward * fPlayerWalk * Time.deltaTime;

        //    // WaitからRunに遷移する
        //    this.animator.SetBool(key_isRun, true);

        //    if (rightRot <= 1.0f)
        //    {
        //        this.transform.Rotate(new Vector3(0, 45, 0));
        //    }
        //}
        //else {
        //    rightRot = 0.0f;
        //}


        //-------------WASD------------------で移動２
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z += 1;
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z -= 1;
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }

        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の反対方向(-velocity)に回す回転とします
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }


        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        //左回転
        if (Input.GetKey(KeyCode.Q))
        {
            leftRot += 0.01f;

            if (leftRot < 1.0f)
            {
                this.transform.eulerAngles -= new Vector3(0, PlayerRot, 0);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.A))
            {
                leftRot = 0.0f;
            }
        }

        //右回転
        if (Input.GetKey(KeyCode.E))
        {
            rightRot += 0.01f;

            if (rightRot < 1.0f)
            {
                this.transform.eulerAngles += new Vector3(0, PlayerRot, 0);
            }
        }
        else
        {
            if (!Input.GetKey(KeyCode.D))
            {
                rightRot = 0.0f;
            }
        }
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        if (!Input.anyKey){
            // RunからWaitに遷移する
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isJump,false);

            //回転軸を更新する
            fPivot = this.transform.forward;

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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                bJumpFlg = false;
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

}
