using UnityEngine;

namespace Lessons.M3.Y3
{
    public class DinoDiagnostics : MonoBehaviour
    {
        public int coins = 7;
        public int needCoins = 10;
        public float speed = 6f;
        public float minSpeed = 5f;
        public string skinName = "Dino";
        public string bannedName = "Cheater";

        private void Start()
        {
            Debug.Log($"hasEnoughCoins = {coins >= needCoins}");
            Debug.Log($"isFastEnough = {speed > minSpeed}");
            Debug.Log($"isDefaultSkin = {skinName == "Dino"}");
            Debug.Log($"isNameAllowed = {skinName != bannedName}");
            Debug.Log($"isNonNegativeCoins = {coins >= 0}");
        }
    }
}

