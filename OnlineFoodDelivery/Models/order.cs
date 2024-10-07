//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineFoodDelivery.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class order
    {
        public int orderid { get; set; }
        public Nullable<int> productid { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> amount { get; set; }
        public string DeliveryAddress { get; set; }
        [Required(ErrorMessage = "paymentmethod is required")]
        public string paymentmethod { get; set; }
        [Required(ErrorMessage = "cardnumber is required")]
        public Nullable<long> cardnumber { get; set; }
        [Required(ErrorMessage = "cardholdername is required")]
        public string cardholdername { get; set; }
        [Required(ErrorMessage = "cvv is required")]
        public Nullable<int> cvv { get; set; }
        public System.DateTime expirydate { get; set; }
    
        public virtual product product { get; set; }
    }
}
