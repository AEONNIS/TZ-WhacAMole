using System.Collections.Generic;
using UnityEngine;
using WhacAMole.Model;

namespace WhacAMole.Infrastructure
{
    public class Pool<T> where T : MonoBehaviour, INameable
    {
        private readonly List<T> _obects;
        private readonly Transform _content;

        public Pool(Transform content)
        {
            _obects = new List<T>();
            _content = content;
        }

        public T Get(T template, Transform parent)
        {
            int index = _obects.FindIndex(obj => obj.Name == template.Name);
            Debug.Log(template.Name);

            if (index != -1)
            {
                T result = _obects[index];
                _obects.RemoveAt(index);
                return SetParentAndState(result, parent, true);
            }
            else
            {
                return Object.Instantiate(template, parent);
            }
        }

        public void Return(T obj) => _obects.Add(SetParentAndState(obj, _content, false));

        private T SetParentAndState(T obj, Transform parent, bool state)
        {
            obj.transform.SetParent(parent, false);
            obj.gameObject.SetActive(state);
            return obj;
        }
    }
}
