using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //[SerializeField] float _speed = -1f;
    [SerializeField] GameObject _enemyLaserPrefab;
    [SerializeField] GameObject firebox;
    GameObject player;
    Vector3 _vel;
    float _speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
         InvokeRepeating("SpawnLaser", 2, 3f);
         player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _vel = calcVel();
        float angle = Mathf.Atan2(_vel.y, _vel.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position += _vel*Time.deltaTime*_speed;
    }

    Vector3 calcVel(){
        Vector3 playerLoc = player.GetComponent<Transform>().transform.position;
        Vector3 enemyLoc = transform.position;
        Vector3 dir = playerLoc - enemyLoc;
        dir.Normalize();
        return(dir);
    }

    void SpawnLaser(){
        GameObject laser = Instantiate(_enemyLaserPrefab, firebox.transform.position, Quaternion.identity);
        laser.GetComponent<EnemyLaser>().setVelocity(transform.up);
        Vector3 dir = transform.up;
        dir.Normalize();
        laser.transform.Rotate(new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90));
    }

    void OnTriggerEnter2D(Collider2D collider){
        // if enemies hit meteor destroy both
        if(collider.gameObject.name == "Meteor(Clone)"){
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }

        // if the player shoots an enemy we destroy both the laser and the enemy 
        if(collider.gameObject.name == "Laser(Clone)"){
            Destroy(gameObject);
            Destroy(collider.gameObject);
            State.Instance.IncreaseScore(10);
        }
    }
}
