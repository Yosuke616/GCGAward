using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PauseScrip : MonoBehaviour
{
    //�K�v�ɂȂ��Ă���{�^���̐ݒ�
    [SerializeField] private GameObject Option_Btn;
    [SerializeField] private GameObject Controll_Btn;
    [SerializeField] private GameObject Title_Btn;

    //����ɏo��������UI
    [SerializeField] private GameObject Option_UI;
    [SerializeField] private GameObject Controll_UI;
    [SerializeField] private GameObject Title_Ui;

    //�y�z���o���ē������Ă���
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //�O���ɓ����������ɓ�����
    //true�ŊO�� false�œ���
    private bool bInOrOut;
    private int nCnt;
    //���𓮂������Ƃ��Ɉ�񂾂����W��ς���ϐ�
    private bool bChangeFlg;

    //����ł��邩�ǂ����̃t���O
    //true�ő���ł��� false�ő���s��
    private bool bPause;
    

    //�K�v�ɂȂ��Ă���{�^����ǉ����Ă���
    public enum PAUSE_BUTTON {
        OPTION_BUTTON,
        CONTROLL_BUTTON,
        TITLE_BUTTON,

        MAXBUTTON
    }

    private PAUSE_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        eButton = PAUSE_BUTTON.OPTION_BUTTON;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;
        bPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        //����t���O���I���̎��ɑ���ł���
        if (bPause) {
            //�|�[�Y���j���[�ɓ��������̑���
            //�傫�����f�t�H���g�ɕύX���Ă���
            Option_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Controll_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Title_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

            //�}�E�X�Ń{�^����T��

            //�I�u�W�F�N�g��ω�������ϐ����쐬����
            GameObject ChangeObj = SetPointObj();

            //�g�傷��
            ChangeObj.transform.localScale = new Vector3(1.25f,1.25f,1.25f);

            //�y�z�𓮂������
            MoveBrackets();

        }
        else {
            //�I�v�V�����⑀������̒��g�̑���

        }
    }

    private GameObject SetPointObj() {
        //�I�΂�Ă���񋓑̂ɂ���ď���������e��ύX����
        GameObject obj = null;
        switch (eButton)
        {
            case PAUSE_BUTTON.OPTION_BUTTON:
                obj = Option_Btn;
                break;
            case PAUSE_BUTTON.CONTROLL_BUTTON:
                obj = Controll_Btn;
                break;
            case PAUSE_BUTTON.TITLE_BUTTON:
                obj = Title_Btn;
                break;
        }

        if (bChangeFlg) {
            Right_Bracket.transform.position = new Vector3(obj.transform.position.x + 200.0f, obj.transform.position.y, obj.transform.position.z);
            Left_Bracket.transform.position = new Vector3(obj.transform.position.x - 150.0f, obj.transform.position.y, obj.transform.position.z);
            bChangeFlg = false;
        }

        return obj;
    }

    public void SetButton(int nButton) {
        PAUSE_BUTTON btn = (PAUSE_BUTTON)Enum.ToObject(typeof(PAUSE_BUTTON),nButton);
        eButton = btn;
    }

    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        bInOrOut = true;
    }

    private void MoveBrackets() {
        if (bInOrOut)
        {
            //�O��
            Right_Bracket.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30)
            {
                nCnt = 0;
                bInOrOut = false;
            }

        }
        else
        {
            //����
            Right_Bracket.transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position -= new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30)
            {
                nCnt = 0;
                bInOrOut = true;
            }
        }
    }

}
