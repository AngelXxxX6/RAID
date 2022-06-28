using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   
    [SerializeField] private float weight;
    public string Name => name;
    public float Weight => weight;
    public void SetName(string val)
    {
        name = val;
    }
   
    public void SetVes(float val)
    {
        weight = val;
    }
  
    void Start()
    {
        SetName(gameObject.name);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
