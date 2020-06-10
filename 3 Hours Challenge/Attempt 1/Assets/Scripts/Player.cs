using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    LevelDesigner m_levelDesigner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(m_levelDesigner.GetCombo() > 3)
        {
            Destroy(collision.collider.gameObject);
        }else if(collision.collider.gameObject.tag == "Trap") {
            Lose();
        }else if(collision.collider.gameObject.tag == "Arrival")
        {
            Win();
        }

        m_levelDesigner.ResetCombo();
    }

    public void SetLevelDesigner(LevelDesigner _levelDesigner)
    {
        m_levelDesigner = _levelDesigner;
    }


    private void Lose()
    {
        Destroy(transform.parent);
    }

    private void Win()
    {

    }
}
