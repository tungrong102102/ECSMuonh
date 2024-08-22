using Unity.Entities;
using UnityEngine;

namespace Tung
{
    public class ExecuteAuthoring : MonoBehaviour
    {
        [Header("Step 1")]
        public bool ObstacleSpwner;
            
        [Header("Step 2")]
        public bool PlayerSpawner;
        public bool PlayerMovement;
        private PlayerSpawner _playerSpawner;
        [Header("Step 3")]
        public bool BallSpawner;
        public bool BallMovement;
        class Baker : Baker<ExecuteAuthoring>
        {
            public override void Bake(ExecuteAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                if (authoring.ObstacleSpwner) AddComponent<ObstacleSpawner>(entity);
                if (authoring.PlayerSpawner) AddComponent<PlayerSpawner>(entity);
                if (authoring.PlayerMovement) AddComponent<PlayerMovement>(entity);
                if (authoring.BallSpawner) AddComponent<BallSpawner>(entity);
                if (authoring.BallMovement) AddComponent<NewBallMovement>(entity);
                authoring._playerSpawner = new PlayerSpawner();
            }
        }
    }

    public struct ObstacleSpawner : IComponentData
    {

    }

    public struct PlayerMovement : IComponentData
    {

    }

    public struct PlayerSpawner : IComponentData
    {
    }
    public struct BallSpawner : IComponentData
    {
    }
    public struct BallMovement : IComponentData
    {
    }

    public struct NewBallMovement : IComponentData
    {
    }
}