using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathCreation.Examples
{

    public class PathFollower : MonoBehaviour
    {
        public enum BotRoadSpawn
        {
            Left = -1,
            Right = 1,
            Center = 0,
        }

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;

        public float speed;

        private float width;
        private float multiplyRoad;
        private float distanceTravelled;
        private float roadLenght;

        RoadMeshCreator roadMeshCreator;

        private Vector3 botRoadTransform;

        private BotRoadSpawn _botRoadSpawn;

        private float botDirection;


        private void Awake()
        {
            roadMeshCreator = pathCreator.GetComponent<RoadMeshCreator>();

            width = roadMeshCreator.roadWidth / 2;

            multiplyRoad = width / 2;

            roadLenght = pathCreator.path.length;

        }

        void Start()
        {

            if (pathCreator != null)
            {
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            getSpawn();
            botMove();
        }

        BotRoadSpawn getSpawn()
        {
            if (transform.position.x == 0 || transform.position.x >= roadLenght - 10)
            {
                var r = UnityEngine.Random.Range(-1, 2);
                _botRoadSpawn = (BotRoadSpawn)r;
                botDirection = r * multiplyRoad;
            }
            return _botRoadSpawn;
        }
        void botMove()
        {
            botRoadTransform = new Vector3(0, 0, botDirection);

            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) + botRoadTransform;
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                transform.Rotate(0, 0, 90);
            }
        }

        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}