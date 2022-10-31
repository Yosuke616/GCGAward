using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CSceneTitle : MonoBehaviour
{
    public enum MODE
    {
        START = 0,
        TUTORIAL,
        OPTION,
        QUIT,
        MODE_MAX,
    }
    [SerializeField] GameObject[] UIObjects;
    [SerializeField] private FadeManager fadeManager;
    [SerializeField] public AudioClip[] audioClips;

    private MODE mode;
    private MODE oldMode;
    private bool isMouse;
    public AudioSource audioSource;
    private bool useController;
    private float ControllerDeadZone = 0.5f;
    private bool dpadFlg;
    //private Image[] UIImages;
    //private List<List<Sprite>> SpriteList;
    // Start is called before the first frame update
    void Start()
    {
        if(Gamepad.current == null)
        {
            useController = false; 
        }
        else
        {
            useController = true;
        }
        //fadeManager.SceneIn();
        mode = MODE.START;
        SetUI(mode, true);
        isMouse = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X���쒆�łȂ��ꍇ�A�L�[���͂��󂯕t����
        if (!isMouse)
        {
            // �L�[���͏���
            KeyInput();
        }
        // �}�E�X���͒��̎�
        else
        {
            for (int check = 0; check < UIObjects.Length; ++check)
            {
                // �I����Ԃ̏ꍇmode�ϐ����X�V����
                if(UIObjects[check].GetComponent<CTitleButton>().GetIsSelected())
                {
                    mode = (MODE)check;
                    check = UIObjects.Length;       // for���𔲂���
                }
            }
        }

        // �I�����ύX����Ă���ꍇ�{�^���̃e�N�X�`����ύX����
        if (mode != oldMode)
        {
            // ���ʉ��Đ�
            audioSource.PlayOneShot(audioClips[0]);
            // UI�X�V����
            SetUI(oldMode, false);      // �ߋ���UI�����Z�b�g����
            SetUI(mode, true);          // ���݂�UI���X�V����
        }
        oldMode = mode;
        //Debug.Log("MODE:" + mode);
    }

    #region mouse move
    public void MouseMove(bool flg)
    {
        isMouse = flg;
    }
    #endregion

    // �L�[���͏���
    #region key input
    private void KeyInput()
    {
        // �L�[�{�[�h��L�[ / �R���g���[���[��{�^�� ���@��I��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            --mode;

            if (mode < MODE.START)
            {
                mode = MODE.QUIT;
            }
        }

        // �L�[�{�[�h���L�[ / �R���g���[���[���{�^�� ���@��I��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++mode;

            if (mode >= MODE.MODE_MAX)
            {
                mode = MODE.START;
            }
        }

        // Enter�L�[������
        if(Input.GetKeyDown(KeyCode.Return))
        {
            // ���ʉ��Đ�
            audioSource.PlayOneShot(audioClips[1]);
            // �V�[���J��
            ChangeScene(mode);
        }
        if(useController)
        {
            // �L�[�{�[�h��L�[ / �R���g���[���[��{�^�� ���@��I��
            if ((Gamepad.current.leftStick.ReadValue().y>ControllerDeadZone||(Gamepad.current.dpad.ReadValue().y>ControllerDeadZone))&&!dpadFlg)
            {
                dpadFlg = true;
                --mode;

                if (mode < MODE.START)
                {
                    mode = MODE.QUIT;
                }
            }

            // �L�[�{�[�h���L�[ / �R���g���[���[���{�^�� ���@��I��
            if ((Gamepad.current.leftStick.ReadValue().y < -ControllerDeadZone||(Gamepad.current.dpad.ReadValue().y < -ControllerDeadZone)) && !dpadFlg)
            {
                dpadFlg = true;
                ++mode;

                if (mode >= MODE.MODE_MAX)
                {
                    mode = MODE.START;
                }
            }
            if(dpadFlg&& ((Gamepad.current.dpad.ReadValue().y == 0)&&(Gamepad.current.leftStick.ReadValue().y ==0)))
            {
                dpadFlg = false;
            }
            // Enter�L�[������
            if (Gamepad.current.aButton.isPressed)
            {
                // ���ʉ��Đ�
                audioSource.PlayOneShot(audioClips[1]);
                // �V�[���J��
                ChangeScene(mode);
            }
        }
    }
    #endregion

    // UI�X�V����
    #region update ui
    private void SetUI(MODE selectMode, bool flg)
    {
        UIObjects[(int)selectMode].GetComponent<CTitleButton>().SetSelected(flg);
    }
    #endregion

    // �V�[���ύX
    #region change scene
    public void ChangeScene(MODE selectMode)
    {
        switch (selectMode)
        {
            // �X�^�[�g �� �Q�[���J�n
            case MODE.START:
                fadeManager.SceneOut("GameScene");
                break;
            // �`���[�g���A�� �� �`���[�g���A���J�n
            case MODE.TUTORIAL:
                fadeManager.SceneOut("TutorialScene");
                break;

            // �I�v�V���� �� �I�v�V��������
            case MODE.OPTION:
                break;

            // �Q�[���I�� �� �Q�[���I��
            case MODE.QUIT:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                break;
        }
    }
    #endregion
}
