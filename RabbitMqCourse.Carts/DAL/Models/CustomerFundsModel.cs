using System.ComponentModel.DataAnnotations;

namespace RabbitMqCourse.Carts.DAL.Models;

public class CustomerFundsModel
{
    [Key]
    public long CustomerId { get; set; }
    public decimal CurrentFunds { get; set; }
}
