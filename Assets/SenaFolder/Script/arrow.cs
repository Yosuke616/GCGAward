using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private enum STATE_ARROW
    {
        ARROW_CHARGE = 0,       // チャージ状態
        ARROW_NORMAL,           // 通常状態
        ARROW_CHARGEMAX,        // 最大チャージ状態
    }

    private STATE_ARROW g_state;
    [SerializeField] private GameObject PrefabArrow;       // 矢のオブジェクト
    [SerializeField] private GameObject spawner;
    [SerializeField] private float maxChargeTime;
    private GameObject objArrow;
    private float fChargeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        g_state = STATE_ARROW.ARROW_NORMAL;
        fChargeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 更新処理
        UpdateState(g_state);
        // 左クリックでチャージ
        if (Input.GetMouseButtonDown(0))
            ChangeState(STATE_ARROW.ARROW_CHARGE);      // チャージ状態に変更する

        // チャージ中に左クリック離されたらチャージ解除
        if (g_state == STATE_ARROW.ARROW_CHARGE || g_state == STATE_ARROW.ARROW_CHARGEMAX)
            if(Input.GetMouseButtonUp(0))
            ChangeState(STATE_ARROW.ARROW_NORMAL);      // 通常状態に変更する

        Debug.Log(g_state);
        Debug.Log("Charge" + (int)fChargeTime);
    }

    /*
    * @brief 状態を変更した時に一度だけ呼ばれる関数
    * @param changeState 変更する先の状態
    * @sa arrow::Update()
    * @details 状態を変更したいときに数値の初期化など始めに一度だけ実行する処理を入れる
    */
    private void ChangeState(STATE_ARROW changeState)
    {
        switch(changeState)
        {
            // 通常状態
            case STATE_ARROW.ARROW_NORMAL:
                g_state = STATE_ARROW.ARROW_NORMAL;
                fChargeTime = 0.0f;     // チャージを0にする
                Destroy(objArrow);      // 矢を消滅させる
                break;

            // チャージ状態
            case STATE_ARROW.ARROW_CHARGE:
                g_state = STATE_ARROW.ARROW_CHARGE;
                // 矢を武器の子オブジェクトとして出す
                objArrow = Instantiate(PrefabArrow, spawner.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
                objArrow.transform.parent = this.transform;
                break;

            // 最大チャージ状態
            case STATE_ARROW.ARROW_CHARGEMAX:
                g_state = STATE_ARROW.ARROW_CHARGEMAX;
                break;
        }
    }
    
    /*
    * @brief 状態ごとの更新処理
    * @param UpdateState 更新する状態
    * @sa arrow::Update()
    * @details 矢の状態を取得して毎フレーム実行する処理を書く
    */
    private void UpdateState(STATE_ARROW UpdateState)
    {
        switch(UpdateState)
        {
            // 通常状態
            case STATE_ARROW.ARROW_NORMAL:
                break;

            // チャージ状態
            case STATE_ARROW.ARROW_CHARGE:
                fChargeTime += Time.deltaTime;
                // maxChargeTime以上チャージすると最大チャージ状態にする
                if (fChargeTime > maxChargeTime)
                    ChangeState(STATE_ARROW.ARROW_CHARGEMAX);
                break;

            // 最大チャージ状態
            case STATE_ARROW.ARROW_CHARGEMAX:
                fChargeTime = maxChargeTime;
                break;
        }
    }
   

}
