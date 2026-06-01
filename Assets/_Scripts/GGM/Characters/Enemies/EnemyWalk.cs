using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase3D
    {
        [Header("Way Points")]
        public GameObject[] wayPoints;
        public float minDistance = 1f;        
        public float speed = 2f;

        private int _index = 0;

        private void Update()
        {
            if (Vector3.Distance(transform.position, wayPoints[_index].transform.position) < minDistance)
            {
                _index++;
                if (_index >= wayPoints.Length) _index = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, wayPoints[_index].transform.position, speed * Time.deltaTime);
            transform.LookAt(wayPoints[_index].transform);
        }
    }
}
