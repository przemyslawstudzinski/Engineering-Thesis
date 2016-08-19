using ApplicationToSupportAndControlDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public static class RequisitionOfCalories
    {
        private const float SEDENTARY_FACTOR = 0.3F;
        private const float LIGHT_FACTOR_WOMEN = 0.5F;
        private const float LIGHT_FACTOR_MEN = 0.6F;
        private const float MODERATELY_FACTOR_WOMEN = 0.6F;
        private const float MODERATELY_FACTOR_MEN = 0.7F;
        private const float VERY_FACTOR_WOMEN = 0.9F;
        private const float VERY_FACTOR_MEN = 1.1F;
        private const float EXTREMELY_FACTOR_WOMEN = 1.2F;
        private const float EXTREMELY_FACTOR_MEN = 1.4F;
        private const float THERMIC_EFFECT_OF_FOOD_FACTOR = 0.1F;
        private const float CHANGE_WEIGHT_FACTOR = 0.15F;

        public static int CalculateRequisitionOfCalories(User user)
        {
            float basalMetabolicRate = GetBasalMetabolicRate(user.Sex, user.Weight, user.Height, user.Age);
            float thermicEffectOfExercise;
            if(user.Sex.Equals(Sex.Female))
            {
                thermicEffectOfExercise = GetThermicEffectOfExerciseForWomen(basalMetabolicRate, user.Activity);
            }
            else
            {
                thermicEffectOfExercise = GetThermicEffectOfExerciseForMen(basalMetabolicRate, user.Activity);
            }
            float thermicEffectOfFood = GetThermicEffectOfFood(basalMetabolicRate, thermicEffectOfExercise);
            float totalDailyEnergyExpenditure = GetTotalDailyEnergyExpenditure(basalMetabolicRate,
                thermicEffectOfExercise, thermicEffectOfFood);

            totalDailyEnergyExpenditure = AddUserGoal(user.Goal, totalDailyEnergyExpenditure);
            return Convert.ToInt32(totalDailyEnergyExpenditure);
        }

        private static float AddUserGoal(UserGoal goal, float totalDailyEnergyExpenditure)
        {
            if (goal.Equals(UserGoal.MaintainWeight))
            {
                return totalDailyEnergyExpenditure;
            }
            else if (goal.Equals(UserGoal.LoseWieght))
            {
                return totalDailyEnergyExpenditure - (totalDailyEnergyExpenditure * CHANGE_WEIGHT_FACTOR);
            }
            else
            {
                return totalDailyEnergyExpenditure + (totalDailyEnergyExpenditure * CHANGE_WEIGHT_FACTOR);
            }
        }

        private static float GetThermicEffectOfFood(float basalMetabolicRate, float thermicEffectOfExercise)
        {
            return (basalMetabolicRate + thermicEffectOfExercise) * THERMIC_EFFECT_OF_FOOD_FACTOR;
        }
        private static float GetBasalMetabolicRate(Sex sex, float weight, float height, float age)
        {
            double result;
            if (sex.Equals(Sex.Female))
            {
                result = (9.5634 * weight) + (1.8496 * height) - (4.6756 * age) + 655.0955;
            }
            else
            {
                result = (13.7516 * weight) + (5.0033 * height) - (6.755 * age) + 66.473;
            }
            return Convert.ToSingle(result);
        }

        private static float GetThermicEffectOfExerciseForMen(float basalMetabolicRate, ActivityLevel userActivity)
        {
            switch (userActivity)
            {
                case ActivityLevel.Sedentary:
                    basalMetabolicRate = basalMetabolicRate * SEDENTARY_FACTOR;
                    break;
                case ActivityLevel.LightlyActive:
                    basalMetabolicRate = basalMetabolicRate * LIGHT_FACTOR_MEN;
                    break;
                case ActivityLevel.ModeratelyActive:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR_MEN;
                    break;
                case ActivityLevel.VeryActive:
                    basalMetabolicRate = basalMetabolicRate * VERY_FACTOR_MEN;
                    break;
                case ActivityLevel.ExtremelyActive:
                    basalMetabolicRate = basalMetabolicRate * EXTREMELY_FACTOR_MEN;
                    break;
                default:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR_MEN;
                    break;
            }
            return basalMetabolicRate;
        }

        private static float GetThermicEffectOfExerciseForWomen(float basalMetabolicRate, ActivityLevel userActivity)
        {
            switch (userActivity)
            {
                case ActivityLevel.Sedentary:
                    basalMetabolicRate = basalMetabolicRate * SEDENTARY_FACTOR;
                    break;
                case ActivityLevel.LightlyActive:
                    basalMetabolicRate = basalMetabolicRate * LIGHT_FACTOR_WOMEN;
                    break;
                case ActivityLevel.ModeratelyActive:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR_WOMEN;
                    break;
                case ActivityLevel.VeryActive:
                    basalMetabolicRate = basalMetabolicRate * VERY_FACTOR_WOMEN;
                    break;
                case ActivityLevel.ExtremelyActive:
                    basalMetabolicRate = basalMetabolicRate * EXTREMELY_FACTOR_WOMEN;
                    break;
                default:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR_WOMEN;
                    break;
            }
            return basalMetabolicRate;
        }

        private static float GetTotalDailyEnergyExpenditure(float basalMetabolicRate, float thermicEffectOfExercise,
            float thermicEffectOfFood)
        {
            return basalMetabolicRate + thermicEffectOfExercise + thermicEffectOfFood;
        }
    }
}
