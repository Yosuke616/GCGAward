using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    //�G���ǂ������Ă������߂̑��
    [SerializeField] GameObject player;

    //�e�I�u�W�F�N�g
    [SerializeField] GameObject bullet;
    private float fBulletspeed = 10.0f;

    //���C���g�p���Ď��E�𐧌䂷��
    private RaycastHit rayCastHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

                        //�e�𔭎˂��邺
                        var shot = Instantiate(bullet,this.transform.position, Quaternion.identity);
                        shot.GetComponent<Rigidbody>().velocity = this.transform.forward.normalized * fBulletspeed;

                        new WaitForSeconds(1.0f);
                    }
                }

            }        
            //-----------------------------------------------------------------------------------------------------------------------
        }
    }
}
