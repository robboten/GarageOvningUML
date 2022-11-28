using GarageOvningUML.Vehicles;
using System.Collections;

namespace GarageOvningUML.Garage
{
    public class GenericGarage<T> : IEnumerable<T> where T : Vehicle
    {
       // private readonly List<T> _items = new();
        private readonly T[] varr;
        public readonly int Capacity;

        public GenericGarage(int capacity)
        {
            Capacity = capacity;
            varr = new T[Capacity];
        }

        public T[] GetArr()
        {
            return varr;
        }

        public bool Add(T item)
        {
            // _items.Add(item);
            
            //see if there is any place left in the garage
            for (var i = 0; i < varr.Length; i++)
            {
                if (varr[i] == null)
                {
                    varr[i] = item;
                    return true;
                }
    
            }
            return false;
            
        }

        public bool Remove(T item)
        {
           // _items.Remove(item);

            //find right vehicle
            for (var i = 0; i < varr.Length; i++)
            {
                if (varr[i] == item)
                {
                    varr[i] = null;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var v in varr)
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

    }


}
