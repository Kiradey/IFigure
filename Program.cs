using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace figure
{
    public interface IFigure
    {
        double Perimeter();
        double Square();
        void Charact();
        void Info();
    }
    public class FigureCollection : IEnumerable<IFigure>
    {
        public List<IFigure> figures;
        public FigureCollection(List<IFigure> figures)
        {
            this.figures = figures;
        }
        public IEnumerator<IFigure> GetEnumerator()
        {
            return figures.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Triangle : IFigure
    {
        public int FirstSide { get; private set; }
        public int SecondSide { get; private set; }
        public int ThirdSide { get; private set; }
        public Triangle(int FirstSide, int SecondSide, int ThirdSide)
        {
            this.FirstSide = FirstSide;
            this.SecondSide = SecondSide;
            this.ThirdSide = ThirdSide;
        }
        public double Perimeter()
        {
            return (FirstSide + SecondSide + ThirdSide);
        }
        public double Square()
        {
            double halfOfPerim = Perimeter() / 2.0;
            double x = halfOfPerim * (halfOfPerim - FirstSide) * (halfOfPerim - SecondSide) * (halfOfPerim - ThirdSide);
            return Math.Sqrt(x);
        }
        public void Charact()
        {
            if (FirstSide == SecondSide && SecondSide == ThirdSide && FirstSide == ThirdSide) Console.Write("Равносторонний.");
            else if (FirstSide == SecondSide || SecondSide == ThirdSide || FirstSide == ThirdSide) Console.Write("Равнобедренный.");
            else Console.Write("Разносторонний.");
            if (Math.Pow(FirstSide, 2) + Math.Pow(SecondSide, 2) == Math.Pow(ThirdSide, 2) || Math.Pow(FirstSide, 2) + Math.Pow(ThirdSide, 2) == Math.Pow(SecondSide, 2) || Math.Pow(SecondSide, 2) + Math.Pow(ThirdSide, 2) == Math.Pow(FirstSide, 2)) Console.Write("Прямоугольный.");
            Console.WriteLine();
        }
        public void Info()
        {
            Console.Write($" Треугольник   {FirstSide}, {SecondSide}, {ThirdSide}    {(int)Perimeter()}       {(int)Square()}    ");
        }
    }
    public class Rectangle : IFigure
    {
        public int FirstSide { get; private set; }
        public int SecondSide { get; private set; }
        public Rectangle(int FirstSide, int SecondSide)
        {
            this.FirstSide = FirstSide;
            this.SecondSide = SecondSide;
        }
        public double Perimeter()
        {
            return 2 * (FirstSide + SecondSide);
        }
        public double Square()
        {
            return FirstSide * SecondSide;
        }
        public void Charact()
        {
            Console.WriteLine();
        }
        public void Info()
        {
            if (FirstSide == SecondSide)
            {
                Console.Write($" Квадрат       {FirstSide}, {SecondSide}       {(int)Perimeter()}       {(int)Square()}  ");

            }
            else
                Console.Write($" Прямоугольник {FirstSide}, {SecondSide}       {(int)Perimeter()}       {(int)Square()}  ");
        }
    }
    public class Oval : IFigure
    {
        public int FirstSide { get; private set; }
        public int SecondSide { get; private set; }
        public Oval(int FirstSide, int SecondSide)
        {
            this.FirstSide = FirstSide;
            this.SecondSide = SecondSide;
        }
        public double Perimeter()
        {
            return 2 * Math.PI * Math.Sqrt((Math.Pow(FirstSide, 2) + Math.Pow(SecondSide, 2)) / 2);
        }
        public double Square()
        {
            return Math.PI * FirstSide * SecondSide;
        }
        public void Charact()
        {
            Console.WriteLine();
        }
        public void Info()
        {
            if (FirstSide == SecondSide)
            {
                Console.Write($" Круг          {FirstSide}, {SecondSide}       {(int)Perimeter()}       {(int)Square()}    ");
            }
            else
                Console.Write($" Овал          {FirstSide}, {SecondSide}       {(int)Perimeter()}       {(int)Square()}    ");
        }
    }
    internal class Program
    {
        static void Main()
        {
            Console.Write("Введите размер массива:");
            int listCount;
            if (!int.TryParse(Console.ReadLine(), out listCount) || listCount <= 0)
            {
                Console.WriteLine("Ошибка ввода. Введите положительное целое число.");
                Main();
                return;
            }
            Console.WriteLine("№  Вид фигуры  Стороны Периметр Площадь Характеристика");
            Console.WriteLine("-------------------------------------------------------");
            Random rand = new Random();
            List<IFigure> figures = new List<IFigure>();
            Stopwatch time = new Stopwatch();
            for (int i = 1; i <= listCount; i++)
            {
                int figureCount = rand.Next(0, 3);
                IFigure figure = null;
                switch (figureCount)
                {
                    case 0:
                        {
                            int FS = rand.Next(1, 9);
                            int SS = rand.Next(1, 9);
                            int TS = rand.Next(1, 9);
                            if (FS + SS <= TS || FS + TS <= SS || SS + TS <= FS)
                            {
                                i--;
                                continue;
                            }
                            else
                            {
                                figure = new Triangle(FS, SS, TS);
                            }
                        }
                        break;
                    case 1:
                        {
                            int FS = rand.Next(1, 9);
                            int SS = rand.Next(1, 9);
                            figure = new Rectangle(FS, SS);
                        }
                        break;
                    case 2:
                        {
                            int FS = rand.Next(1, 9);
                            int SS = rand.Next(1, 9);
                            figure = new Oval(FS, SS);
                        }
                        break;
                }
                if (figure != null)
                {
                    Console.Write(i);
                    figures.Add(figure);
                    figure.Info();
                    figure.Charact();
                }
            }
            FigureCollection collection = new FigureCollection(figures);
            time.Start();
            Console.WriteLine("\nОбход всех фигур через foreach:");
            foreach (var figure in collection)
            {
                Console.WriteLine($"Фигура:{figure.GetType().Name}, {(int)figure.Square()}");
            }
            time.Stop();
            TimeSpan ts = time.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}.{4:00}",
           ts.Hours, ts.Minutes, ts.Seconds,
           ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Время выполнения: " + elapsedTime);

            time.Start();
            Console.WriteLine("\nОбход всех фигур через while:");
            int index = 0;
            while (index < collection.figures.Count)
            {
                var figure = collection.figures[index];
                Console.WriteLine($"Фигура:{figure.GetType().Name}, {(int)figure.Square()}");
                index++;
            }
            time.Stop();
            ts = time.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}.{4:00}",
          ts.Hours, ts.Minutes, ts.Seconds,
          ts.Milliseconds, ts.Ticks / 10);
            Console.WriteLine("Время выполнения: " + elapsedTime);
            /*
            Console.WriteLine("\nВыборка фигур с площадью больше 10:");
            var selectedFigures = collection.Where(figure => figure.Square() > 10);
            foreach (var figure in selectedFigures)
            {
                Console.WriteLine($"Фигура:{figure.GetType().Name}, {(int)figure.Square()}");
            }

            Console.WriteLine("\nСортировка фигур по площади:");
            var sortedFigures = collection.OrderBy(figure => figure.Square());
            foreach (var figure in sortedFigures)
            {
                Console.WriteLine($"Фигура:{figure.GetType().Name}, {(int)figure.Square()}");
            }

            Console.WriteLine("\nГруппировка фигур по типу:");
            var groupedFigures = collection.GroupBy(figure => figure.GetType().Name);
            foreach (var group in groupedFigures)
            {
                Console.WriteLine($"Тип: {group.Key}, Количество: {group.Count()}");
            }

            bool anySquareGreaterThan20 = collection.Any(figure => figure.Square() > 20);
            bool allSquaresGreaterThan5 = collection.All(figure => figure.Square() > 5);
            Console.WriteLine($"\nЕсть ли фигуры с площадью больше 20: {anySquareGreaterThan20}");
            Console.WriteLine($"Все ли фигуры имеют площадь больше 5: {allSquaresGreaterThan5}");

            double totalSquare = collection.Sum(figure => figure.Square());
            double minSquare = collection.Min(figure => figure.Square());
            double maxSquare = collection.Max(figure => figure.Square());
            Console.WriteLine($"\nСумма площадей всех фигур: {(int)totalSquare}");
            Console.WriteLine($"Минимальная площадь: {(int)minSquare}");
            Console.WriteLine($"Максимальная площадь: {(int)maxSquare}");
            */

            Console.WriteLine("Повторить? (0-нет), (1-да)");
            int x = int.Parse(Console.ReadLine());
            if (x == 1) { Main(); }
        }
    }
}
