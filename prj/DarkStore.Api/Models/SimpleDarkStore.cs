namespace DarkStore.Api.Models
{
    public class SimpleDarkStore
    {
        public string DarkStoreFullName = "Super Dark Store";

        public int DarkStoreNumber = 11;

        public DateOnly DarkStoreDateOfOpen = new(2021, 1, 1);

        internal int InternalDarkStoreNumber = 222;




        // Object
        public DarkStore SuperStore = new()
        {
            Id = Guid.Parse("A2B9203E-0FDB-471A-B5D5-1E2FC8804CB3"),
            FullName = "Super Mega Store",
            Number = 123
        };

        //array
        public int[] Numbers = new[] { 1, 3, 10, 15 };

        //IEnumerable
        public IEnumerable<DarkStore> Stores = new[]
        {
            new DarkStore(){
                Id = Guid.Parse("A2B9203E-0FDB-471A-B5D5-1E2FC8804CB3"),
                FullName = "Region Store",
                Number = 101
            },
            new DarkStore(){
                Id = Guid.Parse("3B7CE12E-457B-404B-B073-A708A3DD9873"),
                FullName = "District Nord Store",
                Number = 102
            },
            new DarkStore(){
                Id = Guid.Parse("E655A287-E12D-47D6-BF6D-D9CF18228F3F"),
                FullName = "District Ost Store",
                Number = 103
            },
            new DarkStore(){
                Id = Guid.Parse("93639029-9813-4F73-89C0-2F6DF2291297"),
                FullName = "District South Store",
                Number = 104
            },
             new DarkStore(){
                Id = Guid.Parse("8279600C-462B-40D6-A586-A57ADFAF88B9"),
                FullName = "District West Store",
                Number = 105
            }
        };

        //Exception
        public string IsDirectoryExists(string dirName)
        {
            if (String.IsNullOrEmpty(dirName))
                throw new DirectoryNotFoundException();

            return dirName;
        }


        //Event
        public event EventHandler DarkStoreClosedEvent;
        public virtual void RaiseDarkStoreClosedEvent()
        {
            DarkStoreClosedEvent(this, EventArgs.Empty);
        }


        // PRIVATE with parameters
        #region Private method

        private float GetDivide(float a, float b)
        {
            if (b == 0)
                throw new DivideByZeroException();

            return a / b;
        }

        public float GetSomeCalculation(float a, float b, int c)
        {
            // some difficult calculation
            return GetCalcForThreeVariables(a, c, b) + GetDivide(a, b);
        }

        public float GetCalcForThreeVariables(float var1, float var2, float var3)
        {
            // some difficult calculation...
            return Math.Abs(var1 + var2 + var3);
        }

        #endregion Private


        #region Real method for MemberData      

        /// <summary>
        /// смены и количество сотрудников 
        /// </summary>
        public interface IDarkStoreShifts
        {
            /// <summary>
            /// Время (час) старта смены
            /// </summary>
            int TimeFrom { get; }

            /// <summary>
            /// Время (час) окончания смены
            /// </summary>
            int TimeTo { get; }

            /// <summary>
            /// Количество сотрудников на смене
            /// </summary>
            int EmployeesCount { get; }
        }

        public int GetEmployeesCount(IEnumerable<IDarkStoreShifts> shiftsData)
        {
            int result = 0;

            int curHour = DateTime.Now.Hour;

            if (shiftsData != null && shiftsData.Count() > 0)
            {
                result = shiftsData.FirstOrDefault(shifts =>
                    (shifts.TimeFrom <= shifts.TimeTo && curHour >= shifts.TimeFrom && curHour < shifts.TimeTo) ||
                    (shifts.TimeFrom > shifts.TimeTo && (curHour >= shifts.TimeFrom || curHour < shifts.TimeTo)))
                    .EmployeesCount;
            }

            return result;
        }

        #endregion Real method        

    }
}
