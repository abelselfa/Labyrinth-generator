using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPowerUps : MonoBehaviour
{
    List<Vector2> map2D;
    public bool showMap2D = true;
    private void Awake()
    {
        map2D = new List<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addToMap2D(Collision collision)
    {
        if (collision.gameObject.tag == "suelo" && !map2D.Contains(new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.z)))
        {

            map2D.Add(new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.z));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        addToMap2D(collision);
    }
}
