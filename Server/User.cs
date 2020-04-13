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
            this.No = no;

            this.Index = 1;
            this.IP = ip;

            this.ID = id;
            this.Name = name;

            this.Level = level;
            this.Exp = exp;
            this.MaxExp = 100;
            this.Gold = gold;

            this.HP = 100;
            this.MaxHP = 100;
            this.MP = 100;
            this.MaxMP = 100;

            this.Map = 1;
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
    }
}
