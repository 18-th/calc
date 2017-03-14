using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StationCalculator
{
    public partial class Form1 : Form
    {

        struct sequence
        {
            public sequence(string permutation, int numberOfPermutation)
            {
                num = numberOfPermutation;
                firstMag = new int[3] { (int)char.GetNumericValue(permutation[0]), (int)char.GetNumericValue(permutation[1]), (int)char.GetNumericValue(permutation[2]) };
                secondMag = new int[3] { (int)char.GetNumericValue(permutation[3]), (int)char.GetNumericValue(permutation[4]), (int)char.GetNumericValue(permutation[5]) };
            }
            public int num;
            public int[] firstMag;
            public int[] secondMag;
        }
        struct pair
        {
            public pair(string pairInfo)
            {
                first = (int)char.GetNumericValue(pairInfo[0]);
                second = (int)char.GetNumericValue(pairInfo[2]);
            }
            public pair(int f, int s)
            {
                first = f;
                second = s;
            }
            public int first;
            public int second;
        }
        struct magistralPiece
        {
            public magistralPiece(int start, int end, int len)
            {
                from = start;
                to = end;
                length = len;
            }
            public int from;
            public int to;
            public int length;
        }
        public Form1()
        {
            InitializeComponent();
        }
        sequence[] variants = new sequence[720];
        List<pair> transactons = new List<pair>();
        int[,] amountOfInfo = new int[6, 6];
        List<magistralPiece> firstMagistralOut = new List<magistralPiece>();
        List<magistralPiece> secondMagistralOut = new List<magistralPiece>();
        int currentTime;

        void GetSequences()
        {
            string temp;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader("mutes.txt");
            while ((temp = file.ReadLine()) != null)
            {
                variants[counter] = new sequence(temp, counter);
                counter++;
            }
            file.Close();
        }
        void GetTransactions()
        {
            string temp;
            System.IO.StreamReader file = new System.IO.StreamReader("transes.txt");
            while ((temp = file.ReadLine()) != null)
            {
                transactons.Add(new pair(temp));
                amountOfInfo[((int)char.GetNumericValue(temp[0])) - 1, ((int)char.GetNumericValue(temp[2])) - 1] = Convert.ToInt32(temp.Substring(3));
            }
            file.Close();
        }
        int[,] GetPrioritiesMatrix(sequence currentSequence)
        {
            int[,] priorMat = new int[6, 6];
            for (int i = 0; i < transactons.Count; i++)
            {
                if ((currentSequence.firstMag.Contains<int>(transactons[i].first) && currentSequence.firstMag.Contains<int>(transactons[i].second))
                    || (currentSequence.secondMag.Contains<int>(transactons[i].first) && currentSequence.secondMag.Contains<int>(transactons[i].second)))
                {
                    priorMat[transactons[i].first - 1, transactons[i].second - 1] = 1;
                }
                else
                {
                    priorMat[transactons[i].first - 1, transactons[i].second - 1] = 2;
                }
            }
            return priorMat;
        }
        int FindMinSequence(int[,] infoAmountMatrix)
        {
            int minIndex = 0;
            int[,] infoMatrix = new int[6, 6];
            for (int i = 0; i < infoAmountMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < infoAmountMatrix.GetLength(1); j++)
                {
                    infoMatrix[i, j] = infoAmountMatrix[i, j];
                }
            }
            List<pair> timesOfTrials = new List<pair>();
            foreach (sequence c in variants)
            {
                //Переставляем матрицу количества передач
                for (int i = 0; i < infoAmountMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < infoAmountMatrix.GetLength(1); j++)
                    {
                        infoMatrix[i, j] = infoAmountMatrix[i, j];
                    }
                }
                //
                int[,] priorityMatrix = GetPrioritiesMatrix(c);
                int firstMagStationsToParallel = 0;
                int secondMagStationsToParallel = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (priorityMatrix[i, j] == 1 && c.firstMag.Contains(i + 1))
                        {
                            firstMagStationsToParallel++;
                        }
                        else if (priorityMatrix[i, j] == 1 && c.secondMag.Contains(i + 1))
                        {
                            secondMagStationsToParallel++;
                        }
                    }
                }
                bool sended = false;
                pair firstMagMaxInd = new pair(0, 0);
                pair secondMagMaxInd = new pair(0, 0);
                int firstMagMaxTrans = 0;
                int secondMagMaxTrans = 0;
                bool firstFrozen = false;
                bool secondFrozen = false;
                //Список для теста, составлять для каждой ситуации - слишком жирно по производительности
                //List<magistralPiece> firstMagistralOut = new List<magistralPiece>();
                //List<magistralPiece> secondMagistralOut = new List<magistralPiece>();
                //
                int checker = 0;
                int time = 0;
                while (!sended)
                {
                    if (!firstFrozen)
                        firstMagMaxTrans = 0;
                    if (!secondFrozen)
                        secondMagMaxTrans = 0;
                    checker = 0;
                    if (secondMagStationsToParallel == 0 || firstMagStationsToParallel == 0)
                        break;
                    for (int i = 0; i < 6; i++) //Проходим по массиву в поисках подходящих для передачи элементов
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (priorityMatrix[i, j] == 1 && infoMatrix[i, j] != 0)
                            {
                                if (c.firstMag.Contains(i + 1) && !firstFrozen)
                                {
                                    if (infoMatrix[i, j] > firstMagMaxTrans)
                                    {
                                        firstMagMaxTrans = infoMatrix[i, j];
                                        firstMagMaxInd.first = i;
                                        firstMagMaxInd.second = j;
                                        checker++;
                                    }
                                }
                                else if (c.secondMag.Contains(i + 1) && !secondFrozen)
                                {
                                    if (infoMatrix[i, j] > secondMagMaxTrans)
                                    {
                                        secondMagMaxTrans = infoMatrix[i, j];
                                        secondMagMaxInd.first = i;
                                        secondMagMaxInd.second = j;
                                        checker++;
                                    }
                                }
                            }
                        }
                    }
                    if (secondMagMaxTrans > firstMagMaxTrans && infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] > 0)
                    {
                        infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= firstMagMaxTrans;
                        infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= firstMagMaxTrans;
                        secondMagMaxTrans -= firstMagMaxTrans;
                        firstMagStationsToParallel--;
                        time += firstMagMaxTrans;
                        //secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, firstMagMaxTrans));
                        //firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, firstMagMaxTrans));
                        secondFrozen = true;
                        firstFrozen = false;
                    }
                    else if (secondMagMaxTrans < firstMagMaxTrans && infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] > 0)
                    {
                        infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= secondMagMaxTrans;
                        infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= secondMagMaxTrans;
                        firstMagMaxTrans -= secondMagMaxTrans;
                        secondMagStationsToParallel--;
                        time += secondMagMaxTrans;
                        //firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, secondMagMaxTrans));
                        //secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, secondMagMaxTrans));
                        secondFrozen = false;
                        firstFrozen = true;
                    }
                    else if (secondMagMaxTrans == firstMagMaxTrans && infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] > 0)
                    {
                        infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= firstMagMaxTrans;
                        infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= secondMagMaxTrans;
                        secondMagStationsToParallel--;
                        firstMagStationsToParallel--;
                        time += secondMagMaxTrans;
                        //firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, firstMagMaxTrans));
                        //secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, secondMagMaxTrans));
                        secondFrozen = false;
                        firstFrozen = false;
                    }
                    if (checker == 0)
                        sended = true;
                }
                //Добавляем оставшиеся передачи по одной магистрали,а затем полные передачи
                bool over = false;
                while (!over)
                {
                    checker = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (priorityMatrix[i, j] == 1 && infoMatrix[i, j] != 0)
                            {
                                if (c.firstMag.Contains(i + 1))
                                {
                                    time += infoMatrix[i, j];
                                    infoMatrix[i, j] = 0;
                                    checker++;
                                }
                                else if (c.secondMag.Contains(i + 1))
                                {
                                    time += infoMatrix[i, j];
                                    infoMatrix[i, j] = 0;
                                    checker++;
                                }
                            }
                        }
                    }
                    if (checker == 0)
                        over = true;
                }
                for(int i=0;i<6;i++)
                {
                    for(int j=0;j<6;j++)
                    {
                        if (priorityMatrix[i, j] == 2 && infoMatrix[i, j] != 0)
                        {
                            time += infoMatrix[i, j];
                            infoMatrix[i, j] = 0;
                        }
                    }
                }
                timesOfTrials.Add(new pair(time, c.num));
            }
            int minTime = timesOfTrials.First<pair>().first;
            foreach (pair p in timesOfTrials)
            {
                if (minTime > p.first)
                {
                    minIndex = p.second;
                    minTime = p.first;
                }
            }
            return minIndex;
        }
        void SetOutputData(int[,] infoAmountMatrix, sequence top)
        {
            int[,] infoMatrix = new int[6, 6];
            for (int i = 0; i < infoAmountMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < infoAmountMatrix.GetLength(1); j++)
                {
                    infoMatrix[i, j] = infoAmountMatrix[i, j];
                }
            }
            sequence c = top;
            //Переставляем матрицу количества передач
            for (int i = 0; i < infoAmountMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < infoAmountMatrix.GetLength(1); j++)
                {
                    infoMatrix[i, j] = infoAmountMatrix[i, j];
                }
            }
            //
            int[,] priorityMatrix = GetPrioritiesMatrix(c);
            int firstMagStationsToParallel = 0;
            int secondMagStationsToParallel = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (priorityMatrix[i, j] == 1 && c.firstMag.Contains(i + 1))
                    {
                        firstMagStationsToParallel++;
                    }
                    else if (priorityMatrix[i, j] == 1 && c.secondMag.Contains(i + 1))
                    {
                        secondMagStationsToParallel++;
                    }
                }
            }
            bool sended = false;
            pair firstMagMaxInd = new pair(0, 0);
            pair secondMagMaxInd = new pair(0, 0);
            int firstMagMaxTrans = 0;
            int secondMagMaxTrans = 0;
            bool firstFrozen = false;
            bool secondFrozen = false;
            //Список для теста, составлять для каждой ситуации - слишком жирно по производительности
            //
            int checker = 0;
            int time = 0;
            while (!sended)
            {
                if (!firstFrozen)
                    firstMagMaxTrans = 0;
                if (!secondFrozen)
                    secondMagMaxTrans = 0;
                checker = 0;
                if (secondMagStationsToParallel == 0 || firstMagStationsToParallel == 0)
                    break;
                for (int i = 0; i < 6; i++) //Проходим по массиву в поисках подходящих для передачи элементов
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (priorityMatrix[i, j] == 1 && infoMatrix[i, j] != 0)
                        {
                            if (c.firstMag.Contains(i + 1) && !firstFrozen)
                            {
                                if (infoMatrix[i, j] > firstMagMaxTrans)
                                {
                                    firstMagMaxTrans = infoMatrix[i, j];
                                    firstMagMaxInd.first = i;
                                    firstMagMaxInd.second = j;
                                    checker++;
                                }
                            }
                            else if (c.secondMag.Contains(i + 1) && !secondFrozen)
                            {
                                if (infoMatrix[i, j] > secondMagMaxTrans)
                                {
                                    secondMagMaxTrans = infoMatrix[i, j];
                                    secondMagMaxInd.first = i;
                                    secondMagMaxInd.second = j;
                                    checker++;
                                }
                            }
                        }
                    }
                }
                if (secondMagMaxTrans > firstMagMaxTrans && infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] > 0)
                {
                    infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= firstMagMaxTrans;
                    infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= firstMagMaxTrans;
                    secondMagMaxTrans -= firstMagMaxTrans;
                    firstMagStationsToParallel--;
                    time += firstMagMaxTrans;
                    secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, firstMagMaxTrans));
                    firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, firstMagMaxTrans));
                    secondFrozen = true;
                    firstFrozen = false;
                }
                else if (secondMagMaxTrans < firstMagMaxTrans && infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] > 0)
                {
                    infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= secondMagMaxTrans;
                    infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= secondMagMaxTrans;
                    firstMagMaxTrans -= secondMagMaxTrans;
                    secondMagStationsToParallel--;
                    time += secondMagMaxTrans;
                    firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, secondMagMaxTrans));
                    secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, secondMagMaxTrans));
                    secondFrozen = false;
                    firstFrozen = true;
                }
                else if (secondMagMaxTrans == firstMagMaxTrans && infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] > 0)
                {
                    infoMatrix[firstMagMaxInd.first, firstMagMaxInd.second] -= firstMagMaxTrans;
                    infoMatrix[secondMagMaxInd.first, secondMagMaxInd.second] -= secondMagMaxTrans;
                    secondMagStationsToParallel--;
                    firstMagStationsToParallel--;
                    time += secondMagMaxTrans;
                    firstMagistralOut.Add(new magistralPiece(firstMagMaxInd.first + 1, firstMagMaxInd.second + 1, firstMagMaxTrans));
                    secondMagistralOut.Add(new magistralPiece(secondMagMaxInd.first + 1, secondMagMaxInd.second + 1, secondMagMaxTrans));
                    secondFrozen = false;
                    firstFrozen = false;
                }
                if (checker == 0)
                    sended = true;
            }
            //Добавляем оставшиеся передачи по одной магистрали,а потом полные передачи
            bool over = false;
            while (!over)
            {
                checker = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (priorityMatrix[i, j] == 1 && infoMatrix[i, j] != 0)
                        {
                            if (c.firstMag.Contains(i + 1))
                            {
                                firstMagistralOut.Add(new magistralPiece(i + 1, j + 1, infoMatrix[i, j]));
                                secondMagistralOut.Add(new magistralPiece(0, 0, infoMatrix[i, j]));
                                infoMatrix[i, j] = 0;
                                checker++;
                            }
                            else if (c.secondMag.Contains(i + 1))
                            {
                                secondMagistralOut.Add(new magistralPiece(i + 1, j + 1, infoMatrix[i, j]));
                                firstMagistralOut.Add(new magistralPiece(0, 0, infoMatrix[i, j]));
                                infoMatrix[i, j] = 0;
                                checker++;
                            }
                        }
                    }
                }
                if (checker == 0)
                    over = true;
            }
            for(int i=0;i<6;i++)
            {
                for(int j=0;j<6;j++)
                {
                    if (priorityMatrix[i, j] == 2 && infoMatrix[i, j] != 0)
                    {
                        firstMagistralOut.Add(new magistralPiece(i + 1, j + 1, infoMatrix[i, j]));
                        secondMagistralOut.Add(new magistralPiece(i + 1, j + 1, infoMatrix[i, j]));
                        infoMatrix[i, j] = 0;
                        checker++;
                    }
                }
            }
            SetCurrentTime();
            pictureBox1.Invalidate();
        }
        void VizualiseStationsInfo(int stationNumber)
        {
            //Вывод на экран значений для минимального номера
            ULLabel.Text = variants[stationNumber].firstMag[0].ToString();
            UMLable.Text = variants[stationNumber].firstMag[1].ToString();
            URLabel.Text = variants[stationNumber].firstMag[2].ToString();
            DLLabel.Text = variants[stationNumber].secondMag[0].ToString();
            DCLabel.Text = variants[stationNumber].secondMag[1].ToString();
            DRLabel.Text = variants[stationNumber].secondMag[2].ToString();
        }
        void SetCurrentTime()
        {
            int curTime = 0;
            int temp1 = 0;
            int temp2 = 0;
            foreach (magistralPiece p in firstMagistralOut)
            {
                temp1 += p.length;
            }
            foreach (magistralPiece p in secondMagistralOut)
            {
                temp2 += p.length;
            }
            if (temp2 > temp1)
                curTime = temp2;
            else
                curTime = temp1;
            StringBuilder defaultString = new StringBuilder("Time for a current variant: ");
            currentTimeLabel.Text = defaultString + " " +  curTime.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetSequences();
            GetTransactions();
            int minSequenceNumber = FindMinSequence(amountOfInfo);
            SetOutputData(amountOfInfo,variants[minSequenceNumber]);
            int minTime = 0;
            int temp1 = 0;
            int temp2 = 0;
            foreach(magistralPiece p in firstMagistralOut)
            {
                temp1 += p.length;
            }
            foreach(magistralPiece p in secondMagistralOut)
            {
                temp2 += p.length;
            }
            if (temp2 > temp1)
                minTime = temp2;
            else
                minTime = temp1;
            minCostLabel.Text = "Minimum time to transfer data is: " + minTime.ToString();
            positionLabel.Text = positionLabel.Text + " " + (minSequenceNumber+1).ToString();
            //Вывод на экран значений для минимального номера
            VizualiseStationsInfo(minSequenceNumber);
            //Заполнение значений на выбор
            for(int i =0;i<720;i++)
            {
                comboBox1.Items.Add((i + 1).ToString());
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen blackPen = new Pen(Color.Black, 5);
            Pen whitePen = new Pen(Color.White, 5);
            PointF firstBasePoint = new PointF(0,60);
            PointF secondBasePoint = new PointF(0, 145);
            List<PointF> firstGraphPointsFilled = new List<PointF>();
            List<PointF> firstGraphPointsEmpty = new List<PointF>();
            List<PointF> secondGraphPointsFilled = new List<PointF>();
            List<PointF> secondGraphPointsEmpty = new List<PointF>();
            firstGraphPointsFilled.Add(firstBasePoint);
            secondGraphPointsFilled.Add(secondBasePoint);
            PointF temp1 = firstBasePoint;
            PointF temp2 = secondBasePoint;
            List<Label> marks = new List<Label>();
            foreach (magistralPiece p in firstMagistralOut)
            {
                if(p.from != 0 && p.to!= 0)
                {
                    temp1 = new PointF(temp1.X + p.length * 12, firstBasePoint.Y);
                    firstGraphPointsFilled.Add(temp1);
                    g.DrawLine(Pens.Red, temp1.X, temp1.Y - 50, temp1.X, temp1.Y + 50);
                    g.DrawString(p.from.ToString() + "->" + p.to.ToString(), Font,Brushes.Black,new PointF(temp1.X - 25 , temp1.Y - 25));
                    g.DrawString(p.length.ToString(), Font, Brushes.Black, new PointF(temp1.X - 25, temp1.Y + 10));
                }
                else
                {
                    firstGraphPointsEmpty.Add(temp1);
                    temp1 = new PointF(temp1.X + p.length * 12, firstBasePoint.Y);
                    firstGraphPointsEmpty.Add(temp1);
                    g.DrawString(p.length.ToString(), Font, Brushes.Green, new PointF(temp1.X - 25, temp1.Y + 10));
                }
                
            }
            foreach (magistralPiece p in secondMagistralOut)
            {
                if (p.from != 0 && p.to != 0)
                {
                    temp2 = new PointF(temp2.X + p.length * 12, secondBasePoint.Y);
                    secondGraphPointsFilled.Add(temp2);
                    g.DrawLine(Pens.Red, temp2.X, temp2.Y - 50, temp2.X, temp2.Y + 50);
                    g.DrawString(p.from.ToString() + "->" + p.to.ToString(), Font, Brushes.Black, new PointF(temp2.X - 25, temp2.Y - 25));
                    g.DrawString(p.length.ToString(), Font, Brushes.Black, new PointF(temp2.X - 25, temp2.Y + 10));
                }
                else
                {
                    secondGraphPointsEmpty.Add(temp2);
                    temp2 = new PointF(temp2.X + p.length * 12, secondBasePoint.Y);
                    secondGraphPointsEmpty.Add(temp2);
                    g.DrawString(p.length.ToString(), Font, Brushes.Green, new PointF(temp2.X - 25, temp2.Y + 10));
                }
            }
            g.DrawLines(blackPen, firstGraphPointsFilled.ToArray<PointF>());
            g.DrawLines(blackPen, secondGraphPointsFilled.ToArray<PointF>());
            if (firstGraphPointsEmpty.Count >= 2)
                g.DrawLines(whitePen, firstGraphPointsEmpty.ToArray<PointF>());
            if(secondGraphPointsEmpty.Count>=2)
                g.DrawLines(whitePen, secondGraphPointsEmpty.ToArray<PointF>());
            firstMagistralOut.Clear();
            secondMagistralOut.Clear();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawEllipse(Pens.Black, 20, 20, 30, 30);
            g.DrawEllipse(Pens.Black, 20, 80, 30, 30);
            g.DrawEllipse(Pens.Black, 70, 20, 30, 30);
            g.DrawEllipse(Pens.Black, 70, 80, 30, 30);
            g.DrawEllipse(Pens.Black, 120, 20, 30, 30);
            g.DrawEllipse(Pens.Black, 120, 80, 30, 30);
            g.DrawLine(Pens.Black, 10, 75, 150, 75);
            g.DrawLine(Pens.Black, 10, 55, 150, 55);
            g.DrawLine(Pens.Black, 85, 75, 85, 55);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            int sequenceNumber = int.Parse(comboBox1.SelectedItem.ToString());
            SetOutputData(amountOfInfo, variants[sequenceNumber - 1]);
            VizualiseStationsInfo(sequenceNumber - 1);
        }
    }
}
