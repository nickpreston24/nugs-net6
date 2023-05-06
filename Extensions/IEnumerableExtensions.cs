using System.Collections;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CodeMechanic.Extensions
{
    public static partial class IEnumerableExtensions
    {
        private static readonly IDictionary<Type, ICollection<PropertyInfo>> _propertyCache =
           new Dictionary<Type, ICollection<PropertyInfo>>();

        public static DataTable ToDataTable<T>(this List<T> collection,
            PropertyInfo[] props = null)
        {
            DataTable table = new DataTable();

            if (collection.IsNullOrEmpty())
            {
                return table;
            }

            var properties = props ?? _propertyCache
                   .TryGetProperties<T>(true)
                   .ToArray();

            if (properties.Length == 0)
            {
                return table;
            }

            object[] values = new object[properties.Length];

            try
            {
                foreach (T item in collection ?? Enumerable.Empty<T>())
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = properties[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return table;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null || !collection.Any();

        public static bool IsIEnumerable(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public static bool IsGenericList(this Type type)
        {
            //if (type == null)
            //{
            //    throw new ArgumentNullException("type");
            //}
            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        // if needed, you can also return the type used as generic argument
                        return true;
                    }
                }
            }
            return false;
        }



        /// <summary>
        /// https://medium.com/@adamramberg/create-better-code-using-delegates-and-extension-methods-in-unity-6d14256ca4d5
        /// 
        /// Usage:
        /// 
        /// private List<Unicorn> unicorns = new List<Unicorn>();
        ///  Vector3 playerPosition;

        ///  static Func<Unicorn, Vector3, bool> isCloseTo = (Unicorn unicorn, Vector3  /   position) => (position - unicorn.transform.position).magnitude > 5f;

        ///  void Update()
        ///  {
        ///      unicorns.ForEach(@if: isCloseTo, then: Unicorn.GiveHug, arg1:     playerPosition);
        //  }
        /// </summary>
        public static void ForEach<T1, A1>(this List<T1> list
            , Func<T1, A1, bool> @if
            , Action<T1, A1> then
            , A1 arg1)
        {
            for (int i = 0; list != null && i < list.Count; ++i)
            {
                if (@if(list[i], arg1))
                {
                    then(list[i], arg1);
                }
            }
        }

        //    static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> enumerable)
        //=> enumerable.SelectMany(e => e);


        /// <summary>
        /// Stores the properties we wish to use again so we only have to run Reflection once per property.
        /// </summary>
        private static readonly IDictionary<Type, ICollection<PropertyInfo>> propertyStore =
            new Dictionary<Type, ICollection<PropertyInfo>>();
        public static IEnumerable<R> Map<T, R>(
            this IEnumerable<T> collection,
            Func<T, R> map
        ) => collection.Select(item => map(item));

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) => source?.SelectMany(x => x);




        public static IEnumerable<T> Slice<T>(
            this IEnumerable<T> sequence,
            int startIndex,
            int count
        )
        {
            //if (sequence == null)
            //{
            //    throw new ArgumentNullException(nameof(sequence));
            //}

            //if (startIndex < 0)
            //{
            //    throw new ArgumentOutOfRangeException(nameof(startIndex));
            //}

            //if (count < 0)
            //{
            //    throw new ArgumentOutOfRangeException(nameof(count));
            //}

            // optimization for anything implementing IList<T>
            return !(sequence is IList<T> list) ? sequence.Skip(startIndex).Take(count) : _(count);
            IEnumerable<T> _(int countdown)
            {
                var listCount = list.Count;
                var index = startIndex;
                while (index < listCount && countdown-- > 0)
                {
                    yield return list[index++];
                }
            }
        }

        public static IEnumerable<TSource> TakeLast<TSource>(
            this IEnumerable<TSource> source,
            int count
        )
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is ICollection<TSource> col
              ? col.Slice(Math.Max(0, col.Count - count), int.MaxValue)
              : _();
            IEnumerable<TSource> _()
            {
                if (count <= 0)
                {
                    yield break;
                }

                var q = new Queue<TSource>(count);

                foreach (var item in source)
                {
                    if (q.Count == count)
                    {
                        q.Dequeue();
                    }

                    q.Enqueue(item);
                }

                foreach (var item in q)
                {
                    yield return item;
                }
            }
        }

        //From: https://github.com/morelinq/MoreLINQ/blob/master/MoreLinq/MaxBy.cs#L43
        public static TSource MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector
        )
        {
            return source.MaxBy(selector, null);
        }

        public static TSource MaxBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            IComparer<TKey> comparer
        )
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            comparer = comparer ?? Comparer<TKey>.Default;
            return ExtremumBy(source, selector, (x, y) => comparer.Compare(x, y));
        }

        static TSource ExtremumBy<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            Func<TKey, TKey, int> comparer
        )
        {
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                    throw new InvalidOperationException("Sequence contains no elements");

                var extremum = sourceIterator.Current;
                var key = selector(extremum);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer(candidateProjected, key) > 0)
                    {
                        extremum = candidate;
                        key = candidateProjected;
                    }
                }

                return extremum;
            }
        }

        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector
        )
        {
            return source.MinBy(selector, null);
        }

        public static TSource MinBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector,
            IComparer<TKey> comparer
        )
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            comparer = comparer ?? Comparer<TKey>.Default;
            return ExtremumBy(source, selector, (x, y) => -Math.Sign(comparer.Compare(x, y)));
        }

        public static IEnumerable<T> AsEmpty<T>(this IEnumerable<T> source) =>
            Enumerable.Empty<T>();

        public static IEnumerable<T> Interweave<T>(this IEnumerable<IEnumerable<T>> inputs)
        {
            var enumerators = new List<IEnumerator<T>>();
            try
            {
                foreach (var input in inputs)
                {
                    enumerators.Add(input.GetEnumerator());
                }

                while (true)
                {
                    enumerators.RemoveAll(
                        enumerator =>
                        {
                            if (enumerator.MoveNext())
                            {
                                return false;
                            }

                            enumerator.Dispose();
                            return true;
                        }
                    );

                    if (enumerators.Count == 0)
                    {
                        yield break;
                    }

                    foreach (var enumerator in enumerators)
                    {
                        yield return enumerator.Current;
                    }
                }
            }
            finally
            {
                if (enumerators != null)
                {
                    foreach (var e in enumerators)
                    {
                        e.Dispose();
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int batchSize)
        {
            using (var enumerator = items.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.EnumerateSome(batchSize);
                }
            }
        }

        /// <summary>
        /// Alternative Batch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        //public static IEnumerable<IEnumerable<T>> Batch<T>(
        //    this IEnumerable<T> items,
        //    int maxBatchSize
        //)
        //{
        //    return items
        //        .Select((item, index) => new { item, index })
        //        .GroupBy(pairs => pairs.index / maxBatchSize)
        //        .Select(mapped => mapped.Select(pair => pair.item));
        //}

        internal static IEnumerable<T> EnumerateSome<T>(this IEnumerator<T> enumerator, int count)
        {
            var list = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                list.Add(enumerator.Current);
                if (!enumerator.MoveNext())
                {
                    break;
                }
            }

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public static string ToCsv<T>(this IEnumerable<T> items) where T : class
        {
            var csvBuilder = new StringBuilder();
            var properties = propertyStore[typeof(T)];

            foreach (var item in items ?? Enumerable.Empty<T>())
            {
                string line = string.Join(
                    ",",
                    properties
                        .Select(property => property.GetValue(item, null).ToCsvValue())
                        .ToArray()
                );

                csvBuilder.AppendLine(line);
            }

            return csvBuilder.ToString();
        }

        private static string ToCsvValue<T>(this T item)
        {
            if (item == null)
            {
                return "\"\"";
            }

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }

            if (double.TryParse(item.ToString(), out double dummy))
            {
                return string.Format("{0}", item);
            }

            return string.Format("\"{0}\"", item);
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                return Enumerable.Empty<T>();
            }

            var cached = collection;

            foreach (var item in cached ?? Enumerable.Empty<T>())
            {
                action(item);
            }

            return collection;
        }

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> collection, int count) =>
            collection.OrderBy(g => Guid.NewGuid()).Take(count);

        public static T TakeFirstRandom<T>(this IEnumerable<T> collection) =>
            collection.OrderBy(c => Guid.NewGuid()).FirstOrDefault();


        static Random _random = new Random();
        public static T[] Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(_random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }

            return array;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) => list.ToArray().Shuffle();

        /// <summary>
        /// Moves up an item to the specified index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="target_index">Index of the item.</param>
        /// <returns></returns>
        public static IEnumerable<T> MoveUp<T>(this IEnumerable<T> enumerable, int target_index)
        {
            int i = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (target_index.Equals(i))
                {
                    var previous = enumerator.Current;
                    if (enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                    yield return previous;
                    break;
                }
                yield return enumerator.Current;
            }
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerable<T> GetItemsWhere<T>(
            this IEnumerable<T> collection,
            LambdaExpression predicate
        )
        {
            try
            {
                var where_condition = predicate.Compile();
                return collection.Where(x => (bool)where_condition.DynamicInvoke(x));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetItemsWhere<T>(
            this IEnumerable<T> collection,
            Expression<Func<T, bool>> predicate,
            int numDesired = 1
        )
        {
            try
            {
                Func<T, bool> where_condition = predicate.Compile();
                return collection.Where(where_condition).Take(numDesired);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetItemsWhere<T>(
            this IEnumerable<T> collection,
            Expression<Func<T, bool>> predicate
        )
        {
            try
            {
                Func<T, bool> where_condition = predicate.Compile();
                return collection.Where(where_condition);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> GetRandomItemsWhere<T>(
            this IEnumerable<T> collection,
            Expression<Func<T, bool>> predicate,
            int count
        )
        {
            try
            {
                Func<T, bool> where_condition = predicate.Compile();
                return collection.Where(where_condition).OrderBy(c => Guid.NewGuid()).Take(count);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
