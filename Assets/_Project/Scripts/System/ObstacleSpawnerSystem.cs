using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Tung
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    partial struct ObstacleSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Config>();
            state.RequireForUpdate<ObstacleSpawner>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var config = SystemAPI.GetSingleton<Config>();
            var rand = new Random(123);
            var scale = config.ObstacleRadius * 2;
            for (int colum = 0; colum < config.NumColumns; colum++)
            {
                for (int row = 0; row < config.NumRows; row++)
                {
                    var obstacle = state.EntityManager.Instantiate(config.ObstaclePrefab);
                    state.EntityManager.SetComponentData(obstacle, new LocalTransform
                    {
                        Position = new float3
                        {
                            x = (colum * config.ObstacleGridCellSize) + rand.NextFloat(config.ObstacleOffset),
                            y = 0,
                            z = (row * config.ObstacleGridCellSize) + rand.NextFloat(config.ObstacleOffset),
                        },
                        Scale = scale,
                        Rotation = quaternion.identity,
                    });
                }
            }

        }
    }
}