using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomHelpers
{
    public enum ObjectType
    {
        Bullet,
    }

    [Serializable]
    public struct Pool
    {
        public ObjectType objectType;
        public GameObject prefab;
        public int size;
    }
    
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler instance;

        public List<Pool> pools;
        
        private Dictionary<ObjectType, Queue<GameObject>> _poolDictionary;
        private Dictionary<ObjectType, Transform> _poolContainersDictionary;

    
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            InitPools();
        }

        private void InitPools()
        {
            _poolDictionary = new Dictionary<ObjectType, Queue<GameObject>>();
            _poolContainersDictionary = new Dictionary<ObjectType, Transform>();
        
            foreach (var pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                Transform parent = Instantiate(new GameObject(), transform, false).transform;
                parent.name = pool.prefab.name + "s' Pool";
                
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject go = Instantiate(pool.prefab, parent);
                    go.SetActive(false);
                    objectPool.Enqueue(go);
                }
            
                _poolContainersDictionary.Add(pool.objectType, parent);
                _poolDictionary.Add(pool.objectType, objectPool);
            }
        }

        public GameObject GetObjectFromPool(ObjectType objectType, Vector3 pos, Quaternion rot)
        {
            GameObject needObj = _poolDictionary[objectType].Dequeue();
        
            needObj.SetActive(true);
            needObj.transform.position = pos;
            needObj.transform.rotation = rot;

            return needObj;
        }

        public GameObject GetObjectFromPool(ObjectType objectType)
        {
            GameObject needObj = _poolDictionary[objectType].Dequeue();
        
            needObj.SetActive(true);
            return needObj;
        }

        public IEnumerator ReturnToPoolWithDelay(ObjectType objectType, GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnToPool(objectType, obj);
        }
    
        public void ReturnToPool(ObjectType objectType, GameObject obj)
        {
            _poolDictionary[objectType].Enqueue(obj);
            obj.transform.SetParent(_poolContainersDictionary[objectType], false);
            obj.SetActive(false);
        }
    
    }
}