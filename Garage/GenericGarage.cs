using GarageOvningUML.Vehicles;
using System.Collections;

namespace GarageOvningUML.Garage
{
    public class GenericGarage<T> : IEnumerable<T> where T : IVehicle
    {
       // private readonly List<T> _items = new();
        private readonly T[] vehicleArray;
        public readonly int Capacity;

        public GenericGarage(int capacity)
        {
            Capacity = capacity;
            vehicleArray = new T[Capacity];
        }

        public T[] GetArr()
        {
            return vehicleArray;
        }

        public bool Add(T item)
        {
            // _items.Add(item);
            
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
           // _items.Remove(item);

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
