using UnityEngine;

using Fusion;

namespace NSMB.Extensions {
    public static class FusionExtensions {

        public static bool IsActive(this TickTimer timer, NetworkRunner runner) {
            return !timer.ExpiredOrNotRunning(runner);
        }

        public static float? RemainingRenderTime(this TickTimer timer, NetworkRunner runner) {
            float? timeRemaining = timer.RemainingTime(runner);
            if (!timeRemaining.HasValue) {
                return null;
            }

            return Mathf.Max(0, (float) timeRemaining - (runner.LocalAlpha * runner.DeltaTime));
        }

        public static PlayerData GetPlayerData(this PlayerRef player) {
            SessionData.Instance.PlayerDatas.TryGet(player, out PlayerData value);
            return value;
        }

        public static bool TryGetPlayerData(this PlayerRef player, out PlayerData data) {
            return SessionData.Instance.PlayerDatas.TryGet(player, out data);
        }

        public static PlayerData GetLocalPlayerData(this NetworkRunner runner) {
            return GetPlayerData(runner.LocalPlayer);
        }

        public static bool TryGetPlayerData(this NetworkRunner runner, out PlayerData data) {
            return TryGetPlayerData(runner.LocalPlayer, out data);
        }

        public static CharacterData GetCharacterData(this PlayerRef player) {
            return player.GetPlayerData().GetCharacterData();
        }

        public static CharacterData GetCharacterData(this PlayerData data) {
            return ScriptableManager.Instance.characters[data ? data.CharacterIndex : Settings.Instance.generalCharacter];
        }
    }
}
