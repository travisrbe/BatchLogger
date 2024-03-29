﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Batch
    {
        public Batch(double specificGravity,
            int offsetYanPpm, 
            double volumeLiters,
            string ownerUserId,
            Guid yeastId)
        {
            SpecificGravity = specificGravity;
            OffsetYanPpm = offsetYanPpm;
            VolumeLiters = volumeLiters;
            OwnerUserId = ownerUserId;
            YeastId = yeastId;

            LogEntries = [];
            NutrientAdditions = [];
            UserBatches = [];
        }

        public Batch()
        {
            SpecificGravity = 0;
            OffsetYanPpm = 0;
            VolumeLiters = 0;
            OwnerUserId = string.Empty;
            YeastId = new Guid();

            LogEntries = [];
            NutrientAdditions = [];
            UserBatches = [];
        }

        //database generated
        public Guid Id { get; set; }

        //required inputs
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        public double SpecificGravity { get; set; }
        public int OffsetYanPpm { get; set; }
        public double VolumeLiters { get; set; }

        //optional inputs
        [MaxLength(2048)]
        public string? Ingredients { get; set; }
        [MaxLength(2048)]
        public string? Process { get; set; }
        public bool GoFermUsed { get; set; } = true;
        public double? phReading { get; set; }

        //calculated
        public double? Brix { get; set; }
        public double? SugarPpm { get; set; }
        public int? SubtotalYanPpm {get; set;}
        public int? TotalTargetYanPpm { get; set; }
        public int? RemainderPpmNeeded { get; set; }
        public int? RemainderNutrientId { get; set; }
        public double? RemainderNutrientGrams { get; set; }
        public bool IsComplete { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public bool IsPublic { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [MaxLength(450)]
        public String OwnerUserId { get; set; }
        public virtual User Owner { get; set; } = null!;

        public Guid YeastId { get; set; }
        public virtual Yeast Yeast { get; set; } = null!;

        public virtual ICollection<BatchLogEntry> LogEntries { get; set; }
        public virtual ICollection<NutrientAddition> NutrientAdditions { get; set; }
        public virtual ICollection<UserBatch> UserBatches { get; set; }
    }
}
