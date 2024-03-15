using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using Services.ServiceHelpers;
using System.Xml.Schema;

namespace Services
{
    internal sealed class NutrientAdditionService : INutrientAdditionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private CalculatorHelper calcHelper = new CalculatorHelper();
        public NutrientAdditionService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<NutrientAdditionDto> Create(string userId, Guid batchId, Guid nutrientId, int priority, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, batchId, cancellationToken);
            if (UserOwnsBatch)
            {
                Nutrient? sourceNutrient = await _repositoryManager.NutrientRepository
                    .FindByCondition(x => x.Id == nutrientId && x.IsDeleted == false)
                    .SingleOrDefaultAsync(cancellationToken: cancellationToken);
                
                if (sourceNutrient == null)
                {
                    throw new NutrientNotFoundException(nutrientId);
                }
                NutrientAddition nutrientAddition = new NutrientAddition() {
                    NameOverride = sourceNutrient.Name,
                    MaxGramsPerLiterOverride = sourceNutrient.MaxGramsPerLiter,
                    YanPpmPerGramOverride = sourceNutrient.YanPpmPerGram,
                    EffectivenessMutiplierOverride = sourceNutrient.EffectivenessMutiplier,
                    IsDeleted = false,
                    BatchId = batchId,
                    NutrientId = nutrientId,
                    Priority = priority,
                };

                _repositoryManager.NutrientAdditionRepository.Create(nutrientAddition);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                NutrientAdditionDto nutrientAdditionDto = nutrientAddition.Adapt<NutrientAdditionDto>();
                return nutrientAdditionDto;
            }
            else
            {
                throw new UserDoesNotOwnBatchException(userId, batchId);
            }
        }
        public async Task<IEnumerable<NutrientAdditionDto>> GetByBatchId(string userId, Guid batchId, CancellationToken cancellationToken)
        {
            bool UserOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, batchId, cancellationToken);
            bool UserContributesBatch = await _repositoryManager.BatchRepository
                .UserContributesBatch(userId, batchId, cancellationToken);
            if (UserOwnsBatch || UserContributesBatch)
            {
                var nutrientAdditions =
                    _repositoryManager.NutrientAdditionRepository.FindByCondition(x => x.BatchId == batchId && x.IsDeleted == false)
                    .OrderBy(x => x.Priority)
                    .ToList();
                return nutrientAdditions.Adapt<IEnumerable<NutrientAdditionDto>>();
            }
            else
            {
                throw new UserNotAuthorizedException(userId, batchId);
            }
        }

        public async Task<NutrientAdditionDto> Update(string userId, NutrientAdditionDto nuAdd, CancellationToken cancellationToken)
        {
            try
            {
                bool UserOwnsBatch = await _repositoryManager.BatchRepository
                    .UserOwnsBatch(userId, nuAdd.BatchId, cancellationToken);
                var nutrientAddition = await _repositoryManager.NutrientAdditionRepository
                    .FindSingleOrDefaultAsync(x => x.Id == nuAdd.Id) ?? throw new NutrientNotFoundException(nuAdd.Id);

                if (UserOwnsBatch)
                {
                    _repositoryManager.NutrientAdditionRepository
                        .Update(nuAdd.Adapt<NutrientAddition>());
                    await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                    nuAdd = nutrientAddition.Adapt<NutrientAdditionDto>();
                    return nuAdd;
                }
                else
                {
                    throw new UserDoesNotOwnBatchException(userId, nuAdd.BatchId);
                }
            }
            catch (Exception)
            {
                throw new SaveFailedException(nuAdd.Id);
            }
            
        }

        public async Task<IEnumerable<NutrientAdditionDto>> SetStackPreset(string userId, Guid batchId, Guid stackPresetId, CancellationToken cancellationToken)
        {
            try
            {
                Batch batch = await _repositoryManager.BatchRepository.GetByIdAsync(batchId, cancellationToken)
                    ?? throw new BatchNotFoundException(batchId);
                if (batch.OwnerUserId == userId)
                {
                    List<NutrientAdditionDto> NuAddDtos = [];

                    var presetLookups = await _repositoryManager.StackPresetLookupRepository
                        .FindByConditionAsync(spl => spl.StackPresetId == stackPresetId);
                    var nutrients = await _repositoryManager.NutrientRepository.FindAllAsync();
                    Nutrient? sourceNutrient;
                    NutrientAdditionDto NuAddDto = new NutrientAdditionDto();

                    foreach (var lookup in presetLookups)
                    {
                        sourceNutrient = nutrients.Where(n => n.Id == lookup.NutrientId).SingleOrDefault();

                        if (sourceNutrient != null)
                        {
                            NuAddDto = new NutrientAdditionDto()
                            {
                                NameOverride = sourceNutrient.Name,
                                MaxGramsPerLiterOverride = sourceNutrient.MaxGramsPerLiter,
                                YanPpmPerGramOverride = sourceNutrient.YanPpmPerGram,
                                EffectivenessMutiplierOverride = sourceNutrient.EffectivenessMutiplier,
                                BatchId = batchId,
                                NutrientId = sourceNutrient.Id,
                                Priority = lookup.Priority,
                            };
                            NuAddDtos.Add(NuAddDto);
                        }
                        else
                        {
                            throw new NutrientNotFoundException(lookup.Id);
                        }
                    }
                    IEnumerable<NutrientAdditionDto> nutrientAdditionDtos = NuAddDtos;
                    calcHelper.CalculateNutrients(ref batch, ref nutrientAdditionDtos);

                    List<NutrientAddition> nuAdds = NuAddDtos.Adapt<List<NutrientAddition>>();
                    
                    for (int i = 0; i < nuAdds.Count(); i++)
                    {
                        nuAdds[i].IsDeleted = false;
                        _repositoryManager.NutrientAdditionRepository.Create(nuAdds[i]);
                    }

                    //foreach(var nuAdd in nuAdds)
                    //{
                    //    nuAdd.IsDeleted = false;
                    //    _repositoryManager.NutrientAdditionRepository.Create(nuAdd);
                    //}
                    await _repositoryManager.UnitOfWork.SaveChangesAsync();

                    return nuAdds.Adapt<IEnumerable<NutrientAdditionDto>>();
                }
                else
                {
                    throw new UserDoesNotOwnBatchException(userId, batchId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<NutrientAdditionDto>> UpdateRange(string userId, IEnumerable<NutrientAdditionDto> nuAddDtos, CancellationToken cancellationToken)
        {
            List<NutrientAdditionDto> results = new();
            if (nuAddDtos.Count() > 0)
            {
                var batchId = nuAddDtos.ToList().First().BatchId;
                var batch = await _repositoryManager.BatchRepository.GetByIdAsync(batchId, cancellationToken);
                bool userOwnsBatch = false;

                //validate batch exists
                if (batch != null)
                {
                    userOwnsBatch = (batch.OwnerUserId == userId);
                }
                else
                {
                    throw new BatchNotFoundException(batchId);
                }

                //validate ownership
                if (userOwnsBatch)
                {
                    calcHelper.CalculateNutrients(ref batch, ref nuAddDtos);
                    foreach (var nuAddDto in nuAddDtos)
                    {
                        try
                        {
                            //Get from database
                            var nuAdd = await _repositoryManager.NutrientAdditionRepository
                                .FindSingleOrDefaultAsync(x => x.Id == nuAddDto.Id) ?? throw new NutrientNotFoundException(nuAddDto.Id);
                            //map changes onto tracked db object
                            nuAdd = nuAddDto.Adapt<NutrientAddition>();
                            //Update
                            _repositoryManager.NutrientAdditionRepository.Update(nuAdd);
                            //add updated NuAdd to return list.
                            results.Add(nuAdd.Adapt<NutrientAdditionDto>());
                        }
                        catch (Exception)
                        {
                            throw new SaveFailedException(nuAddDto.Id);
                        }
                    }
                    try
                    {
                        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Bulk save failed: " + ex.Message);
                    }
                }
                else
                {
                    throw new UserDoesNotOwnBatchException(userId, batchId);
                }
            }
            return results;
        }

        public async Task<IEnumerable<NutrientAdditionDto>> Reset(string userId, IEnumerable<NutrientAdditionDto> nuAddDtos, CancellationToken cancellationToken)
        {
            List<NutrientAdditionDto> results = new();
            if (nuAddDtos.Count() > 0)
            {
                var batchId = nuAddDtos.ToList().First().BatchId;
                var batch = await _repositoryManager.BatchRepository.GetByIdAsync(batchId, cancellationToken);
                bool userOwnsBatch = false;

                //validate batch exists
                if (batch != null)
                {
                    userOwnsBatch = (batch.OwnerUserId == userId);
                }
                else
                {
                    throw new BatchNotFoundException(batchId);
                }

                //validate ownership
                if (userOwnsBatch)
                {
                    foreach (var nuAddDto in nuAddDtos)
                    {
                        try
                        {
                            //update each
                            var nuAdd = await _repositoryManager.NutrientAdditionRepository
                                .FindSingleOrDefaultAsync(x => x.Id == nuAddDto.Id) ?? throw new NutrientNotFoundException(nuAddDto.Id);

#pragma warning disable CS0618 // Hard delete is OK here.
                            _repositoryManager.NutrientAdditionRepository
                                .HardDelete(nuAdd);
#pragma warning restore CS0618 // Hard delete is OK here.
                        }
                        catch (Exception)
                        {
                            throw new SaveFailedException(nuAddDto.Id);
                        }
                    }
                    try
                    {
                        await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
                        IEnumerable<NutrientAddition> nuAdds = await _repositoryManager.NutrientAdditionRepository
                            .FindByConditionAsync(na => na.BatchId == batch.Id);
                        results = nuAdds.Adapt<IEnumerable<NutrientAdditionDto>>().ToList();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Nutrient Additions reset failed: " + ex.Message);
                    }
                }
                else
                {
                    throw new UserDoesNotOwnBatchException(userId, batchId);
                }
            }
            return results;
        }

        public async Task Delete(string userId, NutrientAdditionDto nuAddDto, CancellationToken cancellationToken)
        {
            bool userOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, nuAddDto.BatchId, cancellationToken);
            if (userOwnsBatch)
            {
                NutrientAddition nutrientAddition = nuAddDto.Adapt<NutrientAddition>();
#pragma warning disable CS0618 // Hard delete is OK here.
                _repositoryManager.NutrientAdditionRepository
                    .HardDelete(nutrientAddition);
#pragma warning restore CS0618 // Hard delete is OK here.
                await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new UserDoesNotOwnBatchException(userId, nuAddDto.BatchId);
            }
        }

        public async Task<NutrientAdditionDto> RestoreDefaultValues(string userId, NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            try
            {
                bool userOwnsBatch = await _repositoryManager.BatchRepository
                .UserOwnsBatch(userId, nutrientAdditionDto.BatchId, cancellationToken);
                if (userOwnsBatch)
                {
                    Nutrient sourceNutrient = await _repositoryManager.NutrientRepository
                        .FindSingleOrDefaultAsync(n => n.Id == nutrientAdditionDto.NutrientId)
                        ?? throw new NutrientNotFoundException(nutrientAdditionDto.NutrientId);
                    NutrientAddition nuAdd = await _repositoryManager.NutrientAdditionRepository
                        .FindSingleOrDefaultAsync(na => na.Id == nutrientAdditionDto.Id)
                        ?? throw new NutrientAdditionNotFoundException(nutrientAdditionDto.NutrientId);

                    nuAdd.NameOverride = sourceNutrient.Name;
                    nuAdd.EffectivenessMutiplierOverride = sourceNutrient.EffectivenessMutiplier;
                    nuAdd.MaxGramsPerLiterOverride = sourceNutrient.MaxGramsPerLiter;
                    nuAdd.YanPpmPerGramOverride = sourceNutrient.YanPpmPerGram;

                    _repositoryManager.NutrientAdditionRepository.Update(nuAdd);
                    await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

                    return nuAdd.Adapt<NutrientAdditionDto>();
                }
                else
                {
                    throw new UserDoesNotOwnBatchException(userId, nutrientAdditionDto.BatchId);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
