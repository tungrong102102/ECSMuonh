using UnityEngine;
using Unity.Entities;

namespace Tung
{
    class PlayerAuthoring : MonoBehaviour
    {
        class Baker : Baker<PlayerAuthoring>
        {
            private Player _player;
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                //AddComponent<Player>(entity);
                _player = new Player();


                //TODO: Step5
            }
        }

    }
    public struct Player : IComponentData
    {

    }
}