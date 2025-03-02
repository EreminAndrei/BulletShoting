using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;    
    [SerializeField] private float _delay;
    
    private ObjectPool<Bullet> _bulletPool;    

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet> (CreateBullet, null, OnPutBackInPool, defaultCapacity: 12);
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(_bullet);
        return bullet;
    }

    private void ShootBullet()
    {
        var bullet = _bulletPool.Get();
        bullet.BulletFall += OnBulletFall;
        bullet.Init(transform.position);
    }

    private void OnPutBackInPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnBulletFall(Bullet bullet)
    {
        _bulletPool.Release(bullet);
        bullet.BulletFall -= OnBulletFall;
    }

    private IEnumerator Shooting()
    {
        bool isShooting = true;
        var delay = new WaitForSeconds(_delay);

        while (isShooting)
        {
           yield return delay;
           ShootBullet();
        }
    }
}
