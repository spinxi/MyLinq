namespace MyLinq;

public static class MyLinq
{
    #region Filtering

    /// <summary>
    /// Filters a sequence of values based on a predicate.
    /// </summary>
    public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        
        IEnumerator<T> iterator = source.GetEnumerator();
        
        
        while (iterator.MoveNext())
        {
            if (predicate(iterator.Current))
                yield return iterator.Current;
        }
        
        /*foreach (var item in source)
        {
            if (predicate(item))
                yield return item;
        }*/
    }

    #endregion

    #region Projection

    /// <summary>
    /// Projects each element of a sequence into a new form.
    /// </summary>
    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
        {
            yield return selector(item);
        }
    }

    /// <summary>
    /// Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence.
    /// </summary>
    public static IEnumerable<TResult> MySelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
    {
        foreach (var item in source)
        {
            var innerList = selector(item);
            foreach (var innerItem in innerList)
            {
                yield return innerItem;
            }
        }
    }

    #endregion

    #region Partitioning

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    public static IEnumerable<T> MyTake<T>(this IEnumerable<T> source, int count)
    {
        int itemsReturned = 0;
        foreach (var element in source)
        {
            if (itemsReturned < count)
            {
                yield return element;
                itemsReturned += 1;
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
    /// </summary>
    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> source, int count)
    {
        int itemsSkipped = 0;
        foreach (var element in source)
        {
            if (itemsSkipped < count)
            {
                itemsSkipped += 1;
            }
            else
            {
                yield return element;
            }
        }
    }

    #endregion

    #region Ordering

    /// <summary>
    /// Sorts the elements of a sequence in ascending order according to a key.
    /// </summary>
    public static IEnumerable<TSource> MyOrderBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
        where TKey : IComparable<TKey>
    {
        List<TSource> newList = source.MyToList();
        newList.Sort((x, y) => selector(x).CompareTo(selector(y)));

        foreach (var element in newList)
        {
            yield return element;
        }
    }

    /// <summary>
    /// Sorts the elements of a sequence in descending order according to a key.
    /// </summary>
    public static IEnumerable<TSource> MyOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
        where TKey : IComparable<TKey>
    {
        List<TSource> newList = source.MyToList();
        newList.Sort((x, y) => selector(y).CompareTo(selector(x)));

        foreach (var element in newList)
        {
            yield return element;
        }
    }

    /// <summary>
    /// Inverts the order of the elements in a sequence.
    /// </summary>
    public static IEnumerable<TSource> MyReverse<TSource>(this IEnumerable<TSource> source)
    {
        List<TSource> newList = source.MyToList();
        newList.Reverse();

        foreach (var element in newList)
        {
            yield return element;
        }
    }

    #endregion

    #region Element Operators

    /// <summary>
    /// Returns the first element of a sequence.
    /// </summary>
    public static T MyFirst<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            return item;
        }
        throw new InvalidOperationException("The source sequence is empty.");
    }

    /// <summary>
    /// Returns the first element in a sequence that satisfies a specified condition.
    /// </summary>
    public static T MyFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return item;
        }
        throw new InvalidOperationException("No element satisfies the condition.");
    }

    /// <summary>
    /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    public static T MyFirstOrDefault<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            return item;
        }
        return default(T);
    }

    /// <summary>
    /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
    /// </summary>
    public static T MyFirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return item;
        }
        return default(T);
    }

    #endregion

    #region Quantifiers

    /// <summary>
    /// Determines whether a sequence contains any elements.
    /// </summary>
    public static bool MyAny<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// </summary>
    public static bool MyAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Determines whether all elements of a sequence satisfy a condition.
    /// </summary>
    public static bool MyAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (!predicate(item))
                return false;
        }
        return true;
    }

    #endregion

    #region Aggregation

    /// <summary>
    /// Returns the number of elements in a sequence.
    /// </summary>
    public static int MyCount<T>(this IEnumerable<T> source)
    {
        int count = 0;
        foreach (var item in source)
        {
            count += 1;
        }
        return count;
    }

    /// <summary>
    /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
    /// </summary>
    public static int MyCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int count = 0;
        foreach (var item in source)
        {
            if (predicate(item))
                count += 1;
        }
        return count;
    }

    /// <summary>
    /// Computes the sum of a sequence of Int32 values.
    /// </summary>
    public static int MySum(this IEnumerable<int> source)
    {
        int sum = 0;
        foreach (var element in source)
        {
            sum += element;
        }
        return sum;
    }

    #endregion

    #region Conversion

    /// <summary>
    /// Creates a List<T> from an IEnumerable<T>.
    /// </summary>
    public static List<T> MyToList<T>(this IEnumerable<T> source)
    {
        List<T> listFromSource = new List<T>();
        foreach (var item in source)
        {
            listFromSource.Add(item);
        }
        return listFromSource;
    }

    #endregion
}