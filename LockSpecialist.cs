using System;

namespace HeistPartTwo
{
    public class LockSpecialist : IRobber
    {
        public string Name {get; set;}
        public int SkillLevel {get; set;}
        public int PercentageCut {get; set;}
        public void PerformSkill(Bank bank)
        {
            bank.VaultScore = bank.VaultScore - SkillLevel;
            Console.WriteLine($"{Name} is tyring to crack the code. Decreased security {SkillLevel} points.");
            if (bank.VaultScore <= 0)
            {
                Console.WriteLine($"{Name} has opened the vault!");
            }
        }
    }
}