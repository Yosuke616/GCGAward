using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private MODE mode;
    private MODE oldMode;
    private bool isMouse;
    private AudioSource audioSource;
    //private Image[] UIImages;
    //private List<List<Sprite>> SpriteList;
    // Start is called before the first frame update
    void Start()
    {
        fadeManager.SceneIn();
        mode = MODE.START;
        SetUI(mode, true);
        isMouse = false;
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
            // �V�[���J��
            ChangeScene(mode);
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
    private void ChangeScene(MODE selectMode)
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
                break;
        }
    }
    #endregion
}
