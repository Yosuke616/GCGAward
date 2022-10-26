using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EighteenRot : MonoBehaviour
{
    //アニメーションのフラグ管理
    private const string key_isRot = "isRot";
    private const string key_isRun = "isRun";
    private const string key_isAttack = "isAttack";
    private Animator animator;

    //追いかける対象
    private GameObject player;

    //弾を撃つ場所
    private GameObject firePoint;
    //弾を撃つスピード
    [Header("弾のスピード")]
    [SerializeField] private float bullet_Speed = 20.0f;
    [Header("次撃つまでの時間")]
    [SerializeField] private int BULLET_DELTTIME = 300;
    int nBullet_Time;
    //弾オブジェクトの取得
    private GameObject bullet;

    //追いかけるかどうか
    [Header("追いかけるかどうか")]
    [SerializeField] private bool bChase;

    //レイを制御する
    private RaycastHit rayCastHit;

    //次の行動に何秒で移るか
    [Header("次の行動までの時間")]
    [SerializeField] private int ACTTIME = 180;
    private int nActTime;
    private bool bAct;

    //移動量保存のための変数
    private Vector3 trans;
    //どれくらい数値を変更させたか
    private int Act_Num;

    //デフォルトの動きをしているかどうか
    private bool DefaultMove;
    //元の場所に戻るフラグ
    private bool ComeBackFlg;

    //スクリプトの情報を保存する
    private BoxCollider BC;
    //スタート地点を保存しておく変数
    private Vector3 Start_Pos;
    //スタート時点での向きを保存しておく変数
    private Vector3 Start_Rot;
    //向きを元に戻すかどうかのフラグ
    private bool Change_Rot;

    //チェイスフラグ
    private bool Chase;

    //頭
    private GameObject head;

    //ウェーブマネージャー取得
    private WaveManager WM;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのタグを持っているオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //弾のタグを持っているオブジェクトを取得
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        //弾を撃てるかどうかの初期化
        nBullet_Time = BULLET_DELTTIME;
        //行動するかどうかの時間を0にする
        nActTime = 0;
        //falseで行動しないtrueで行動する
        bAct = false;
        //回転は0にする
        Act_Num = 0;
        //デフォルトの動きをするかどうか
        DefaultMove = false;
        //回帰フラグはオフにしておく
        ComeBackFlg = false;
        //スタート地点
        Start_Pos = this.transform.position;
        //スタート地点の回転の値を保存しておく
        Start_Rot = this.transform.eulerAngles;
        //チェイスフラグはオフにしておく
        Chase = false;
        //元に戻すフラグはオフにしておく
        Change_Rot = false;
        //頭の設定
        head = this.transform.GetChild(0).gameObject;
        //弾を撃つ場所の設定
        firePoint = this.transform.GetChild(1).gameObject;
        bChase = true;

        //ゲーム開始時点でその場所に当たり判定を生成する
        var Obj = new GameObject("StartPos");
        Obj.transform.position = Start_Pos;
        Obj.AddComponent<SphereCollider>();
        Obj.GetComponent<SphereCollider>().isTrigger = true;
        BC = Obj.AddComponent<BoxCollider>();
        Obj.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
        Obj.GetComponent<BoxCollider>().enabled = false;
        Obj.tag = "Enemy_Start_Pos";

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        this.animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //元の角度に戻す
        if (Change_Rot)
        {
            this.transform.eulerAngles = Start_Rot;

            if (this.transform.eulerAngles == Start_Rot)
            {
                DefaultMove = false;
                Change_Rot = false;
                //アニメーションを全てオフに
                this.animator.SetBool(key_isRot, false);
                this.animator.SetBool(key_isRun, false);
                this.animator.SetBool(key_isAttack, false);
            }
        }

        //一定時間で行動できるようにする
        if (!DefaultMove)
        {
            //一定時間で行動できるようにする
            if (!bAct)
            {
                nActTime++;
            }

            if (nActTime > ACTTIME)
            {
                if (!bAct)
                {
                    Act_Num = 0;
                }
                bAct = true;
            }


            //アクションフラグがオンだったら行動する
            if (bAct)
            {
                //一定時間ごとに180度回転させる
                this.transform.Rotate(new Vector3(0, 1, 0));
                Act_Num++;
                if (Act_Num >= 180)
                {
                    Act_Num = 0;
                    bAct = false;
                    nActTime = 0;
                }
            }
        }
        else
        {
            if (!Chase)
            {
                //元の場所に戻す
                ComeBackFlg = true;
                //内部の当たり判定をアクティブにする
                BC.GetComponent<BoxCollider>().enabled = true;

                //回転もさせる
                Quaternion lookRotation = Quaternion.LookRotation(Start_Pos - this.transform.position, Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;
                transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

                //元の場所に戻る動く
                transform.position = Vector3.MoveTowards(this.transform.position, Start_Pos, Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");

            //プレイヤーの方向に向かってくる---------------------------------------------------------------------------------------
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - this.transform.position, Vector3.up);

            lookRotation.z = 0;
            lookRotation.x = 0;

            //ここから下にレイを使ってオブジェクトや視界を制御する
            Vector3 diffDis = this.transform.position - player.transform.position;
            Vector3 axis = Vector3.Cross(this.transform.forward, diffDis);
            float viewAngle = Vector3.Angle(transform.forward, diffDis) * (axis.y < 0 ? -1 : 1);
            viewAngle += 180;

            if (viewAngle < 45 || viewAngle > 315)
            {
                //レイを飛ばす
                Vector3 diff = player.transform.position - transform.position;
                float distance = diff.magnitude;
                Vector3 direction = diff.normalized;
                Vector3 eyeHeightPos = transform.position + new Vector3(0, 1, 0);
                //13番目のレイヤーとは衝突しないレイヤーマスクを作成
                int layerMask = ~(1 << 13);
                RaycastHit[] hitsOb = Physics.RaycastAll(eyeHeightPos, direction, distance, layerMask);
                //衝突したオブジェクトが一個のみでプレイヤーだった場合Chaceモードに
                if (hitsOb.Length == 1)
                {
                    if (hitsOb[0].transform.gameObject.CompareTag("Player"))
                    {
                        //デフォルトムーブをオンにする
                        DefaultMove = true;
                        //チェイスフラグをオンにする
                        Chase = true;

                        //向きを変える
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

                        Vector3 p = new Vector3(0f, 0f, 0.05f);

                        //移動させる
                        if (bChase)
                        {
                            transform.Translate(p);
                        }

                        nBullet_Time--;
                        if (nBullet_Time < 0)
                        {
                            //弾を発射する
                            Vector3 bulletPosition = firePoint.transform.position;
                            //上で取得した場所に弾を出現
                            GameObject newBall = Instantiate(bullet, bulletPosition, this.transform.rotation);
                            // 出現させたボールのforward(z軸方向)
                            Vector3 directions = newBall.transform.forward;
                            // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
                            newBall.GetComponent<Rigidbody>().AddForce(direction * bullet_Speed, ForceMode.Impulse);
                            // 出現させたボールの名前を"bullet"に変更
                            newBall.name = bullet.name;
                            // 出現させたボールを0.8秒後に消す
                            Destroy(newBall, 2.0f);
                            nBullet_Time = BULLET_DELTTIME;

                        }
                    }
                }

            }
            //-----------------------------------------------------------------------------------------------------------------------

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //矢が当たった場合、自身と矢を消滅させる
        if (collision.gameObject.tag == "Arrow")
        {
            //スコアを加算させる
            WM.AddScore(100);
            Destroy(this.gameObject);
            WM.AddBreakEnemy();
            WM.DecEnemy();



            //HPを回復させる

        }

        //元の場所に戻るフラグ
        if (ComeBackFlg)
        {
            if (collision.gameObject.tag == "Enemy_Start_Pos")
            {
                ComeBackFlg = false;
                BC.GetComponent<BoxCollider>().enabled = false;

                Change_Rot = true;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Chase = false;
    }

}
