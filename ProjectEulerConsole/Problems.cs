using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProjectEulerConsole
{
    public static class Problems
    {
        #region DynamicMethod

        public static string DoProblem(int problemNumber, object optionalParam = null)
        {
            //Gets the method of the problem to solve.
            MethodInfo getMethod = (typeof(Problems)).GetMethod($"Problem{problemNumber}Answer");


            //Sees if the method is correct and then tries to solve it.
            if (getMethod != null)
            {
                Stopwatch sw = new Stopwatch();

                sw.Start();
                object result;
                if (getMethod.GetParameters().Length == 1 && optionalParam != null)
                {
                    if (optionalParam is long)
                        result = getMethod.Invoke(null, new object[] { long.Parse(optionalParam.ToString()) });
                    else if (optionalParam is int)
                        result = getMethod.Invoke(null, new object[] { int.Parse(optionalParam.ToString()) });
                    else
                        result = getMethod.Invoke(null, new object[] { optionalParam.ToString() });
                }
                else if (optionalParam is object[] && getMethod.GetParameters().Length == (optionalParam as object[]).Length)
                    result = getMethod.Invoke(null, optionalParam as object[]);
                else if (optionalParam == null)
                {
                    object[] emptyParameters = new object[getMethod.GetParameters().Length];
                    for (int i = 0; i < emptyParameters.Length; i++)
                    {
                        emptyParameters[i] = Type.Missing;
                    }
                    result = getMethod.Invoke(null, BindingFlags.OptionalParamBinding |
            BindingFlags.InvokeMethod |
            BindingFlags.CreateInstance,
            null,
            emptyParameters,
            CultureInfo.InvariantCulture);
                }
                else
                {
                    result = null;
                }

                sw.Stop();
                if (result != null)
                    return result.ToString() + $" ({sw.ElapsedMilliseconds.ToString()}ms)";
                else
                    return "You entered the incorrect parameters!";
            }


            return "Problem number not recognised.";
        }

        #endregion
    }
}
