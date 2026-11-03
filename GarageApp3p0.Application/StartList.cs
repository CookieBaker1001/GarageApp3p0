using GarageApp3p0.Application.Model;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;

namespace GarageApp.Model
{
    internal static class StartList
    {
        private static List<Vehicle> startList = new List<Vehicle>() {
            new Car() { Make = "Progen", Model = "Emerus", RegNumber = "DJR273", Year = 2003, SeatCount = 2, Color = "blå", HasSunRoof = true },
            new Car() { Make = "Pfister", Model = "811", RegNumber = "JEK736", Year = 2016, SeatCount = 4, Color = "grön", HasSunRoof = false },
            new Car() { Make = "Dewbauchee", Model = "Vagner", RegNumber = "TBA558", Year = 2010, SeatCount = 2, Color = "lila", HasSunRoof = false },
            new Car() { Make = "Benefactor", Model = "Krieger", RegNumber = "ETU266", Year = 2011, SeatCount = 5, Color = "svart", HasSunRoof = true },
            
            new Motorcycle() { Make = "Nagasaki", Model = "Shinobi", RegNumber = "HEI490", Year = 2011, SeatCount = 2, Color = "orange", HasSidecar = true },
            new Motorcycle() { Make = "Pegassi", Model = "Ruffian", RegNumber = "GEW115", Year = 2008, SeatCount = 1, Color = "svart", HasSidecar = false },
            new Motorcycle() { Make = "Maibatsu", Model = "Manchez", RegNumber = "ASH371", Year = 2016, SeatCount = 1, Color = "rosa", HasSidecar = false },
            new Motorcycle() { Make = "Shitzu", Model = "Hakucho", RegNumber = "ASH371", Year = 2012, SeatCount = 1, Color = "guld", HasSidecar = false },
            
            new Airplane() { Make = "Nagasaki", Model = "Ultralight", RegNumber = "JEU631", Year = 2002, SeatCount = 8, Color = "gul", EngineCount = 1, MaxHeight = 10000 },
            new Airplane() { Make = "Mamoth", Model = "Hydra", RegNumber = "JWY991", Year = 2018, SeatCount = 12, Color = "grön", EngineCount = 1, MaxHeight = 35000 },
            new Airplane() { Make = "Buckingham", Model = "Miljet", RegNumber = "VCE732", Year = 2012, SeatCount = 20, Color = "ljusgrå", EngineCount = 2, MaxHeight = 22500 },
            new Airplane() { Make = "Buckingham", Model = "Nimbus", RegNumber = "HWP826", Year = 2021, SeatCount = 4, Color = "svart", EngineCount = 2, MaxHeight = 25000 },
            
            new Boat() { Make = "Shitzu", Model = "Longfin", RegNumber = "KSE182", Year = 2011, SeatCount = 8, Color = "mörkgrå", PropulsionType = PropulsionType.Engine },
            new Boat() { Make = "Pegassi", Model = "Speeder", RegNumber = "ELJ221", Year = 2014, SeatCount = 10, Color = "silver", PropulsionType = PropulsionType.Engine },
            new Boat() { Make = "Dinka", Model = "Marquis", RegNumber = "SUF291", Year = 2008, SeatCount = 44, Color = "vit", PropulsionType = PropulsionType.Sail },
            new Boat() { Make = "Buckingham", Model = "Tug", RegNumber = "EDA457", Year = 2009, SeatCount = 8, Color = "lila", PropulsionType = PropulsionType.Rowing },
            
            new Bus() { Make = "MLT", Model = "Dune", RegNumber = "GWO838", Year = 2004, SeatCount = 30, Color = "röd", EngineType = EngineType.Diesel },
            new Bus() { Make = "Brute", Model = "Shuttle", RegNumber = "KEI-534", Year = 2009, SeatCount = 75, Color = "turkos", EngineType = EngineType.Gasoline },
            new Bus() { Make = "Vapid", Model = "Dashound", RegNumber = "BRW368", Year = 2014, SeatCount = 85, Color = "gul", EngineType = EngineType.Diesel},
            new Bus() { Make = "Karin", Model = "Brickade", RegNumber = "HJV903", Year = 2005, SeatCount = 60, Color = "grå", EngineType = EngineType.Electric },
        };

        public static List<Vehicle> GetRandomList(int maxSize) {
            int[] indeces = new int[startList.Count];
            for (int i = 0; i < startList.Count; i++) indeces[i] = i;

            Random rnd = new Random();
            for (int i = 0; i < startList.Count; i++) {
                int j = rnd.Next(0, startList.Count);
                int tmp = indeces[i];
                indeces[i] = indeces[j];
                indeces[j] = tmp;
            }

            List<Vehicle> randomList = new List<Vehicle>();
            Vehicle v;
            int index = 0;
            int currentSize = 0;
            bool exit = false;
            while (!exit && index < indeces.Length) { 
                v = startList[indeces[index++]];
                if ((currentSize + v.ParkingSize) <= maxSize)
                {
                    randomList.Add(v);
                    currentSize += v.ParkingSize;
                }
                else 
                {
                    exit = true;
                }
            }
            return randomList;
        }
    }
}
