using System;
using HealthSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterLife : MonoBehaviour, IHealthEvents
{
    [FormerlySerializedAs("InitialLife")] 
    public int initialLife;

    [FormerlySerializedAs("MaxLife")] 
    public int maxLife;

    public IHealth Health { get; private set; }

    private void Awake()
    {
        var life = new LifePoint(initialLife);
        var lifePoint = new LifePoint(maxLife);
        
        Health = new BasicHealth(life, lifePoint, this);
    }

    public void Death()
    {
        Health.Damage(new LifePoint(1));
        
        throw new NotImplementedException();
    }
}