﻿namespace EITracker.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? Email { get; set; }
    }
}
