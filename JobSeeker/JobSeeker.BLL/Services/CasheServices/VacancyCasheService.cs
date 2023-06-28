﻿using AutoMapper;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.CasheServices
{
	public class VacancyCasheService : IVacancyCacheService
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;
		private readonly IMemoryCache _memoryCache;

		private const string KeyToCache = "OldVacancies";

		public VacancyCasheService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IMemoryCache memoryCache)
		{
			_repositoryWrapper = repositoryWrapper;
			_mapper = mapper;
			_memoryCache = memoryCache;
		}
		public IEnumerable<VacancyShortDTO> GetCachedVacancies()
		{
			if (!_memoryCache.TryGetValue(KeyToCache, out List<VacancyShortDTO>? cachedVacancies))
			{
				InitializeCache();
			}
			_memoryCache.TryGetValue(KeyToCache, out List<VacancyShortDTO>? cachedVacanciesEnsure);
			return cachedVacanciesEnsure!;
		}
		public void AddVacanciesToCache(IEnumerable<VacancyShortDTO> vacanciesToAdd)
		{
			if (_memoryCache.TryGetValue(KeyToCache, out List<VacancyShortDTO>? cachedVacancies))
			{
				cachedVacancies?.AddRange(vacanciesToAdd);
			}
		}
		public void ClearCache()
		{
			List<VacancyShortDTO> emptyList = new();

			var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
			_memoryCache.Set(KeyToCache, emptyList, cacheEntryOptions);
		}
		public void InitializeCache()
		{
			var vacancies = GetOldVacancies();

			var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
			_memoryCache.Set(KeyToCache, vacancies, cacheEntryOptions);
		}
		private IEnumerable<VacancyShortDTO> GetOldVacancies()
		{
			var oldVacancies = _repositoryWrapper.VacancyRepository
				.GetAllAsync(predicate: s => s.CreatedDate == DateTime.Today).Result;

			var shortVacancyDTO = _mapper.Map<IEnumerable<VacancyShortDTO>>(oldVacancies);
			return shortVacancyDTO;
		}

	}
}
