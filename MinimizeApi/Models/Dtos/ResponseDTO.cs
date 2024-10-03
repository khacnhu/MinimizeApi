namespace MinimizeApi.Models.Dtos
{
   public record ResponseDTO(string Name, string Description, double Price, int Quantity, DateTime Date)
    {
        public double TotalPrice => Price*Quantity;
    }

}
