using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaPlayer : MonoBehaviour
{
    // �v���C���[�̏��
    #region plater state
    public enum PLAYERSTATE
    {
        PLAYER_ALIVE = 0,       // �������
        PLAYER_DEAD,            // ���S���
    }
    #endregion

    #region serialize field
    [Header("�v���C���[�̍ő�HP")]
    [SerializeField] private int nMaxHp;        // �v���C���[��HP�̍ő�l
    [Header("HP�o�[1�}�X��HP")]
    [SerializeField] private int nValHp;        // 1�}�X��HP��
    [SerializeField] private GameObject prefabHPBar;        // HP�o�[�̃v���n�u
    [SerializeField] private GameObject[] objHPBar;
    #endregion

    // �ϐ��錾
    #region variable
    private int nCurrentHp;     // ���݂�HP
    PLAYERSTATE playerState;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentHp = nMaxHp;     // HP�̏�����
        playerState = PLAYERSTATE.PLAYER_ALIVE;     // ������Ԃɐݒ肷��
        for (int num = 0; num < objHPBar.Length; ++num)
            objHPBar[num].GetComponent<CHPBar>().SetHpBarParam(num, nMaxHp / nValHp);
        //SetHpUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(playerState);
       
        Debug.Log(nCurrentHp);
    }

    /*
     * @brief ��Ԃ̍X�V(���t���[�����s�����)
     * @param PLAYERSTATE �v���C���[�̏��
     * @sa CPlayer::Update
     * @details �v���C���[�̏�Ԃ��擾���A��Ԃɍ��킹���X�V���������s����
   �@*/
    #region update state
    private void UpdateState(PLAYERSTATE state)
    {
        switch (state)
        {
            // ������Ԃ̎�
            case PLAYERSTATE.PLAYER_ALIVE:
                // Z�L�[��HP�����炷(�f�o�b�O�p)
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    AddHp(-1);
                    objHPBar[0].GetComponent<CHPBar>().AddValue(-1);
                }
                // HP��0�ɂȂ����玀�S��ԂɕύX����
                if (nCurrentHp <= 0)
                    ChangeState(PLAYERSTATE.PLAYER_DEAD);
                break;
            // ���S��Ԃ̎�
            case PLAYERSTATE.PLAYER_DEAD:
                break;
        }
    }
    #endregion

    /*
    * @brief ��Ԃ̍X�V(��Ԃ��ύX���ꂽ�Ƃ���1�x�������s�����)
    * @param PLAYERSTATE �ύX��̏��
    * @sa CPlayer::UpdateState
    * @details �v���C���[�̏�Ԃ��擾���A��Ԃɍ��킹�����������s����
  �@*/
    #region change state
    private void ChangeState(PLAYERSTATE state)
    {
        switch (state)
        {
            // ������ԂɕύX���鎞
            case PLAYERSTATE.PLAYER_ALIVE:
                // ���ɉ������Ȃ�(�����Ƃ����������d�l����������ǉ�����)
                playerState = PLAYERSTATE.PLAYER_ALIVE;
                break;
            // ���S��Ԃ̎�
            case PLAYERSTATE.PLAYER_DEAD:
                playerState = PLAYERSTATE.PLAYER_DEAD;
                Debug.Log("<color=red>GAMEOVER</color>");
                break;
        }

    }
    #endregion

    /*
     * @brief HP�o�[�̃Z�b�g
     * @param setNum �ݒu����HP�o�[�̌�
     * @sa CPlayer::Start
     * @details HP�̕�������ݒ肵�A�A������HP�o�[��ݒu����
   �@*/
    #region set hp UI
    private void SetHpUI()
    {
        //for (int num = 0; num < 5; ++num)
        //{
        //    GameObject hpBar = Instantiate(prefabHPBar);
        //    hpBar.GetComponent<CHPBar>().SetHpBarParam(num);
        //    hpBar.transform.SetParent()
        //}
    }
    #endregion

    /*
     * @brief HP�̉��Z
     * @param num HP�̉��Z��
     * @sa �_���[�W����������Ƃ�
     * @details HP��num�����Z����
   �@*/
    #region add hp
    public void AddHp(int num)
    {
        nCurrentHp += num;
    }
    #endregion
}
