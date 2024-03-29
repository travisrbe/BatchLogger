﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Yeast
    {
        public Yeast(string manufacturer, string brand, string name, string multiplierName) 
        {
            Manufacturer = manufacturer;
            Brand = brand;
            Name = name;
            Batches = [];
            MultiplierName = multiplierName;
        }
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Manufacturer { get; set; }
        [MaxLength(256)]
        public string Brand { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public double NutrientReqMult { get; set; }
        public string MultiplierName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }
}
