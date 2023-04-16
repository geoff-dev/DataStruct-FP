using System.Collections.Generic;
using UnityEngine;

namespace PowerPoint {
public class ObjectPoolManager : MonoBehaviour {
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObject(GameObject gameObj) {
        if (!objectPool.TryGetValue(gameObj.name, out Queue<GameObject> objectList))
            return CreateNewObject(gameObj);
        if (objectList.Count == 0)
            return CreateNewObject(gameObj);

        GameObject _object = objectList.Dequeue();
        _object.SetActive(true);
        return _object;
    }

    private GameObject CreateNewObject(GameObject gameObj) {
        GameObject newGO = Instantiate(gameObj);
        newGO.name = gameObj.name;
        return newGO;
    }

    public void ReturnGameObject(GameObject gameObj) {
        if (objectPool.TryGetValue(gameObj.name, out Queue<GameObject> objectList)) {
            objectList.Enqueue(gameObj);
        } else {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObj);
            objectPool.Add(gameObj.name, newObjectQueue);
        }
        gameObj.SetActive(false);
    }
}
}
