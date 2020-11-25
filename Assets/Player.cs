using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _speed = 4f;
    float _turnSensitivity = 200f;
    
    [SerializeField] GameObject background;
    [SerializeField] Camera cam;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject firebox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        // Collider2D resident = cam.GetComponent<Collider2D>();
        // Collider2D zone = background.GetComponent<Collider2D>();
        // if(zone.bounds.Contains(resident.bounds.max) && zone.bounds.Contains(resident.bounds.min)){
        //     Debug.Log("ahhhh");
        // }

        if(Input.GetButtonDown("Fire1")){
            Fire();
        }

        Move();

    
        // Vector3 cam_pos = cam.transform.position;
        // Vector3 cam_size = cam.GetComponent<Collider2D>().bounds.size;
        // Vector3 size = background.GetComponent<Collider2D>().bounds.size;
        // Vector3 bg_pos = background.transform.position;
    //cam_pos.x + cam_size.x > bg_pos.x + size.x || cam_pos.x - cam_size.x < bg_pos.x - size.x ||
        // if(cam_pos.y + cam_size.y > bg_pos.y + size.y || cam_pos.y - cam_size.y < bg_pos.y - size.y){
        //     // recenter background
        //     Vector3 new_pos = cam.transform.position;
        //     new_pos.z = 25;
        //     background.transform.position = new_pos;
        // }
    }   

    void Fire(){
        // create direction to fire to
        Vector3 dir = -transform.up;
        
        // direction of firing is same as the velocity
        Vector3 vel = dir;
        
        dir.Normalize();
        
        // create the laser
        GameObject laser = Instantiate(_laserPrefab, firebox.transform.position, Quaternion.identity);
        
        // set the laser velocity 
        laser.GetComponent<Laser>().setVelocity(vel);
        
        // rotate the laser to face the proper direction
        laser.transform.Rotate(new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90));

    }

    void Move(){
        // rotate player left and right
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward*_turnSensitivity*Time.deltaTime);
            cam.transform.Rotate(Vector3.forward*_turnSensitivity*Time.deltaTime);
            background.transform.Rotate(Vector3.forward*_turnSensitivity*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
            cam.transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
            background.transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
        }

        // move player forward
        if (Input.GetKey(KeyCode.W)){
            transform.position += -transform.up * _speed * Time.deltaTime;
            cam.transform.position += -transform.up * _speed * Time.deltaTime;
            background.transform.position += -transform.up * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)){
            transform.position += transform.up * _speed * Time.deltaTime;
            cam.transform.position += transform.up * _speed * Time.deltaTime;
            background.transform.position += transform.up * _speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Meteor(Clone)" || collider.gameObject.name == "EnemyLaser(Clone)" || collider.gameObject.name == "Enemy(Clone)"){
            Destroy(gameObject);
            State.Instance.InitiateGameOver();
        }
    }
}
