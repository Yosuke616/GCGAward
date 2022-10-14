using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFieldUIController : MonoBehaviour
{
    #region serialize field
    [Header("テクスチャ")]
    [SerializeField] private Sprite[] UITex;        // 貼るアニメーション
    [Header("切替間隔時間(秒)")]
    [SerializeField] private float nChangeTime;       // 切り替わるまでの時間
    #endregion

    #region variable
    private Image imgUI;        // テクスチャを貼るImage
    private float nCurrentTime;     // 現在の経過時間
    private int nTexNum;            // テクスチャの合計枚数
    private int nCurrentTexNum;     // 現在表示しているテクスチャの番号
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentTime = 0.0f;
        nCurrentTexNum = 0;
        imgUI = GetComponent<Image>();
        imgUI.sprite = UITex[nCurrentTexNum];
        nTexNum = UITex.Length;
    }

    // Update is called once per frame
    void Update()
    {
        nCurrentTime += Time.deltaTime;
        if(nCurrentTime > nChangeTime)
        {
            // テクスチャの切替
            ++nCurrentTexNum;
            if (nCurrentTexNum > nTexNum - 1)
                nCurrentTexNum = 0;
            imgUI.sprite = UITex[nCurrentTexNum];
            nCurrentTime = 0.0f;
        }
    }
}
