using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    //�G�I�u�W�F�N�g
    [SerializeField] private GameObject enemy; 

    //�X�R�A�����Z����ׂ̂��
    [SerializeField] private GameObject score;
    private CountText scScore;     // �X�R�A�̏��i�[�p

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        scScore = score.GetComponent<CountText>();
        pos = enemy.transform.position;
        pos.y = enemy.transform.position.y + 1;
        this.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        //����
        if (enemy == null) {
            Destroy(this.gameObject);
            return;
        }

        //��ɓG�ɒǏ]������
        Vector3 pos = this.transform.position;
        pos = enemy.transform.position;
        pos.y = enemy.transform.position.y + 1;
        this.transform.position = pos;


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Arrow") {
            //�X�R�A�����Z������
            scScore.AddScore(100000);

            //�I�u�W�F�N�g�����ł�����
            Destroy(collision.gameObject);      // ������ł�����
            Destroy(this.gameObject);      // ���g�����ł�����
        }
    }
}
