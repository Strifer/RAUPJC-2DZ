using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak1.Interfaces;

namespace Zadatak1.Models
{
    public class GenericList<X> : IGenericList<X>
    {

        private X[] _internalStorage;
        private int count;

        public GenericList()
        {
            _internalStorage = new X[2];
            count = 0;
        }

        public GenericList(int initialSize)
        {
            if (initialSize <= 0)
            {
                throw new ArgumentException("Collection size must be a positive number.");
            }
            _internalStorage = new X[initialSize];
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void Add(X item)
        {
            if (_internalStorage.Length == count)
            {
                IncreaseSize();
            }
            _internalStorage[count] = item;
            count++;
        }

        private void IncreaseSize()
        {
            X[] newStorage = new X[_internalStorage.Length * 2];
            for (int i = 0; i < _internalStorage.Length; i++)
            {
                newStorage[i] = _internalStorage[i];
            }
            this._internalStorage = newStorage;
        }

        public void Clear()
        {
            _internalStorage = new X[_internalStorage.Length];
            count = 0;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < count; i++)
            {
                if (item.Equals(_internalStorage[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public X GetElement(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < count; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Remove(X item)
        {
            return RemoveAt(IndexOf(item));
        }

        public bool RemoveAt(int index)
        {
            if (index >= count || index < 0)
            {
                return false;
            }

            for (int i = index; i < count - 1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            count--;
            return true;

        }

        override public string ToString()
        {
            if (count == 0)
            {
                return "[]";
            }
            StringBuilder sb = new StringBuilder(count + 2);
            sb.Append("[");
            for (int i = 0; i < count - 1; i++)
            {
                sb.Append(_internalStorage[i].ToString() + ", ");
            }
            sb.Append(_internalStorage[count - 1].ToString() + "]");
            return sb.ToString();
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private class GenericListEnumerator<T> : IEnumerator<T>
        {
            private GenericList<T> genericList;
            private int index;

            public GenericListEnumerator(GenericList<T> genericList)
            {
                this.genericList = genericList;
                this.index = -1;
            }

            public T Current
            {
                get
                {
                    try
                    {
                        return genericList.GetElement(index);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                index++;
                return (index < genericList.Count);
            }

            public void Reset()
            {
                this.index = -1;
            }
        }
    }
}
