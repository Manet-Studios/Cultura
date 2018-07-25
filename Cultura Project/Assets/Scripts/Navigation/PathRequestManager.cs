using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cultura.Navigation
{
    [RequireComponent(typeof(Pathfinding))]
    public class PathRequestManager : MonoBehaviour
    {
        private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        private PathRequest currentPathRequest;
        private Pathfinding pathfinding;

        private static PathRequestManager instance;
        private bool processingPath;

        private void Awake()
        {
            pathRequestQueue = new Queue<PathRequest>();
            instance = this;
            pathfinding = GetComponent<Pathfinding>();
        }

        public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector2[], bool> callback)
        {
            if (instance == null) instance = FindObjectOfType<PathRequestManager>();

            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);

            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }

        private void TryProcessNext()
        {
            if (!processingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                processingPath = true;

                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
            }
        }

        public void FinishedProcessingPath(Vector2[] path, bool success)
        {
            currentPathRequest.callback(path, success);
            processingPath = false;
            TryProcessNext();
        }
    }

    internal struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;

        public Action<Vector2[], bool> callback;

        public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector2[], bool> callback)
        {
            this.pathStart = pathStart;
            this.pathEnd = pathEnd;
            this.callback = callback;
        }
    }
}