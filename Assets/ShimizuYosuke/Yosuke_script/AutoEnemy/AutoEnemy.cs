using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnemy : MonoBehaviour
{

    //�����������I�u�W�F�N�g
    [SerializeField]private GameObject CreateObj;

    private List<GameObject> spawnList = new List<GameObject>();

    [Header("�ǂ̂��炢�̍����ɐ������邩�����߂�")]
    [SerializeField] private float Create_Height = 100;

    //�����o�����̕ϐ�
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        //�^�O����������"Ground"�̃^�O��t���Ă���z�����郊�X�g���쐬����
        GameObject[] tags = GameObject.FindGameObjectsWithTag("Ground_Spawn");

        //�ԍ��U�蕪���X�N���v�g���A�^�b�`����
        this.gameObject.AddComponent<NumPower>();

        //�G�̔ԍ��U�蕪���p�ϐ�
        int nEnemy = 0;

        //���X�g�ɓ������I�u�W�F�N�g�̏��ɃI�u�W�F�N�g�����
        foreach (GameObject obj in tags) {
            GameObject ray = Instantiate(CreateObj,new Vector3(obj.transform.position.x, Create_Height, obj.transform.position.z), Quaternion.identity);
            //���C���΂����߂̃X�N���v�g���A�^�b�`����
            ray.AddComponent<RayToGround>();
            ray.AddComponent<RayView>();
            ray.GetComponent<RayToGround>().SetNumber(nEnemy);
            ray.tag = "Spawn_Enemy";
            spawnList.Add(ray);
            nEnemy++;
            cnt++;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetNum() {
        return cnt;
    }

    public List<GameObject> GetList() {
        return spawnList;
    }
}
