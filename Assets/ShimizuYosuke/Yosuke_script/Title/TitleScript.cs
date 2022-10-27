using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class TitleScript : MonoBehaviour
{
    //�I�u�W�F�N�g�̐ݒ�
    [SerializeField] GameObject Start_Btn;
    [SerializeField] GameObject Tutorial_Btn;
    [SerializeField] GameObject Option_Btn;
    [SerializeField] GameObject End_Btn;

    //�I�v�V�����o����[��
    [SerializeField] private GameObject Pause_UI;

    //�y�z���o���ē������Ă���
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //�O���ɓ����������ɓ�����
    //true�ŊO�� false�œ���
    private bool bInOrOut;
    private int nCnt;
    //���𓮂������Ƃ��Ɉ�񂾂����W��ς���ϐ�
    private bool bChangeFlg;

    //���Ԑݒ�
    [Header("�ǂ̈ʂ̎��ԂŃ{�^�����䂷�邩")]
    [SerializeField] private int DELTTIME = 10;
    private int nDeltTime;

    //�f�t�H���g�̏ꏊ
    private Vector3 Str_Pos;
    private Vector3 Tuto_Pos;
    private Vector3 Opt_Pos;
    private Vector3 End_Pos;

    //�K�v�ɂȂ��Ă���{�^����ǉ����Ă���
    public enum TITLE_BUTTON {
        START_BUTTON,
        TUTORIAL_BUTTON,
        OPTION_BUTTON,
        END_BUTTON,

        MAX_BUTTON
    }

    private TITLE_BUTTON eButton;

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        //���߂͎n�߂�{�^����I�����Ă���
        eButton = TITLE_BUTTON.START_BUTTON;
        Pause_UI.SetActive(false);

        //�f�t�H���g�̏ꏊ��ۑ����Ă���
        Str_Pos = Start_Btn.transform.position;
        Tuto_Pos = Tutorial_Btn.transform.position;
        Opt_Pos = Option_Btn.transform.position;
        End_Pos = End_Btn.transform.position;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        nDeltTime++;

        //�{�^���ł̏���
        //���Ԃ�������x���ƈړ�����悤�ɂ���
        if (nDeltTime > DELTTIME) {
            //�{�^���̑I�����ł���悤�ɂ���
            if (Input.GetKey(KeyCode.W)) {
                eButton--;
                if (eButton < 0) {
                    eButton = TITLE_BUTTON.MAX_BUTTON - 1;
                }
                bChangeFlg = true;
                nCnt = 0;
                nDeltTime = 0;
                bInOrOut = true;
            }
            if (Input.GetKey(KeyCode.S)) {
                eButton++;
                if (eButton >= TITLE_BUTTON.MAX_BUTTON) {
                    eButton = 0;
                }
                bChangeFlg = true;
                nCnt = 0;
                nDeltTime = 0;
                bInOrOut = true;
            }
        }

        //�傫�����f�t�H���g�ɕς���
        Start_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Tutorial_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        Option_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        End_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //�ꏊ���f�t�H���g�ɕۑ����Ă���
        Start_Btn.transform.position = Str_Pos;
        Tutorial_Btn.transform.position = Tuto_Pos;
        Option_Btn.transform.position = Opt_Pos;
        End_Btn.transform.position = End_Pos;
        //���{����\���ɂ��Ă���
        GameObject Str_Chil = Start_Btn.transform.GetChild(0).gameObject;
        GameObject Tuto_Chil = Tutorial_Btn.transform.GetChild(0).gameObject;
        GameObject Opt_Chil = Option_Btn.transform.GetChild(0).gameObject;
        GameObject End_Chil = End_Btn.transform.GetChild(0).gameObject;
        Str_Chil.SetActive(false);
        Tuto_Chil.SetActive(false);
        Opt_Chil.SetActive(false);
        End_Chil.SetActive(false);

        //�I�΂�Ă���񋓑̕ϐ��ɂ���ď���������e��ς���
        switch (eButton) {
            case TITLE_BUTTON.START_BUTTON:
                //���W�������E�ɂ��炷
                Start_Btn.transform.position = new Vector3(Str_Pos.x - 100.0f, Str_Pos.y, Str_Pos.z);
                Start_Btn.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                //���{����o��
                Str_Chil.SetActive(true);
                //�������̏ꏊ��ύX����
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Start_Btn.transform.position.x + 240.0f, Start_Btn.transform.position.y, Start_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Start_Btn.transform.position.x - 250.0f, Start_Btn.transform.position.y, Start_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.TUTORIAL_BUTTON:
                //���W�����ɂ��炷
                Tutorial_Btn.transform.position = new Vector3(Tuto_Pos.x - 100.0f,Tuto_Pos.y,Tuto_Pos.z);
                Tutorial_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //���{����o��
                Tuto_Chil.SetActive(true);
                //�������̏ꏊ��ύX����
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Tutorial_Btn.transform.position.x + 335.0f, Tutorial_Btn.transform.position.y, Tutorial_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Tutorial_Btn.transform.position.x - 325.0f, Tutorial_Btn.transform.position.y, Tutorial_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.OPTION_BUTTON:
                Option_Btn.transform.position = new Vector3(Opt_Pos.x - 100.0f, Opt_Pos.y, Opt_Pos.z);
                Option_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Opt_Chil.SetActive(true);
                //�������̏ꏊ��ύX����
                if (bChangeFlg) {
                    Right_Bracket.transform.position = new Vector3(Option_Btn.transform.position.x + 260.0f, Option_Btn.transform.position.y-20.0f, Option_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(Option_Btn.transform.position.x - 250.0f, Option_Btn.transform.position.y-20.0f, Option_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.END_BUTTON:
                End_Btn.transform.position = new Vector3(End_Pos.x - 100.0f, End_Pos.y, End_Pos.z);
                End_Btn.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                End_Chil.SetActive(true);
                //�������̏ꏊ��ύX����
                if (bChangeFlg)
                {
                    Right_Bracket.transform.position = new Vector3(End_Btn.transform.position.x + 175.0f, End_Btn.transform.position.y+15.0f, End_Btn.transform.position.z);
                    Left_Bracket.transform.position = new Vector3(End_Btn.transform.position.x - 125.0f, End_Btn.transform.position.y+15.0f, End_Btn.transform.position.z);
                    bChangeFlg = false;
                }
                break;
            case TITLE_BUTTON.MAX_BUTTON:
                //�傫�����f�t�H���g�ɕς���
                Start_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Tutorial_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Option_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                End_Btn.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //�ꏊ���f�t�H���g�ɕۑ����Ă���
                Start_Btn.transform.position = Str_Pos;
                Tutorial_Btn.transform.position = Tuto_Pos;
                Option_Btn.transform.position = Opt_Pos;
                End_Btn.transform.position = End_Pos;
                //���{����\���ɂ��Ă���
                Str_Chil = Start_Btn.transform.GetChild(0).gameObject;
                Tuto_Chil = Tutorial_Btn.transform.GetChild(0).gameObject;
                Opt_Chil = Option_Btn.transform.GetChild(0).gameObject;
                End_Chil = End_Btn.transform.GetChild(0).gameObject;
                Str_Chil.SetActive(false);
                Tuto_Chil.SetActive(false);
                Opt_Chil.SetActive(false);
                End_Chil.SetActive(false);
                break;
            default: break;
        }

        //�J�[�\�����������֐�
        //�������̏ꏊ��ύX����
        MoveBrackets();


        //�{�^���������ꂽ�Ƃ��̏���
        if (Input.GetKey(KeyCode.Return) || Input.GetMouseButtonDown(0)) {
            switch (eButton)
            {
                case TITLE_BUTTON.START_BUTTON:
                    SceneManager.LoadScene("YosukeScene");
                    break;
                case TITLE_BUTTON.TUTORIAL_BUTTON:
                    SceneManager.LoadScene("GameScene");
                    break;
                case TITLE_BUTTON.OPTION_BUTTON:
                    if (Mathf.Approximately(Time.timeScale, 1f))
                    {
                        Time.timeScale = 0f;
                        Pause_UI.SetActive(true);
                    }
                    break;
                case TITLE_BUTTON.END_BUTTON:
                    //UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
                    break;
                default: break;
            }
        }

    }

    //�{�^���ɃZ�b�g������
    public void SetButton(int nButton) {
        TITLE_BUTTON btn = (TITLE_BUTTON)Enum.ToObject(typeof(TITLE_BUTTON), nButton);
        eButton = btn;
    }

    private void MoveBrackets() {
        if (bInOrOut)
        {
            //�O��
            Right_Bracket.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            Left_Bracket.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);

            nCnt++;
            if (nCnt > 30) {
                nCnt = 0;
                bInOrOut = false;
            }

        }
        else {
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

    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        nDeltTime = 0;
        bInOrOut = true;
    }
}
