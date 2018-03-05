using Ncels.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW.Ncels.Database.Helpers
{

    public static class PriceHelperKz
    {

        public static readonly Dictionary<String, String> NumberNamesMap = new Dictionary<String, String>()
        {
        {"1", "бiр"},
        {"2", "екi"},
        {"3", "үш"},
        {"4", "төрт"},
        {"5", "бес"},
        {"6", "алты"},
        {"7", "жетi"},
        {"8", "сегiз"},
        {"9", "тоғыз"},

        {"10", "он"},
        {"20", "жиырма"},
        {"30", "отыз"},
        {"40", "қырық"},
        {"50", "елу"},
        {"60", "алпыс"},
        {"70", "жетпiс"},
        {"80", "сексен"},
        {"90", "тоқсан"},
        {"100", "жүз"},
        };
        public static readonly Dictionary<int, String> RatioMap = new Dictionary<int, String>()
        {
        {1, "мың" },
        {2, "миллион"},
        {3, "миллиард"},
        {4, "триллион"},
        {5, "квадриллион"},
        {6, "квинтиллион"},
        {7, "секстиллион"},
        {8, "септиллион"},
        {9, "октиллион"},
        {10, "нониллион"},
        };

        public static String ToWord(int numberInteger)
        {

            try
            {
                String number = numberInteger.ToString();
                if (number != null && number.Length > 0)
                {
                    if (number.Length == 1 && number[0].ToString() == "0")
                    {
                        return "0 ";
                    }

                    int ratio = 0;
                    int mod = 0;
                    StringBuilder builder = new StringBuilder();
                    String temp = "";
                    for (int i = 0; i < number.Length; i++)
                    {
                        ratio = (number.Length - i) / 3;
                        mod = (number.Length - i) % 3;

                        temp = number[i].ToString();
                        if (mod == 1)
                        {
                            if (temp.Equals("0"))
                            {
                                for (int j = i; j < number.Length; j++)
                                {
                                    if ("0".Equals(number[j]))
                                    {
                                        i++;
                                    }
                                }

                            }
                            else
                            {
                                builder.Append(NumberNamesMap[temp]).Append(" ");
                            }

                            if (RatioMap.ContainsKey(ratio))
                            {
                                builder.Append(RatioMap[ratio]).Append(" ");
                            }
                            else if (!RatioMap.ContainsKey(ratio) && ratio > 0)
                            {
                                throw new Exception("Cannot map ratio " + ratio);
                            }
                        }

                        if (mod == 2)
                        {
                            if (!"0".Equals(temp))
                            {
                                temp += "0";
                                builder.Append(NumberNamesMap[temp]).Append(" ");
                            }
                        }

                        if (mod == 0)
                        {
                            if (!"0".Equals(temp))
                            {
                                if ("1".Equals(temp))
                                {
                                    builder.Append(NumberNamesMap["100"]).Append(" ");
                                }
                                else
                                {
                                    builder.Append(NumberNamesMap[temp]).Append(" ").Append(NumberNamesMap["100"]).Append(" ");
                                }
                            }

                        }
                    }

                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("GenerateLoginCode Exception", ex);
            }

            return "";
        }

        public static string ConvertKzNumbers(double number)
        {
            var strNumber = String.Format("{0:0.00}", number);
            var arrStrNumber = strNumber.Split(',');
            var beforePoint = int.Parse(arrStrNumber[0]);
            var afterPoint = int.Parse(arrStrNumber[1]);

            return ToWord(beforePoint) + "тенге " + ToWord(afterPoint) + "тиын";
        }
    }

}