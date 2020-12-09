using System.Collections.Generic;
using BLL.App.DTO;

namespace WebApp.ViewModels
{
    public class CartDeliveryViewModel
    {
        public Dictionary<string,Dictionary<string, string>>? PaymentMethods { get; set; }
        public Cart Cart { get; set; } = default!;
        public IEnumerable<DeliveryType>? DeliveryTypes { get; set; }
        public DeliveryType? DeliveryType { get; set; }
        public Delivery? Delivery { get; set; }

    }
}