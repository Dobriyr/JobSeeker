using JobSeeker.BLL.Dto.Vacancy;


namespace JobSeeker.Tests.DataTypeObjects.Vacancies
{
	public class VacanctyDtoTests
	{

		[Fact]
		public void VacancyDto_PropertiesGetSetTest()
		{
			// Arrange
			VacancyDto vacancyDtoSet = new VacancyDto();
			int id = 1;
			string name = "name";
			string link = "link";
			DateTime createdDate = DateTime.MinValue;
			string location = "location";
			string company = "company";
			bool remote = true;
			string description = "description";


			// Act
			vacancyDtoSet.Id = id;
			vacancyDtoSet.Name = name;
			vacancyDtoSet.Link = link;
			vacancyDtoSet.CreatedDate = createdDate;
			vacancyDtoSet.Location = location;
			vacancyDtoSet.Company = company;
			vacancyDtoSet.Remote = remote;
			vacancyDtoSet.Description = description;

			// Assert
			Assert.True(
				vacancyDtoSet.Id == id &&
				vacancyDtoSet.Name == name &&
				vacancyDtoSet.Link == link &&
				vacancyDtoSet.CreatedDate == createdDate &&
				vacancyDtoSet.Location == location &&
				vacancyDtoSet.Company == company &&
				vacancyDtoSet.Remote == remote &&
				vacancyDtoSet.Description == description
			 );
		}

		[Fact]
		public void VacancyDto_EqualsMethodReturnFalseOnIncorectTypeTest()
		{
			// Arrange
			VacancyDto vacancyDto = GetVacancy();

			// Act
			bool result = vacancyDto.Equals("something that is not type of VacancyDto");

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void VacancyDto_EqualsMethodTest()
		{
			// Arrange
			VacancyDto vacancyDtoAnother = GetAnotherVacancy();
			VacancyDto vacancyDto1 = GetVacancy();
			VacancyDto vacancyDto2 = GetVacancy();

			// Act
			bool resultTrue = vacancyDto1.Equals(vacancyDto2);
			bool resultFalse = vacancyDto1.Equals(vacancyDtoAnother);

			// Assert
			Assert.Multiple(
					() => Assert.True(resultTrue),
					() => Assert.False(resultFalse)
				);
		}

		[Fact]
		public void VacancyDto_GetHashCodeTest()
		{
			// Arrange
			VacancyDto vacancyDtoAnother = GetAnotherVacancy();
			VacancyDto vacancyDto1 = GetVacancy();
			VacancyDto vacancyDto2 = GetVacancy();

			// Act
			int resultVacancyDto1 = vacancyDto1.GetHashCode();
			int resultVacancyDto2 = vacancyDto2.GetHashCode();
			int resultVacancyDtoAnother = vacancyDtoAnother.GetHashCode();

			// Assert
			Assert.Multiple(
					() => Assert.True(resultVacancyDto1 == resultVacancyDto2),
					() => Assert.False(resultVacancyDto1 == resultVacancyDtoAnother)
				) ;
		}

		[Fact]
		public void VacancyDto_GetHashCodeCreateCorectHashTest()
		{
			// Arrange
			VacancyDto vacancyDtoAnother = GetAnotherVacancy();
			int expected = HashCode.Combine(
				vacancyDtoAnother.Id, vacancyDtoAnother.Name, vacancyDtoAnother.Link, vacancyDtoAnother.CreatedDate);

			// Act
			int hashResult = vacancyDtoAnother.GetHashCode();

			// Assert
			Assert.True(expected == hashResult);
		}


		private VacancyDto GetVacancy() => new VacancyDto
		{
			Id = 1,
			Name = "Name1",
			Link = "Link1",
			CreatedDate = DateTime.MaxValue,
			Location = "Location1",
			Company = "Company1",
			Remote = true,
			Views = 1,
			Responses = 2,
			Description = "Description1"
		};
		private VacancyDto GetAnotherVacancy() => new VacancyDto
		{
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
		};
	}
}
