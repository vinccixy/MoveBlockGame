using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MoveBlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int N = 4;
        Button[] buttons = new Button[N * N];


        private void button1_Click(object sender, EventArgs e)
        {

            //产生所有按钮
            GenerateAllButtons();

            //打乱顺序
            Shuffle();

        }

        void Shuffle()
        {
            //多次随机交换两个按钮
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(buttons.Length);
                int b = rnd.Next(buttons.Length);

                Button btna = buttons[a];
                Button btnb = buttons[b];

                Swap(btna, btnb);

            }

        }

        void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;

            for (int i = 0; i < buttons.Length; i++)
            {

                Button btn = new Button();

                int r = i / N;
                int c = i % N;

                btn.Text = (i + 1).ToString();
                btn.Top = y0 + r * d;
                btn.Left = x0 + c * d;
                btn.Width = w;
                btn.Height = w;
                btn.Visible = true;

                btn.Click += new EventHandler(btn_Click); //注册事件

                buttons[i] = btn;
                this.Controls.Add(btn);

            }

            buttons[N * N - 1].Visible = false;

        }

        void Swap(Button btna, Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;

        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            Button blank = FindHiddenButton();

            int a = GetButtonIndex(btn);
            int b = GetButtonIndex(blank);


             //判断是否与空白块相邻，如果是，则交换

           if (IsNeighbor(a, b))
            {

                Swap(btn, blank);

                blank.Focus();

            }

            //判断是否完成了
            if (ResultIsOk())
            {
                MessageBox.Show("ok");
            }

        }


        Button FindHiddenButton()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (!buttons[i].Visible)
                {
                    return buttons[i];
                }
            }
            return null;
        }


        int GetButtonIndex(Button btn) //找到一个按钮的下标
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == btn)
                {
                    return i;
                }
            }
            return -1;
        }


        bool IsNeighbor(int a, int b) //判断是否相邻
        {
            int r1 = a / N, c1 = a % N;
            int r2 = b / N, c2 = b % N;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) //左右相邻
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;
        }


 
        bool ResultIsOk()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Text != (i + 1).ToString())
                {
                    return false;
                }
            }
            return true;

        }
    }
}