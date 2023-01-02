using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipaController : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3);
    }
    void Update()
    {
        transform.Translate(Vector2.left * 10 * Time.deltaTime);
    }
}
