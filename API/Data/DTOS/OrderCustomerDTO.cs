using CodeFirst2._0.Enum;

namespace API.Data.DTOS
{
    public class OrderCustomerDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public State OrderState { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
    }
}
