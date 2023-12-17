using FluentAssertions;
using System.Collections;
using DarkStore.Api.Models;
using static DarkStore.Api.Models.SimpleDarkStore;


namespace DarkStore.Api.Tests.Unit
{
    public class SimpleDarkStoreTests
    {
        private readonly SimpleDarkStore _sut = new();

        [Fact]
        public void String_TestExample()
        {
            var storeFullName = _sut.DarkStoreFullName;

            storeFullName.Should().NotBeEmpty();
            storeFullName.Should().Be("Super Dark Store");
            //Assert.Equal(storeFullName, "Super Dark Store");
            storeFullName.Should().StartWith("Super");
            storeFullName.Should().Contain("Dark");
            storeFullName.Should().EndWith("Store");
        }

        [Fact]
        public void Integer_TestExample()
        {
            var storeNumber = _sut.DarkStoreNumber;

            storeNumber.Should().BePositive();
            storeNumber.Should().BeInRange(10, 60);
            storeNumber.Should().BeGreaterThan(0).And.BeLessThan(100);
        }

        [Fact]
        public void Date_TestExample()
        {
            var storeDateOfOpen = _sut.DarkStoreDateOfOpen;

            storeDateOfOpen.Should().Be(new(2021, 1, 1));
            storeDateOfOpen.Should().BeAfter(new(2000, 1, 1));
            storeDateOfOpen.Should().BeBefore(new(2025, 1, 1));
        }

        [Fact]
        public void Object_TestExample()
        {
            var expected = new Models.DarkStore
            {
                Id = Guid.Parse("A2B9203E-0FDB-471A-B5D5-1E2FC8804CB3"),
                FullName = "Super Mega Store",
                Number = 123
            };

            var store = _sut.SuperStore;

            store.Should().NotBeNull();
            store.Should().BeEquivalentTo(expected);
            //Assert.Equal(store, expected);
        }

        [Fact]
        public void Array_TestExample()
        {
            var numbers = _sut.Numbers.As<int[]>();

            numbers.Should().Contain(3);
        }

        [Fact]
        public void IEnumerable_TestExample()
        {
            var expected1 = new Models.DarkStore
            {
                Id = Guid.Parse("A2B9203E-0FDB-471A-B5D5-1E2FC8804CB3"),
                FullName = "Super Mega Store",
                Number = 123
            };

            var expected2 = new Models.DarkStore
            {
                Id = Guid.Parse("93639029-9813-4F73-89C0-2F6DF2291297"),
                FullName = "District South Store",
                Number = 104
            };

            var stores = _sut.Stores.As<Models.DarkStore[]>();

            //stores.Should().HaveCount(3); 
            stores.Should().ContainEquivalentOf(expected2);
            //stores.Should().Contain(expected2);
            stores.Should().Contain(x => x.FullName.Contains("District") && x.Number == 103);

        }

        [Fact]
        public void Exception_TestExample()
        {
            Action result = () => _sut.IsDirectoryExists("");

            //Func<string> result2 = () => _sut.IsDirectoryExists("dir"); 
            //string directoryResult = result2();

            result.Should()
                .Throw<DirectoryNotFoundException>()
                .WithMessage("Attempted to access a path that is not on the disk.");
        }

        [Fact]
        public void EventRaised_TestExample()
        {
            // монитор происход€щего в классе
            var monitor = _sut.Monitor();

            //_sut.RaiseDarkStoreClosedEvent(); // - вызов событи€ 

            monitor.Should().Raise("DarkStoreClosedEvent");
        }

        [Fact]
        public void Internal_TestExample()
        {
            // see DarkStore.Api project file for details (<InternalsVisibleTo Include="DarkStore.Api.Tests.Unit"/>)
            var _internal = _sut.InternalDarkStoreNumber;

            _internal.Should().Be(222);
        }

        [Theory]
        [InlineData(1, 2, 25)]
        [InlineData(5, 5, 2)]
        [InlineData(-5, -5, 2)]
        public void Private_TestExample1(float a, float b, int c)
        {
            var result = _sut.GetSomeCalculation(a, b, c);

            result.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(2, 0, 2)]
        [InlineData(12, 0, 12)]
        public void Private_TestExample2(float a, float b, int c)
        {
            Action result = () => _sut.GetSomeCalculation(a, b, c);

            result.Should()
                .Throw<DivideByZeroException>()
                .WithMessage("Attempted to divide by zero.");
        }


        #region MemberData_TestExample

        #region TestData for MemberData_TestExample

        public class DarkStoreShiftsTestData
        {
            public class ShiftData : IDarkStoreShifts
            {
                public int TimeFrom { get; set; }
                public int TimeTo { get; set; }
                public int EmployeesCount { get; set; }
            }

            public static IEnumerable<object[]> ListDarkStoreShifts()
            {
                yield return new object[]
                {
                    new List<ShiftData>
                    {
                        new ShiftData { TimeFrom = 0,  TimeTo = 8,  EmployeesCount = 30 },
                        new ShiftData { TimeFrom = 8,  TimeTo = 16, EmployeesCount = 35 },
                        new ShiftData { TimeFrom = 16, TimeTo = 0,  EmployeesCount = 39 }
                    }
                };

                yield return new object[]
                {
                    new List<ShiftData>
                    {
                        new ShiftData { TimeFrom = 0,  TimeTo = 6,   EmployeesCount = 40 },
                        new ShiftData { TimeFrom = 6,  TimeTo = 12,  EmployeesCount = 41 },
                        new ShiftData { TimeFrom = 12, TimeTo = 18,  EmployeesCount = 45 },
                        new ShiftData { TimeFrom = 18, TimeTo = 0,   EmployeesCount = 40 }
                    }
                };
            }
        }
        #endregion


        [Theory]
        [MemberData(nameof(DarkStoreShiftsTestData.ListDarkStoreShifts),
                    MemberType = typeof(DarkStoreShiftsTestData))]
        public void MemberData_TestExample(List<DarkStoreShiftsTestData.ShiftData> shiftDataList)
        {

            var count = _sut.GetEmployeesCount(shiftDataList);

            count.Should().BeGreaterThan(0);

            switch (shiftDataList.Count())
            {
                case 3:
                    count.Should().BeInRange(30, 39); break;
                case 4:
                    count.Should().BeInRange(40, 49); break;
            }

        }

        #endregion


        #region ClassData_TestExample

        public class GetCalcForThreeVariablesTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 5, 5, 0, 10 };
                yield return new object[] { -5, -5, 1, 9 };
                yield return new object[] { -15, -5, -10, 30 };
                yield return new object[] { 15, 5, 10, 30 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        // [InlineData(5, 5, 0, 10)]
        // [InlineData(-5, -5, 1, 9)]
        // [InlineData(-15, -5, -10, 30)]
        // [InlineData(15, 5, 10, 30)]
        [ClassData(typeof(GetCalcForThreeVariablesTestData))]
        public void ClassData_TestExample(float firstNumber, float secondNumber, float thirdNumber, float expectedResult)
        {
            var result = _sut.GetCalcForThreeVariables(firstNumber, secondNumber, thirdNumber);

            //Assert.Equal(expectedResult, result);
            result.Should().BePositive();
            result.Should().Be(expectedResult);
        }


        #endregion

    }

}