using UnityEngine;
using UnityEngine.Tilemaps;

using Fusion;

namespace NSMB.Tiles {

    [CreateAssetMenu(fileName = "CoinTile", menuName = "ScriptableObjects/Tiles/CoinTile", order = 1)]
    public class CoinTile : BreakableBrickTile, IHaveTileDependencies {

        //---Static Variables
        private static readonly Vector2 SpawnOffset = new(0, -0.25f);

        //---Serialized Variables
        [SerializeField] private TileBase resultTile;

        public override bool Interact(BasicEntity interacter, InteractionDirection direction, Vector3 worldLocation, out bool bumpSound) {
            if (base.Interact(interacter, direction, worldLocation, out bumpSound))
                return true;

            bumpSound = true;

            Vector2Int tileLocation = Utils.Utils.WorldToTilemapPosition(worldLocation);

            PlayerController player = null;
            if (interacter is PlayerController controller)
                player = controller;
            else if (interacter is KoopaWalk koopa)
                player = koopa.PreviousHolder;

            if (player) {
                if (player.State == Enums.PowerupState.MegaMushroom) {
                    //Break

                    //Tilemap
                    GameManager.Instance.tileManager.SetTile(tileLocation, null);

                    //Particle
                    //TODO:
                    //object[] parametersParticle = new object[]{tileLocation.x, tileLocation.y, "BrickBreak", new Vector3(particleColor.r, particleColor.g, particleColor.b)};
                    //GameManager.Instance.SendAndExecuteEvent(Enums.NetEventIds.SpawnParticle, parametersParticle, ExitGames.Client.Photon.SendOptions.SendUnreliable);

                    player.PlaySound(Enums.Sounds.World_Block_Break);
                    return true;
                }

                //Give coin to player
                Coin.GivePlayerCoin(player, worldLocation + (Vector3) (Vector2.one / 4f));
            } else {
                interacter.PlaySound(Enums.Sounds.World_Coin_Collect);
            }

            Bump(interacter, direction, worldLocation);

            bool downwards = direction == InteractionDirection.Down;
            GameManager.Instance.rpcs.BumpBlock((short) tileLocation.x, (short) tileLocation.y, this,
                resultTile, downwards, SpawnOffset, true, NetworkPrefabRef.Empty);

            return false;
        }

        public TileBase[] GetTileDependencies() {
            return new TileBase[] { resultTile };
        }
    }
}
