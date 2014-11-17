using UnityEngine;
using System.Collections;
using System;

namespace Assets.Bosses {
    public class BossController : MonoBehaviour {
        delegate void PhaseDelegate();

        public Transform launcherObject,
                         beamSpawnPoint;

        private PhaseDelegate phase;

        private float idleTimer = 0f;
        public float idleDuration = 3f;

        public Transform ballSpawnPoint;
        public GameObject ballPrefab;
        private int ballAttacksCount = 0;
        private int ballAttacksMax = 3;
        private float ballTimer = 0f;
        public float ballCooldown = 0.5f;

        private float beamAttackTimer = 0;
        public float beamAttackDuration = 2f;

        private GameObject player;

        void Start() {
            phase = new PhaseDelegate(Idle);
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update() {
            phase();

            launcherObject.rotation = HelperFunctions2D.Look2D(launcherObject, player.transform);
        }

        void Idle() {
            if (idleTimer >= idleDuration) {
                idleTimer = 0f;
                phase = new PhaseDelegate(AttackBall);
                return;
            }
            idleTimer += Time.deltaTime;
        }

        void AttackBall() {
            if (ballAttacksCount >= ballAttacksMax) {
                ballAttacksCount = 0;
                ballTimer = 0f;
                phase = new PhaseDelegate(Idle);
                return;
            }

            if (ballTimer <= 0) {
                ballTimer = ballCooldown;
                Instantiate(ballPrefab, ballSpawnPoint.position, launcherObject.rotation);
                ballAttacksCount++;
                ballTimer = ballCooldown;
            }

            ballTimer -= Time.deltaTime;
        }

        void AttackBeam() {

        }
    }
}