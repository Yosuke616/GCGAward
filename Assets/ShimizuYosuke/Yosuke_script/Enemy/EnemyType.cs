using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    //敵のタイプを列挙体で判別する
    public enum ENEMY_TYPE {
        ENEMY_ROLL_90 = 0,
        ENEMY_ROLL_180,
        WALK_ENEMY,
        STOP_ENEMY
    }

    [Header("敵の種類")]
    [Header("1:90度回転")]
    [Header("2:180度回転")]
    [Header("3:歩く")]
    [Header("4:止まって撃つ")]
    [SerializeField] private ENEMY_TYPE eType = 0;

    [Header("HPの回復量")]
    [SerializeField] private int nRecovery = 0;

    //敵が誰を追いかけるかの対象
    private GameObject player;

    //弾丸関係
    [Header("弾関係")]
    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float bullet_speed = 20.0f;

    [Header("次撃てるまでの時間")]
    [SerializeField] int bullet_deltTime = 480;
    int nBullet_Time;

    [Header("敵が攻撃するかしないかのフラグ")]
    [Header("true :攻撃する")]
    [Header("false:攻撃しない")]
    [SerializeField] private bool bAttackFlg = true;

    [Header("敵が追いかけるか追いかけないかのフラグ")]
    [Header("true :追いかける")]
    [Header("false:追いかけない")]
    [SerializeField] private bool bChaseFlg = true;

    //レイを使用して視界を制御する
    private RaycastHit rayCastHit;

    //行動するためのカウント(共有)
    [Header("何秒で次の行動をするか")]
    [SerializeField] private int ACTTIME = 180;
    private int nActTime;
    private bool bAct;

    [Header("敵の歩くスピード")]
    [SerializeField] private float WALK_SPEED = 5.0f;
    [Header("敵がどの位歩くか")]
    [SerializeField] private float MOVE_AREA = 500.0f; 

    //移動量保存のための変数
    private Vector3 trans;
    //どれくらい数値を変更させたか
    private int Act_Num;
    //回転さえるためのフラグ
    private bool Rotflg;

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

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの取得
        player = GameObject.Find("Player");
        //時間の初期化
        nBullet_Time = bullet_deltTime;
        //行動するときの時間を0にする
        nActTime = 0;
        //falseで行動しない trueで行動する
        bAct = false;
        //回転は0にしておく
        Act_Num = 0;
        //回転フラグはオフにするよ
        Rotflg = false;
        //デフォルトの動きをするかどうか
        DefaultMove = false;
        //回帰フラグはオフにしておく
        ComeBackFlg = false;
        //スタート地点を保存しておく
        Start_Pos = this.transform.position;
        //スタート地点でのベクトルを保存しておく
        Start_Rot = this.transform.eulerAngles;
        //チェイスフラグはオフにしておく
        Chase = false;
        //元に戻すフラグはオフにしておく
        Change_Rot = false;
        //頭の設定
        head = transform.GetChild(0).gameObject;

        //ゲーム開始時点でその場所に当たり判定を生成する
        var Obj = new GameObject("StartPos");
        Obj.transform.position = Start_Pos;
        Obj.AddComponent<SphereCollider>();
        Obj.GetComponent<SphereCollider>().isTrigger = true;
        BC = Obj.AddComponent<BoxCollider>();
        Obj.GetComponent<BoxCollider>().size = new Vector3(0.1f,0.1f,0.1f);
        Obj.GetComponent<BoxCollider>().enabled = false;
        Obj.tag = "Enemy_Start_Pos";
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの取得
        player = GameObject.FindGameObjectWithTag("Player");

        //後で消す
        //頭オブジェクトが消えたら自身も消す
        if (head = null) {
            Destroy(this.gameObject);
            return;
        }

        //元の角度に戻す
        if (Change_Rot) {
            this.transform.eulerAngles = Start_Rot;

            if (this.transform.eulerAngles == Start_Rot) {
                DefaultMove = false;
                Change_Rot = false;
            }

        }

        if (!DefaultMove)
        {
            //一定時間で行動できるようにする
            if (!bAct)
            {
                nActTime++;
            }

            if (nActTime > ACTTIME)
            {
                //敵のステータスによって保存する内容を変更する
                //基本的には現在の座標と回転軸
                if (!bAct)
                {
                    switch (eType)
                    {
                        case ENEMY_TYPE.ENEMY_ROLL_90:
                        case ENEMY_TYPE.ENEMY_ROLL_180:
                            Act_Num = 0;
                            break;
                        case ENEMY_TYPE.WALK_ENEMY:
                            trans = this.transform.position;
                            break;
                    }
                }
                bAct = true;
            }

            //アクションフラグがオンだったら構想する
            if (bAct)
            {
                //敵の種類ごとに行動パターンを変更する
                switch (eType)
                {
                    case ENEMY_TYPE.ENEMY_ROLL_90:
                        //一定時間ごとに90度回転させる
                        this.transform.Rotate(new Vector3(0, 1, 0));
                        Act_Num++;
                        if (Act_Num >= 90)
                        {
                            Act_Num = 0;
                            bAct = false;
                            nActTime = 0;
                        }
                        break;
                    case ENEMY_TYPE.ENEMY_ROLL_180:
                        //一定時間ごとに180度回転させる
                        this.transform.Rotate(new Vector3(0, 1, 0));
                        Act_Num++;
                        if (Act_Num >= 180)
                        {
                            Act_Num = 0;
                            bAct = false;
                            nActTime = 0;
                        }
                        break;
                    case ENEMY_TYPE.WALK_ENEMY:
                        //移動させるか回転させるか
                        if (Rotflg)
                        {
                            //一定時間ごとに180度回転させる
                            this.transform.Rotate(new Vector3(0, 1, 0));
                            Act_Num++;
                            if (Act_Num >= 180)
                            {
                                Act_Num = 0;
                                bAct = false;
                                nActTime = 0;
                                Rotflg = false;
                            }
                        }
                        else
                        {
                            //移動時間を制御する
                            Act_Num++;
                            if (Act_Num > MOVE_AREA)
                            {
                                //回転させる
                                Rotflg = true;
                                Act_Num = 0;
                            }
                            else
                            {
                                //正面方向に移動させる
                                this.transform.position += this.transform.forward * Time.deltaTime * WALK_SPEED;
                            }
                        }
                        break;
                    case ENEMY_TYPE.STOP_ENEMY:

                        break;
                }
            }
        }
        else {
            //チェイスフラグをオフだった場合
            if (!Chase) {
                //元の場所に戻す
                ComeBackFlg = true;
                //内部の当たり判定をアクティブにする
                BC.GetComponent<BoxCollider>().enabled = true;

                //回転もさせる
                Quaternion lookRotation = Quaternion.LookRotation(Start_Pos - this.transform.position,Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;
                transform.rotation = Quaternion.Lerp(this.transform.rotation,lookRotation,0.1f);

                //元の場所に戻る動く
                transform.position = Vector3.MoveTowards(this.transform.position, Start_Pos, Time.deltaTime);
            }

        }

        Debug.Log("DefaultMove:"+ DefaultMove);
        Debug.Log("ComeBackFlg:" + ComeBackFlg);
        Debug.Log("Chase:" + Chase);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //プレイヤーの方向に向かってくる---------------------------------------------------------------------------------------
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - this.transform.position, Vector3.up);
            Debug.Log(234567);

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
                        if (bChaseFlg) {
                            transform.Translate(p);
                        }

                        nBullet_Time--;
                        if (nBullet_Time < 0)
                        {
                            if (bAttackFlg) {
                                //弾を発射する
                                Vector3 bulletPosition = firingPoint.transform.position;
                                //上で取得した場所に弾を出現
                                GameObject newBall = Instantiate(bullet, bulletPosition, this.transform.rotation);
                                // 出現させたボールのforward(z軸方向)
                                Vector3 directions = newBall.transform.forward;
                                // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
                                newBall.GetComponent<Rigidbody>().AddForce(direction * bullet_speed, ForceMode.Impulse);
                                // 出現させたボールの名前を"bullet"に変更
                                newBall.name = bullet.name;
                                // 出現させたボールを0.8秒後に消す
                                Destroy(newBall, 2.0f);
                                nBullet_Time = bullet_deltTime;
                            }
                        }
                    }
                }

            }
            //-----------------------------------------------------------------------------------------------------------------------
        }
    }

    //タグで当たり判定を取る
    private void OnCollisionEnter(Collision collision)
    {
        //矢が当たった場合、自身と矢を消滅させる
        if (collision.gameObject.tag == "Arrow")
        {
            //スコアを加算させる
            WaveManager WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            WM.AddScore(100);



            //HPを回復させる
            GameObject obj = GameObject.Find("unitychan");

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
