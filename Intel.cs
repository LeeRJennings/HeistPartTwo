namespace HeistPartTwo
{
    public class Intel
    {
        public string Name {get; set;}
        public int Score {get; set;}

        public Intel(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}