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
                Console.WriteLine("Введите номер задачи!!!");
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

        static void Task2()
        {
            //2.Написать рекурсивную функцию обхода графа в глубину.
            //Небольшие изменения

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
                                                                                                            //Стек для хранения узлов для дальнейшей проверки
            Console.WriteLine("Матрица сложности!!!");
            if (!flag)
            {
                ShowMatrix(matrix, matrixSize);
                Console.WriteLine("введите номер начального узла");
                int minValue = 0;
                int minValueRowIndex = 0;
                int minValueColIndex = 0;
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)                                                    //В цикле пробежались по всем ячейкам и выбрали минимальный
                    {
                        if (matrix[i, j].Value != 0)
                        {
                            //Если min.Value пустой и ячейка мастрицы не нулевая, то обновим значение минимального значения
                            if (minValue == 0)
                            {
                                minValue = matrix[i, j].Value;                                
                            }
                            else if (minValue > matrix[i, j].Value)
                            {
                                minValue = matrix[i, j].Value;                      //в value буду хранить значение минимального элемента строки матрицы
                                minValueRowIndex = i;                               //в Check буду хранить индекс минимального элемента строки
                                minValueColIndex = j;
                            }
                        }
                    }
                }
                matrix[minValueRowIndex, minValueColIndex].Check = 1;
                for (int j = 0; j < matrixSize; j++)                                //Пробежались по столбцу матрицы и отметили все элементы 2
                {
                    if (matrix[j, minValueColIndex].Check != 1)
                        matrix[j, minValueColIndex].Check = 2;
                }
                for(int k =0; k < matrixSize; k++)
                {
                    if (matrix[minValueRowIndex, k].Check != 1)
                        matrix[minValueRowIndex, k].Check = 3;
                }
                ShowMatrix(matrix, matrixSize);
                DepthGraph(matrix, matrixSize);

                Console.WriteLine("Задача завершена!!!");
                Console.ReadLine();
            }
        }

        static void ShowMatrix(NodeState[,] matrix, int size)
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)                                                        //Выведем все значения матрицы смежности в консоль
            {
                for (int j = 0; j < size; j++)
                {
                    ConsoleColor old = Console.ForegroundColor;
                    if(matrix[i, j].Check==1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matrix[i, j].Value + " ");
                        Console.ForegroundColor = old;
                    }
                    else if(matrix[i, j].Check == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(matrix[i, j].Value + " ");
                        Console.ForegroundColor = old;
                    }
                    else if (matrix[i, j].Check == 3)
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

        static void DepthGraph(NodeState[,] matrix, int size)
        {
            NodeState min = new NodeState();
            int minRowIndex = 0;
            int minColIndex = 0;
            bool check = false;
            for(int i = 0; i<size; i++)
            {
                for (int j = 0; j < size; j++)                                                           //В цикле пробежались по всем ячейкам и выбрали минимальный
                {
                    if(matrix[i, j].Value != 0 && matrix[i, j].Check == 2)
                    {
                        //Если min.Value пустой и ячейка мастрицы не нулевая, то обновим значение минимального значения
                        if (min.Value == 0 && matrix[i, j].Check != 1)
                        {
                            min.Value = matrix[i, j].Value;
                            min.Check = i;
                        }
                        else if (min.Value > matrix[i, j].Value && matrix[i, j].Check != 1)
                        {
                            min.Value = matrix[i, j].Value;     //в value буду хранить значение минимального элемента строки матрицы
                                                                //в Check буду хранить индекс минимального элемента строки
                            minRowIndex = i;
                            minColIndex = j;
                            check = true;
                        }
                    }                    
                }
            }

            if(check)                                                       //Если минимальный элемент был найден, то вычеркнем строку и добавим рабочий столбец
            {
                matrix[minRowIndex, minColIndex].Check = 1;                 //минимальный элемент матрицы отметил 1            
                for (int j = 0; j < size; j++)                              //Пробежались по столбцу матрицы и отметили все элементы 2
                {
                    if (matrix[j, minRowIndex].Check != 1 && matrix[j, minRowIndex].Check != 3)
                        matrix[j, minRowIndex].Check = 2;                   //Рабочие элементы столбца
                }
                ShowMatrix(matrix, size);
                for (int k = 0; k < size; k++)
                {
                    if (matrix[minRowIndex, k].Check != 1)
                        matrix[minRowIndex, k].Check = 3;                   //Более не используемые ячейки данной строки
                }

                ShowMatrix(matrix, size);

                DepthGraph(matrix, size);
            }
        }

        static void Task3()
        {
            //3.Написать функцию обхода графа в ширину.
            Graph newGraph = new Graph("GraphFile.txt");
            newGraph.ShowMatrix();
            newGraph.Width();

        }
    }

    public class NodeState
    {
        public int Value { get; set; }                                                          //Значение
        public int Check { get; set; }                                                         //флаг использованности             
    }

    public class Graph
    {

        string fileName = "GraphFile.txt";
        StreamReader FS;
        List<string> stringMatrix = new List<string>();
        NodeState[,] matrix;
        Queue<int> myQue = new Queue<int>();
        int Size = 0;

        public Graph(string path)
        {
            //2.Написать рекурсивную функцию обхода графа в глубину.
            //Небольшие изменения
            FS = new StreamReader(path);
            while (!FS.EndOfStream)                                                                         //Считываем все строки файла в список строк
            {
                stringMatrix.Add(FS.ReadLine());
            }

            Size = stringMatrix[0].Split(' ').Length;
            matrix = new NodeState[stringMatrix.Capacity, stringMatrix.Capacity];
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
        }

        public void ShowMatrix()
        {
            Console.WriteLine();
            for (int i = 0; i < Size; i++)                                                        //Выведем все значения матрицы смежности в консоль
            {
                for (int j = 0; j < Size; j++)
                {
                    ConsoleColor old = Console.ForegroundColor;
                    if (matrix[i, j].Check == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matrix[i, j].Value + " ");
                        Console.ForegroundColor = old;
                    }
                    else if (matrix[i, j].Check == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(matrix[i, j].Value + " ");
                        Console.ForegroundColor = old;
                    }
                    else if (matrix[i, j].Check == 3)
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

        public void Width()
        {
            Console.WriteLine("Введите начальный узел: ");
            int start = Convert.ToInt16(Console.ReadLine());
            myQue.Enqueue(start);
            WidthAlgorithm();
        }

        private void WidthAlgorithm()
        {
            bool check = false;
            if(myQue.Count!=0)
            {
                int rowIndex = myQue.Dequeue();
                
                for(int i =0; i< Size; i++)
                {
                    if (matrix[rowIndex, i].Value > 0 && matrix[rowIndex, i].Check != 1)
                    {
                        myQue.Enqueue(i);
                        matrix[rowIndex, i].Check = 2;          //2 будет означать  что данные узлы добавлены в очередь
                        check = true;
                    }   
                }

                for (int j = 0; j < Size; j++)                  //Отметим все строки столбца rowIndex единицей т.к. этот узел обработан и к нему возвращаться не следует
                {
                    if(matrix[j, rowIndex].Check != 1 && matrix[j, rowIndex].Check != 2)
                    {
                        matrix[j, rowIndex].Check = 1;
                    }
                    else
                        matrix[j, rowIndex].Check = 1;
                }
                if(check)
                {
                    ShowMatrix();
                    Console.WriteLine();                   
                }
                    
                WidthAlgorithm();
            }
        }
    }
}
