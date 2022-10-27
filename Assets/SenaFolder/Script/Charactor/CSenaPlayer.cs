using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CCharactorManager))]
#endif

public class CSenaPlayer : CCharactorManager
{
    #region serialize field
    //[SerializeField] private GameObject prefabHPBar;        // HP�o�[�̃v���n�u
    [SerializeField] private GameObject DeadEffect;
    [SerializeField] private GameObject objWeapon;              // ����I�u�W�F�N�g
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject HPNumUI;                // HP�o�[�̐����\�LUI
    #endregion

    // �ϐ��錾
    #region variable
    private CHARACTORSTATE playerState;
    private CHPText hpText;
    private GameObject[] objHPTexts;

    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();      // HP�̏�����
        //InitAtk();      // �U���͂̏�����
        playerState = CHARACTORSTATE.CHARACTOR_ALIVE;     // ������Ԃɐݒ肷��
        //SetHPBar();     // HP�o�[UI�̏����擾����

        // HP�̐����\��UI�I�u�W�F�N�g���擾����
        var children = new GameObject[HPNumUI.transform.childCount];
        for(int i = 0; i < children.Length; ++i)
            children[i] = HPNumUI.transform.GetChild(i).gameObject;
        objHPTexts = children;
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

        //Debug.Log("HP" + nCurrentHp);
        //Debug.Log("PlayerAtk" + nCurrentAtk);
    }
    #endregion

    /*
    * @brief ��Ԃ̍X�V(���t���[�����s�����)
    * @param CHARACTORSTATE �v���C���[�̏��
    * @sa CPlayer::Update
    * @details �v���C���[�̏�Ԃ��擾���A��Ԃɍ��킹���X�V���������s����
  �@*/
    #region update state
    private void UpdateState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // ������Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                // Z�L�[��HP�����炷(�f�o�b�O�p)
                #region debug dec hp
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    nCurrentHp = 0;
                }
                #endregion

                //CalcFrontBarNum();
                // HP���ύX���ꂽ�ꍇ�AHP�����\��UI��ύX����
                if (nCurrentHp != nOldHp)
                   
                // HP��0�ɂȂ����玀�S��ԂɕύX����
                if (nCurrentHp <= 0)
                    ChangeState(CHARACTORSTATE.CHARACTOR_DEAD);
                break;
            // ���S��Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_DEAD:
                break;
        }
        nOldHp = nCurrentHp;    
    }
    #endregion

    /*
    * @brief ��Ԃ̍X�V(��Ԃ��ύX���ꂽ�Ƃ���1�x�������s�����)
    * @param CHARACTORSTATE �ύX��̏��
    * @sa CPlayer::UpdateState
    * @details �v���C���[�̏�Ԃ��擾���A��Ԃɍ��킹�����������s����
  �@*/
    #region change state
    private void ChangeState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // ������ԂɕύX���鎞
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                // ���ɉ������Ȃ�(�����Ƃ����������d�l����������ǉ�����)
                playerState = CHARACTORSTATE.CHARACTOR_ALIVE;
                break;
            // ���S��Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_DEAD:
                playerState = CHARACTORSTATE.CHARACTOR_DEAD;
                Instantiate(DeadEffect, transform.position, Quaternion.identity);
                StartCoroutine("DestroyPlayer");
                Debug.Log("<color=red>GAMEOVER</color>");
                break;
        }

    }
    #endregion

    /*
     * @brief �O�ʂ�HP�o�[��ύX����
     * @param num �ύX�����
     * @sa �|���`���[�W���ꂽ�Ƃ�
     * @details ������HP�ɉ�����FrontHPBar��nChangeBGHPBar�Ԗڂ̐��l��ύX����
�@  */
    #region dec front hp bar
    public void DecFrontHPBar(int num)
    {
        //AddFrontBar(num);
    }
    #endregion

    /*
     * @brief �w�ʂ�HP�o�[��ύX����
     * @param num �ύX�����
     * @sa �|���`���[�W���ꂽ�Ƃ�
     * @details ������HP�ɉ�����BGHPBar��nChangeBGHPBar�Ԗڂ̐��l��ύX����
�@  */
    #region dec bg hp bar
    public void DecBGHPBar(int num)
    {
        AddBGBar(num);
    }
    #endregion

    /*
     * @brief HP�o�[�̃��Z�b�g
     * @param num HP�̉��Z��
     * @sa �_���[�W����������Ƃ�
     * @details HP��num�����Z����
  �@*/
    #region reset hp bar
    //public void ResetHPBar()
    //{
    //    objFrontHPBar[nChangeBGHPBar].GetComponent<CFrontHPBar>().ResetBarValue();
    //}
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
        Kesu elf = GameObject.Find("ElfPlayer").GetComponent<Kesu>();
        elf.SetDeathAnim();
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
        GameOverUI.SetActive(true);
        GameOverScript GOS = GameObject.Find("EventSystem").GetComponent<GameOverScript>();
        GOS.SetUseFlg(true);
    }
    #endregion

    public override void ChangeHPFront(int num)
    {
        //nCurrentHp += num;
        for(int i = 0; i < objHPTexts.Length; ++i)
        {
            objHPTexts[i].GetComponent<CHPText>().ChangeHPNum(num);
        }
        HPFrontBar.GetComponent<CHPBarFront>().MoveBar(num);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            Kesu elf = GameObject.Find("ElfPlayer").GetComponent<Kesu>();
            elf.SetDamageAnim();

            ChangeHPFront(-10);
            ChangeHPBG(-10);
        }
    }

}
