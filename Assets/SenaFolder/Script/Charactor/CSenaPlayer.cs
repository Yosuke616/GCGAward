using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPManager))]
#endif

public class CSenaPlayer : CHPManager
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
  
    
    
    [SerializeField] private GameObject prefabHPBar;        // HP�o�[�̃v���n�u
    
    [SerializeField] private GameObject DeadEffect;
    #endregion

    // �ϐ��錾
    #region variable
    PLAYERSTATE playerState;
    
    private int nChangeHPBar;       // �ύX����HP�o�[�̔ԍ�(���݂�HP����v�Z����)
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        resetHP();      // HP�̏�����
        playerState = PLAYERSTATE.PLAYER_ALIVE;     // ������Ԃɐݒ肷��
        SetHPBar();     // HP�o�[UI�̏����擾����
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        UpdateState(playerState);
        //Debug.Log(nCurrentHp);

        if (Input.GetKeyDown(KeyCode.K))
            nCurrentHp = 0;

        if (nCurrentHp <= 0)
            Debug.Log("Dead");
    }
    #endregion

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
                #region debug dec hp
                //if (Input.GetKeyDown(KeyCode.Z))
                //{
                //    AddHp(-1);
                //}
                #endregion

                // �ύX����HP�o�[�̔ԍ��̌v�Z
                // HP�����^���̎��A�ԍ���1����邽�ߒ�������
                if (nCurrentHp == nMaxHp)
                    nChangeHPBar = nCurrentHp / (nMaxHp / nValNum) - 1;     
                else
                    nChangeHPBar = nCurrentHp / (nMaxHp / nValNum);   
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
    * @brief �|���`���[�W���ꂽ�Ƃ��Ɏ��s���鏈��
    * @param nDecHP HP�̏����
    * @sa �|���`���[�W���ꂽ�Ƃ�
    * @details ������HP�ɉ�����FrontHPBar�̐��l��ύX����
 �@  */
    #region dec front bar
    public void DecFrontBar(int nDecHP)
    {
        // FrontHPBar�̒l�����炷
        objFrontHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
    }
    #endregion

    /*
    * @brief �|�����˂��ꂽ�Ƃ��Ɏ��s���鏈��
    * @param nDecHP HP�̏����
    * @sa �|�����˂��ꂽ�Ƃ�
    * @details ���ۂ�HP�����炷
 �@  */
    #region dec bg bar
    public void DecBGBar(int nDecHP)
    {
        // HP�����炷
        objBGHPBar[nChangeHPBar].GetComponent<CHPBar>().AddValue(nDecHP);
        nCurrentHp += nDecHP;
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
                Instantiate(DeadEffect, transform.position, Quaternion.identity);
                StartCoroutine("DestroyPlayer");
                Debug.Log("<color=red>GAMEOVER</color>");
                break;
        }

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
        DecFrontBar(num);
        DecBGBar(num);
    }
    #endregion

    /*
    * @brief HP�o�[�̃��Z�b�g
    * @param num HP�̉��Z��
    * @sa �_���[�W����������Ƃ�
    * @details HP��num�����Z����
  �@*/
    #region reset hp bar
    public void ResetHPBar()
    {
        objFrontHPBar[nChangeHPBar].GetComponent<CFrontHPBar>().ResetBarValue();
    }
    #endregion

    #region set hp bar
    public void SetHpBar()
    {
        objBGHPBar[nChangeHPBar].GetComponent<CBGHPBar>().changeBarValue();
    }
    #endregion

    /*
     * @brief HP�̎擾
     * @return int �v���C���[��HP
 �@  */
    #region get hp
    public int GetHp()
    {
        return nCurrentHp;
    }
    #endregion

    #region set hp
    public void SetHp(int num)
    {
        nCurrentHp += num;
    }
    #endregion

    #region destroy player

    private IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    #endregion 

}
