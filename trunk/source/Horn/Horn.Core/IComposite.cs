using System.Collections.Generic;

namespace Horn.Core
{
    public interface IComposite<T>
    {
        void Add(T item);

        void Remove(T item);

        T Parent { get; set; }

        IList<T> Children { get;}
    }
}