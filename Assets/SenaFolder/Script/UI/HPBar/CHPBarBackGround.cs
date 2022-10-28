using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPBarBackGround : MonoBehaviour
{
    #region serialize field
    [Header("演出開始時間")]
    [SerializeField] private float fStartTime;
    [Header("1秒に減るHPの量"),Range(1, 10)]
    [SerializeField] private float fValuePerSec;
    #endregion

    #region variable
    private float fPerChangeValue;          // 1フレームで変更する量
    private float fChangeValue;             // 変更する量
    private int nMaxHp;               // プレイヤーの最大HP
    private GameObject objParent;           // プレイヤーのオブジェクト
    private Slider slider;
    private bool isMove;                    // スライダーが動いているかどうか
    private bool isValueDec;                // 数値を減らすのか増やすのか
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの情報の取得
        objParent = transform.root.gameObject;
        if(objParent.name == "Canvas")
        {
            objParent = GameObject.FindWithTag("Player");
        }

        nMaxHp = objParent.GetComponent<CCharactorManager>().nMaxHp;  // プレイヤーの最大HPを取得する

        // スライダーの値の設定
        slider = GetComponent<Slider>();
        slider.maxValue = nMaxHp;         // スライダーの最大値
        slider.value = nMaxHp;            // スライダーの現在値

        // フラグの初期化
        isMove = false;
        fPerChangeValue = 60.0f / 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isMove);
        if (isMove)
        {
            // 値を減らすとき
            if (isValueDec)
            {
                slider.value -= fPerChangeValue * Time.deltaTime;
                if (slider.value <= fChangeValue)
                    isMove = false;
            }
            // 値を増やすとき
            else 
            {
                slider.value += fPerChangeValue * Time.deltaTime;
                if (slider.value >= fChangeValue)
                    isMove = false;
            }
        }
        else
        {
            slider.value = objParent.GetComponent<CCharactorManager>().nCurrentHp;
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
        // スライダーが動いている最中であれば、最終の値に変更する
        if(isMove)
        {
            slider.value = fChangeValue;
        }
        isValueDec = num < 0;
        // 変更先の値を格納する
        fChangeValue = slider.value + num;
        //Debug.Log("バーが動きます");
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
