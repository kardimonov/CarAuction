using CarAuction.Data.Models;
using CarAuction.Logic.Interfaces;
using System;

namespace CarAuction.Logic.Services
{
    internal class CarGradeService : ICarGradeService
    {
        public int CalculateGrade(Car car)
        {
            var grade = 50;
            grade = CorrectByAge(car.Year, grade);
            grade = CorrectByOdometer(car.Odometer, grade);
            grade = CorrectByState(car.SmallScratches, car.StrongScratches, car.SuspensionProblems, car.ElectricsFailures, grade);
            
            return grade;
        }

        private static int CorrectByAge(DateTime year, int grade)
        {
            var span = DateTime.Today.Subtract(year);
            var zeroTime = new DateTime(1, 1, 1);
            var carAge = (zeroTime + span).Year - 1;
            return grade - carAge;
        }

        private static int CorrectByOdometer(int odometer, int grade)
        {
            return grade <= 30
                ? grade
                : odometer > 300000 
                    ? 30 
                    : grade;
        }

        private static int CorrectByState(bool smallScratches, bool strongScratches, bool suspensionProblems, bool electricsFailures, int grade)
        {
            var rate = 1.0;
            if (smallScratches)
            {
                rate *= 1.04;
            }
            if (strongScratches)
            {
                rate *= 1.08;
            }
            if (suspensionProblems)
            {
                rate *= 1.06;
            }
            if (electricsFailures)
            {
                rate *= 1.08;
            }
            return (int)Math.Round(grade / rate);
        }
    }
}
