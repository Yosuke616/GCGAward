/**
 * @file        Player_Walk
 * @author      Shimizu_Yosuke
 * @date        2022/10/5
 * @brief       プレイヤーの移動を制御する
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Walk : MonoBehaviour
{
    //プレイヤーのステータス
    public enum PLAYER_STATE {
        IDEL_STATE,
        WALK_STATE,
        RUN_STATE,
        JUMP_STATE,

        MAX_STATE
    }

    [Header("音の種類")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fall;

    private AudioSource AS;

    [Header("ピッチの変更")]
    [SerializeField] private float pitchRange = 0.1f;

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

    //プレイヤーの状態
    private PLAYER_STATE eState;
  
    //どれくらい回転しているかを保存する
    private float leftRot;
    private float rightRot;

    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    private const string key_isWalk = "isWalk";

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度
    [SerializeField] private float applySpeed = 0.2f;       // 回転の適用速度
    [SerializeField] private FollowCamera refCamera;        // カメラの水平回転を参照する用
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

        AS = GetComponent<AudioSource>();

        eState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //何も押していないときは待機
        if (!Input.anyKey)
        {
            eState = 0;
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalk, false);
            this.animator.SetBool(key_isJump, false);
        }

        //シフトだけ押しているときはアニメーション消す
        if (Input.GetKey(KeyCode.LeftShift)) {
            eState = 0;
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isWalk, false);
            this.animator.SetBool(key_isJump, false);
        }

        //-------------WASD------------------走る
        // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z += 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.z -= 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 0.1f;
            eState = PLAYER_STATE.WALK_STATE;
            this.animator.SetBool(key_isWalk, true);
        }

        if (eState == PLAYER_STATE.WALK_STATE)
        {
            moveSpeed = 5.0f;
            this.animator.SetBool(key_isWalk, true);
            this.animator.SetBool(key_isRun, false);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                eState = PLAYER_STATE.RUN_STATE;
                this.animator.SetBool(key_isRun, true);
            }
        }


        if (eState == PLAYER_STATE.RUN_STATE)
        {
            moveSpeed = 10.0f;

            if (Input.GetKey(KeyCode.W))
            {
                velocity.z += 0.1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity.x -= 0.1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity.z -= 0.1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity.x += 0.1f;
            }
        }

        //===============================================================================================================================
        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;
        // いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {
            // プレイヤーの回転(transform.rotation)の更新
            // 無回転状態のプレイヤーのZ+方向(後頭部)を、
            // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                  applySpeed);

            // プレイヤーの位置(transform.position)の更新
            // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
            transform.position += refCamera.hRotation * velocity;
        }
        //===============================================================================================================================

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

        // Wait or Run からJumpに切り替える処理
        // スペースキーを押下している
        if (Input.GetKey(KeyCode.Space) && !bJumpFlg)
        {
            bJumpFlg = true;
            //プレイヤーに上ベクトルの力を加える
            rb.AddForce(0, fJumpPower, 0);
            eState = PLAYER_STATE.JUMP_STATE;
            this.animator.SetBool(key_isJump, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                if (bJumpFlg) {
                    bJumpFlg = false;
                    this.animator.SetBool(key_isRun, true);
                    this.animator.SetBool(key_isRun, false);
                    this.animator.SetBool(key_isWalk, false);
                }
                this.animator.SetBool(key_isJump, false);
            }
        }
    }

    public void WalkSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange,pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(walk);
    }

    public void RunSound() {
        AS.pitch = 2.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(run);
    }

    public void JumpSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(jump);
    }

    public void FallSound() {
        AS.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(fall);
    }

}
