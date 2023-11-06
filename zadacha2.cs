using System;
using System.Reflection;

namespace IndexableObjectsUtilityPayments
{
    interface IIndexable
    {
        object this[int index] { get; set; }
        int Length { get; }
    }

    class IndexableObject : IIndexable
    {
        private object[] data;

        public IndexableObject(int length)
        {
            data = new object[length];
        }

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= data.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return data[index];
            }
            set
            {
                if (index < 0 || index >= data.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                data[index] = value;
            }
        }

        public int Length
        {
            get { return data.Length; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var payments = new IndexableObject(4);
            payments[0] = "Отопление";
            payments[1] = "Вода";
            payments[2] = "Газ";
            payments[3] = "Текущий ремонт";
            Console.Write(" Введите 'true' для осени и зимы или 'false' для весны и лета. ");
            bool isAutumnOrWinter = false;
            if (!bool.TryParse(Console.ReadLine(), out isAutumnOrWinter))
            {
                Console.WriteLine("Некорректный ввод. Введите 'true' для осени и зимы или 'false' для весны и лета.");
                return;
            }

            Console.Write("Введите количество проживающих людей: ");
            int numberOfResidents;
            if (!int.TryParse(Console.ReadLine(), out numberOfResidents))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число для количества проживающих.");
                return;
            }


            double[] baseRates = { 5.0, 3.0, 2.5, 4.0 }; // базовые тарифы
            int area = 100; // метраж помещения
            double[] discounts = { 0.3, 0.5 }; // скидки для ветеранов труда и ветеранов войны

            double totalCost = 0;

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("| Вид платежа        | Начислено  | Льготная скидка | Итого |");
            Console.WriteLine("--------------------------------------------------------");

            for (int i = 0; i < payments.Length; i++)
            {
                double currentCost = baseRates[i] * (i == 0 && isAutumnOrWinter ? 1.2 : 1) * (i == 1 ? numberOfResidents : 1) * (i == 2 ? numberOfResidents : 1) * (i == 3 ? area : 1);
                double totalDiscount = 0;

                if (i == 0)
                {
                    currentCost = baseRates[i] * (isAutumnOrWinter ? 1.2 : 1) * area;
                }
                else if (i == 1 || i == 2)
                {
                    currentCost = baseRates[i] * numberOfResidents;
                }

                if (i < discounts.Length)
                {
                    totalDiscount = currentCost * discounts[i];
                }

                totalCost += currentCost - totalDiscount;

                Console.WriteLine($"| {payments[i],-18} | {currentCost,9:F2} | {totalDiscount,15:F2} | {currentCost - totalDiscount,5:F2} |");
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"| Итого             | {"-",9} | {"-",15} | {totalCost,5:F2} |");
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}


