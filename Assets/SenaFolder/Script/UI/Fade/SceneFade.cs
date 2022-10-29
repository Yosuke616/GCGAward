using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static CSceneName;

public class SceneFade : MonoBehaviour
{
    //[System.NonSerialized]
    public enum STATE_FADE
    {
        FADE_IN = 0,
        FADE_OUT,
        FADE_FIN,
        FADE_MAX,
    }
    [SerializeField] private GameObject LoadUI;
    private Image image;            // 画像の情報
    private float fTimer;           // 経過時間
    private STATE_FADE fadeState;   // フェードしているかどうか
    private float fMaxTime;        // フェードにかける時間
    private string szScene = null;
    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        LoadUI.SetActive(false);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = 0.0f;
        switch (fadeState)
        {
            // フェードイン中
            case STATE_FADE.FADE_IN:
                if (UpdateFadeTimer())
                {
                    Debug.Log("FadeFin");
                    fadeState = STATE_FADE.FADE_FIN;
                }
                alpha = (fMaxTime - fTimer) / fMaxTime;
                image.color = new Color(0, 0, 0, alpha);
                break;

            case STATE_FADE.FADE_OUT:
                if (UpdateFadeTimer())
                {
                    Debug.Log("FadeFin");
                    fadeState = STATE_FADE.FADE_FIN;
                    Debug.Log("ChangeScene");
                    LoadUI.SetActive(true);
                    StartCoroutine("LoadData");
                }
                alpha = fTimer / fMaxTime;
                image.color = new Color(0, 0, 0, alpha);
                break;

            // フェード終了
            case STATE_FADE.FADE_FIN:
                fTimer = 0.0f;
                break;
        }
        Debug.Log(fadeState);

    }

    public void PanelFade(STATE_FADE fade, float time)
    {
        fMaxTime = time;       // タイマーの設定
        fadeState = fade;       // フェードモードの設定
        Debug.Log(fadeState);
    }

    public void GetChangeSceneName(string name)
    {
        szScene = name;
    }

    private bool UpdateFadeTimer()
    {
        // タイマー更新
        fTimer += Time.deltaTime;
        // 一定時間を経過しているかを返す
        return fTimer > fMaxTime;
    }

    private IEnumerator LoadData()
    {
        async = SceneManager.LoadSceneAsync(szScene);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
