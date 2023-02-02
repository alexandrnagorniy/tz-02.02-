using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime);

        if (transform.position.z > 7)
            Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        GameplayController.Instance.Check(type, transform.position, gameObject);
        
    }
}
