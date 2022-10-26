using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTeamLogoStaging : MonoBehaviour
{
    [Header("�`�[�����S�\������")]
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
            // �^�C�g���V�[���ɑJ�ڂ���
            SceneManager.LoadScene("TitleScene");
        }
    }
}
