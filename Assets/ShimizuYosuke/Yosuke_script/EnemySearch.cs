using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //敵が追いかけていくための大賞
    [SerializeField] GameObject player;

    //弾オブジェクト
    [SerializeField] GameObject bullet;
    private float fBulletspeed = 10.0f;

    //レイを使用して視界を制御する
    private RaycastHit rayCastHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

                        //弾を発射するぜ
                        var shot = Instantiate(bullet,this.transform.position, Quaternion.identity);
                        shot.GetComponent<Rigidbody>().velocity = this.transform.forward.normalized * fBulletspeed;

                        new WaitForSeconds(1.0f);
                    }
                }

            }        
            //-----------------------------------------------------------------------------------------------------------------------
        }
    }
}
