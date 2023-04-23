using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Instance

    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    private GameObject objectToPool;
    private Queue<GameObject> objectsPool;
    private Transform spawnedObjectsHolder;

    private void OnEnable()
    {
        objectsPool = new Queue<GameObject>();
    }

    public GameObject PoolObject(GameObject objectToPool, Vector3 pos)
    {
        this.objectToPool = objectToPool;
        
        if (objectsPool.Count > 0)
        {
            foreach(GameObject obj in objectsPool)
            {
                if (!obj.activeInHierarchy) 
                {
                    obj.transform.position = pos;
                    return obj;
                }
            }
        }

        CreateObjectParentIfNeeded();

        GameObject newObject = Instantiate(this.objectToPool, pos, Quaternion.identity);
        newObject.name = this.objectToPool.name + "_" + objectsPool.Count + "_Pooled";
        newObject.transform.SetParent(spawnedObjectsHolder);
        newObject.SetActive(false);
        objectsPool.Enqueue(newObject);
        return newObject;
    }
    
    private void CreateObjectParentIfNeeded()
    {
        //creates object to parent pooled objects to avoid messy scene...

        if (spawnedObjectsHolder == null)
        {
            string holderName = "ObjectPoolHolder";
            var parentObject = GameObject.Find(holderName);
            if (parentObject != null)
            {
                spawnedObjectsHolder = parentObject.transform;
            }
            else
            {
                spawnedObjectsHolder = new GameObject(holderName).transform;
            }

        }
    }
    
}