using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject<T> : System.IObservable<T>, IObserver<T>
{
    private List<IObserver<T>> m_Observers = new List<IObserver<T>>();

    public void OnCompleted()
    {
        for (int i = 0; i < m_Observers.Count; i++)
        {
            m_Observers[i].OnCompleted();
        }
    }

    public void OnError(Exception error)
    {
        for (int i = 0; i < m_Observers.Count; i++)
        {
            m_Observers[i].OnError(error);
        }
    }

    public void OnNext(T value)
    {
        for (int i = 0; i < m_Observers.Count; i++)
        {
            m_Observers[i].OnNext(value);
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        m_Observers.Add(observer);
        return new Unsubscriber(m_Observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        List<IObserver<T>> m_Observers;
        private IObserver<T> m_Observer;
        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            m_Observers = observers;
            m_Observer = observer;
        }
        public void Dispose()
        {
            if (m_Observers.Contains(m_Observer))
            {
                m_Observers.Remove(m_Observer);
            }
        }
    }
}

public class ObservableProperty<T> : IObservable<T>
{
    private T m_Value;
    private readonly Subject<T> m_Subject = new Subject<T>();
    public T Value
    {
        get => m_Value;
        set
        {
            if(!EqualityComparer<T>.Default.Equals(m_Value, value))
            {
                m_Value = value;
                m_Subject.OnNext(m_Value);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        return m_Subject.Subscribe(observer);
    }
}
