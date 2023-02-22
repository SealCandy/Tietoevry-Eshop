namespace API.Data.DTOS
{
    public class NewOrderDTO
    {
        public virtual List<OrderLineDTO> OrderLinesDTO { get; set; }
    }
}
