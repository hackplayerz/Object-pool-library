using System.Collections.Generic;
using UnityEngine;
namespace ObjectPool
{
    public class ObjectPool<T> where T : Component
    {
        private List<T> _pool = new List<T>();
        private int _count = 0;
        private T _prefab;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
            Create();
        }

        /// <summary>
        /// 오브젝트 풀에 객체 생성
        /// </summary>
        public void Create()
        {
            T unit = UnityEngine.Object.Instantiate(_prefab).GetComponent<T>();
            unit.gameObject.SetActive(false);
            _pool.Add(unit);
        }

        /// <summary>
        /// 풀에 저장된 객체 가져옴
        /// </summary>
        /// <returns>저장된 객체</returns>
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
        /// <summary>
        /// 풀 초기화
        /// </summary>
        public void Clear()
        {
            foreach(var obj in _pool)
            {
                if(obj)
                {
                    GameObject.Destroy(obj.gameObject);
                }
            }
            _pool.Clear();
            _count = 0;
        }
    }

}