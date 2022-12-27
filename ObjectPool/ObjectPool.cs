using System.Collections.Generic;
using UnityEngine;
namespace ObjectPool
{
    public class ObjectPool<T> where T : Component
    {
        private List<T> _pool = new List<T>();
        private int _count = 0;
        private T _prefab = null;
        private Transform _parent;


        public ObjectPool(T prefab, Transform parent = null)
        {
            this._prefab = prefab;
            Create();
            _parent=parent;
        }

        /// <summary>
        /// 오브젝트 풀에 객체 생성
        /// </summary>
        public void Create()
        {
            T unit = UnityEngine.Object.Instantiate(_prefab).GetComponent<T>();
            unit.gameObject.SetActive(false);
            if(_parent)
            {
                unit.transform.SetParent(_parent);
            }
            _pool.Add(unit);
        }

        /// <summary>
        /// 풀에 저장된 객체 가져옴
        /// </summary>
        /// <returns>저장된 객체</returns>
        public T Pop()
        {
            // 0이 없다면 생성
            if(!_pool[0])
                Create();
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
            // count번째 객체가 소멸되있다면
            // 객체를 풀에서 빼고 전체검사
            // 사용 가능한 번째부터 사용
            if (!_pool[_count])
            {
                for(int i = 0; i < _pool.Count;i++)
                {
                    if (!_pool[i])
                    {
                        _pool.Remove(_pool[i]);
                    }
                }
            }
            // 마지막 인덱스 번쨰 객체가 사용중이면 새로 생성
            if (_pool[^1].gameObject.activeSelf)
            {
                Create();
            }
            // 아니면 _count로 지정 후 사용
            _count = _pool.Count-1;

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