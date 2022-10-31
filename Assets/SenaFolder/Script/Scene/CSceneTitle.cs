using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    [SerializeField] public AudioClip[] audioClips;

    private MODE mode;
    private MODE oldMode;
    private bool isMouse;
    public AudioSource audioSource;
    private bool useController;
    private float ControllerDeadZone = 0.5f;
    private bool dpadFlg;
    //private Image[] UIImages;
    //private List<List<Sprite>> SpriteList;
    // Start is called before the first frame update
    void Start()
    {
        if(Gamepad.current == null)
        {
            useController = false; 
        }
        else
        {
            useController = true;
        }
        //fadeManager.SceneIn();
        mode = MODE.START;
        SetUI(mode, true);
        isMouse = false;
        audioSource = GetComponent<AudioSource>();
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
            // 効果音再生
            audioSource.PlayOneShot(audioClips[0]);
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
            // 効果音再生
            audioSource.PlayOneShot(audioClips[1]);
            // シーン遷移
            ChangeScene(mode);
        }
        if(useController)
        {
            // キーボード上キー / コントローラー上ボタン →　上選択
            if ((Gamepad.current.leftStick.ReadValue().y>ControllerDeadZone||(Gamepad.current.dpad.ReadValue().y>ControllerDeadZone))&&!dpadFlg)
            {
                dpadFlg = true;
                --mode;

                if (mode < MODE.START)
                {
                    mode = MODE.QUIT;
                }
            }

            // キーボード下キー / コントローラー下ボタン →　上選択
            if ((Gamepad.current.leftStick.ReadValue().y < -ControllerDeadZone||(Gamepad.current.dpad.ReadValue().y < -ControllerDeadZone)) && !dpadFlg)
            {
                dpadFlg = true;
                ++mode;

                if (mode >= MODE.MODE_MAX)
                {
                    mode = MODE.START;
                }
            }
            if(dpadFlg&& ((Gamepad.current.dpad.ReadValue().y == 0)&&(Gamepad.current.leftStick.ReadValue().y ==0)))
            {
                dpadFlg = false;
            }
            // Enterキー→決定
            if (Gamepad.current.aButton.isPressed)
            {
                // 効果音再生
                audioSource.PlayOneShot(audioClips[1]);
                // シーン遷移
                ChangeScene(mode);
            }
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
    public void ChangeScene(MODE selectMode)
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
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                break;
        }
    }
    #endregion
}
