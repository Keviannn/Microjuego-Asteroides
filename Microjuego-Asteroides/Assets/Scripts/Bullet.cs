using UnityEngine;
using UnityEngine.UI;


public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    public BulletPool _ownerPool; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {}

    private void OnEnable()
    {
        CancelInvoke(nameof(Release));
        Invoke(nameof(Release), maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag ("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            Release();
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    private void Release()
    {
        _ownerPool.ReturnBullet(gameObject);
    }
}
