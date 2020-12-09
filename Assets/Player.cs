using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _speed = 3f;
    float _turnSensitivity = 200f;
    float upper = 15.36f;
    float lower = -15.36f;
    float left = -20.48f;
    float right = 20.48f;


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
        if(Input.GetButtonDown("Fire1")){
            Fire();
        }

        Move();

        UpdateBackground();
    }   

    void UpdateBackground(){
        if(transform.position.y >= background.transform.position.y + upper){
            background.transform.position = new Vector3(background.transform.position.x, background.transform.position.y + upper, 14);
        }
        else if(transform.position.y <= background.transform.position.y + lower){
            background.transform.position = new Vector3(background.transform.position.x, background.transform.position.y + lower, 14);
        }
        if(transform.position.x <= background.transform.position.x + left){
            background.transform.position = new Vector3(background.transform.position.x + left, background.transform.position.y, 14);
        }
        else if(transform.position.x >= background.transform.position.x + right){
            background.transform.position = new Vector3(background.transform.position.x + right, background.transform.position.y, 14);
        }
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
            //background.transform.Rotate(Vector3.forward*_turnSensitivity*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
            cam.transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
            //background.transform.Rotate(-Vector3.forward*_turnSensitivity*Time.deltaTime);
        }

        // move player forward
        if (Input.GetKey(KeyCode.W)){
            transform.position += -transform.up * _speed * Time.deltaTime;
            cam.transform.position += -transform.up * _speed * Time.deltaTime;
            //background.transform.position += -transform.up * _speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)){
            transform.position += transform.up * _speed * Time.deltaTime;
            cam.transform.position += transform.up * _speed * Time.deltaTime;
            //background.transform.position += transform.up * _speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.name == "Meteor(Clone)" || collider.gameObject.name == "EnemyLaser(Clone)" || collider.gameObject.name == "Enemy(Clone)"){
            Destroy(gameObject);
            State.Instance.InitiateGameOver();
        }
    }
}
