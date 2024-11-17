using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Coroutine coroutine;
    private Vector2 bulletDir;

    private float bulletSpeed, lifeTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 bulletDir, float bulletSpeed, float lifeTime)
    {
        this.bulletDir = bulletDir;
        this.bulletSpeed = bulletSpeed;
        this.lifeTime = lifeTime;
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(DeactiveAfterTime());
    }

    private void Update()
    {
        rigid.velocity = bulletDir * bulletSpeed;
    }

    private IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
