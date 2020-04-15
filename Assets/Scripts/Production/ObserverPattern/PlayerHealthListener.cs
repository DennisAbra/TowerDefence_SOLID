using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListener : MonoBehaviour, IObserver<int>
{
    [SerializeField] Text m_TextField;

    Player m_Player;
    IDisposable m_Subscription;

    private void Awake()
    {
        m_Player = FindObjectOfType<Player>();
        m_Subscription = m_Player.Health.Subscribe(this);
        UpdateTextField(m_Player.Health.Value);
    }

    void UpdateTextField(int playerHealth)
    {
        if(playerHealth <= 0 )
        {
            m_TextField.text = "Player Died";
        }
        else
        {
            m_TextField.text = "Health: " + playerHealth.ToString();
        }
    }

    public void OnCompleted()
    {}

    public void OnError(Exception error)
    {}

    public void OnNext(int value)
    {
        UpdateTextField(value);
    }
}
