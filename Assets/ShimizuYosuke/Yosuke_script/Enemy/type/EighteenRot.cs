using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EighteenRot : MonoBehaviour
{
    //�A�j���[�V�����̃t���O�Ǘ�
    private const string key_isRot = "isRot";
    private const string key_isRun = "isRun";
    private const string key_isAttack = "isAttack";
    private Animator animator;

    //�ǂ�������Ώ�
    private GameObject player;

    //�e�����ꏊ
    private GameObject firePoint;
    //�e�����X�s�[�h
    [Header("�e�̃X�s�[�h")]
    [SerializeField] private float bullet_Speed = 20.0f;
    [Header("�����܂ł̎���")]
    [SerializeField] private int BULLET_DELTTIME = 300;
    int nBullet_Time;
    //�e�I�u�W�F�N�g�̎擾
    private GameObject bullet;

    //�ǂ������邩�ǂ���
    [Header("�ǂ������邩�ǂ���")]
    [SerializeField] private bool bChase;

    //���C�𐧌䂷��
    private RaycastHit rayCastHit;

    //���̍s���ɉ��b�ňڂ邩
    [Header("���̍s���܂ł̎���")]
    [SerializeField] private int ACTTIME = 180;
    private int nActTime;
    private bool bAct;

    //�ړ��ʕۑ��̂��߂̕ϐ�
    private Vector3 trans;
    //�ǂꂭ�炢���l��ύX��������
    private int Act_Num;

    //�f�t�H���g�̓��������Ă��邩�ǂ���
    private bool DefaultMove;
    //���̏ꏊ�ɖ߂�t���O
    private bool ComeBackFlg;

    //�X�N���v�g�̏���ۑ�����
    private BoxCollider BC;
    //�X�^�[�g�n�_��ۑ����Ă����ϐ�
    private Vector3 Start_Pos;
    //�X�^�[�g���_�ł̌�����ۑ����Ă����ϐ�
    private Vector3 Start_Rot;
    //���������ɖ߂����ǂ����̃t���O
    private bool Change_Rot;

    //�`�F�C�X�t���O
    private bool Chase;

    //��
    private GameObject head;

    //�E�F�[�u�}�l�[�W���[�擾
    private WaveManager WM;

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̃^�O�������Ă���I�u�W�F�N�g���擾
        player = GameObject.FindGameObjectWithTag("Player");
        //�e�̃^�O�������Ă���I�u�W�F�N�g���擾
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        //�e�����Ă邩�ǂ����̏�����
        nBullet_Time = BULLET_DELTTIME;
        //�s�����邩�ǂ����̎��Ԃ�0�ɂ���
        nActTime = 0;
        //false�ōs�����Ȃ�true�ōs������
        bAct = false;
        //��]��0�ɂ���
        Act_Num = 0;
        //�f�t�H���g�̓��������邩�ǂ���
        DefaultMove = false;
        //��A�t���O�̓I�t�ɂ��Ă���
        ComeBackFlg = false;
        //�X�^�[�g�n�_
        Start_Pos = this.transform.position;
        //�X�^�[�g�n�_�̉�]�̒l��ۑ����Ă���
        Start_Rot = this.transform.eulerAngles;
        //�`�F�C�X�t���O�̓I�t�ɂ��Ă���
        Chase = false;
        //���ɖ߂��t���O�̓I�t�ɂ��Ă���
        Change_Rot = false;
        //���̐ݒ�
        head = this.transform.GetChild(0).gameObject;
        //�e�����ꏊ�̐ݒ�
        firePoint = this.transform.GetChild(1).gameObject;
        bChase = true;

        //�Q�[���J�n���_�ł��̏ꏊ�ɓ����蔻��𐶐�����
        var Obj = new GameObject("StartPos");
        Obj.transform.position = Start_Pos;
        Obj.AddComponent<SphereCollider>();
        Obj.GetComponent<SphereCollider>().isTrigger = true;
        BC = Obj.AddComponent<BoxCollider>();
        Obj.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
        Obj.GetComponent<BoxCollider>().enabled = false;
        Obj.tag = "Enemy_Start_Pos";

        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        this.animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //���̊p�x�ɖ߂�
        if (Change_Rot)
        {
            this.transform.eulerAngles = Start_Rot;

            if (this.transform.eulerAngles == Start_Rot)
            {
                DefaultMove = false;
                Change_Rot = false;
                //�A�j���[�V������S�ăI�t��
                this.animator.SetBool(key_isRot, false);
                this.animator.SetBool(key_isRun, false);
                this.animator.SetBool(key_isAttack, false);
            }
        }

        //��莞�Ԃōs���ł���悤�ɂ���
        if (!DefaultMove)
        {
            //��莞�Ԃōs���ł���悤�ɂ���
            if (!bAct)
            {
                nActTime++;
            }

            if (nActTime > ACTTIME)
            {
                if (!bAct)
                {
                    Act_Num = 0;
                }
                bAct = true;
            }


            //�A�N�V�����t���O���I����������s������
            if (bAct)
            {
                //��莞�Ԃ��Ƃ�180�x��]������
                this.transform.Rotate(new Vector3(0, 1, 0));
                Act_Num++;
                if (Act_Num >= 180)
                {
                    Act_Num = 0;
                    bAct = false;
                    nActTime = 0;
                }
            }
        }
        else
        {
            if (!Chase)
            {
                //���̏ꏊ�ɖ߂�
                ComeBackFlg = true;
                //�����̓����蔻����A�N�e�B�u�ɂ���
                BC.GetComponent<BoxCollider>().enabled = true;

                //��]��������
                Quaternion lookRotation = Quaternion.LookRotation(Start_Pos - this.transform.position, Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;
                transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, 0.1f);

                //���̏ꏊ�ɖ߂铮��
                transform.position = Vector3.MoveTowards(this.transform.position, Start_Pos, Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");

            //�v���C���[�̕����Ɍ������Ă���---------------------------------------------------------------------------------------
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - this.transform.position, Vector3.up);

            lookRotation.z = 0;
            lookRotation.x = 0;

            //�������牺�Ƀ��C���g���ăI�u�W�F�N�g�⎋�E�𐧌䂷��
            Vector3 diffDis = this.transform.position - player.transform.position;
            Vector3 axis = Vector3.Cross(this.transform.forward, diffDis);
            float viewAngle = Vector3.Angle(transform.forward, diffDis) * (axis.y < 0 ? -1 : 1);
            viewAngle += 180;

            if (viewAngle < 45 || viewAngle > 315)
            {
                //���C���΂�
                Vector3 diff = player.transform.position - transform.position;
                float distance = diff.magnitude;
                Vector3 direction = diff.normalized;
                Vector3 eyeHeightPos = transform.position + new Vector3(0, 1, 0);
                //13�Ԗڂ̃��C���[�Ƃ͏Փ˂��Ȃ����C���[�}�X�N���쐬
                int layerMask = ~(1 << 13);
                RaycastHit[] hitsOb = Physics.RaycastAll(eyeHeightPos, direction, distance, layerMask);
                //�Փ˂����I�u�W�F�N�g����݂̂Ńv���C���[�������ꍇChace���[�h��
                if (hitsOb.Length == 1)
                {
                    if (hitsOb[0].transform.gameObject.CompareTag("Player"))
                    {
                        //�f�t�H���g���[�u���I���ɂ���
                        DefaultMove = true;
                        //�`�F�C�X�t���O���I���ɂ���
                        Chase = true;

                        //������ς���
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

                        Vector3 p = new Vector3(0f, 0f, 0.05f);

                        //�ړ�������
                        if (bChase)
                        {
                            transform.Translate(p);
                        }

                        nBullet_Time--;
                        if (nBullet_Time < 0)
                        {
                            //�e�𔭎˂���
                            Vector3 bulletPosition = firePoint.transform.position;
                            //��Ŏ擾�����ꏊ�ɒe���o��
                            GameObject newBall = Instantiate(bullet, bulletPosition, this.transform.rotation);
                            // �o���������{�[����forward(z������)
                            Vector3 directions = newBall.transform.forward;
                            // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
                            newBall.GetComponent<Rigidbody>().AddForce(direction * bullet_Speed, ForceMode.Impulse);
                            // �o���������{�[���̖��O��"bullet"�ɕύX
                            newBall.name = bullet.name;
                            // �o���������{�[����0.8�b��ɏ���
                            Destroy(newBall, 2.0f);
                            nBullet_Time = BULLET_DELTTIME;

                        }
                    }
                }

            }
            //-----------------------------------------------------------------------------------------------------------------------

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //����������ꍇ�A���g�Ɩ�����ł�����
        if (collision.gameObject.tag == "Arrow")
        {
            //�X�R�A�����Z������
            WM.AddScore(100);
            Destroy(this.gameObject);
            WM.AddBreakEnemy();
            WM.DecEnemy();



            //HP���񕜂�����

        }

        //���̏ꏊ�ɖ߂�t���O
        if (ComeBackFlg)
        {
            if (collision.gameObject.tag == "Enemy_Start_Pos")
            {
                ComeBackFlg = false;
                BC.GetComponent<BoxCollider>().enabled = false;

                Change_Rot = true;

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Chase = false;
    }

}
