using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    //�G�̃^�C�v��񋓑̂Ŕ��ʂ���
    public enum ENEMY_TYPE {
        ENEMY_ROLL_90 = 0,
        ENEMY_ROLL_180,
        WALK_ENEMY,
        STOP_ENEMY
    }

    [Header("�G�̎��")]
    [Header("1:90�x��]")]
    [Header("2:180�x��]")]
    [Header("3:����")]
    [Header("4:�~�܂��Č���")]
    [SerializeField] private ENEMY_TYPE eType = 0;

    [Header("HP�̉񕜗�")]
    [SerializeField] private int nRecovery = 0;

    //�G���N��ǂ������邩�̑Ώ�
    private GameObject player;

    //�e�ۊ֌W
    [Header("�e�֌W")]
    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float bullet_speed = 20.0f;

    [Header("�����Ă�܂ł̎���")]
    [SerializeField] int bullet_deltTime = 480;
    int nBullet_Time;

    [Header("�G���U�����邩���Ȃ����̃t���O")]
    [Header("true :�U������")]
    [Header("false:�U�����Ȃ�")]
    [SerializeField] private bool bAttackFlg = true;

    [Header("�G���ǂ������邩�ǂ������Ȃ����̃t���O")]
    [Header("true :�ǂ�������")]
    [Header("false:�ǂ������Ȃ�")]
    [SerializeField] private bool bChaseFlg = true;

    //���C���g�p���Ď��E�𐧌䂷��
    private RaycastHit rayCastHit;

    //�s�����邽�߂̃J�E���g(���L)
    [Header("���b�Ŏ��̍s�������邩")]
    [SerializeField] private int ACTTIME = 180;
    private int nActTime;
    private bool bAct;

    [Header("�G�̕����X�s�[�h")]
    [SerializeField] private float WALK_SPEED = 5.0f;
    [Header("�G���ǂ̈ʕ�����")]
    [SerializeField] private float MOVE_AREA = 500.0f; 

    //�ړ��ʕۑ��̂��߂̕ϐ�
    private Vector3 trans;
    //�ǂꂭ�炢���l��ύX��������
    private int Act_Num;
    //��]�����邽�߂̃t���O
    private bool Rotflg;

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

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�̎擾
        player = GameObject.Find("Player");
        //���Ԃ̏�����
        nBullet_Time = bullet_deltTime;
        //�s������Ƃ��̎��Ԃ�0�ɂ���
        nActTime = 0;
        //false�ōs�����Ȃ� true�ōs������
        bAct = false;
        //��]��0�ɂ��Ă���
        Act_Num = 0;
        //��]�t���O�̓I�t�ɂ����
        Rotflg = false;
        //�f�t�H���g�̓��������邩�ǂ���
        DefaultMove = false;
        //��A�t���O�̓I�t�ɂ��Ă���
        ComeBackFlg = false;
        //�X�^�[�g�n�_��ۑ����Ă���
        Start_Pos = this.transform.position;
        //�X�^�[�g�n�_�ł̃x�N�g����ۑ����Ă���
        Start_Rot = this.transform.eulerAngles;
        //�`�F�C�X�t���O�̓I�t�ɂ��Ă���
        Chase = false;
        //���ɖ߂��t���O�̓I�t�ɂ��Ă���
        Change_Rot = false;
        //���̐ݒ�
        head = transform.GetChild(0).gameObject;

        //�Q�[���J�n���_�ł��̏ꏊ�ɓ����蔻��𐶐�����
        var Obj = new GameObject("StartPos");
        Obj.transform.position = Start_Pos;
        Obj.AddComponent<SphereCollider>();
        Obj.GetComponent<SphereCollider>().isTrigger = true;
        BC = Obj.AddComponent<BoxCollider>();
        Obj.GetComponent<BoxCollider>().size = new Vector3(0.1f,0.1f,0.1f);
        Obj.GetComponent<BoxCollider>().enabled = false;
        Obj.tag = "Enemy_Start_Pos";
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̎擾
        player = GameObject.FindGameObjectWithTag("Player");

        //��ŏ���
        //���I�u�W�F�N�g���������玩�g������
        if (head = null) {
            Destroy(this.gameObject);
            return;
        }

        //���̊p�x�ɖ߂�
        if (Change_Rot) {
            this.transform.eulerAngles = Start_Rot;

            if (this.transform.eulerAngles == Start_Rot) {
                DefaultMove = false;
                Change_Rot = false;
            }

        }

        if (!DefaultMove)
        {
            //��莞�Ԃōs���ł���悤�ɂ���
            if (!bAct)
            {
                nActTime++;
            }

            if (nActTime > ACTTIME)
            {
                //�G�̃X�e�[�^�X�ɂ���ĕۑ�������e��ύX����
                //��{�I�ɂ͌��݂̍��W�Ɖ�]��
                if (!bAct)
                {
                    switch (eType)
                    {
                        case ENEMY_TYPE.ENEMY_ROLL_90:
                        case ENEMY_TYPE.ENEMY_ROLL_180:
                            Act_Num = 0;
                            break;
                        case ENEMY_TYPE.WALK_ENEMY:
                            trans = this.transform.position;
                            break;
                    }
                }
                bAct = true;
            }

            //�A�N�V�����t���O���I����������\�z����
            if (bAct)
            {
                //�G�̎�ނ��Ƃɍs���p�^�[����ύX����
                switch (eType)
                {
                    case ENEMY_TYPE.ENEMY_ROLL_90:
                        //��莞�Ԃ��Ƃ�90�x��]������
                        this.transform.Rotate(new Vector3(0, 1, 0));
                        Act_Num++;
                        if (Act_Num >= 90)
                        {
                            Act_Num = 0;
                            bAct = false;
                            nActTime = 0;
                        }
                        break;
                    case ENEMY_TYPE.ENEMY_ROLL_180:
                        //��莞�Ԃ��Ƃ�180�x��]������
                        this.transform.Rotate(new Vector3(0, 1, 0));
                        Act_Num++;
                        if (Act_Num >= 180)
                        {
                            Act_Num = 0;
                            bAct = false;
                            nActTime = 0;
                        }
                        break;
                    case ENEMY_TYPE.WALK_ENEMY:
                        //�ړ������邩��]�����邩
                        if (Rotflg)
                        {
                            //��莞�Ԃ��Ƃ�180�x��]������
                            this.transform.Rotate(new Vector3(0, 1, 0));
                            Act_Num++;
                            if (Act_Num >= 180)
                            {
                                Act_Num = 0;
                                bAct = false;
                                nActTime = 0;
                                Rotflg = false;
                            }
                        }
                        else
                        {
                            //�ړ����Ԃ𐧌䂷��
                            Act_Num++;
                            if (Act_Num > MOVE_AREA)
                            {
                                //��]������
                                Rotflg = true;
                                Act_Num = 0;
                            }
                            else
                            {
                                //���ʕ����Ɉړ�������
                                this.transform.position += this.transform.forward * Time.deltaTime * WALK_SPEED;
                            }
                        }
                        break;
                    case ENEMY_TYPE.STOP_ENEMY:

                        break;
                }
            }
        }
        else {
            //�`�F�C�X�t���O���I�t�������ꍇ
            if (!Chase) {
                //���̏ꏊ�ɖ߂�
                ComeBackFlg = true;
                //�����̓����蔻����A�N�e�B�u�ɂ���
                BC.GetComponent<BoxCollider>().enabled = true;

                //��]��������
                Quaternion lookRotation = Quaternion.LookRotation(Start_Pos - this.transform.position,Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;
                transform.rotation = Quaternion.Lerp(this.transform.rotation,lookRotation,0.1f);

                //���̏ꏊ�ɖ߂铮��
                transform.position = Vector3.MoveTowards(this.transform.position, Start_Pos, Time.deltaTime);
            }

        }

        Debug.Log("DefaultMove:"+ DefaultMove);
        Debug.Log("ComeBackFlg:" + ComeBackFlg);
        Debug.Log("Chase:" + Chase);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //�v���C���[�̕����Ɍ������Ă���---------------------------------------------------------------------------------------
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - this.transform.position, Vector3.up);
            Debug.Log(234567);

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
                        if (bChaseFlg) {
                            transform.Translate(p);
                        }

                        nBullet_Time--;
                        if (nBullet_Time < 0)
                        {
                            if (bAttackFlg) {
                                //�e�𔭎˂���
                                Vector3 bulletPosition = firingPoint.transform.position;
                                //��Ŏ擾�����ꏊ�ɒe���o��
                                GameObject newBall = Instantiate(bullet, bulletPosition, this.transform.rotation);
                                // �o���������{�[����forward(z������)
                                Vector3 directions = newBall.transform.forward;
                                // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
                                newBall.GetComponent<Rigidbody>().AddForce(direction * bullet_speed, ForceMode.Impulse);
                                // �o���������{�[���̖��O��"bullet"�ɕύX
                                newBall.name = bullet.name;
                                // �o���������{�[����0.8�b��ɏ���
                                Destroy(newBall, 2.0f);
                                nBullet_Time = bullet_deltTime;
                            }
                        }
                    }
                }

            }
            //-----------------------------------------------------------------------------------------------------------------------
        }
    }

    //�^�O�œ����蔻������
    private void OnCollisionEnter(Collision collision)
    {
        //����������ꍇ�A���g�Ɩ�����ł�����
        if (collision.gameObject.tag == "Arrow")
        {
            //�X�R�A�����Z������
            WaveManager WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            WM.AddScore(100);



            //HP���񕜂�����
            GameObject obj = GameObject.Find("unitychan");

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
