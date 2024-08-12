namespace University_Management_Project
{
    public class Message
    {
        public readonly string Name;
        public readonly string Describrion;

        public static readonly Message Success = new Message("Success");

        public Message(string name, string describtion)
        {
            Name = name;
            Describrion = describtion;
        }

        public Message(string name)
        {
            Name = name;
            Describrion = "";
        }
    }
}