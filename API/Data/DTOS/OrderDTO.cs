using CodeFirst2._0.Enum;
using API.Data.Models;

namespace API.Data.DTOS
{
    public class OrderDTO
    {

        public string Number { get; set; }
        public State OrderState { get; set; }
        public DateTime DateTime { get; set; }

        public virtual List<OrderLineDTO> OrderLinesDTO { get; set; }


    }
}
