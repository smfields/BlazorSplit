using System.Collections;

namespace BlazorSplit.OptionTypes;

public class SingleOrArray<TValue> : IList<TValue>
{
    public IList<TValue> Values { get; }

    public SingleOrArray(IEnumerable<TValue> values)
    {
        Values = values?.ToList() ?? throw new ArgumentNullException(nameof(values));
    }

    public SingleOrArray(TValue value)
    {
        Values = new List<TValue>
        {
            value ?? throw new ArgumentNullException(nameof(value))
        };
    }

    #region IList<TValue> Members

    public IEnumerator<TValue> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Values).GetEnumerator();
    }

    public void Add(TValue item)
    {
        Values.Add(item);
    }

    public void Clear()
    {
        Values.Clear();
    }

    public bool Contains(TValue item)
    {
        return Values.Contains(item);
    }

    public void CopyTo(TValue[] array, int arrayIndex)
    {
        Values.CopyTo(array, arrayIndex);
    }

    public bool Remove(TValue item)
    {
        return Values.Remove(item);
    }

    public int Count => Values.Count;

    public bool IsReadOnly => Values.IsReadOnly;

    public int IndexOf(TValue item)
    {
        return Values.IndexOf(item);
    }

    public void Insert(int index, TValue item)
    {
        Values.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Values.RemoveAt(index);
    }

    public TValue this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    #endregion

    public static implicit operator SingleOrArray<TValue>(TValue value)
    {
        return new SingleOrArray<TValue>(value);
    }

    public static implicit operator SingleOrArray<TValue>(List<TValue> values)
    {
        return new SingleOrArray<TValue>(values);
    }

    public static implicit operator SingleOrArray<TValue>(TValue[] values)
    {
        return new SingleOrArray<TValue>(values);
    }
}