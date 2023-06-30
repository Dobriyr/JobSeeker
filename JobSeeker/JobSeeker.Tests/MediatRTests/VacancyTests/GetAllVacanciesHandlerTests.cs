using AutoMapper;
using FluentResults;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.MediatR.ResultVariations;
using JobSeeker.BLL.MediatR.Vacancy.GetAll;
using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace JobSeeker.Tests.MediatRTests.VacancyTests
{
	public class GetAllVacanciesHandlerTests
	{
		private readonly Mock<IRepositoryWrapper> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;

		public GetAllVacanciesHandlerTests()
		{
			_mockRepository = new Mock<IRepositoryWrapper>();
			_mockMapper = new Mock<IMapper>();
		}
		[Fact]
		public async Task GetAll_RetunsCorrectType()
		{
			// Arrange
			RepositorySetup();
			MapperSetup();
			var handler = new GetAllVacanciesHandler(_mockRepository.Object, _mockMapper.Object);

			// Act
			var result = await handler.Handle(new GetAllVacanciesQuery(), CancellationToken.None);

			// Assert
			Assert.IsType<Result<IEnumerable<VacancyDto>>>(result);
		}
		
		[Fact]
		public async Task GetAll_RetunsCorrectData()
		{
			// Arrange
			RepositorySetup();
			MapperSetup();
			var handler = new GetAllVacanciesHandler(_mockRepository.Object, _mockMapper.Object);

			// Act
			var result = await handler.Handle(new GetAllVacanciesQuery(), CancellationToken.None);

			// Assert
			Assert.Equal(result.Value, GetVacancyDtos());
		}
	
		[Fact]
		public async Task GetAll_ReturnsSuccessResult()
		{
			// Arrange
			RepositorySetup();
			MapperSetup();
			var handler = new GetAllVacanciesHandler(_mockRepository.Object, _mockMapper.Object);

			// Act
			var result = await handler.Handle(new GetAllVacanciesQuery(), CancellationToken.None);

			// Assert
			Assert.True(result.IsSuccess);
		}

		[Fact]
		public async Task GetAll_WithEmptyData_ReturnsNullResult()
		{
			// Arrange
			RepositorySetup(IsEmpty: true);
			MapperSetup();
			var handler = new GetAllVacanciesHandler(_mockRepository.Object, _mockMapper.Object);

			// Act
			var result = await handler.Handle(new GetAllVacanciesQuery(), CancellationToken.None);

			// Assert
			Assert.IsType<NullResult<IEnumerable<VacancyDto>>>(result);
		}

		private void RepositorySetup(bool IsEmpty = false)
		{
			if(IsEmpty)
			{
				_mockRepository.Setup(repo => repo.VacancyRepository
					.GetAllAsync(It.IsAny<Expression<Func<Vacancy, bool>>>(),
						It.IsAny<Func<IQueryable<Vacancy>,
						IIncludableQueryable<Vacancy, Vacancy>>?>()))!.ReturnsAsync(new List<Vacancy>());
			}
			else
			{
				_mockRepository.Setup(repo => repo.VacancyRepository
					.GetAllAsync(It.IsAny<Expression<Func<Vacancy, bool>>>(),
						It.IsAny<Func<IQueryable<Vacancy>,
						IIncludableQueryable<Vacancy, Vacancy>>?>())).ReturnsAsync(GetVacancies);
			}
		}
		private void MapperSetup()
		{
			_mockMapper.Setup(mapper => mapper.Map<IEnumerable<VacancyDto>>(It.IsAny<IEnumerable<Vacancy>>()))
				.Returns(GetVacancyDtos);
		}
		private static List<Vacancy> GetVacancies()
		{
			return new(){
				new(){
					Id = 1,
					Name = "Name1",
					Link = "Link1",
					CreatedDate = DateTime.MinValue,
					Location = "Location1",
					Company = "Company1",
					Remote = false,
					Views = 1,
					Responses = 2,
					Description = "Description1"
				},
				new(){
					Id = 2,
					Name = "Name2",
					Link = "Link2",
					CreatedDate = DateTime.MinValue,
					Location = "Location2",
					Company = "Company2",
					Remote = true,
					Views = 2,
					Responses = 3,
					Description = "Description2"
				},
				new(){
					Id = 3,
					Name = "Name3",
					Link = "Link3",
					CreatedDate = DateTime.MaxValue,
					Location = "Location3",
					Company = "Company3",
					Remote = true,
					Views = 3,
					Responses = 4,
					Description = "Description3"
				},

			};
		}
		private static List<VacancyDto> GetVacancyDtos()
		{
			return new(){
				new(){
					Id = 1,
					Name = "Name1",
					Link = "Link1",
					CreatedDate = DateTime.MinValue,
					Location = "Location1",
					Company = "Company1",
					Remote = true,
					Views = 1,
					Responses = 2,
					Description = "Description1"
				},
				new(){
					Id = 2,
					Name = "Name2",
					Link = "Link2",
					CreatedDate = DateTime.MinValue,
					Location = "Location2",
					Company = "Company2",
					Remote = false,
					Views = 2,
					Responses = 3,
					Description = "Description2"
				},
				new(){
					Id = 3,
					Name = "Name3",
					Link = "Link3",
					CreatedDate = DateTime.MaxValue,
					Location = "Location3",
					Company = "Company3",
					Remote = true,
					Views = 3,
					Responses = 4,
					Description = "Description3"
				},

			};
		}
	}
}
