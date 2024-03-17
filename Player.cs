using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField]private int _health = 100;
    private bool _hasGun = false;
    private bool _hasRifle = false;
    [SerializeField] private GameObject _gunInfo;

    #region Encapsulations
    public bool HasGun { get => _hasGun; set => _hasGun = value; }
    public bool HasRifle { get => _hasRifle; set => _hasRifle = value; }
    public int Health { get => _health; set => _health = value; }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "gun")
        {
            _hasGun = true;
            _gunInfo.SetActive(true);
            Destroy(collision.gameObject);            
        }
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;
        if(Health >= 0)
            _gameManager.HealthBar.value = Health;
        if (Health <= 0)
            Die();
    }

    void Die()
    {
        Time.timeScale = 0;
    }
}