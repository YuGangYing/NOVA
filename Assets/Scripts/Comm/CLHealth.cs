using UnityEngine;
using System.Collections;

public class CLHealth : MonoBehaviour
{
    public float HealthPointMax;
    public float HealthPoint;

    public void Damage(float damageValue)
    {
        if (HealthPoint <= damageValue)
        {
            HealthPoint = 0;
            SendMessage("OnHealthDie", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            HealthPoint -= damageValue;
        }
    }

    public bool IsAlive()
    {
        return HealthPoint > 0;
    }

    public bool IsDead()
    {
        return !IsAlive();
    }
}