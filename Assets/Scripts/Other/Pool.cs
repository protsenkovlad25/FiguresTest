using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Proc
{
    public class Pool<T> where T : Component
    {
        public System.Action<T> OnCreateNew;
        public System.Action<T> OnReturned;

        private readonly T m_Prefab;

        private readonly Transform m_Parent;
        private readonly Dictionary<T, bool> m_Objects;

        public Dictionary<T, bool> Objects => m_Objects;
        public List<T> ObjectsList => m_Objects.Keys.ToList();

        public Pool(T prefab, Transform parent = null, int count = 0)
        {
            m_Prefab = prefab;
            m_Parent = parent;

            m_Objects = new Dictionary<T, bool>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isNew = false)
        {
            T clone = Object.Instantiate(m_Prefab, Vector3.zero, Quaternion.identity);

            clone.transform.SetParent(m_Parent);
            clone.gameObject.SetActive(false);

            if (isNew) OnCreateNew?.Invoke(clone);
            
            m_Objects.Add(clone, false);

            return clone;
        }
        private void ActivateObject(T obj)
        {
            m_Objects[obj] = true;

            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
        }
        private void DeactivateObject(T obj)
        {
            m_Objects[obj] = false;
            
            obj.gameObject.SetActive(false);

            if (m_Parent)
                obj.transform.SetParent(m_Parent);
        }

        public T Take()
        {
            T freeObject = null;
            
            foreach (var obj in m_Objects)
            {
                if (obj.Value == true)
                {
                    continue;
                }

                freeObject = obj.Key;
                break;
            }

            if (freeObject != null)
            {
                ActivateObject(freeObject);

                return freeObject;
            }

            T newObject = CreateObject(true);

            ActivateObject(newObject);

            return newObject;
        }
        public void Return(T obj)
        {
            DeactivateObject(obj);

            OnReturned?.Invoke(obj);
        }
        public void ReturnAll()
        {
            foreach (var obj in m_Objects)
                Return(obj.Key);
        }

        public void ClearPool()
        {
            ReturnAll();

            for (int i = m_Objects.Count - 1; i >= 0; i--)
                Object.Destroy(m_Objects.ElementAt(i).Key.gameObject);

            m_Objects.Clear();
        }
    }
}
