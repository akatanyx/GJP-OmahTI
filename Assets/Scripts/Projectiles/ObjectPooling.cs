using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform spawner;
    [SerializeField] private int poolSize = 4;
    [SerializeField] private bool poolCanExpand = true;
    //[SerializeField] private string bulletType;

    private List<GameObject> pooledObjects;
    private GameObject parentObject;
    // Update is called once per frame
    private void Start()
    {
        parentObject = new GameObject("CharacterBulletPool");
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
        if (poolCanExpand)
        {
            return AddObjectToPool();
        }
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
