using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySpawner : MonoBehaviour
{
    #region serialize field
    [SerializeField] private GameObject objEnemy;
    #endregion

    #region variable
    private Vector3 fPos;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fPos = transform.position;
        CreateEnemy(objEnemy, fPos);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //    CreateEnemy(objEnemy, fPos);
    }

    private void CreateEnemy(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, Quaternion.identity);
    }
}
