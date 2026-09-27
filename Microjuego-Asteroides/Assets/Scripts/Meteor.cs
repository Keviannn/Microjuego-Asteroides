using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject smallMeteorPrefab;
    public float miniSpeed = 3f;
    private float maxTimeLife = 4f;

    public void Die(Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        Vector3 tangent = new Vector3(-normal.y, normal.x, 0f);

        SpawnMini(tangent);
        SpawnMini(-tangent);

        Destroy(gameObject);
    }

    private void SpawnMini(Vector3 direction)
    {
        GameObject small = Instantiate(smallMeteorPrefab, transform.position + direction * 0.4f, Quaternion.identity);
        small.GetComponent<Rigidbody>().linearVelocity = direction * miniSpeed;
        Destroy(small, maxTimeLife);
    }
}
