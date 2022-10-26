using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSceneTitle : MonoBehaviour
{
    public enum MODE
    {
        START = 0,
        TUTORIAL,
        OPTION,
        QUIT,
        MODE_MAX,
    }
    [SerializeField] GameObject[] UIObjects;
    [SerializeField] private FadeManager fadeManager;

    private MODE mode;
    private MODE oldMode;
    private bool isMouse;
    private AudioSource audioSource;
    //private Image[] UIImages;
    //private List<List<Sprite>> SpriteList;
    // Start is called before the first frame update
    void Start()
    {
        fadeManager.SceneIn();
        mode = MODE.START;
        SetUI(mode, true);
        isMouse = false;
    }

    // Update is called once per frame
    void Update()
    {
        // マウス操作中でない場合、キー入力を受け付ける
        if (!isMouse)
        {
            // キー入力処理
            KeyInput();
        }
        // マウス入力中の時
        else
        {
            for (int check = 0; check < UIObjects.Length; ++check)
            {
                // 選択状態の場合mode変数を更新する
                if(UIObjects[check].GetComponent<CTitleButton>().GetIsSelected())
                {
                    mode = (MODE)check;
                    check = UIObjects.Length;       // for文を抜ける
                }
            }
        }

        // 選択が変更されている場合ボタンのテクスチャを変更する
        if (mode != oldMode)
        {
            // UI更新処理
            SetUI(oldMode, false);      // 過去のUIをリセットする
            SetUI(mode, true);          // 現在のUIを更新する
        }
        oldMode = mode;
        //Debug.Log("MODE:" + mode);
    }

    #region mouse move
    public void MouseMove(bool flg)
    {
        isMouse = flg;
    }
    #endregion

    // キー入力処理
    #region key input
    private void KeyInput()
    {
        // キーボード上キー / コントローラー上ボタン →　上選択
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            --mode;

            if (mode < MODE.START)
            {
                mode = MODE.QUIT;
            }
        }

        // キーボード下キー / コントローラー下ボタン →　上選択
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++mode;

            if (mode >= MODE.MODE_MAX)
            {
                mode = MODE.START;
            }
        }

        // Enterキー→決定
        if(Input.GetKeyDown(KeyCode.Return))
        {
            // シーン遷移
            ChangeScene(mode);
        }
    }
    #endregion

    // UI更新処理
    #region update ui
    private void SetUI(MODE selectMode, bool flg)
    {
        UIObjects[(int)selectMode].GetComponent<CTitleButton>().SetSelected(flg);
    }
    #endregion

    // シーン変更
    #region change scene
    private void ChangeScene(MODE selectMode)
    {
        switch (selectMode)
        {
            // スタート → ゲーム開始
            case MODE.START:
                fadeManager.SceneOut("GameScene");
                break;
            // チュートリアル → チュートリアル開始
            case MODE.TUTORIAL:
                fadeManager.SceneOut("TutorialScene");
                break;

            // オプション → オプション制御
            case MODE.OPTION:
                break;

            // ゲーム終了 → ゲーム終了
            case MODE.QUIT:
                break;
        }
    }
    #endregion
}
