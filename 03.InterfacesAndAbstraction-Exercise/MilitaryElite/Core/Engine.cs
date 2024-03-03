
using MilitaryElite.Core.Interfaces;
using MilitaryElite.Enums;
using MilitaryElite.Models;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            string command = string.Empty;

            List<Private> privates = new List<Private>();

            while ((command = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] soldierInfo = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string soldierType = soldierInfo[0];
                    string id = soldierInfo[1];
                    string firstName = soldierInfo[2];
                    string lastName = soldierInfo[3];

                    ISoldier soldier;

                    if (soldierType == "Private")
                    {
                        decimal salary = decimal.Parse(soldierInfo[4]);

                        soldier = new Private(id, firstName, lastName, salary);

                        Private priv = new(id, firstName, lastName, salary);
                        privates.Add(priv);
                    }
                    else if (soldierType == "LieutenantGeneral")
                    {
                        decimal salary = decimal.Parse(soldierInfo[4]);

                        List<Private> privatesToAdd = new List<Private>();

                        for (int i = 5; i < soldierInfo.Length; i++)
                        {
                            privatesToAdd.Add(privates.First(p => p.Id == soldierInfo[i]));
                        }

                        soldier = new LieutenantGeneral(id, firstName, lastName, salary, privatesToAdd);
                    }
                    else if (soldierType == "Engineer")
                    {
                        decimal salary = decimal.Parse(soldierInfo[4]);

                        string corps = soldierInfo[5];

                        Corps EngineerCorps;

                        if (corps == "Airforces")
                        {
                            EngineerCorps = Corps.Airforces;
                        }
                        else if (corps == "Marines")
                        {
                            EngineerCorps = Corps.Marines;
                        }
                        else
                        {
                            throw new Exception();
                        }

                        List<Repair> repairs = new List<Repair>();

                        for (int i = 6; i < soldierInfo.Length; i += 2)
                        {
                            string partName = soldierInfo[i];
                            int hoursWorked = int.Parse(soldierInfo[i + 1]);

                            Repair repair = new(partName, hoursWorked);
                            repairs.Add(repair);
                        }

                        soldier = new Engineer(id, firstName, lastName, salary, EngineerCorps, repairs);
                    }
                    else if (soldierType == "Commando")
                    {
                        decimal salary = decimal.Parse(soldierInfo[4]);

                        string corps = soldierInfo[5];

                        Corps CommandoCorps;

                        if (corps == "Airforces")
                        {
                            CommandoCorps = Corps.Airforces;
                        }
                        else if (corps == "Marines")
                        {
                            CommandoCorps = Corps.Marines;
                        }
                        else
                        {
                            throw new Exception();
                        }

                        List<Mission> missions = new List<Mission>();

                        for (int i = 6; i < soldierInfo.Length; i += 2)
                        {
                            string codeName = soldierInfo[i];

                            State CurrentState;

                            if (soldierInfo[i + 1] == "inProgress")
                            {
                                CurrentState = State.inProgress;
                            }
                            else if (soldierInfo[i + 1] == "Finished")
                            {
                                CurrentState = State.Finished;
                            }
                            else
                            {
                                continue;
                            }

                            Mission mission = new(codeName, CurrentState);
                            missions.Add(mission);
                        }

                        soldier = new Commando(id, firstName, lastName, salary, CommandoCorps, missions);
                    }
                    else // Spy
                    {
                        int codeNumber = int.Parse(soldierInfo[4]);

                        soldier = new Spy(id, firstName, lastName, codeNumber);
                    }

                    Console.WriteLine(soldier);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
}
