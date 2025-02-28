﻿namespace CarFactory_Domain
{
    public class Chassis
    {
        public Chassis(string description, bool valid, int doorAmountTotal)
        {
            Description = description;
            ValidConstruction = valid;
            DoorAmount = doorAmountTotal;
        }
        public string Description { get; }
        public bool ValidConstruction { get; }
        public int DoorAmount { get; }
    }
}
