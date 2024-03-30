using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> standarts;

        protected Robot()
        {
            BatteryLevel = BatteryCapacity;
            standarts = new List<int>();
        }

        public string Model
        {
            get => model; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }

        public int BatteryLevel
        {
            get => batteryLevel; 
            private set
            {
                batteryLevel = value;
            }
        }

        public int ConvertionCapacityIndex
        {
            get => convertionCapacityIndex; 
            private set
            {
                convertionCapacityIndex = value;
            }
        }

        public IReadOnlyCollection<int> InterfaceStandards 
            => standarts;

        public void Eating(int minutes)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteService(int consumedEnergy)
        {
            throw new NotImplementedException();
        }

        public void InstallSupplement(ISupplement supplement)
        {
            throw new NotImplementedException();
        }
    }
}
