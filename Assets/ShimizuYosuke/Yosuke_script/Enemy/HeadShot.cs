using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    //敵オブジェクト
    [SerializeField] private GameObject enemy; 

    //スコアを加算する為のやつ
    [SerializeField] private GameObject score;
    private CountText scScore;     // スコアの情報格納用

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        scScore = score.GetComponent<CountText>();
        pos = enemy.transform.position;
        pos.y = enemy.transform.position.y + 1;
        this.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        //消す
        if (enemy == null) {
            Destroy(this.gameObject);
            return;
        }

        //常に敵に追従させる
        Vector3 pos = this.transform.position;
        pos = enemy.transform.position;
        pos.y = enemy.transform.position.y + 1;
        this.transform.position = pos;


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Arrow") {
            //スコアを加算させる
            scScore.AddScore(100000);

            //オブジェクトを消滅させる
            Destroy(collision.gameObject);      // 矢を消滅させる
            Destroy(this.gameObject);      // 自身を消滅させる
        }
    }
}
