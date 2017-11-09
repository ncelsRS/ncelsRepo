using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Ncels.Database.Helpers
{
    public enum TextCase { Nominative/*Кто? Что?*/, Genitive/*Кого? Чего?*/, Dative/*Кому? Чему?*/, Accusative/*Кого? Что?*/, Instrumental/*Кем? Чем?*/, Prepositional/*О ком? О чём?*/ };

    public static class RuDateAndMoneyConverter
    {
        static string[] monthNamesGenitive =
        {
            "", "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря"
        };

        static string zero = "ноль";
        static string firstMale = "один";
        static string firstFemale = "одна";
        static string firstFemaleAccusative = "одну";
        static string firstMaleGenetive = "одно";
        static string secondMale = "два";
        static string secondFemale = "две";
        static string secondMaleGenetive = "двух";
        static string secondFemaleGenetive = "двух";

        static string[] from3till19 =
        {
            "", "три", "четыре", "пять", "шесть",
            "семь", "восемь", "девять", "десять", "одиннадцать",
            "двенадцать", "тринадцать", "четырнадцать", "пятнадцать",
            "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать"
        };
        static string[] from3till19Genetive =
        {
            "", "трех", "четырех", "пяти", "шести",
            "семи", "восеми", "девяти", "десяти", "одиннадцати",
            "двенадцати", "тринадцати", "четырнадцати", "пятнадцати",
            "шестнадцати", "семнадцати", "восемнадцати", "девятнадцати"
        };
        static string[] tens =
        {
            "", "двадцать", "тридцать", "сорок", "пятьдесят",
            "шестьдесят", "семьдесят", "восемьдесят", "девяносто"
        };
        static string[] tensGenetive =
        {
            "", "двадцати", "тридцати", "сорока", "пятидесяти",
            "шестидесяти", "семидесяти", "восьмидесяти", "девяноста"
        };
        static string[] hundreds =
        {
            "", "сто", "двести", "триста", "четыреста",
            "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот"
        };
        static string[] hundredsGenetive =
        {
            "", "ста", "двухсот", "трехсот", "четырехсот",
            "пятисот", "шестисот", "семисот", "восемисот", "девятисот"
        };
        static string[] thousands =
        {
            "", "тысяча", "тысячи", "тысяч"
        };
        static string[] thousandsAccusative =
        {
            "", "тысячу", "тысячи", "тысяч"
        };
        static string[] millions =
        {
            "", "миллион", "миллиона", "миллионов"
        };
        static string[] billions =
        {
            "", "миллиард", "миллиарда", "миллиардов"
        };
        static string[] trillions =
        {
            "", "трилион", "трилиона", "триллионов"
        };
        static string[] rubles =
        {
            "", "рубль", "рубля", "рублей"
        };
        static string[] copecks =
        {
            "", "копейка", "копейки", "копеек"
        };
        static string[] tenge =
        {
            "", "тенге", "тенге", "тенге"
        };
        static string[] tiyn =
        {
            "", "тиын", "тиын", "тиын"
        };
        static bool IsPluralGenitive(int _digits)
        {
            if (_digits >= 5 || _digits == 0)
                return true;

            return false;
        }
        static bool IsSingularGenitive(int _digits)
        {
            if (_digits >= 2 && _digits <= 4)
                return true;

            return false;
        }
        static int lastDigit(long _amount)
        {
            long amount = _amount;

            if (amount >= 100)
                amount = amount % 100;

            if (amount >= 20)
                amount = amount % 10;

            return (int)amount;
        }

        public static string ToTextTenge(double _amount, bool _firstCapital)
        {
            long rublesAmount = (long)Math.Floor(_amount);
            long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            //int lastRublesDigit = lastDigit(rublesAmount);
            //int lastCopecksDigit = lastDigit(copecksAmount);

            string s = NumeralsToTxt(rublesAmount, TextCase.Nominative, true, _firstCapital) + " ";
            string c = NumeralsToTxt(copecksAmount, TextCase.Nominative, true, _firstCapital) + " ";

            string row = String.Format(" {0} целых {1} сотых", s, c);

            return row.Trim();
        }

        /// <summary>
        /// Десять тысяч тенге 67 тиын
        /// </summary>
        /// <param name="_amount"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string CurrencyToTxtTenge(double _amount, bool _firstCapital)
        {
            //Десять тысяч рублей 67 копеек
            long tengeAmount = (long)Math.Floor(_amount);
            long tiynAmount = ((long)Math.Round(_amount * 100)) % 100;
            int lastRublesDigit = lastDigit(tengeAmount);
            int lastCopecksDigit = lastDigit(tiynAmount);

            string s = NumeralsToTxt(tengeAmount, TextCase.Nominative, true, _firstCapital) + " ";

            if (IsPluralGenitive(lastRublesDigit))
            {
                s += tenge[3] + " ";
            }
            else if (IsSingularGenitive(lastRublesDigit))
            {
                s += tenge[2] + " ";
            }
            else
            {
                s += tenge[1] + " ";
            }

            s += String.Format("{0:00} ", tiynAmount);

            if (IsPluralGenitive(lastCopecksDigit))
            {
                s += tiyn[3] + " ";
            }
            else if (IsSingularGenitive(lastCopecksDigit))
            {
                s += tiyn[2] + " ";
            }
            else
            {
                s += tiyn[1] + " ";
            }

            return s.Trim();
        }

        /// <summary>
        /// Десять тысяч рублей 67 копеек
        /// </summary>
        /// <param name="_amount"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string CurrencyToTxt(double _amount, bool _firstCapital)
        {
            //Десять тысяч рублей 67 копеек
            long rublesAmount = (long)Math.Floor(_amount);
            long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            int lastRublesDigit = lastDigit(rublesAmount);
            int lastCopecksDigit = lastDigit(copecksAmount);

            string s = NumeralsToTxt(rublesAmount, TextCase.Nominative, true, _firstCapital) + " ";

            if (IsPluralGenitive(lastRublesDigit))
            {
                s += rubles[3] + " ";
            }
            else if (IsSingularGenitive(lastRublesDigit))
            {
                s += rubles[2] + " ";
            }
            else
            {
                s += rubles[1] + " ";
            }

            s += String.Format("{0:00} ", copecksAmount);

            if (IsPluralGenitive(lastCopecksDigit))
            {
                s += copecks[3] + " ";
            }
            else if (IsSingularGenitive(lastCopecksDigit))
            {
                s += copecks[2] + " ";
            }
            else
            {
                s += copecks[1] + " ";
            }

            return s.Trim();
        }
        /// <summary>
        /// 10 000 (Десять тысяч) рублей 67 копеек
        /// </summary>
        /// <param name="_amount"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string CurrencyToTxtFull(double _amount, bool _firstCapital)
        {
            //10 000 (Десять тысяч) рублей 67 копеек
            long rublesAmount = (long)Math.Floor(_amount);
            long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            int lastRublesDigit = lastDigit(rublesAmount);
            int lastCopecksDigit = lastDigit(copecksAmount);

            string s = String.Format("{0:N0} ({1}) ", rublesAmount, NumeralsToTxt(rublesAmount, TextCase.Nominative, true, _firstCapital));

            if (IsPluralGenitive(lastRublesDigit))
            {
                s += rubles[3] + " ";
            }
            else if (IsSingularGenitive(lastRublesDigit))
            {
                s += rubles[2] + " ";
            }
            else
            {
                s += rubles[1] + " ";
            }

            s += String.Format("{0:00} ", copecksAmount);

            if (IsPluralGenitive(lastCopecksDigit))
            {
                s += copecks[3] + " ";
            }
            else if (IsSingularGenitive(lastCopecksDigit))
            {
                s += copecks[2] + " ";
            }
            else
            {
                s += copecks[1] + " ";
            }

            return s.Trim();
        }
        /// <summary>
        /// 10 000 рублей 67 копеек  
        /// </summary>
        /// <param name="_amount"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string CurrencyToTxtShort(double _amount, bool _firstCapital)
        {
            //10 000 рублей 67 копеек        
            long rublesAmount = (long)Math.Floor(_amount);
            long copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            int lastRublesDigit = lastDigit(rublesAmount);
            int lastCopecksDigit = lastDigit(copecksAmount);

            string s = String.Format("{0:N0} ", rublesAmount);

            if (IsPluralGenitive(lastRublesDigit))
            {
                s += rubles[3] + " ";
            }
            else if (IsSingularGenitive(lastRublesDigit))
            {
                s += rubles[2] + " ";
            }
            else
            {
                s += rubles[1] + " ";
            }

            s += String.Format("{0:00} ", copecksAmount);

            if (IsPluralGenitive(lastCopecksDigit))
            {
                s += copecks[3] + " ";
            }
            else if (IsSingularGenitive(lastCopecksDigit))
            {
                s += copecks[2] + " ";
            }
            else
            {
                s += copecks[1] + " ";
            }

            return s.Trim();
        }
        static string MakeText(int _digits, string[] _hundreds, string[] _tens, string[] _from3till19, string _second, string _first, string[] _power)
        {
            string s = "";
            int digits = _digits;

            if (digits >= 100)
            {
                s += _hundreds[digits / 100] + " ";
                digits = digits % 100;
            }
            if (digits >= 20)
            {
                s += _tens[digits / 10 - 1] + " ";
                digits = digits % 10;
            }

            if (digits >= 3)
            {
                s += _from3till19[digits - 2] + " ";
            }
            else if (digits == 2)
            {
                s += _second + " ";
            }
            else if (digits == 1)
            {
                s += _first + " ";
            }

            if (_digits != 0 && _power.Length > 0)
            {
                digits = lastDigit(_digits);

                if (IsPluralGenitive(digits))
                {
                    s += _power[3] + " ";
                }
                else if (IsSingularGenitive(digits))
                {
                    s += _power[2] + " ";
                }
                else
                {
                    s += _power[1] + " ";
                }
            }

            return s;
        }

        /// <summary>
        /// реализовано для падежей: именительный (nominative), родительный (Genitive),  винительный (accusative)
        /// </summary>
        /// <param name="_sourceNumber"></param>
        /// <param name="_case"></param>
        /// <param name="_isMale"></param>
        /// <param name="_firstCapital"></param>
        /// <returns></returns>
        public static string NumeralsToTxt(long _sourceNumber, TextCase _case, bool _isMale, bool _firstCapital)
        {
            string s = "";
            long number = _sourceNumber;
            int remainder;
            int power = 0;

            if ((number >= (long)Math.Pow(10, 15)) || number < 0)
            {
                return "";
            }

            while (number > 0)
            {
                remainder = (int)(number % 1000);
                number = number / 1000;

                switch (power)
                {
                    case 12:
                        s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, trillions) + s;
                        break;
                    case 9:
                        s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, billions) + s;
                        break;
                    case 6:
                        s = MakeText(remainder, hundreds, tens, from3till19, secondMale, firstMale, millions) + s;
                        break;
                    case 3:
                        switch (_case)
                        {
                            case TextCase.Accusative:
                                s = MakeText(remainder, hundreds, tens, from3till19, secondFemale, firstFemaleAccusative, thousandsAccusative) + s;
                                break;
                            default:
                                s = MakeText(remainder, hundreds, tens, from3till19, secondFemale, firstFemale, thousands) + s;
                                break;
                        }
                        break;
                    default:
                        string[] powerArray = { };
                        switch (_case)
                        {
                            case TextCase.Genitive:
                                s = MakeText(remainder, hundredsGenetive, tensGenetive, from3till19Genetive, _isMale ? secondMaleGenetive : secondFemaleGenetive, _isMale ? firstMaleGenetive : firstFemale, powerArray) + s;
                                break;
                            case TextCase.Accusative:
                                s = MakeText(remainder, hundreds, tens, from3till19, _isMale ? secondMale : secondFemale, _isMale ? firstMale : firstFemaleAccusative, powerArray) + s;
                                break;
                            default:
                                s = MakeText(remainder, hundreds, tens, from3till19, _isMale ? secondMale : secondFemale, _isMale ? firstMale : firstFemale, powerArray) + s;
                                break;
                        }
                        break;
                }

                power += 3;
            }

            if (_sourceNumber == 0)
            {
                s = zero + " ";
            }

            if (s != "" && _firstCapital)
                s = s.Substring(0, 1).ToUpper() + s.Substring(1);

            return s.Trim();
        }
        public static string NumeralsDoubleToTxt(double _sourceNumber, int _decimal, TextCase _case, bool _firstCapital)
        {
            long decNum = (long)Math.Round(_sourceNumber * Math.Pow(10, _decimal)) % (long)(Math.Pow(10, _decimal));

            string s = String.Format(" {0} целых {1} сотых", NumeralsToTxt((long)_sourceNumber, _case, true, _firstCapital),
                NumeralsToTxt((long)decNum, _case, true, false));
            return s.Trim();
        }

        /// <summary>
        /// название м-ца
        /// </summary>
        /// <param name="_month">с единицы</param>
        /// <param name="_case"></param>
        /// <returns></returns>
        public static string MonthName(int _month, TextCase _case)
        {
            string s = "";

            if (_month > 0 && _month <= 12)
            {
                switch (_case)
                {
                    case TextCase.Genitive:
                        s = monthNamesGenitive[_month];
                        break;
                }
            }

            return s;
        }
        public static string NumeralsRoman(int _number)
        {
            string s = "";

            switch (_number)
            {
                case 1: s = "I"; break;
                case 2: s = "II"; break;
                case 3: s = "III"; break;
                case 4: s = "IV"; break;
            }

            return s;
        }
    }

    //Пример использования


    //double am = 37.67;
    //DateTime dt1 = DateTime.Now;
    //MessageBox.Show(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}"

    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Accusative, false, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Accusative, true, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Genitive, false, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Genitive, true, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Nominative, false, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.NumeralsToTxt((long)am, TextCase.Nominative, true, false)
    //, Environment.NewLine
    //));

    //MessageBox.Show(string.Format("{0}{1}{2}{3}{4}{5}"
    //, RuDateAndMoneyConverter.CurrencyToTxtFull(am, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.CurrencyToTxt(am, false)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.CurrencyToTxtShort(am, false)
    //, Environment.NewLine
    //));

    //MessageBox.Show(string.Format("{0}{1}{2}{3}{4}{5}"
    //, RuDateAndMoneyConverter.DateToTextQuarter(dt1)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.DateToTextSimple(dt1)
    //, Environment.NewLine
    //, RuDateAndMoneyConverter.DateToTextLong(dt1)
    //, Environment.NewLine
    //));
}