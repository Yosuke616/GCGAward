using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawner : MonoBehaviour
{
    #region setialize field
    [SerializeField] private GameObject spawnObj;
    #endregion
    #region variable
    private bool canSpawn;
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            canSpawn = true;
        }
        if (canSpawn)
        {
            Instantiate(spawnObj, transform.position, Quaternion.identity);
            canSpawn = false;
        }
    }

    public void StartSpawn()
    {
        canSpawn = true;
    }
}
