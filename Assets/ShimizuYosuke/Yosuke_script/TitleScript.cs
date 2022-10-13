using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScript : MonoBehaviour
{
    //オブジェクトの設定
    [SerializeField] GameObject Start_Btn;
    [SerializeField] GameObject Option_Btn;
    [SerializeField] GameObject End_Btn;

    //オプション出すよーん
    [SerializeField] private GameObject Pause_UI;

    //時間設定
    [Header("どの位の時間でボタン制御するか")]
    [SerializeField] private int DELTTIME = 10;
    private int nDeltTime;

    //必要になってくるボタンを追加していく
    public enum TITLE_BUTTON {
        START_BUTTON,
        OPTION_BUTTON,
        END_BUTTON,

        MAX_BUTTON
    }

    private TITLE_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        //初めは始めるボタンを選択しておく
        eButton = TITLE_BUTTON.START_BUTTON;
        Pause_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        nDeltTime++;
        //時間がある程度立つと移動するようにする
        if (nDeltTime > DELTTIME) {
            //ボタンの選択ができるようにする
            if (Input.GetKey(KeyCode.W)) {
                eButton--;
                if (eButton < 0) {
                    eButton = TITLE_BUTTON.MAX_BUTTON - 1;
                }
                nDeltTime = 0;
            }
            if (Input.GetKey(KeyCode.S)) {
                eButton++;
                if (eButton >= TITLE_BUTTON.MAX_BUTTON) {
                    eButton = 0;
                }
                nDeltTime = 0;
            }
        }

        //ボタンの色をデフォルトに変更する
        Start_Btn.GetComponent<Button>().image.color = Color.white; 
        Option_Btn.GetComponent<Button>().image.color = Color.white; 
        End_Btn.GetComponent<Button>().image.color = Color.white;
        //大きさもデフォルトに変える
        Start_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        Option_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        End_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        //選ばれている列挙体変数によって処理する内容を変える
        switch (eButton) {
            case TITLE_BUTTON.START_BUTTON:
                Start_Btn.GetComponent<Button>().image.color = Color.red;
                Start_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            case TITLE_BUTTON.OPTION_BUTTON:
                Option_Btn.GetComponent<Button>().image.color = Color.red;
                Option_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            case TITLE_BUTTON.END_BUTTON:
                End_Btn.GetComponent<Button>().image.color = Color.red;
                End_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            default:break;
        }

        //ボタンが押されたときの処理
        if (Input.GetKey(KeyCode.Return)) {
            switch (eButton)
            {
                case TITLE_BUTTON.START_BUTTON:
                    SceneManager.LoadScene("YosukeScene");
                    break;
                case TITLE_BUTTON.OPTION_BUTTON:
                    if (Mathf.Approximately(Time.timeScale, 1f))
                    {
                        Time.timeScale = 0f;
                        Pause_UI.SetActive(true);
                    }
                    break;
                case TITLE_BUTTON.END_BUTTON:
                    UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                    break;
                default: break;
            }
        }

    }
}
