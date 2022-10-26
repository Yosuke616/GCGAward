using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTitleButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("拡大率")]
    [SerializeField, Range(1.0f, 2.0f)] private float magScale;      // mag→magnification(倍率)
    [Header("テクスチャ(ノーマル→選択状態の順番)")]
    public Sprite[] texture;
    [Header("遷移先シーンの名前")]
    [SerializeField] private string szSceneName;
    [SerializeField] private CSceneTitle cScene;        // タイトルマネージャースクリプト

    private Vector2 defScale;       // スケールの初期値
    private Image image;            // ボタンの画像
    private bool isSelected;
    // Start is called before the first frame update
    void Awake()
    {
        isSelected = false;
        // 画像の情報を取得する
        image = GetComponent<Image>();
        // 初期状態の画像を設定する
        image.sprite = texture[0];
        // スケールの初期値を設定する
        defScale = transform.localScale;
    }
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ボタンが押されたときシーン遷移する
    public void OnClick()
    {
        if(szSceneName == "Option" || szSceneName == "Quit")
        {
            // オプション処理orゲーム終了処理
        }
        else 
        {
            SceneManager.LoadScene(szSceneName.ToString());
        }
    }

    // マウスがUI上に来た時
    #region on pointer enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        //SetTexture(true);               // テクスチャを変更する
        cScene.MouseMove(true);         // マウス操作中にする
        isSelected = true;
    }
    #endregion

    // マウスがUI外に出た時
    #region on pointer exit
    public void OnPointerExit(PointerEventData eventData)
    {
        //SetTexture(false);                   // テクスチャを変更する
        cScene.MouseMove(false);         // マウス操作中状態を解除する
        isSelected = false;
    }
    #endregion

    // 選択されている時に呼ばれる関数
    #region set selected
    public void SetSelected(bool flg)
    {
        isSelected = flg;
        SetTexture(isSelected);
    }
    #endregion

    // テクスチャの変更
    #region set texture
    private void SetTexture(bool flg)
    {
        // 選択されている状態に変更する
        if(flg)
        {
            transform.localScale = new Vector2(transform.localScale.x * magScale, transform.localScale.y * magScale);
            image.sprite = texture[1];
        }
        // 選択されていない状態に変更する
        else
        {
            transform.localScale = defScale;
            image.sprite = texture[0];
        }
    }
    #endregion

    // 選択中かどうかの情報を渡す
    #region get is selected
    public bool GetIsSelected()
    {
        return isSelected;
    }
    #endregion 
}
