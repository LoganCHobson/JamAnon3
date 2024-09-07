
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SolarStudios //Logans Library
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        public int poolSize = 0;
        public List<GameObject> objectPool = new List<GameObject>();
        [Header("Events")]
        public UnityEvent onSpawn;
        public UnityEvent onRecycle;
        public UnityEvent onRecycleAll;
        public UnityEvent onInitalize;
        

        // Start is called before the first frame update
        void Start()
        {
            InitializePool();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void InitializePool()
        {
            onInitalize.Invoke();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }
        
        public GameObject Spawn(Vector3 position, Quaternion rotation = default)
        {
            onSpawn.Invoke();
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }
            Debug.LogWarning("Object pool capacity reached.");
            return null;
        }

        public void Recycle(GameObject obj, float delay = 0f)
        {
            StartCoroutine(DeactivateObjectDelayed(obj, delay));
            onRecycle.Invoke();
        }

        private IEnumerator DeactivateObjectDelayed(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
        }

        public void RecycleAll(float delay = 0f)
        {
            foreach(GameObject obj in objectPool)
            {
                if (obj.activeInHierarchy)
                {
                    StartCoroutine(DeactivateObjectDelayed(obj, delay));
                }
                
            }
            onRecycleAll.Invoke();

        }

        public bool IsEmpty()
        {
           
           foreach(GameObject obj in objectPool)
           {
                if(!obj.activeInHierarchy)
                {
                    return false; //Pool ain't empty
                }
               
           }
            return true; //Aint got no gas innit
        }
    }

}
