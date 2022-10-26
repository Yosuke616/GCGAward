using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [Header("�t�F�[�h�ɂ����鎞��")]
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
        // �^�C�g���V�[���ɑJ�ڂ��邽�߂Ƀt�F�[�h�A�E�g����
        FadePanel.PanelFade(SceneFade.STATE_FADE.FADE_OUT, fFadeTime);
        FadePanel.GetChangeSceneName(sceneName);
    }

    public void SceneIn()
    {
        // �Q�[���J�n���̓t�F�[�h�C������
        FadePanel.PanelFade(SceneFade.STATE_FADE.FADE_IN, fFadeTime);
        StartCoroutine("PanelDisable");
    }

    private IEnumerator PanelDisable()
    {
        yield return new WaitForSeconds(fFadeTime);
        // �Q�[����ʂ�UI��F��������ׂɃt�F�[�h�p�l�����A�N�e�B�u�ɂ���
        FadePanel.gameObject.SetActive(false);
    }
}
