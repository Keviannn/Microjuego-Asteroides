using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 30;
    private Vector3 poolPosition = new Vector3(15f, 15f, 0);
    private Queue<GameObject> availableBullets = new Queue<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            AddBullet();
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation, Vector3 target)
    {
        if (availableBullets.Count == 0)
            AddBullet();

        GameObject bullet = availableBullets.Dequeue();

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.targetVector = target;

        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = poolPosition;
        availableBullets.Enqueue(bullet);
    }

    void AddBullet() 
    {
        GameObject bullet = Instantiate(bulletPrefab, poolPosition, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>(); 
        bulletScript._ownerPool = this;
        bullet.SetActive(false);
        availableBullets.Enqueue(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
