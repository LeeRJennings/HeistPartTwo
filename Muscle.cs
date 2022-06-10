using System;

namespace HeistPartTwo
{
    public class Muscle : IRobber
    {
        public string Name {get; set;}
        public string Specialty {get;}
        public int SkillLevel {get; set;}
        public int PercentageCut {get; set;}
        public void PerformSkill(Bank bank)
        {
            bank.SecurityGuardScore = bank.SecurityGuardScore - SkillLevel;
            Console.WriteLine($"{Name} is locked in an intense Kung-Fu battle with the guards. Decreased security {SkillLevel} points.");
            if (bank.SecurityGuardScore <= 0)
            {
                Console.WriteLine($"{Name} has disabled the guards ...... permenantly");
            }
        }

        public Muscle(string name, int skillLevel, int percentageCut)
        {
            Name = name;
            SkillLevel = skillLevel;
            PercentageCut = percentageCut;
            Specialty = "Muscle";
        }
    }
}