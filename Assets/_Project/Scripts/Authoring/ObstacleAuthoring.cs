﻿using Unity.Entities;
using UnityEngine;

namespace Tung
{
    class ObstacleAuthoring : MonoBehaviour
    {
        class Baker : Baker<ObstacleAuthoring>
        {
            public override void Bake(ObstacleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Obstacle>(entity);
            }
        }
    }

    public struct Obstacle : IComponentData
    {

    }
}
