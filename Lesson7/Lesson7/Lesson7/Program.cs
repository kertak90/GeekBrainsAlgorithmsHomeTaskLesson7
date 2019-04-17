using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Lesson7
{
    class Program
    {

        /*
            1. Написать функции, которые считывают матрицу смежности из файла и выводят её на
            экран.
            2. Написать рекурсивную функцию обхода графа в глубину.
            3. Написать функцию обхода графа в ширину.
            4. *Создать библиотеку функций для работы с графами.
             */
        static void Main(string[] args)
        {
            int answer = 0;
            while(true)
            {
                Console.Clear();

                answer = Convert.ToInt32(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    default:
                        break;
                }
            }            
        }

        static void Task1()
        {
            //1.Написать функции, которые считывают матрицу смежности из файла и выводят её на
            //экран.
            string fileName = "GraphFile.txt";
            StreamReader FS = new StreamReader(fileName);           
            List<string> stringMatrix = new List<string>();
            
            while(!FS.EndOfStream)                                                                  //Считываем все строки файла в список строк
            {
                stringMatrix.Add(FS.ReadLine());
            }

            int matrixSize = stringMatrix[0].Split(' ').Length;
            NodeState[,] matrix = new NodeState[stringMatrix.Capacity, stringMatrix.Capacity];
            bool flag = false;
            for(int i=0; i<stringMatrix.Capacity; i++)                                              //Перебираем все строки списка строк
            {
                string[] temp = stringMatrix[i].Split(' ');                                         //Делим строку по пробелу
                if (temp.Length != stringMatrix.Capacity)                                           //если размер полученного массива не равен числу строк в списке то матрица не квадратная
                {
                    Console.WriteLine("Файл с матрицой заполнен с ошибкой");
                    flag = true;
                    break;
                }
                for(int j=0; j<temp.Length; j++)                                                    //поместим все значения из массива в матрицу NodeState объектов
                {
                    matrix[i, j] = new NodeState();
                    matrix[i, j].Value = Convert.ToInt16(temp[j]);
                }
            }
            Console.WriteLine("Матрица сложности!!!");
            if (!flag)
            {
                
            }            
            Console.ReadLine();
        }

        class NodeState
        {
            public int Value { get; set; }                                                          //Значение
            public int Check { get; set; }                                                         //флаг использованности             
        }

        static void Task2()
        {
            //2.Написать рекурсивную функцию обхода графа в глубину.

            string fileName = "GraphFile.txt";
            StreamReader FS = new StreamReader(fileName);
            List<string> stringMatrix = new List<string>();

            while (!FS.EndOfStream)                                                                         //Считываем все строки файла в список строк
            {
                stringMatrix.Add(FS.ReadLine());
            }

            int matrixSize = stringMatrix[0].Split(' ').Length;
            NodeState[,] matrix = new NodeState[stringMatrix.Capacity, stringMatrix.Capacity];
            bool flag = false;
            for (int i = 0; i < stringMatrix.Capacity; i++)                                                 //Перебираем все строки списка строк
            {
                string[] temp = stringMatrix[i].Split(' ');                                                 //Делим строку по пробелу
                if (temp.Length != stringMatrix.Capacity)                                                   //если размер полученного массива не равен числу строк в списке то матрица не квадратная
                {
                    Console.WriteLine("Файл с матрицой заполнен с ошибкой");
                    flag = true;
                    break;
                }
                for (int j = 0; j < temp.Length; j++)                                                       //поместим все значения из массива в матрицу NodeState объектов
                {
                    matrix[i, j] = new NodeState();
                    matrix[i, j].Value = Convert.ToInt16(temp[j]);
                }
            }
            Stack<NodeState> stak = new Stack<NodeState>();                                                 //Стек для хранения узлов для дальнейшей проверки
            Console.WriteLine("Матрица сложности!!!");
            if (!flag)
            {
                ShowMatrix(matrix, matrixSize);
                Console.WriteLine("введите номер начального узла");
                int beginNode = Convert.ToInt16(Console.ReadLine());
                int row = beginNode;
                int collumn = 0;
                DepthGraph(matrix, collumn, matrixSize);
                ShowMatrix(matrix, matrixSize);

            }
        }

        static void ShowMatrix(NodeState[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)                                                        //Выведем все значения матрицы смежности в консоль
            {
                for (int j = 0; j < size; j++)
                {
                    ConsoleColor old = Console.ForegroundColor;
                    if(matrix[i, j].Check==1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(matrix[i, j].Value + " ");
                        Console.ForegroundColor = old;
                    }
                    else
                    {
                        Console.Write(matrix[i, j].Value + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void DepthGraph(NodeState[,] matrix, int collumn, int size)
        {
            NodeState min = new NodeState();
            for(int i=0; i<size; i++)                                                           //В цикле пробежались по всем веткам этого узла
            {
                if (matrix[collumn, i].Value != 0 && min.Value == 0&& matrix[collumn, i].Check!=1&& matrix[collumn, i].Check != 2)
                {
                    min.Value = matrix[collumn, i].Value;
                    min.Check = i;
                }
                else if(matrix[collumn, i].Value != 0 && min.Value > matrix[collumn, i].Value&& matrix[collumn, i].Check != 1&& matrix[collumn, i].Check != 2)
                {
                    min.Value = matrix[collumn, i].Value;
                    min.Check = i;
                }
            }
            
            matrix[collumn,min.Check].Check = 1;            //минимальный элемент матрицы отметил 1            

            for (int i = 0; i < size; i++)                  //Пробежались по строке collumn и отметили не минимальные ветви 2
            {
                if (matrix[collumn, i].Check != 1)
                    matrix[collumn,i].Check = 2;
            }

            for(int j=0; j<size; j++)                       //Пробежались по столбцу матрицы и отметили все элементы 2
            {
                if (matrix[j, min.Check].Check != 1)
                    matrix[j, min.Check].Check = 2;
            }
            if(min.Check!=0)
                DepthGraph(matrix, min.Check, size);
        }

        static void Task3()
        {

        }
    }
}
