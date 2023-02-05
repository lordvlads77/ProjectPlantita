using System;
using HealthSystem;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLife : MonoBehaviour, IHealthEvents
{
    public int initialLife;
    public int maxLife;
    public string dyingAnimation;
    public ParticleSystem hurtEffects;

    public Slider HealthBar;
    
    private int _dyingHashAnimation;
    private Animator _animator;
    private bool _isHurtEffectsNotNull;
    private bool _isHealthBarNull;

    public IHealth Health { get; private set; }

    private void Start()
    {
        _isHealthBarNull = HealthBar == null;
        _isHurtEffectsNotNull = hurtEffects != null;
    }

    private void Awake()
    {
        var life = new LifePoint(initialLife);
        var lifePoint = new LifePoint(maxLife);

        HealthBar.maxValue = maxLife;
        HealthBar.value = initialLife;
        _animator = gameObject.GetComponent<Animator>();
        _dyingHashAnimation = Animator.StringToHash(dyingAnimation);
        Health = new BasicHealth(life, lifePoint, this);
    }

    public void Death()
    {
        if(_dyingHashAnimation < 0) return;

        _animator.Play(_dyingHashAnimation);
        UpdateHealthBar();
    }

    public void Hurt()
    {
        if (!_isHurtEffectsNotNull || hurtEffects.isPlaying) return;
        
        hurtEffects.Play();
        UpdateHealthBar();
    }

    public void Healing()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if(_isHealthBarNull) return;
        
        HealthBar.value = Health.GetCurrentLife().Value;
    }
}