using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject gun;
    private BulletPool bulletPool;

    private Rigidbody _rigid;

    public static int SCORE = 0;
    public static float xBorderLimit = 10f, yBorderLimit = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        bulletPool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        var newPos = transform.position;        
        if(newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit+1;
            Debug.Log("Limite alcanzado derecha");
        }
        else if(newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit-1;
            Debug.Log("Limite alcanzado izquierda");


        }
        else if(newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit+1;            
            Debug.Log("Limite alcanzado arriba");

        }
        else if(newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit-1;
            Debug.Log("Limite alcanzado abajo");

        }
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            bulletPool.GetBullet(gun.transform.position, Quaternion.identity, transform.right);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
