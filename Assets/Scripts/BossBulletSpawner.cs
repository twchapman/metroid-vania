using UnityEngine;
using System.Collections;

public class BossBulletSpawner : MonoBehaviour {
    [Tooltip("Seconds between spawns")]
    public float spawn_interval = 3.0f;

    public GameObject bullet;

    private float spawn_timer;

    void Start() {
        spawn_timer = spawn_interval;
    }

    void Update() {
        spawn_timer -= Time.deltaTime;
        if (spawn_timer <= 0) {
            spawn_timer = spawn_interval;
            SpawnBullet();
        }
    }

    private void SpawnBullet() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}