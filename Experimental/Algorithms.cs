using CodeMechanic.Diagnostics;

namespace nugsnet6.Experimental;

public static class Algorithms
{
    /// <summary>
    /// A super-generic Greedy algorithm
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="goal">If this condition is satisfied, the new, sorted sequence is immediately returned</param>
    /// <param name="limit">A limit to how many times we try (or recurse)</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> Greedy<T, TKey>(
        this List<T> items,
        Func<T, TKey> goal = null
        // , Func<T, TKey> order_by = null
        // , T max = default
        // , T min = default
        // , int limit = 100
        // , int bagCapacity = 100
        ,
        params Func<T, bool>[] ordered_conditions
    )
    {
        var count = items.Count;
        // if (goal == null)
        //     goal = x => string.Empty;

        for (int index = 0; index < ordered_conditions.Length; index++) { }

        // if ((origAmount % 0.25) < origAmount)
        // {
        //     // coins[3] = (int)(origAmount / 0.25);
        //     // remainAmount = origAmount % 0.25;
        //     // origAmount = remainAmount;
        // }


        // int[,] matrix = new int[itemCount + 1, bagCapacity + 1];

        //Go through each item.
        for (int index = 0; index <= count; index++)
        {
            // for (int weight = 0; weight <= bagCapacity; weight++)
            // {
            //     if (index == 0 || weight == 0)
            //     {
            //         matrix[index, weight] = 0;
            //         continue;
            //     }
            //
            //     // var current_index = index - 1;
            //     // var current_item = items[current_index];
            //     // if (current_item.Weight <= weight)
            //     // {
            //     //     matrix[index, weight] = Math.Max(
            //     //         current_item.Value + matrix[index - 1, weight - current_item.Weight]
            //     //         , matrix[index - 1, weight]);
            //     // }
            //     // else
            //     //     matrix[index, weight] = matrix[index - 1, weight];
            // }
        }

        //Because we carry everything forward, the very last item on both indexes is our max val
        // return matrix[itemCount, bagCapacity];

        return default;
    }

    // Adapted from: https://www.guru99.com/knapsack-problem-dynamic-programming.html#2
    public static int[,] Knapsack(this int[] weights, int[] values, int maximum_weight, int n)
    {
        int[,] table_of_options = new int[n + 1, maximum_weight + 1];

        for (int i = 0; i <= n; i++)
        for (int j = 0; j <= maximum_weight; j++)
        {
            table_of_options[i, j] = 0;
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j <= maximum_weight; j++)
            {
                table_of_options[i, j] = table_of_options[i - 1, j];

                if (
                    j >= weights[i - 1]
                    && table_of_options[i, j]
                        < table_of_options[i - 1, j - weights[i - 1]] + values[i - 1]
                )
                {
                    table_of_options[i, j] =
                        table_of_options[i - 1, j - weights[i - 1]] + values[i - 1];
                }

                Console.WriteLine(table_of_options[i, j] + " ");
            }

            Console.WriteLine("\n");
        }

        Console.WriteLine("Max Value:\t" + table_of_options[n, maximum_weight]);

        Console.WriteLine("Selected Packs: ");

        while (n != 0)
        {
            if (table_of_options[n, maximum_weight] != table_of_options[n - 1, maximum_weight])
            {
                Console.WriteLine(
                    "\tPackage "
                        + n
                        + " with W = "
                        + weights[n - 1]
                        + " and Value = "
                        + values[n - 1]
                );

                maximum_weight -= weights[n - 1];
            }

            n--;
        }

        return table_of_options.Dump("final table");
    }
}
