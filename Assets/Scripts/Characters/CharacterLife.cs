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

    public Slider healthBar;
    public GameManager gameManager;

    private int _dyingHashAnimation;
    private Animator _animator;
    private bool _isHurtEffectsNotNull;
    private bool _isHealthBarNull;
    private bool _isGameManagerNotNull;

    public IHealth Health { get; private set; }

    private void Start()
    {
        _isGameManagerNotNull = gameManager != null;
        _isHealthBarNull = healthBar == null;
        _isHurtEffectsNotNull = hurtEffects != null;
    }

    private void Awake()
    {
        var life = new LifePoint(initialLife);
        var lifePoint = new LifePoint(maxLife);


        if (!_isHealthBarNull)
        {
            healthBar.maxValue = maxLife;
            healthBar.value = initialLife;
        }

        _animator = gameObject.GetComponent<Animator>();
        _dyingHashAnimation = Animator.StringToHash(dyingAnimation);
        Health = new BasicHealth(life, lifePoint, this);
    }

    public void Death()
    {
        UpdateHealthBar();

        if (_dyingHashAnimation > 0)
            _animator.Play(_dyingHashAnimation);

        if (_isGameManagerNotNull)
            gameManager.GameOver = true;

    }

    public void Hurt()
    {
        UpdateHealthBar();
        
        if (!_isHurtEffectsNotNull || hurtEffects.isPlaying) return;

        hurtEffects.Play();
    }

    public void Healing()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_isHealthBarNull) return;

        healthBar.value = Health.GetCurrentLife().Value;
    }
}