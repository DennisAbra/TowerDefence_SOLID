using UnityEngine;
using System.Collections.Generic;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class ComponentPool<T> : IPool<T> where T : Component
    {
        uint m_ExpandBy;
        private T m_Prefab;
        Transform m_parent;

        private Stack<T> m_Objects;

        public ComponentPool(uint initSize, T prefab, uint expandBy = 1, Transform parent = null)
        {
            m_Prefab = prefab;
            m_ExpandBy = expandBy;
            m_parent = parent;

            Expand((uint)Mathf.Max(1,initSize));
        }

        void Expand(uint expandBy)
        {
            for(int i = 0; i < expandBy; i++)
            {
                T instance = Object.Instantiate<T>(m_Prefab, m_parent);
                instance.gameObject.AddComponent<ListenOnDisable>().OnDisableGameObject += UnRent;
                m_Objects.Push(instance);
            }
        }

        void UnRent(GameObject gameObj)
        {
            m_Objects.Push(gameObj.GetComponent<T>());
        }

        public T Rent(bool returnActive)
        {
            if(m_Objects.Count == 0)
            {
                Expand(m_ExpandBy);
            }

            T instance = m_Objects.Pop();
            instance.gameObject.SetActive(returnActive);

            return instance;
        }
    }

    public class GameObjectPool : IPool<GameObject>
    {
        uint m_ExpandBy;
        GameObject m_prefab;
        Transform m_parent;

        Stack<GameObject> m_Objects = new Stack<GameObject>();

        public GameObjectPool(uint InitSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
            m_prefab = prefab;
            m_ExpandBy = (uint)Mathf.Max(1, expandBy);
            m_parent = parent;
            m_prefab.SetActive(false);
            Expand((uint)Mathf.Max(1, InitSize));
        }

        private void Expand(uint amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject instance = Object.Instantiate(m_prefab, m_parent);
                ListenOnDisable listenOnDisable = instance.AddComponent<ListenOnDisable>();
                listenOnDisable.OnDisableGameObject += UnRent;
                m_Objects.Push(instance);
            }
        }

        void UnRent(GameObject gameObj)
        {
            m_Objects.Push(gameObj);
        }

        public GameObject Rent(bool returnActive)
        {
            if (m_Objects.Count == 0)
            {
                Expand(m_ExpandBy);
            }

            GameObject instance = m_Objects.Pop();
            instance.SetActive(returnActive);
            return instance;
        }
    }
}