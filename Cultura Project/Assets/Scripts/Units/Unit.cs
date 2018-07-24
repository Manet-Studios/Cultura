using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Navigation;
using System;

namespace Cultura.Units
{
    public class Unit : MonoBehaviour
    {
        public Transform target;
        public float speed;

        private Vector2[] path;
        private int targetIndex;
        private Coroutine followPathCoroutine;

        public void Start()
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }

        private void OnPathFound(Vector2[] newPath, bool success)
        {
            if (success)
            {
                path = newPath;
                if (followPathCoroutine != null) StopCoroutine(followPathCoroutine);
                followPathCoroutine = StartCoroutine(FollowPath());
            }
        }

        private IEnumerator FollowPath()
        {
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length) yield break;

                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}