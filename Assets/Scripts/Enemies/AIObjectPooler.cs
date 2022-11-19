using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 1;
    [SerializeField] private bool poolCanExpand = true;
    //[SerializeField] private string bulletType;
    private List<GameObject> pooledObjects;
    private GameObject parentObject;
    void Start()
    {
        parentObject = GameObject.Find("EnemyPool");
        Refill();
    }

    public void Refill()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //if (poolCanExpand)
        //{
        //    return AddObjectToPool();
        //}
        return null;
    }
    public GameObject AddObjectToPool()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = parentObject.transform;
        //newObject.tag = bulletType;

        pooledObjects.Add(newObject);
        return newObject;
    }
}
