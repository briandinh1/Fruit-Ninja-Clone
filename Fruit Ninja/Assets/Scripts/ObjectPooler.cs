using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;
    public int pooledAmount; // size of this object pool
    private List<GameObject> pool;

	// Use this for initialization
	void Start () {
        pool = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
            pool.Add(CreateObject());
	}
	
    public GameObject GetPooledObject() {
        for (int i = 0; i < pool.Count; i++) {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }
        return CreateObject();
    }

    public GameObject CreateObject() {
        GameObject newObject = (GameObject)Instantiate(pooledObject);
        newObject.SetActive(false);
        pool.Add(newObject);
        return newObject;
    }
}
