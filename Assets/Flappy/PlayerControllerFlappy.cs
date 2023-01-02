using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerFlappy : MonoBehaviour
{
    public float jumpForce;
    public GameObject pipaPrefab;
    public Transform spawnPoint;
    new Rigidbody2D rigidbody;

    bool death, rotate;
    public int flipRotate = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InvokeRepeating("SpawnPipa", 0, 2);
    }
    void Update()
    {
        if (death) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector2.up * jumpForce;
            rotate = true;
            flipRotate++;
        }

        if (rotate)
        {
            RotatePlyaer();

            StartCoroutine(RotatePlayerCoroutine());
            IEnumerator RotatePlayerCoroutine()
            {
                yield return new WaitForSeconds(0.1f);
                rotate = false;
            }
        }

        
    }

    void SpawnPipa()
    {
        Instantiate(pipaPrefab, spawnPoint.transform.position + new Vector3(0, Random.Range(-2, 2), 0), Quaternion.identity);

    }
    void RotatePlyaer()
    {

        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, Vector3.forward * 90 * flipRotate, 2000 * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pipa"))
        {
            death = true;

            StartCoroutine(RestratGameCoroutine());
            IEnumerator RestratGameCoroutine()
            {
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            
        }
    }
}
