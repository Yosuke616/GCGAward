using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [Header("フェードにかける時間")]
    [SerializeField] private float fFadeTime;
    [SerializeField] private SceneFade FadePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneOut(string sceneName)
    {
        FadePanel.gameObject.SetActive(true);
        // タイトルシーンに遷移するためにフェードアウトする
        FadePanel.PanelFade(SceneFade.STATE_FADE.FADE_OUT, fFadeTime);
        FadePanel.GetChangeSceneName(sceneName);
    }

    public void SceneIn()
    {
        // ゲーム開始時はフェードインする
        FadePanel.PanelFade(SceneFade.STATE_FADE.FADE_IN, fFadeTime);
        StartCoroutine("PanelDisable");
    }

    private IEnumerator PanelDisable()
    {
        yield return new WaitForSeconds(fFadeTime);
        // ゲーム画面のUIを認識させる為にフェードパネルを非アクティブにする
        FadePanel.gameObject.SetActive(false);
    }
}
