using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SuperPupSystems.Helper
{
    [RequireComponent(typeof(Timer))]
    public class Bullet : MonoBehaviour
    {
        public int damage = 1;
        public float speed = 20f;
        public float lifeTime = 10f;
        public float destroyDelay = 0.5f;
        public bool destroyOnImpact = true;
        public UnityEvent hitTarget;
        public LayerMask mask;
        public List<string> tags;

        private Vector3 m_lastPosition;
        private RaycastHit m_info;
        private Timer m_timer;
        private bool dead;

        private void Awake()
        {
            if (hitTarget == null)
            {
                hitTarget = new UnityEvent();
            }
        }

        private void Start()
        {
            m_timer = GetComponent<Timer>();
            m_timer.timeout.AddListener(DestroyBullet);

            m_timer.StartTimer(lifeTime);

            // set init position
            m_lastPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Move();

            CollisionCheck();

            m_lastPosition = transform.position;
        }

        private void Move()
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }

        private void CollisionCheck()
        {
            if (Physics.Linecast(m_lastPosition, transform.position, out m_info, mask) && !dead)
            {
                if (tags.Contains(m_info.transform.tag))
                {
                    m_info.transform.GetComponent<Health>()?.Damage(damage);
                    dead = true;
                    hitTarget.Invoke();
                }

                if (destroyOnImpact)
                {

                    hitTarget.Invoke();
                    dead = true;
                    StartCoroutine(DestroyBulletAfterDelay());
                }
            }
        }

        private IEnumerator DestroyBulletAfterDelay()
        {
            yield return new WaitForSeconds(destroyDelay);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
