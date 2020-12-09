using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    Vector3 _velocity;
    float _speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime*_speed*_velocity;
    }

    public void setVelocity(Vector3 velocity){
        _velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Meteor(Clone)"){
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }
}
