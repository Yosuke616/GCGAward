using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class GameOverScript : MonoBehaviour
{
    //�K�v�ɂȂ��Ă���{�^���̐ݒ�
    [SerializeField] private GameObject Retory_Btn;
    [SerializeField] private GameObject Title_Btn;

    //�y�z���o���ē������Ă���
    [SerializeField] private GameObject Right_Bracket;
    [SerializeField] private GameObject Left_Bracket;

    //�������擾����ׂɕϐ���p�ӂ��Ă���
    [Header("�X�R�A�p�e�L�X�g")]
    [SerializeField] private GameObject HeadShot;
    [SerializeField] private GameObject BreakEnemy;
    [SerializeField] private GameObject WaveRun;
    [SerializeField] private GameObject TotalScore;

    //�ݒ肷�邽�߂̕ϐ�
    private Text Head;
    private Text Break;
    private Text Wave;
    private Text Total;


    //�O���ɓ����������ɓ�����
    //true�ŊO�� false�œ���
    private bool bInOrOut;
    private int nCnt;
    //���𓮂������Ƃ��Ɉ�񂾂����W��ς���ϐ�
    private bool bChangeFlg;

    //�Q�[���I�[�o�[UI��������悤�ɂ���t���O
    //true�œ������� false�œ������Ȃ�
    private bool bUseFlg;

    //�X�R�A(��)����X�R�A���������Ă�����
    private WaveManager WM; 

    //�K�v�ɂȂ��Ă���{�^����ǉ�����
    public enum RESULT_BUTTON {
        RETORY_BUTTON,
        TITLE_BUTTON,

        MAX_BUTTON
    }

    private RESULT_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        bUseFlg = false;
        eButton = RESULT_BUTTON.RETORY_BUTTON;
        nCnt = 0;
        bChangeFlg = true;
        bInOrOut = true;

        //�����̃R���|�[�l���g�̎擾
        Head = HeadShot.GetComponent<Text>();
        Break = BreakEnemy.GetComponent<Text>();
        Wave = WaveRun.GetComponent<Text>();
        Total = TotalScore.GetComponent<Text>();

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        int num;
        //�����𐔎��ɕς���
        num = WM.GetHeadShot();
        Head.text = num.ToString();
        num = WM.GetBreakEnemy();
        Break.text = num.ToString();
        num = WM.GetWave();
        Wave.text = num.ToString();
        num = WM.GetScore();
        Total.text = num.ToString();

    }

    // Update is called once per frame
    void Update()
    {

            Debug.Log(2345676543);
        if (bUseFlg) {
            //�X�R�A�\��
            int num;
            //�����𐔎��ɕς���
            num = WM.GetHeadShot();
            Debug.Log(num);
            Head.text = num.ToString();
            num = WM.GetBreakEnemy();
            Break.text = num.ToString();
            num = WM.GetWave();
            Wave.text = num.ToString();
            num = WM.GetScore();
            Total.text = num.ToString();

            //�{�^�����f�t�H���g�̑傫���ɂ��Ă���
            Retory_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            Title_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

            //�I�u�W�F�N�g��ω�������֐����쐬����
            GameObject ChangeObj = SetPointObj();

            //�g�傷��
            ChangeObj.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

            //�y�z�𓮂����֐�
            MoveBrackets();

            //�N���b�N���Č���ł���
            if (Input.GetMouseButtonDown(0)) {
                SelectButton();
            }
        }
    }

    public void SetUseFlg(bool bUse) {
        bUseFlg = bUse;
    }

    private GameObject SetPointObj() {
        //�I�΂�Ă���񋓑̂ɂ���ď���������e��ύX����
        GameObject obj = null;
        switch(eButton){
            case RESULT_BUTTON.RETORY_BUTTON:
                obj = Retory_Btn;
                break;
            case RESULT_BUTTON.TITLE_BUTTON:
                obj = Title_Btn;
                break;
        }

        if (bChangeFlg) {
            Right_Bracket.transform.position = new Vector3(obj.transform.position.x + 185.0f, obj.transform.position.y, obj.transform.position.z);
            Left_Bracket.transform.position = new Vector3(obj.transform.position.x - 50.0f, obj.transform.position.y, obj.transform.position.z);
            bChangeFlg = false;
        }

        return obj;
    }

    private void MoveBrackets(){
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
        else {
            //����
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

    //�}�E�X�ŗ񋓑̂��Z�b�g������
    public void SetButton(int nButton) {
        RESULT_BUTTON btn = (RESULT_BUTTON)Enum.ToObject(typeof(RESULT_BUTTON),nButton);
        eButton = btn;
    }

    //�J�[�\���ړ���������
    public void SetButtonAny() {
        bChangeFlg = true;
        nCnt = 0;
        bInOrOut = true;
    }

    private void SelectButton() {
        switch (eButton) {
            case RESULT_BUTTON.RETORY_BUTTON:
                SceneManager.LoadScene("TitleScene2");
                break;
            case RESULT_BUTTON.TITLE_BUTTON:
                SceneManager.LoadScene("TitleScene2");
                break;
        }
    }

}
