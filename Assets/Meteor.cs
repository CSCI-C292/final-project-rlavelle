using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    float _speed = 2f;
    public Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setVel(Vector3 vel){
        _velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime*_speed*_velocity;
    }
}
