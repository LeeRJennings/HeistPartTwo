using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>();
            List<IRobber> crew = new List<IRobber>();
            Bank randoBank = randomizeBank();
            List<Intel> intel = new List<Intel>();
            int crewCutTotal = 0;

            Hacker sippy = new Hacker("Sippy", 100, 30);
            Muscle karla = new Muscle("Karla", 95, 28);
            LockSpecialist bory = new LockSpecialist("Bory Blark", 85, 25);
            Hacker charlie = new Hacker("Charlie", 46, 12);
            Muscle mac = new Muscle("Mac", 29, 8);
            LockSpecialist dennis = new LockSpecialist("Dennis", 32, 11);

            rolodex.Add(sippy);
            rolodex.Add(karla);
            rolodex.Add(bory);
            rolodex.Add(charlie);
            rolodex.Add(mac);
            rolodex.Add(dennis);

            intel.Add(new Intel("Alarm", randoBank.AlarmScore));
            intel.Add(new Intel("Security Guards", randoBank.SecurityGuardScore));
            intel.Add(new Intel("Vault", randoBank.VaultScore));

            Console.WriteLine($"How many scumbags are in your rolodex? {rolodex.Count}, that's how many scumbags are in there.");
            
            createARobber();
            Console.WriteLine();
            Console.WriteLine($"There are now {rolodex.Count} scumbags in your rolodex.");

            Console.WriteLine();
            intelReport();

            Console.WriteLine();
            string response = "y";
            while (response == "y")
            {
                Console.WriteLine();
                Console.WriteLine("Here's all them dang scumbags you have to pick from for your crew");
                Console.WriteLine("-----------------------------------------------------------------");
                displayRolodex();

                Console.WriteLine($"Enter a scumbag's number to add them to your crew.");
                int robberIndex = int.Parse(Console.ReadLine()) - 1;
                crew.Add(rolodex[robberIndex]);
                crewCutTotal = crew.Sum(c => c.PercentageCut);

                Console.WriteLine("Would you like to add another scumbag to your crew? (Y/N)");
                response = Console.ReadLine().ToLower();
            }

            executeHeist();

            //====================================== methods ======================================
            void createARobber()
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Enter the name of a new scumbag for your rolodex.");
                string newName = Console.ReadLine();

                while (newName != "")
                {
                    Console.WriteLine("What specialty should this scumbag have? (enter a number 1-3)");
                    Console.WriteLine("1) Hacker (disables alarms)");
                    Console.WriteLine("2) Muscle (disarms guards)");
                    Console.WriteLine("3) Lock Specialist (cracks vault)");
                    int newNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("What's this scumbag's skill level? (1-100)");
                    int newSkillLevel = int.Parse(Console.ReadLine());

                    Console.WriteLine("What percentage of the take does this scumbag get? (1-100)");
                    int newPercentageCut = int.Parse(Console.ReadLine());

                    if (newNumber == 1)
                    {
                        rolodex.Add(new Hacker(newName, newSkillLevel, newPercentageCut));
                        Console.WriteLine($"The Hacker {newName} has been added to your rolodex");
                    }
                    else if (newNumber == 2)
                    {
                        rolodex.Add(new Muscle(newName, newSkillLevel, newPercentageCut));
                        Console.WriteLine($"The Muscle {newName} has been added to your rolodex");
                    }
                    else if (newNumber == 3)
                    {
                        rolodex.Add(new LockSpecialist(newName, newSkillLevel, newPercentageCut));
                        Console.WriteLine($"The Lock Specialist {newName} has been added to your rolodex");
                    }
                    newName = "";
                    createARobber();
                }  
            }

            Bank randomizeBank()
            {
                Random rando = new Random();

                Bank bank = new Bank()
                {
                    AlarmScore = rando.Next(101),
                    VaultScore = rando.Next(101),
                    SecurityGuardScore = rando.Next(101),
                    CashOnHand = rando.Next(50000, 1000001)
                };

                return bank;
            }

            void intelReport()
            {
                intel.OrderBy(i => i.Score);
                Console.WriteLine($"Here's an intel report, ya heard? Least Secure: {intel[0].Name} | Most Secure: {intel[2].Name}");
            }

            void displayRolodex()
            {
                for (int i = 0; i < rolodex.Count; i++)
                {
                    if (!crew.Contains(rolodex[i]) && crewCutTotal + rolodex[i].PercentageCut <= 100)
                    {
                        Console.WriteLine($"{i + 1}) Name: {rolodex[i].Name} | Specialty: {rolodex[i].Specialty} | Skill Level: {rolodex[i].SkillLevel} | Cut: {rolodex[i].PercentageCut}");
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            void executeHeist()
            {
                // Console.WriteLine($"Cash: {randoBank.CashOnHand} | Alarms: {randoBank.AlarmScore} | Vault: {randoBank.VaultScore} | Guards: {randoBank.SecurityGuardScore}");
                foreach (IRobber r in crew)
                {
                    r.PerformSkill(randoBank);
                }

                if (randoBank.AlarmScore <= 0 && randoBank.SecurityGuardScore <= 0 && randoBank.VaultScore <= 0)
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Success!!! You and your crew robbed the bank!!!!");

                    decimal take = randoBank.CashOnHand;
                    foreach (IRobber r in crew)
                    {
                        decimal crewMemberTake = (r.PercentageCut / 100m) * randoBank.CashOnHand;
                        Console.WriteLine($"{r.Name} took home {crewMemberTake} dabloons from the successful heist");
                        take = take - crewMemberTake;
                    }
                    Console.WriteLine($"You took home {take} dabloons from the successful heist");
                }
                else
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Failure. Right to jail.");
                }
            }
        }
    }
}