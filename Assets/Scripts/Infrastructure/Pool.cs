using System.Collections.Generic;
using UnityEngine;

namespace WhacAMole.Infrastructure
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly List<T> _pool;
        private readonly Transform _content;

        public Pool(List<T> templates, Transform content)
        {
            _pool = templates;
            _content = content;
        }

        public E Get<E>(E example, Transform parent) where E : T
        {
            int index = _pool.FindIndex(obj => obj is E);

            if (index != -1)
            {
                T result = _pool[index];
                _pool.RemoveAt(index);
                return SetParentAndState(result, parent, true) as E;
            }
            else
            {
                E result = Object.Instantiate(example, parent);
                return result;
            }
        }

        public void Return(T obj) => _pool.Add(SetParentAndState(obj, _content, false));

        private T SetParentAndState(T obj, Transform parent, bool state)
        {
            obj.transform.SetParent(parent, false);
            obj.gameObject.SetActive(state);
            return obj;
        }
    }
}
