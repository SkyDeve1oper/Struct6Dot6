namespace Struct6Dot6
{
    sealed public class GridStudent
    {
        public GridStudent(int Id, string Surname, string Name, string Patronymic,string Group, string Average)
        {
            this.Id = Id;
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Average = Average;
            this.Group = Group;
        }
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Average { get; set; }
        public string Group { get; set; }
    }
}
