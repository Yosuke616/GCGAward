using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTeamLogoStaging : MonoBehaviour
{
    [Header("チームロゴ表示時間")]
    [SerializeField] private float fShowTime;
    [Header("遷移先シーンの名前")]
    [SerializeField] private string szSceneName;
    [SerializeField] private FadeManager fadeManager;

    // Start is called before the first frame update
    void Start()
    {
        fadeManager.SceneIn();
        StartCoroutine("SceneChange");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(fShowTime);
        fadeManager.SceneOut(szSceneName);
    }
}
