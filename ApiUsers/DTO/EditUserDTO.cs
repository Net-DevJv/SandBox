namespace ApiUsers.DTO
{
    public class EditUserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public double Wage { get; set; }
        public string CPF { get; set; }
        public bool Situation { get; set; }
    }
}
