using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate = 0.00f;
    public GameObject impactEffect;
    public GameObject _muzzleFlash;
    public LineRenderer _lineRenderer;

    private float _nextTimeToFire = 0.00f;

    public float FireRate { get => _fireRate; set => _fireRate = value; }

    public void Fire(Transform firePoint)
    {
        if (Time.time < _nextTimeToFire)
            return;

        _nextTimeToFire = Time.time + (1.00f / _fireRate);
        StartCoroutine(Shoot(firePoint));
        StartCoroutine(MuzzleEffect(_muzzleFlash));
    }

    public void Fire2(Transform firePoint)
    {
        if (Time.time < _nextTimeToFire)
            return;

        _nextTimeToFire = Time.time + (1.00f / _fireRate);
        StartCoroutine(Shoot2(firePoint));
    }
    IEnumerator MuzzleEffect(GameObject muzzleFlash)
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        muzzleFlash.SetActive(false);
    }
    private IEnumerator Shoot(Transform firePoint)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hitInfo)
        {
            IDamagable enemy = hitInfo.transform.GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
        }

        else
        {
            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        _lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        _lineRenderer.enabled = false;
    }

    private IEnumerator Shoot2(Transform firePoint)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if (hitInfo)
        {
            IDamagable enemy = hitInfo.transform.GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, firePoint.position + firePoint.up * 100);
        }

        _lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        _lineRenderer.enabled = false;
    }
}
