namespace EShop.Models.DTOS
{
    public class NewOrderDTO
    {
        public virtual List<OrderLineDTO> OrderLinesDTO { get; set; }
    }
}
