using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Tung
{
    class BallAuthoring : MonoBehaviour
    {
        class Baker : Baker<BallAuthoring>
        {
            public override void Bake(BallAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<Ball>(entity);
                AddComponent<Velocity>(entity);

                //TODO: step5
            }
        }
    }

    public struct Ball : IComponentData
    {

    }

    public struct Velocity : IComponentData
    {
       public float2 Value;
    }
}