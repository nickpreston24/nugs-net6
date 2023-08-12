namespace nugsnet6.Experimental;

public static class CurryExtensions
{
    // public static function curry(func) {
    //
    //      return function curried(...args) {
    //          if (args.length >= func.length) {
    //              return func.apply(this, args);
    //          } else {
    //              return function(...args2) {
    //                  return curried.apply(this, args.concat(args2));
    //              }
    //          }
    //      };
    //
    //  }

    // public static void Curry<T>(this Func<T> fn, int fnLength = 1)
    // {
    //     // fnLength = fnLength || fn.length;
    //     //
    //     // return function makeCurry() {
    //     //     var args = slice.call(arguments);
    //     //     if (args.length === fnLength) return fn.apply(this, args);
    //     //     return function () {
    //     //         var newArgs = slice.call(arguments);
    //     //         return makeCurry.apply(this, args.concat(newArgs));
    //     //     }
    //     // }
    // }
}