using System;
using HealthSystem;
using UnityEngine;

public class CharacterLife : MonoBehaviour, IHealthEvents
{
    public int initialLife;
    public int maxLife;
    public string dyingAnimation;
    public ParticleSystem hurtEffects;
    
    private int _dyingHashAnimation;
    private Animator _animator;
    private bool _isHurtEffectsNotNull;

    public IHealth Health { get; private set; }

    private void Start()
    {
        _isHurtEffectsNotNull = hurtEffects != null;
    }

    private void Awake()
    {
        var life = new LifePoint(initialLife);
        var lifePoint = new LifePoint(maxLife);

        _animator = gameObject.GetComponent<Animator>();
        _dyingHashAnimation = Animator.StringToHash(dyingAnimation);
        Health = new BasicHealth(life, lifePoint, this);
    }

    public void Death()
    {
        if(_dyingHashAnimation < 0) return;
        
        _animator.Play(_dyingHashAnimation);
    }

    public void Hurt()
    {
        if(_isHurtEffectsNotNull && !hurtEffects.isPlaying)
            hurtEffects.Play();
    }
}