using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float m_maxRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Rotate(Input.GetAxis("Mouse X"));
        }
        
    }

    void Rotate(float _amplitude)
    {
        transform.Rotate(new Vector3(0f, _amplitude * m_maxRotationSpeed, 0f));
    }
}
