using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //�G���ǂ������Ă������߂̑��
    [SerializeField] GameObject player;

    //�e�̔��ˏꏊ
    [Header("�e�֌W")]
    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 20.0f;

    [Header("�����Ă�܂ł̎���")]
    [SerializeField] int deltTime = 480;
    int nTime;

    //���C���g�p���Ď��E�𐧌䂷��
    private RaycastHit rayCastHit;

    //�X�R�A�����Z����ׂ̂��
    [SerializeField] private GameObject score;
    private CountText scScore;     // �X�R�A�̏��i�[�p

    //��
    [SerializeField] private GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        nTime = deltTime;
        scScore = score.GetComponent<CountText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (head == null) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
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
                        //������ς���
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

                        Vector3 p = new Vector3(0f, 0f, 0.05f);

                        //�ړ�������
                        transform.Translate(p);

                        nTime--;
                        if (nTime < 0) {
                            //�e�𔭎˂���
                            Vector3 bulletPosition = firingPoint.transform.position;
                            //��Ŏ擾�����ꏊ�ɒe���o��
                            GameObject newBall = Instantiate(bullet,bulletPosition,this.transform.rotation);
                            // �o���������{�[����forward(z������)
                            Vector3 directions = newBall.transform.forward;
                            // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������
                            newBall.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
                            // �o���������{�[���̖��O��"bullet"�ɕύX
                            newBall.name = bullet.name;
                            // �o���������{�[����0.8�b��ɏ���
                            Destroy(newBall, 2.0f);
                            nTime = deltTime;
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
        if (collision.gameObject.tag == "Arrow") {
            //�X�R�A�����Z������
            scScore.AddScore(10000);

            //�I�u�W�F�N�g�����ł�����
            Destroy(collision.gameObject);      // ������ł�����
            Destroy(this.gameObject);      // ���g�����ł�����
        }
    }

}
