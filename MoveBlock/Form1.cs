using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MoveBlock {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        const  int  N= 4;//按钮的行数和列数
        Button[,] buttons = new Button[N,N];//按钮的数组-二维

        private void Form1_Load(object sender, EventArgs e) {
            //不采用一个个来手动添加按钮，而采用编程自动添加排块按钮
            //产生按钮
            GenerateAllButton();
        }

        private void start_Bun_Click(object sender, EventArgs e) {
            //打乱顺序
            Shuffle();
        }
        //打乱顺序
        void Shuffle() {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++) {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                //交换函数
                Swap(buttons[a, b], buttons[c,d]);
            }
        }
        //产生所有按钮
        void GenerateAllButton() {
            int y0=10,x0=100,d = 50,w = 45;
            for (int r = 0; r < N; r++) {//row-行
                for(int c = 0; c < N; c++) {//column-列
                    //产生按钮
                    int num = r * N + c;
                    Button button = new Button();
                    button.Text = (num+1).ToString();
                    button.Top = y0 + r * d;
                    button.Left = x0 + c*d;
                    button.Width= w;
                    button.Height = w;
                    button.Visible= true;
                    button.Tag = r * N + c;//标识控件所在行列位置
                    //可以使这个按钮能够点击+=事件
                    //注册事件
                    button.Click += button_Click;
                    buttons[r,c]=button;//放到数组中
                    this.Controls.Add(button);//添加到界面
                }
            }
            buttons[N-1,N-1].Visible= false;//最后一个隐藏显示
        }

        void Swap(Button btna,Button btnb) {
            //文本交换
            string t = btna.Text;
            btna.Text=btnb.Text;
            btnb.Text=t;
            //显示交换
            bool v = btna.Visible;
            btna.Visible=btnb.Visible;
            btnb.Visible=v;

        }
        private void button_Click(object sender, EventArgs e) {
            //sender-事件发出者
            Button button= sender as Button;
            Button blank = FindHiddenButton();//空白按钮
            //判断是否与空白按钮相邻，如果是-则交换
            if (isNeighbor(button, blank)) {
                Swap(button, blank);//交换
                blank.Focus();//设置焦点-没什么用
            }
            //throw new NotImplementedException();
            if (ResultIsOk()) {
                MessageBox.Show("哇，真是个厉害鬼！！！");
            }
        }
        //查找隐藏按钮
        Button FindHiddenButton() {
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < N; j++) {
                    if (!buttons[i,j].Visible) {
                        return buttons[i, j];
                    }
                }
            }
            return null;
        }
       bool isNeighbor(Button btnA, Button btnB) {
            int a = (int)btnA.Tag;//tag中隐藏着行列数
            int b = (int)btnB.Tag;
            int r1 = a / N;int c1 = a % N; //整除行号，余数列号
            int r2 = b / N; int c2 = b % N;
            //左右相邻，上下相邻
            if ((r1 == r2 && ((c1 == c2 + 1) || (c1 == c2 - 1)))
                ||(c1 == c2 && ((r1 == r2 + 1) || (r1 == r2 - 1)))) {
                return true;
            }
            return false;
        }
        //检查是否完成
        bool ResultIsOk() {
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < N; j++) {
                    if (buttons[i,j].Text!=(i*N+j+1).ToString()) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
