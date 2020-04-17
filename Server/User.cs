namespace Server
{
    public class User
    {
        public int No { get; set; }
        public int Index { get; set; }
        public string IP { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public int Gold { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }
        public int Map { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public User(int no, string ip, string id, string name, int level, int exp, int gold)
        {
            No = no;

            Index = 1;
            IP = ip;

            ID = id;
            Name = name;

            Level = level;
            Exp = exp;
            MaxExp = 100;
            Gold = gold;

            HP = 100;
            MaxHP = 100;
            MP = 100;
            MaxMP = 100;

            Map = 1;
            X = 0;
            Y = 0;
            Z = 0;
        }
    }
}
