using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateOfUpdate { get; set; }

        public string Status { get; set; }

        public string Delivery { get; set; }

        public string Payment { get; set; }

        public int Cost { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
