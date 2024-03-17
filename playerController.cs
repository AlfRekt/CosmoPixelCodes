using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]private Player _player;
    [SerializeField]private Weapon _weapon;
    public float speed;
    public float CharSpeed;
    public bool isSideWays;
    public Rigidbody2D rb;
    public Animator _animator;

    [SerializeField] private SpriteRenderer _sr;

    [Header("Gun Stance")]
    [SerializeField] private Sprite _gun;
    [SerializeField] private GameObject _gunStance;

    [Header("Rifle Stance")]
    [SerializeField] private Sprite _rifle;
    [SerializeField] private GameObject _rifleStance;

    [Header("weapon")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform upFirePoint;
    //[SerializeField] private GameObject bulletPrefab;


    private bool isFacingRight = false;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float haxis = Input.GetAxis("Horizontal");
        float vaxis = Input.GetAxis("Vertical");
        rb.velocity = new Vector2((haxis * CharSpeed), (vaxis * CharSpeed));
        if (haxis > 0)
        {
            isSideWays = true;
            speed = 1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else if (haxis < 0)
        {
            isSideWays = true;
            speed = 1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else if (vaxis > 0)
        {
            isSideWays = false;
            speed = 1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", false);
        }
        else if (vaxis < 0)
        {
            isSideWays = false;
            speed = -1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", false);
        }
        else if (vaxis > 0 && haxis != 0)
        {
            isSideWays = true;
            speed = 0.5f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else
        {
            isSideWays = false;
            speed = 0f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", false);
        }
        if (!isFacingRight && haxis > 0)
        {
            Flip();
        }
        else if (isFacingRight && haxis < 0)
        {
            Flip();
        }
        if(_player.HasGun == true)
        {
            _animator.SetBool("isArmed", true);
            _gunStance.SetActive(true);
        }
        if (_player.HasGun && haxis > 0)
        {
            isSideWays = true;
            speed = -1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else if (_player.HasGun && haxis < 0)
        {
            isSideWays = true;
            speed = -1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else if (_player.HasGun && vaxis > 0)
        {
            isSideWays = false;
            speed = 1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", false);
            _gunStance.SetActive(false);
        }
        else if (_player.HasGun && vaxis < 0)
        {
            isSideWays = true;
            speed = -1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }
        else if (_player.HasGun && vaxis > 0 && haxis != 0)
        {
            isSideWays = true;
            speed = -1f;
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isSideWays", true);
        }

        if (Input.GetButtonDown("Fire1") && _player.HasGun && speed == 1f)
        {
            _weapon.Fire2(upFirePoint);
        }

        else if (Input.GetButtonDown("Fire1") && _player.HasGun && speed == -1f)
        {
            _weapon.Fire(firePoint);
        }

        else if (Input.GetButtonDown("Fire1")&&_player.HasGun && speed == 0f)
        {
            _weapon.Fire(firePoint);
        }

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}
