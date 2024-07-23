namespace University_Management_Project
{
    internal class UniversityInputModel
    {
        public string Name;
        public int ID;

        public UniversityInputModel(University university)
        {
            ID = university.ID;
            Name = university.Name;
        }
    }
}