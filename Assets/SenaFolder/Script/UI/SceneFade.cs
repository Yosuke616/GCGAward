using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    //[System.NonSerialized]
    public enum STATE_FADE
    {
        FADE_IN = 0,
        FADE_OUT,
        FADED_MAX,
    }
    private Image image;            // �摜�̏��
    private float fTimer;           // �o�ߎ���
    private bool isFade;            // �t�F�[�h���Ă��邩�ǂ���
    private float fFadeTime;        // �t�F�[�h�ɂ����鎞��
    public bool isFadeFin;

    // Start is called before the first frame update
    void Start()
    {
        isFade = false;
        isFadeFin = false;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFade)
        {
            fTimer += Time.deltaTime;       // �^�C�}�[�X�V

            // ��莞�Ԍo�߂�����t�F�[�h���I��������
            if(fTimer > fFadeTime)
            {
                isFadeFin = true;           // �t�F�[�h���I���������Ƃ�m�点��
            }
        }
    }

    public void PanelFade(STATE_FADE fadeState, float time)
    {
        isFade = true;          // �t�F�[�h���ɂ���
        fFadeTime = time;

        switch (fadeState)
        {
            // �t�F�[�h�C��
            case STATE_FADE.FADE_IN:
                //image.color.a +=  
                break;

            // �t�F�[�h�A�E�g
            case STATE_FADE.FADE_OUT:
                break;
        }
    }
}
