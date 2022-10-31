using System.Collections.Generic;
using UnityEngine;
namespace ObjectPool
{    public class ObjectPool<T> where T : Component
    {
        private List<T> _pool = new List<T>();
        private int _count = 0;
        private T _prefab;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
            Create();
        }

        public void Create()
        {
            T unit = UnityEngine.Object.Instantiate(_prefab).GetComponent<T>();
            unit.gameObject.SetActive(false);
            _pool.Add(unit);
        }

        public T Pop()
        {
            if (_count > _pool.Count - 1)
            {
                if (_pool[0].gameObject.activeSelf)
                {
                    Create();
                }
                else
                {
                    _count = 0;
                }
            }
            _pool[_count].gameObject.SetActive(true);
            return _pool[_count++];
        }
    }

}