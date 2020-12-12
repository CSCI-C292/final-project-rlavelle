using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] GameObject _meteorPrefab;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject player;
    float _xmin;
    float _xmax;
    float _ymin;
    float _ymax;
    float _yspawn_top;
    float _yspawn_bot;
    float _xspawn_left;
    float _xspawn_right;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLocs();
        InvokeRepeating("SpawnMeteor", 0, 1f);
        InvokeRepeating("SpawnEnemy", 0, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocs();
    }

    void UpdateLocs(){
        _xmin = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
        _xmax = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;
        _ymin = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y;
        _ymax = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
        _yspawn_top = Camera.main.ViewportToWorldPoint(new Vector3(0,1.1f,0)).y;
        _yspawn_bot = Camera.main.ViewportToWorldPoint(new Vector3(0,-0.1f,0)).y;
        _xspawn_left = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f,0,0)).x;
        _xspawn_right = Camera.main.ViewportToWorldPoint(new Vector3(1.1f,0,0)).x;
    }

    void SpawnMeteor(){
        Vector2 loc = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 point = loc + Random.insideUnitCircle*10;
        GameObject meteor = Instantiate(_meteorPrefab, point,Quaternion.identity);
        float xvel = Random.Range(-2f,2f);
        float yvel = Random.Range(-2f,2f);
        meteor.GetComponent<Meteor>().setVel(new Vector3(xvel,yvel,0));
    }

    void SpawnEnemy(){
        float r = Random.Range(0,4);
        GameObject enemy;
        // top down
        if(r==0){
            float randX = Random.Range(_xmin,_xmax);
            enemy = Instantiate(_enemyPrefab, new Vector3(randX, _yspawn_top, 0), Quaternion.identity);
        }
        // bottom up
        if(r==1){
            float randX = Random.Range(_xmin,_xmax);
            enemy = Instantiate(_enemyPrefab, new Vector3(randX, _yspawn_bot, 0), Quaternion.identity);
        }
        // left to right
        if(r==2){
            float randY = Random.Range(_ymin,_ymax);
            enemy = Instantiate(_enemyPrefab, new Vector3(_xspawn_left, randY, 0), Quaternion.identity);
        }
        // right to left
        if(r==3){
            float randY = Random.Range(_ymin,_ymax);
            enemy = Instantiate(_enemyPrefab, new Vector3(_xspawn_right, randY, 0), Quaternion.identity);
        }
    }

    void OldSpawnMeteor(){
        float r = Random.Range(0,4);
        GameObject meteor;
        // top down
        if(r==0){
            float randX = Random.Range(_xmin,_xmax);
            meteor = Instantiate(_meteorPrefab, new Vector3(randX, _yspawn_top, 0), Quaternion.identity);
            meteor.GetComponent<Meteor>().setVel(new Vector3(0,-1f,0));
        }
        // bottom up
        if(r==1){
            float randX = Random.Range(_xmin,_xmax);
            meteor = Instantiate(_meteorPrefab, new Vector3(randX, _yspawn_bot, 0), Quaternion.identity);
            meteor.GetComponent<Meteor>().setVel(new Vector3(0,1f,0));
        }
        // left to right
        if(r==2){
            float randY = Random.Range(_ymin,_ymax);
            meteor = Instantiate(_meteorPrefab, new Vector3(_xspawn_left, randY, 0), Quaternion.identity);
            meteor.GetComponent<Meteor>().setVel(new Vector3(1f,0,0));
        }
        // right to left
        if(r==3){
            float randY = Random.Range(_ymin,_ymax);
            meteor = Instantiate(_meteorPrefab, new Vector3(_xspawn_right, randY, 0), Quaternion.identity);
            meteor.GetComponent<Meteor>().setVel(new Vector3(-1f,0,0));
        }
    }
}
