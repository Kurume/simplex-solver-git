using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimplexMethodSolver
{
    public partial class Form1 : Form
    {
        public TextBox[,] vBoxes;
        public List<TextBox> gFreeX;
        public List<TextBox> gBaseX;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateTbx_Click(object sender, EventArgs e)
        {
            if (btnCreateTbx.Text == "Построить сетку")
            {
                vBoxes= createTextBoxes(Int32.Parse(tbVariables.Text), Int32.Parse(tbConstrains.Text));
                createUserEditTB(Int32.Parse(tbVariables.Text), Int32.Parse(tbConstrains.Text), ref gFreeX, ref gBaseX);
                btnCreateTbx.Text = "Рассчитать";
                return;
            }
            if (btnCreateTbx.Text == "Рассчитать")
            {
                //try
                {
                    int vBoxXFree = vBoxes.GetLength(0);
                    int vBoxXBase = vBoxes.GetLength(1);
                    float[,] vSimplexInput = new float[vBoxXFree, vBoxXBase];
                    // переводим данные из TextBoxов в кошерный массив
                    for (int i = 0; i < vBoxXFree; i++)
                        for (int q = 0; q < vBoxXBase; q++)
                        {
                            vSimplexInput[i, q] = float.Parse(vBoxes[i, q].Text);
                        }
                    string[] vFreeXNames = {"34"};
                    string[] vBaseXNames = {"34"};

                    float[] vResult = calculateSimplex(vSimplexInput, ref gFreeX, ref gBaseX);

                    //rtbResult.Text += "Xсвоб = 0 \n";
                    for (int i = gFreeX.Count -1; i > 1; i--)
                        rtbResult.Text +=  gFreeX[i].Text + "= " ;
                        rtbResult.Text += gFreeX[1].Text + " = 0 \n";

                    rtbResult.Text += "MinF = " + vResult[0].ToString() + "\n";

                    for (int i = 1; i < gBaseX.Count; i++)
                    {
                        rtbResult.Text += gBaseX[i].Text + " = " + vResult[i].ToString() + "; ";
                    }
                }
                //catch (Exception ex)
                //{
                //    // тут можно будет покрасивше сообщение сделать
                //    MessageBox.Show(ex.ToString(), "Ошибка при вычислении!");
                //}
            }
        }

        public TextBox[,] createUserEditTB(int aVariables, int aConstrains, ref List<TextBox> aFreeXNames, ref List<TextBox> aBaseXNames)
        {
            // константы для создания сетки
            const int lcLeft = 20;
            const int lcTop = 60;
            const int lcWithd = 40;
            const int lcHeight = 20;

            int nSize, mSize;
            nSize = aVariables - aConstrains + 1;
            mSize = aConstrains + 1;
            TextBox[,] vTextBoxList = new TextBox[nSize, mSize];
            List<TextBox> vFreeXNames = new List<TextBox>();
            List<TextBox> vBaseXNames = new List<TextBox>();
            // создаем сетку, для отображения
            // элементы по горизонтали
            for (int i = nSize; i >= 1; i--)
            {
                TextBox vNewTextBox = new TextBox();
                // размещаем на экране, задаем параметр Text
                vNewTextBox.Top = lcTop;
                vNewTextBox.Left = lcLeft + i * lcWithd;
                vNewTextBox.Width = lcWithd;
                vNewTextBox.Height = lcHeight;
                // заполняем названиями
                if ((i == nSize))
                    vNewTextBox.Text = "b";
                else vNewTextBox.Text = "Xсв" + i.ToString();
                vNewTextBox.Enabled = true;

                this.Controls.Add(vNewTextBox);
                vFreeXNames.Add(vNewTextBox);
            }
            // элементы по вертикали
            for (int q = 0; q < mSize; q++)
            {
                TextBox vNewTextBox = new TextBox();
                // размещаем на экране, задаем параметр Text
                vNewTextBox.Top = lcTop + q * lcHeight + lcHeight;
                vNewTextBox.Left = lcLeft;
                vNewTextBox.Width = lcWithd;
                vNewTextBox.Height = lcHeight;
                // заполняем названиями
                vNewTextBox.Enabled = true;
                if (q == 0)
                {
                    vNewTextBox.Enabled = false;
                    vNewTextBox.Text = "F";
                }
                else vNewTextBox.Text = "Xбаз" + q.ToString();

                this.Controls.Add(vNewTextBox);
                vBaseXNames.Add(vNewTextBox);
            }
            aBaseXNames = vBaseXNames;
            aFreeXNames = vFreeXNames;
            return vTextBoxList;
        }

        public TextBox[,] createTextBoxes(int aVariables, int aConstrains)
        {
            // константы для создания сетки
            const int lcLeft = 20;
            const int lcTop = 60;
            const int lcWithd = 40;
            const int lcHeight = 20;

            int nSize, mSize;
            nSize = aVariables - aConstrains + 1;
            mSize = aConstrains + 1;
            TextBox[,] vTextBoxList = new TextBox[nSize, mSize];

            //создаем сетку, редактируемую пользователем
            for (int i = 0; i < nSize; i++)
                for (int q = 0; q < mSize; q++)
                {
                    TextBox vNewTextBox = new TextBox();
                    vNewTextBox.Top = lcTop + lcHeight*q + lcHeight;
                    vNewTextBox.Left = lcLeft + lcWithd*i + lcWithd;
                    vNewTextBox.Width = lcWithd;
                    vNewTextBox.Height = lcHeight;
                    this.Controls.Add(vNewTextBox);
                    vTextBoxList[i, q] = vNewTextBox;
                }
            // возвращаем элементы управления, что бы можно было потом к ним обратиться
            return vTextBoxList;
        }

        public float[] calculateSimplex(float[,] aSimplexInput, ref List<TextBox> aFreeXNames, ref List<TextBox> aBaseXNames)
        {
            int vSimplexDimxFree = aSimplexInput.GetLength(0);
            int vSimplexDimxBase = aSimplexInput.GetLength(1);

            bool vOptimalSolve = true;

            float[,] vTempTable;
            float[] vResult = new float[vSimplexDimxBase];
            vTempTable = aSimplexInput;
            //vResult = new float[vSimplexDimxFree, 3];
            // общий цикл - цикл действия программы, что бы долго не ждать и не циклить ограничим 100
            for (int vMaxCount = 0; vMaxCount < 100; vMaxCount++)
            {
                for (int i = 0; i < vSimplexDimxFree; i++)
                {

                    if (vTempTable[i, 0] >= 0)
                    {
                        vOptimalSolve = true;
                    }
                    else
                    // нашли отрицательное число, надо преобразовать таблицу
                    {
                        int vMaxNegativeElem = 0;
                        float vMaxNegativeValue = 0;
                        // инициализируем первоначальную переменную
                        for( int i_min  = 0; i_min < vSimplexDimxFree - 1; i_min++)
                            if (vTempTable[i_min, 0] < vMaxNegativeValue)
                            {
                                vMaxNegativeElem = i_min;
                                vMaxNegativeValue = vTempTable[i_min, 0];
                            }
                        // находим в первой строке (F) мах по модулу отрицательный 
                        for (int iMax = 0; iMax < vSimplexDimxFree - 1; iMax++)
                            if(vTempTable[iMax, 0] < 0) 
                                if (Math.Abs(vTempTable[iMax, 0]) > Math.Abs(vTempTable[vMaxNegativeElem, 0]))
                                    vMaxNegativeElem = iMax;
                        // рассматриваем найденный столбец, находим все элты aij > 0
                        int vMinij = 0;
                        // инициализируем первоначальное значение
                        for (int i_min = 0; i_min < vSimplexDimxBase; i_min++)
                            if (vTempTable[vMaxNegativeElem, i_min] > 0)
                                vMinij = i_min;                       

                        for (int j = 0; j < vSimplexDimxBase; j++)
                        {
                            if (vTempTable[vMaxNegativeElem, j] > 0)
                            // во всех i-их строках, в которых aij > 0 считаем bij/aij
                            // находим минимальное из них
                            {
                                if (vTempTable[vSimplexDimxFree - 1, vMinij] / vTempTable[vMaxNegativeElem, vMinij] > vTempTable[vSimplexDimxFree - 1, j] / vTempTable[vMaxNegativeElem, j])
                                {
                                    vMinij = j; // минимальный элемент
                                }
                            }
                        }

                        // ведущий столбец = vMaxNegativeElem, ведущая строка = vMinij
                        // перезаполняем таблицу в соответствии с правилами

                        // меняем местами базисную и свободную переменные
                        string aTempName = aBaseXNames[vMinij].Text;
                        aBaseXNames[vMinij].Text = aFreeXNames[vMaxNegativeElem].Text;
                        aFreeXNames[vMaxNegativeElem].Text = aTempName;
                        // заменяем ведущий элемент на обратную величину
                        vTempTable[vMaxNegativeElem, vMinij] = 1 / vTempTable[vMaxNegativeElem, vMinij];

                        // оставшиеся элементы симплекс таблицы преобразуются по хитрой формуле..
                        // т.е. нам надо исключить все элты, у котрых столбец или строка равны им же у ведущего элта
                        for (int m = 0; m < vSimplexDimxFree; m++)
                            for (int n = 0; n < vSimplexDimxBase; n++)
                            {
                                if (!(n == vMinij))
                                    if (!(m == vMaxNegativeElem))
                                        if (!((m == vSimplexDimxFree - 1) && (n == 0)))
                                        {
                                            vTempTable[m, n] = vTempTable[m, n] - ((vTempTable[vMaxNegativeElem, n] * vTempTable[m, vMinij]) * vTempTable[vMaxNegativeElem, vMinij]);
                                        }
                            }

                        // b0 преобразуем по хитрому алгоритму
                        vTempTable[vSimplexDimxFree - 1, 0] = vTempTable[vSimplexDimxFree - 1, 0] + (vTempTable[vMaxNegativeElem, 0] * vTempTable[vSimplexDimxFree - 1, vMinij]) * vTempTable[vMaxNegativeElem, vMinij];

                        // все элементы ведущего столбца умножаются на (-1/aij), кроме ведушего элта
                        for (int l = 0; l < vSimplexDimxBase; l++)
                        {
                            if (!(l == vMinij))
                                vTempTable[vMaxNegativeElem, l] = (-1) * vTempTable[vMaxNegativeElem, l] * vTempTable[vMaxNegativeElem, vMinij];
                        }
                        // все элементы ведущей строки умножаются на 1/aij, кроме ведущего элта
                        for (int l = 0; l < vSimplexDimxFree; l++)
                        {
                            if (!(l == vMaxNegativeElem))
                                vTempTable[l, vMinij] = vTempTable[l, vMinij] * vTempTable[vMaxNegativeElem, vMinij];
                        }

                        // прерываем цикл, ибо все уже, один элемент нашли, один раз таблицу переделали
                        vOptimalSolve = false;
                        break;
                    }

                }
                 
                if(vOptimalSolve)
                    {
                        // нашли оптимальное решение, возвращаем его..
                        for (int q = 0; q < vSimplexDimxBase; q++)
                        {
                            vResult[q] = vTempTable[vSimplexDimxFree - 1, q];
                        }
                        return vResult;
                    }
               

            }
           return null;
        }
    }
}
