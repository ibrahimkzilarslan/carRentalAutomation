﻿using RentACar.Application.Features.CQRS.Queries.BrandQueries;
using RentACar.Application.Features.CQRS.Results.BrandResults;
using RentACar.Application.Interfaces;
using RentACar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.CQRS.Handlers.BrandHandlers
{
    public class GetBrandByIdQueryHandler
    {
        private readonly IRepository<Brand> _repository;

        public GetBrandByIdQueryHandler(IRepository<Brand> repository)
        {
            _repository = repository;
        }

        public async Task<GetBrandByIdQueryResults> Handle(GetBrandByIdQuery results)
        {
            var values = await _repository.GetByIdAsync(results.Id);
            return new GetBrandByIdQueryResults
            {
                BrandID = values.BrandID,
                Name = values.Name,
            };
        }
    }
}
