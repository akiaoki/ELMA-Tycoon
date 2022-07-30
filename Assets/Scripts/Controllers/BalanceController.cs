using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class BalanceController : MonoBehaviour
    {

        public static float TicketBonus = 1.0f;
        public static float TicketBonusLevelMultiplier = 1.5f;

        public static float TicketSpawnRate = 0.2f;
        public static float TicketSpawnRateLevelMultiplier = 0.4f;

        public static float TicketMovementSpeed = 70.0f;
        public static float TicketMovementSpeedLevelMultiplier = 0.2f;

        public static int LevelUpPeopleRequired = 1;
        public static float LevelUpPeopleRequiredMultiplier = 2.0f;

        public static float GetTicketBonus(int level)
        {
            return TicketBonus + TicketBonus * level * TicketBonusLevelMultiplier;
        }

        public static float GetTicketSpawnRate(int level)
        {
            return TicketSpawnRate + TicketSpawnRate * TicketSpawnRateLevelMultiplier * level;
        }

        public static float GetTicketMovement(int level)
        {
            return TicketMovementSpeed + TicketMovementSpeed * level * TicketMovementSpeedLevelMultiplier;
        }

        public static float GetLevelUpPeopleRequired(int level)
        {
            return LevelUpPeopleRequired + level * LevelUpPeopleRequiredMultiplier;
        }
    }
}