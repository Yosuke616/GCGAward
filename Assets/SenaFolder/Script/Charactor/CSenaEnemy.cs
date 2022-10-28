using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CCharactorManager
{
    #region serialize field
    //[SerializeField] private GameObject sceneManager;
    //[SerializeField] private int nAddScore;
    [SerializeField] private GameObject objDamageUI;
    [SerializeField] private GameObject objHitEffect;
    [Header("�G�̏��Ŏ���")]
    [SerializeField] private float fDestroyTime;
    #endregion

    // �ϐ��錾
    #region variable
    private CScore scScore;     // �X�R�A�̏��i�[�p
    private GameObject objPlayer;
    private CHARACTORSTATE state;
    private GameObject hitEffect;

    private const string key_isDamage = "isDamage";
    private const string key_isDeath = "isDeath";
    private Animator animator;

    private WaveManager WM;

    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();       // HP�̏�����
        InitAtk();      // �U���͂̏�����
        //SetHPBar();
        objPlayer = GameObject.FindWithTag("Player");
        //scScore = sceneManager.GetComponent<CScore>();      // �X�R�A�̏����擾����

        this.animator = GetComponent<Animator>();

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        UpdateState(state);
        //Debug.Log("EnemyAtk" + nCurrentAtk);
        
    }
    #endregion

    #region change state
    private void ChangeState(CHARACTORSTATE state)
    {
        switch(state)
        {
            // ������Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                break;

            // ���S��Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_DEAD:
                //float fLifeTime = objDamageUI.GetComponent<CDamageUI>().fLifeTime;
                //StartCoroutine("DestroyHitEffect",(objHitEffect,fLifeTime));        // 1�b���
                //HP�̉�
                CSenaPlayer obj = GameObject.FindGameObjectWithTag("Player").GetComponent<CSenaPlayer>();
                //obj.ChangeHPFront(10);
                obj.ChangeHp(10);
                WM.AddScore(100);
                WM.AddBreakEnemy();
                WM.DecEnemy();
                StartCoroutine("DestroyEnemy", fDestroyTime);
                break;
        }
    }
    #endregion

    // ���t���[�����s�����
    #region update state
    private void UpdateState(CHARACTORSTATE state)
    {
        switch (state)
        {
            // ������Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_ALIVE:
                //Debug.Log("arrive");
                //Debug.Log("���݂�HP:" + nCurrentHp);
                // HP��0�ɂȂ����Ƃ��Ɏ��S��Ԃɂ���
                if (nCurrentHp <= 0)
                    ChangeState(CHARACTORSTATE.CHARACTOR_DEAD);
                break;
            
            // ���S��Ԃ̎�
            case CHARACTORSTATE.CHARACTOR_DEAD:
                break;

        }
    }
    #endregion

    /*
    * @brief �q�b�g�G�t�F�N�g�̍폜
    * @param GameObject �q�b�g�G�t�F�N�g�̃I�u�W�F�N�g
    * @details �q�b�g�G�t�F�N�g�̃I�u�W�F�N�g���擾���ď��ł�����
  �@*/
    //#region destroy hit effect
    //private IEnumerator DestroyHitEffect(GameObject effect, float lifeTime)
    //{
    //    yield return new WaitForSeconds(lifeTime);
    //    Destroy(effect);    
    //}
    //#endregion

    /*
     * @brief �G�I�u�W�F�N�g�̏���
     * @param float ���ł܂ł̎���
     * @details fTime�b��ɓG�I�u�W�F�N�g�����ł�����
�@   */
    #region destroy enemy
    private IEnumerator DestroyEnemy(float fTime)
    {
        //���S�A�j���[�V�����𗬂�
        this.animator.SetBool(key_isDeath, true);
        yield return new WaitForSeconds(fTime);
        Destroy(gameObject);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // ����������ꍇ�A���g�Ɩ�����ł�����
        if (collision.gameObject.tag == "Arrow")
        {
            //�A�j���[�V�����𗬂�
            this.animator.SetBool(key_isDamage, true);
            //Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // �X�R�A�����Z����
            Destroy(collision.gameObject);      // ������ł�����
            // ����������̃_���[�W�����擾����
            int DamageNum = collision.gameObject.GetComponent<CArrow>().GetArrowAtk();
            // �q�b�g�G�t�F�N�g�Đ�
            hitEffect = Instantiate(objHitEffect);
            // �_���[�W�ʒm
            ChangeHp(-1 * DamageNum);
            //if(nCurrentHp > 0)
            //objDamageUI.GetComponent<CDamageUI>().TellDamaged(DamageNum);
            // �q�b�g�J�[�\���̍Đ�
            GetComponent<CEnemyDamage>().ArrowHit();

        }
        else {
            this.animator.SetBool(key_isDamage, false);
        }
    }
    #endregion
}
