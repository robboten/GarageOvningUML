using GarageOvningUML.Vehicles;
using System.Collections;

namespace GarageOvningUML.Garage
{
    public class GenericGarage<T> : IEnumerable<T> where T : IVehicle
    {
        private readonly T[] vehicleArray;
        public readonly int Capacity;

        //introduce a counter for cars instead of using count() for performance

        public GenericGarage(int capacity)
        {
            Capacity = capacity;
            vehicleArray = new T[Capacity];
        }

        //public T[] GetArr()
        //{
        //    //to get away from nullexceptions when linq loop thru it
        //    return vehicleArray.Where(c => c != null).ToArray();
        //}

        public bool Add(T item)
        {         
            //could be implemented with the findindex linq, but not sure it's better...

            //see if there is any place left in the garage
            for (var i = 0; i < vehicleArray.Length; i++)
            {
                if (vehicleArray[i] == null)
                {
                    vehicleArray[i] = item;
                    return true;
                }
            }
            return false;
            
        }

        public bool Remove(T item)
        {
            //could maybe do this with linq too...

            //find right vehicle
            for (var i = 0; i < vehicleArray.Length; i++)
            {
                if (vehicleArray[i].Equals(item))
                {
                    vehicleArray[i] = default!;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var v in vehicleArray)
            {
                if (v != null)
                    yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }


}
