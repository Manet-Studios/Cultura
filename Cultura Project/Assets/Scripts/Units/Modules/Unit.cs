using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cultura.Navigation;
using System;
using Cultura.Core;
using Sirenix.Serialization;

namespace Cultura.Units
{
    public class Unit : Selectable
    {
        public float speed;

        private Vector2[] path;
        private int targetIndex;
        private Coroutine followPathCoroutine;

        public void FindPath(Vector3 targetPosition, Action<bool> findPathCallback, Action completePathCallback)
        {
            PathRequestManager.RequestPath(transform.position, targetPosition,
                (v2, b) => OnPathFound(v2, b, findPathCallback, completePathCallback));
        }

        private void OnPathFound(Vector2[] newPath, bool success, Action<bool> findPathCallback, Action completePathCallback)
        {
            if (success)
            {
                findPathCallback(true);

                path = newPath;
                if (followPathCoroutine != null) StopCoroutine(followPathCoroutine);
                followPathCoroutine = StartCoroutine(FollowPath(completePathCallback));
            }
            else findPathCallback(false);
        }

        private IEnumerator FollowPath(Action completePathCallback)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        completePathCallback();
                        yield break;
                    }

                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (path != null)
            {
                Gizmos.color = Color.black;

                for (int i = 1; i < path.Length; i++)
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}