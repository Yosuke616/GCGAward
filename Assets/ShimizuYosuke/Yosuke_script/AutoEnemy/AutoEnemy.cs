using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnemy : MonoBehaviour
{
    //�^�O����������"Ground"�̃^�O��t���Ă���z�����郊�X�g���쐬����
    public List<GameObject> GroundList = new List<GameObject>();

    //�����������I�u�W�F�N�g
    [SerializeField]private GameObject CreateObj;

    [Header("�ǂ̂��炢�̍����ɐ������邩�����߂�")]
    [SerializeField] private float Create_Height = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tags = GameObject.FindGameObjectsWithTag("Ground");
        //foreach��"Ground"�̃^�O�̕��Ƀ��X�g�����
        foreach (GameObject obj in tags) {
            GroundList.Add(obj);
        }

        //�ԍ��U�蕪���ׂ̈ɕϐ������
        int cnt = 0;

        //���X�g�ɓ������I�u�W�F�N�g�̏��ɃI�u�W�F�N�g�����
        foreach (GameObject obj in GroundList) {
            GameObject ray = Instantiate(CreateObj,new Vector3(obj.transform.position.x, Create_Height, obj.transform.position.z), Quaternion.identity);
            //���C���΂����߂̃X�N���v�g���A�^�b�`����
            ray.AddComponent<RayToGround>();
            ray.AddComponent<RayView>();
            ray.GetComponent<RayToGround>().SetNumber(cnt);
            cnt++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RemoveListbyID(GameObject id,List<GameObject> list) {
        GroundList.Remove(id);
    }
}
