using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //敵が追いかけていくための大賞
    [SerializeField] GameObject player;

    //弾の発射場所
    [Header("弾関係")]
    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 20.0f;

    [Header("次撃てるまでの時間")]
    [SerializeField] int deltTime = 480;
    int nTime;

    //レイを使用して視界を制御する
    private RaycastHit rayCastHit;

    //スコアを加算する為のやつ
    [SerializeField] private GameObject score;
    private CountText scScore;     // スコアの情報格納用

    //頭
    [SerializeField] private GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        nTime = deltTime;
        scScore = score.GetComponent<CountText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (head == null) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
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
                        //向きを変える
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

                        Vector3 p = new Vector3(0f, 0f, 0.05f);

                        //移動させる
                        transform.Translate(p);

                        nTime--;
                        if (nTime < 0) {
                            //弾を発射する
                            Vector3 bulletPosition = firingPoint.transform.position;
                            //上で取得した場所に弾を出現
                            GameObject newBall = Instantiate(bullet,bulletPosition,this.transform.rotation);
                            // 出現させたボールのforward(z軸方向)
                            Vector3 directions = newBall.transform.forward;
                            // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
                            newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
                            // 出現させたボールの名前を"bullet"に変更
                            newBall.name = bullet.name;
                            // 出現させたボールを0.8秒後に消す
                            Destroy(newBall, 2.0f);
                            nTime = deltTime;
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
        if (collision.gameObject.tag == "Arrow") {
            //スコアを加算させる
            scScore.AddScore(10000);

            //オブジェクトを消滅させる
            Destroy(collision.gameObject);      // 矢を消滅させる
            Destroy(this.gameObject);      // 自身を消滅させる
        }
    }

}
