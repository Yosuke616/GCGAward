using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleScript : MonoBehaviour
{
    //�I�u�W�F�N�g�̐ݒ�
    [SerializeField] GameObject Start_Btn;
    [SerializeField] GameObject Option_Btn;
    [SerializeField] GameObject End_Btn;

    //�I�v�V�����o����[��
    [SerializeField] private GameObject Pause_UI;

    //���Ԑݒ�
    [Header("�ǂ̈ʂ̎��ԂŃ{�^�����䂷�邩")]
    [SerializeField] private int DELTTIME = 10;
    private int nDeltTime;

    //�K�v�ɂȂ��Ă���{�^����ǉ����Ă���
    public enum TITLE_BUTTON {
        START_BUTTON,
        OPTION_BUTTON,
        END_BUTTON,

        MAX_BUTTON
    }

    private TITLE_BUTTON eButton;

    // Start is called before the first frame update
    void Start()
    {
        //���߂͎n�߂�{�^����I�����Ă���
        eButton = TITLE_BUTTON.START_BUTTON;
        Pause_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        nDeltTime++;
        //���Ԃ�������x���ƈړ�����悤�ɂ���
        if (nDeltTime > DELTTIME) {
            //�{�^���̑I�����ł���悤�ɂ���
            if (Input.GetKey(KeyCode.W)) {
                eButton--;
                if (eButton < 0) {
                    eButton = TITLE_BUTTON.MAX_BUTTON - 1;
                }
                nDeltTime = 0;
            }
            if (Input.GetKey(KeyCode.S)) {
                eButton++;
                if (eButton >= TITLE_BUTTON.MAX_BUTTON) {
                    eButton = 0;
                }
                nDeltTime = 0;
            }
        }

        //�{�^���̐F���f�t�H���g�ɕύX����
        Start_Btn.GetComponent<Button>().image.color = Color.white; 
        Option_Btn.GetComponent<Button>().image.color = Color.white; 
        End_Btn.GetComponent<Button>().image.color = Color.white;
        //�傫�����f�t�H���g�ɕς���
        Start_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        Option_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        End_Btn.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        //�I�΂�Ă���񋓑̕ϐ��ɂ���ď���������e��ς���
        switch (eButton) {
            case TITLE_BUTTON.START_BUTTON:
                Start_Btn.GetComponent<Button>().image.color = Color.red;
                Start_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            case TITLE_BUTTON.OPTION_BUTTON:
                Option_Btn.GetComponent<Button>().image.color = Color.red;
                Option_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            case TITLE_BUTTON.END_BUTTON:
                End_Btn.GetComponent<Button>().image.color = Color.red;
                End_Btn.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                break;
            default:break;
        }

        //�{�^���������ꂽ�Ƃ��̏���
        if (Input.GetKey(KeyCode.Return)) {
            switch (eButton)
            {
                case TITLE_BUTTON.START_BUTTON:
                    SceneManager.LoadScene("YosukeScene");
                    break;
                case TITLE_BUTTON.OPTION_BUTTON:
                    if (Mathf.Approximately(Time.timeScale, 1f))
                    {
                        Time.timeScale = 0f;
                        Pause_UI.SetActive(true);
                    }
                    break;
                case TITLE_BUTTON.END_BUTTON:
                    UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
                    break;
                default: break;
            }
        }

    }
}
