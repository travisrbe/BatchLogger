using Contracts;
using Domain.Entities;
using Domain.Exceptions;

namespace Services.ServiceHelpers
{
    public class CalculatorHelper
    {
        public void CalculateBrix(ref Batch batch)
        {
            //This calculation is approximate, there are many approximations.
            //This version of the equation via MeadMakr.com's Allen Jones
            batch.Brix = Math.Round(182.9622 * Math.Pow(batch.SpecificGravity, 3)
                - 777.3009 * Math.Pow(batch.SpecificGravity, 2)
                + 1264.5170 * batch.SpecificGravity
                - 670.1831, 1);
        }
        public void CalculateSugarGramsPerLiter(ref Batch batch)
        {
            if (batch.Brix == null) CalculateBrix(ref batch);
            if (batch.Brix != null)
            {
                //Sugar g/L = Brix * SpecificGravity * 10. See my white paper to understand why.
                batch.SugarPpm = (double)Math.Round(batch.SpecificGravity * (double)batch.Brix * 10, 0);
            }
            else
            {
                if (batch.Brix == null) throw new CalculatorFailedException($"Null: Batch.Brix for batch {batch.Id}");
            }
        }

        public void CalculateTargetYan(ref Batch batch)
        {
            if (batch.SugarPpm == null) CalculateSugarGramsPerLiter(ref batch);

            if (batch.SugarPpm != null && batch.Yeast != null)
            {
                //(Sugar g/L * Yeast Nutrient Multiplier) - Offset
                batch.TotalTargetYanPpm = (int)Math.Round((double)((batch.SugarPpm * batch.Yeast.NutrientReqMult) - batch.OffsetYanPpm), 0);
            }
            else
            {
                if (batch.Yeast == null) throw new CalculatorFailedException($"Null: Batch.Yeast for batch {batch.Id}");
                if (batch.SugarPpm == null) throw new CalculatorFailedException($"Null: Batch.SugarPpm for batch {batch.Id}");
            }
        }

        public void CalculateNutrients(ref Batch batch, ref IEnumerable<NutrientAdditionDto> nuAddDtos)
        {
            if (batch.TotalTargetYanPpm == null) CalculateTargetYan(ref batch);
            batch.SubtotalYanPpm = 0;

            double addGramsPerLiter = 0;
            foreach (NutrientAdditionDto nuAdd in nuAddDtos)
            {
                if (batch.SubtotalYanPpm < batch.TotalTargetYanPpm && batch.TotalTargetYanPpm > 0)
                {
                    if (nuAdd.YanPpmPerGramOverride == null || nuAdd.YanPpmPerGramOverride <= 0)
                    {
                        addGramsPerLiter = 0;
                    }
                    else
                    {
                        //if (remaining required YAN)/(this nutrient YAN PPM / g) > max allowed
                        if ((((double)batch.TotalTargetYanPpm - ((double?)batch.SubtotalYanPpm ?? 0)) / (double)nuAdd.YanPpmPerGramOverride) > (nuAdd.MaxGramsPerLiterOverride ?? Int32.MaxValue))
                        {
                            //add maximum allowed YAN
                            addGramsPerLiter = nuAdd.MaxGramsPerLiterOverride ?? Int32.MaxValue;
                        }
                        else
                        {
                            //calculate how much to meet requirements
                            addGramsPerLiter = (((double)batch.TotalTargetYanPpm - (double)(batch.SubtotalYanPpm ?? 0)) / (double)nuAdd.YanPpmPerGramOverride);
                        }
                    }
                    //calculate how much PPM YAN this adds
                    nuAdd.YanPpmAdded = (int)Math.Round(addGramsPerLiter * nuAdd.YanPpmPerGramOverride * nuAdd.EffectivenessMutiplierOverride ?? 1, 0);
                    //convert grams / Liter to TOTAL grams
                    nuAdd.GramsToAdd = Math.Round(addGramsPerLiter * batch.VolumeLiters, 2);
                    batch.SubtotalYanPpm += nuAdd.YanPpmAdded;
                }
                else
                {
                    nuAdd.YanPpmAdded = 0;
                    nuAdd.GramsToAdd = 0;
                }
            }
        }

        //public void CalculateRemainder(ref Batch batch, ref NutrientAddition remainderNutrient)
        //{
        //    if (batch.TotalTargetYanPpm == null) CalculateTargetYan(ref batch);
        //    if (batch.SubtotalYanPpm == null) CalculateNutrients(ref batch);

        //    if (batch.TotalTargetYanPpm > 0 && batch.RemainderPpmNeeded != null)
        //    {
        //        //calculate the remainder
        //        batch.RemainderNutrientGrams = Math.Round((double)batch.TotalTargetYanPpm - batch.SubtotalYanPpm ?? 0, 0);
        //        if (batch.RemainderNutrientGrams > 0)
        //        {
        //            remainderNutrient.GramsToAdd = Math.Round(
        //                ((double)(batch.RemainderPpmNeeded / 
        //                (remainderNutrient.YanPpmPerGramOverride * remainderNutrient.EffectivenessMutiplierOverride ?? 1))
        //                * batch.VolumeLiters)
        //                , 2);
        //            batch.NutrientAdditions.Add(remainderNutrient);
        //        }
        //    }
        //    else
        //    {
        //        if (batch.TotalTargetYanPpm == null) throw new CalculatorFailedException($"Null: Batch.TotalTargetYanPPM for batch {batch.Id}");
        //        if (batch.RemainderPpmNeeded == null) throw new CalculatorFailedException($"Null: Batch.RemainderPpmNeeded for batch {batch.Id}");
        //    }
        //}
    }
}
