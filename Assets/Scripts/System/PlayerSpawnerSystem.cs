using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Tung
{
    [UpdateAfter(typeof(ObstacleSpawner))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    partial struct PlayerSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerSpawner>();
            state.RequireForUpdate<Config>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var config = SystemAPI.GetSingleton<Config>();

#if true
            foreach (var obstacleTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Obstacle>())
            {
                var player = state.EntityManager.Instantiate(config.PlayerPrefab);

                state.EntityManager.SetComponentData(player, new LocalTransform
                {
                    Position = new float3
                    {
                        x = obstacleTransform.ValueRO.Position.x + config.PlayerOffset,
                        y = 1,
                        z = obstacleTransform.ValueRO.Position.z + config.PlayerOffset,
                    },
                    Scale = 1,
                    Rotation = quaternion.identity,
                });
            }
#else
            var query = SystemAPI.QueryBuilder().WithAll<LocalTransform, Obstacle>().Build();
            var localTransformTypeHandle = SystemAPI.GetComponentTypeHandle<LocalTransform>(true);

            var chunks = query.ToArchetypeChunkArray(Allocator.Temp);
            foreach (var chunk in chunks)
            {
                var localTransforms = chunk.GetNativeArray(ref localTransformTypeHandle);

                for (var i = 0; i < chunk.Count; i++)
                {
                    var obstacleTransform = localTransforms[i];

                    var player = state.EntityManager.Instantiate(config.PlayerPrefab);
                    state.EntityManager.SetComponentData(player, new LocalTransform
                    {
                        Position = new float3
                        {
                            x = obstacleTransform.Position.x + config.PlayerOffset,
                            y = 1,
                            z = obstacleTransform.Position.z + config.PlayerOffset,
                        },
                        Scale = 1,
                        Rotation = quaternion.identity,
                    });
                }
            }
#endif
        }
    }
}