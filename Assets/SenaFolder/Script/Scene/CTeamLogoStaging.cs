using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTeamLogoStaging : MonoBehaviour
{
    [Header("チームロゴ表示時間")]
    [SerializeField] private float fShowTime;

    private float fTimer;
    // Start is called before the first frame update
    void Start()
    {
        fTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        fTimer += Time.deltaTime;

        if(fTimer > fShowTime)
        {
            // タイトルシーンに遷移する
            SceneManager.LoadScene("TitleScene");
        }
    }
}
