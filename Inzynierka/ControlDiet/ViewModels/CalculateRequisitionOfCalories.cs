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
        private const float SEDENTARY_FACTOR = 0.2F;
        private const float LIGHT_FACTOR = 0.375F;
        private const float MODERATELY_FACTOR = 0.55F;
        private const float VERY_FACTOR = 0.725F;
        private const float EXTREMELY_FACTOR = 0.9F;
        private const float THERMIC_EFFECT_OF_FOOD_FACTOR = 0.1F;
        private const float CHANGE_WEIGHT_FACTOR = 0.15F;

        public static int CalculateRequisitionOfCalories(User user)
        {
            float basalMetabolicRate = GetBasalMetabolicRate(user.Sex, user.Weight, user.Height, user.Age);
            float thermicEffectOfExercise;
            thermicEffectOfExercise = GetThermicEffectOfExercise(basalMetabolicRate, user.Activity);
            
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
            else if (goal.Equals(UserGoal.LoseWeight))
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

        private static float GetThermicEffectOfExercise(float basalMetabolicRate, ActivityLevel userActivity)
        {
            switch (userActivity)
            {
                case ActivityLevel.Sedentary:
                    basalMetabolicRate = basalMetabolicRate * SEDENTARY_FACTOR;
                    break;
                case ActivityLevel.Lightly:
                    basalMetabolicRate = basalMetabolicRate * LIGHT_FACTOR;
                    break;
                case ActivityLevel.Moderately:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR;
                    break;
                case ActivityLevel.Very:
                    basalMetabolicRate = basalMetabolicRate * VERY_FACTOR;
                    break;
                case ActivityLevel.Extremely:
                    basalMetabolicRate = basalMetabolicRate * EXTREMELY_FACTOR;
                    break;
                default:
                    basalMetabolicRate = basalMetabolicRate * MODERATELY_FACTOR;
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
