using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarBackGround : MonoBehaviour
{
    #region serialize field
    [Header("演出開始時間")]
    [SerializeField] private float fStartTime;
    [Header("演出継続時間"),Range(0.01f, 2.0f)]
    [SerializeField] private float fStagingTime;
    #endregion

    #region variable
    private float fPerChangeValue;          // 1フレームで変更する量
    private float fChangeValue;             // 変更する量
    private int nPlayerMaxHp;               // プレイヤーの最大HP
    private GameObject objPlayer;           // プレイヤーのオブジェクト
    private Slider slider;
    private bool isMove;                    // スライダーが動いているかどうか
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの情報の取得
        objPlayer = GameObject.FindWithTag("Player").gameObject;        // プレイヤーのオブジェクトを取得する
        nPlayerMaxHp = objPlayer.GetComponent<CCharactorManager>().nMaxHp;  // プレイヤーの最大HPを取得する

        // スライダーの値の設定
        slider = GetComponent<Slider>();
        slider.maxValue = nPlayerMaxHp;         // スライダーの最大値
        slider.value = nPlayerMaxHp;            // スライダーの現在値

        // フラグの初期化
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            slider.value -= fPerChangeValue;

            if (slider.value <= fChangeValue)
                isMove = false;
        }
    }

    /*
     * @brief バーの値を変更する
     * @param num 変更する量
     * @sa 矢が発射されたとき/敵に攻撃を受けた時
　  */
    #region move bar
    public void MoveBar(int num)
    {
        // プレイヤーのHPを減らす場合
        //if (num < 0)
        // 変更先の値を格納する
        fChangeValue = slider.value + num;
        // 1フレームで変更する量を計算する
        fPerChangeValue = Mathf.Abs(num) / (60.0f * fStagingTime);
        Debug.Log("バーが動きます");
        StartCoroutine("setSliderMove");
    }
    #endregion 

    private IEnumerator setSliderMove()
    {
        // 演出開始時間が経過した後、スライダーを動かす
        yield return new WaitForSeconds(fStartTime);
        isMove = true;      // スライダーを動かす
    }
}
