using System.Collections.Generic;

namespace ShootingWebAgent.DataModels.APIModel
{
    public class DisagJson
    {
        public int Id { get; set; }

        public string MessageType { get; set; }
        public string MessageVerb { get; set; }
        public bool Sequential { get; set; }
        public int Ranges { get; set; }
        public List<Object> Objects { get; set; }
        public string UUID { get; set; }
    }

    public class Club
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string UUID { get; set; }
    }

    public class Shooter
    {
        public int Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Birthyear { get; set; }
        public string InternalID { get; set; }
        public string Identification { get; set; }
        public string Team { get; set; }
        public Club Club { get; set; }
    }

    public class Competition
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Evaluation { get; set; }
        public string DateTime { get; set; }
        public string UUID { get; set; }
    }

    public class MenuItem
    {
        public int Id { get; set; }

        public string MenuID { get; set; }
        public string MenuPointName { get; set; }
        public string MenuItemName { get; set; }
        public string UUID { get; set; }
    }

    public class Object
    {
        public int Id { get; set; }

        public double DecimalValue { get; set; }
        public string ShotDateTime { get; set; }
        public string TLStatus { get; set; }
        public int LastTLChange { get; set; }
        public string Source { get; set; }
        public int Range { get; set; }
        public Shooter Shooter { get; set; }
        public Competition Competition { get; set; }
        public string DiscType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Distance { get; set; }
        public int Count { get; set; }
        public int FullValue { get; set; }
        public double DecValue { get; set; }
        public int Run { get; set; }
        public bool IsValid { get; set; }
        public bool IsWarmup { get; set; }
        public bool IsHot { get; set; }
        public bool IsDummy { get; set; }
        public bool IsInnerten { get; set; }
        public bool IsShootoff { get; set; }
        public MenuItem MenuItem { get; set; }
        public string Remark { get; set; }
        public string UUID { get; set; }
    }
}
