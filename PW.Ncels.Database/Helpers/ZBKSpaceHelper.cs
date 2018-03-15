using Ncels.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.Ncels.Database.Helpers
{

    public static class ZBKSpaceHelper
    {
        public static Dictionary<int, string> BuildName(string name, int limit1, int limit2, int limit3, string shortName, string tail)
        {
            string[] arr = null;

            if (name.Length > (limit1 + limit2 + limit3))
            {
                arr = (shortName +" "+ tail).Split(' ');
            }
            else
            {
                arr = name.Split(' ');
            }

            StringBuilder builder = new StringBuilder();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            int j = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if ((builder.ToString() + arr[i]).Length < limit1)
                {
                    builder.Append(arr[i] + " ");
                }
                else
                {
                    dictionary.Add(1, builder.ToString());
                    j = i;
                    builder.Clear();
                    break;
                }

                if (i == (arr.Length - 1))
                {
                    dictionary.Add(1, builder.ToString());
                    j = i + 1;
                    builder.Clear();
                }
            }

            for (int i = j; i < arr.Length; i++)
            {
                if ((builder.ToString() + arr[i]).Length < limit2)
                {
                    builder.Append(arr[i] + " ");
                }
                else
                {
                    dictionary.Add(2, builder.ToString());
                    j = i;
                    builder.Clear();
                    break;
                }
                if (i == (arr.Length - 1))
                {
                    dictionary.Add(2, builder.ToString());
                    j = i + 1;
                    builder.Clear();
                }
            }


            for (int i = j; i < arr.Length; i++)
            {
                builder.Append(arr[i] + " ");
            }

            dictionary.Add(3, builder.ToString());

            return dictionary;
        }
    }

}