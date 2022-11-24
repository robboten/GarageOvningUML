using GarageOvningUML.Vehicles;
using System.Collections;

namespace GarageOvningUML.Garage
{
    /// <remarks>implements ienum</remarks>
    /// 

    //missing interface atm
    public class Garage<T> : IEnumerable// where T : Vehicle, IEnumerable<T>
    {
        private Vehicle[] VehicleArray;
        public readonly int Capacity;

        public Garage(int capacity)
        {
            Capacity = capacity;
            VehicleArray = new Vehicle[Capacity];
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var v in VehicleArray)
            {
                if (v == null)
                {
                    break;
                }
                yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal bool Add(Vehicle v)
        {
            //see if there is any place left in the garage
            for (var i = 0; i < VehicleArray.Length; i++)
            {
                if (VehicleArray[i] == null)
                {
                    VehicleArray[i] = v;
                    return true;
                }
            }
            return false;
        }
        internal bool Remove(Vehicle v)
        {
            //find right vehicle
            for (var i = 0; i < VehicleArray.Length; i++)
            {
                if (VehicleArray[i] == v)
                {
                    VehicleArray[i] = null;
                    return true;
                }
            }
            return false;
        }

        public void testing()
        {
            VehicleArray.Select(x=>x.GetType()).Distinct().Count();
        }
    }
}