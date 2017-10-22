using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_visualization
{
    public partial class Form1 : Form
    {
        public class Фигура
        {
            public string тип;
            public int x;
            public int y;

            public Фигура() { }
            public Фигура(string _тип, int _x, int _y)
            {
                тип = _тип;
                x = _x;
                y = _y;
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        #region глобальные переменные
        static int ход = 0;
        static bool ходБелых = true;
        static bool ходЧерных = true;
        static Image доска;
        static Image белаяПешка;
        static Image белаяТура;
        static Image белыйКонь;
        static Image белыйСлон;
        static Image белыйКороль;
        static Image белыйФерзь;
        static Image чернаяПешка;
        static Image чернаяТура;
        static Image черныйКонь;
        static Image черныйСлон;
        static Image черныйКороль;
        static Image черныйФерзь;
        static List<Фигура> БелыеФигуры;
        static List<Фигура> ЧерныеФигуры;
        static string[] СписокХодов;
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            доска = new Bitmap("images/доска.png");
            //СписокХодов = new string[]
            белаяПешка = new Bitmap("images/пешка1.png");
            белаяТура = new Bitmap("images/тура1.png");
            белыйКонь = new Bitmap("images/конь1.png");
            белыйСлон = new Bitmap("images/слон1.png");
            белыйФерзь = new Bitmap("images/ферзь1.png");
            белыйКороль = new Bitmap("images/король1.png");
            чернаяПешка = new Bitmap("images/пешка.png");
            чернаяТура = new Bitmap("images/тура.png");
            черныйКонь = new Bitmap("images/конь.png");
            черныйСлон = new Bitmap("images/слон.png");
            черныйФерзь = new Bitmap("images/ферзь.png");
            черныйКороль = new Bitmap("images/король.png");
            БелыеФигуры = new List<Фигура>();
            БелыеФигуры.Add(new Фигура("пешка", 1, 2));
            БелыеФигуры.Add(new Фигура("пешка", 2, 2));
            БелыеФигуры.Add(new Фигура("пешка", 3, 2));
            БелыеФигуры.Add(new Фигура("пешка", 4, 2));
            БелыеФигуры.Add(new Фигура("пешка", 5, 2));
            БелыеФигуры.Add(new Фигура("пешка", 6, 2));
            БелыеФигуры.Add(new Фигура("пешка", 7, 2));
            БелыеФигуры.Add(new Фигура("пешка", 8, 2));
            БелыеФигуры.Add(new Фигура("тура", 1, 1));
            БелыеФигуры.Add(new Фигура("конь", 2, 1));
            БелыеФигуры.Add(new Фигура("слон", 3, 1));
            БелыеФигуры.Add(new Фигура("король", 5, 1));
            БелыеФигуры.Add(new Фигура("ферзь", 4, 1));
            БелыеФигуры.Add(new Фигура("тура", 8, 1));
            БелыеФигуры.Add(new Фигура("конь", 7, 1));
            БелыеФигуры.Add(new Фигура("слон", 6, 1));
            ЧерныеФигуры = new List<Фигура>();
            ЧерныеФигуры.Add(new Фигура("пешка", 1, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 2, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 3, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 4, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 5, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 6, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 7, 7));
            ЧерныеФигуры.Add(new Фигура("пешка", 8, 7));
            ЧерныеФигуры.Add(new Фигура("тура", 1, 8));
            ЧерныеФигуры.Add(new Фигура("конь", 2, 8));
            ЧерныеФигуры.Add(new Фигура("слон", 3, 8));
            ЧерныеФигуры.Add(new Фигура("король", 5, 8));
            ЧерныеФигуры.Add(new Фигура("ферзь", 4, 8));
            ЧерныеФигуры.Add(new Фигура("тура", 8, 8));
            ЧерныеФигуры.Add(new Фигура("конь", 7, 8));
            ЧерныеФигуры.Add(new Фигура("слон", 6, 8));
        }

        int toScreenX(int x)
        {
            if (x == 1) { return 275; }
            if (x == 2) { return 318; }
            if (x == 3) { return 365; }
            if (x == 4) { return 409; }
            if (x == 5) { return 453; }
            if (x == 6) { return 497; }
            if (x == 7) { return 540; }
            if (x == 8) { return 587; }
            return 0;
        }
        int toScreenY(int y)
        {
            if (y == 1) { return 365; }
            if (y == 2) { return 320; }
            if (y == 3) { return 275; }
            if (y == 4) { return 230; }
            if (y == 5) { return 185; }
            if (y == 6) { return 140; }
            if (y == 7) { return 100; }
            if (y == 8) { return 54; }
            return 0;
        }
        int toDigit(char c)
        {
            if (c == 'a') { return 1; }
            if (c == 'b') { return 2; }
            if (c == 'c') { return 3; }
            if (c == 'd') { return 4; }
            if (c == 'e') { return 5; }
            if (c == 'f') { return 6; }
            if (c == 'g') { return 7; }
            if (c == 'h') { return 8; }
            return 0;
        }
        bool свободно(int x1, int y1, int x2, int y2)
        {
            #region для туры
            if (x1 == x2)  // Ra5 -> a1
            {
                foreach (Фигура ф in БелыеФигуры)
                {
                    if (y1 < y2)
                    {
                        if (ф.x == x1 && ф.y > y1 && ф.y < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ф.x == x1 && ф.y < y1 && ф.y > y2)
                        {
                            return false;
                        }
                    }
                }
                foreach (Фигура ф in ЧерныеФигуры)
                {
                    if (y1 < y2)
                    {
                        if (ф.x == x1 && ф.y > y1 && ф.y < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ф.x == x1 && ф.y < y1 && ф.y > y2)
                        {
                            return false;
                        }
                    }

                }
                return true;

            }
            if (y1 == y2) //c1 e1
            {
                foreach (Фигура ф in БелыеФигуры)
                {
                    if (x1 < x2)
                    {
                        if (ф.y == y1 && ф.x > x1 && ф.x < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ф.y == y1 && ф.x < x1 && ф.x > y2)
                        {
                            return false;
                        }
                    }
                }
                foreach (Фигура ф in ЧерныеФигуры)
                {
                    if (x1 < x2)
                    {
                        if (ф.y == y1 && ф.x > x1 && ф.x < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ф.y == y1 && ф.x < x1 && ф.x > y2)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            #endregion
            #region для слона
            if (x1 != x2 && y1 != y2) // Bf1 -> c4 (e2, d3)
            {
                List<Point> ТочкиМеждуКлетками = new List<Point>();
                int x = x1;
                int y = y1;
                while (x != x2)
                {
                    if (x2 > x1) { x++; }
                    else { x--; }
                    if (y2 > y1) { y++; }
                    else { y--; }
                    ТочкиМеждуКлетками.Add(new Point(x, y));
                }

                foreach (Фигура ф in БелыеФигуры)
                {
                    if (ТочкиМеждуКлетками.Contains(new Point(ф.x, ф.y)))
                    {
                        return false;
                    }
                }
                return true;
            }
            #endregion
            return true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            #region рисуем доску
            g.DrawImage(доска, 250, 30, 400, 400);
            #endregion
            #region рисуем фигуры
            foreach (Фигура ф in БелыеФигуры)
            {
                if (ф.тип == "пешка")
                {
                    g.DrawImage(белаяПешка, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "тура")
                {
                    g.DrawImage(белаяТура, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "конь")
                {
                    g.DrawImage(белыйКонь, toScreenX(ф.x), toScreenY(ф.y)-3);
                }
                if (ф.тип == "слон")
                {
                    g.DrawImage(белыйСлон, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "король")
                {
                    g.DrawImage(белыйКороль, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "ферзь")
                {
                    g.DrawImage(белыйФерзь, toScreenX(ф.x), toScreenY(ф.y));
                }
                
            }
            foreach (Фигура ф in ЧерныеФигуры)
            {
                if (ф.тип == "пешка")
                {
                    g.DrawImage(чернаяПешка, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "тура")
                {
                    g.DrawImage(чернаяТура, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "слон")
                {
                    g.DrawImage(черныйСлон, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "конь")
                {
                    g.DrawImage(черныйКонь, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "король")
                {
                    g.DrawImage(черныйКороль, toScreenX(ф.x), toScreenY(ф.y));
                }
                if (ф.тип == "ферзь")
                {
                    g.DrawImage(черныйФерзь, toScreenX(ф.x), toScreenY(ф.y));
                }
            }
            #endregion
        }

        private void pictureBox1_Click(object sender, EventArgs e) // open
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                СписокХодов = File.ReadAllLines(filename);
            }
            foreach (string p in СписокХодов)
            {
                richTextBox1.Text += p + "\n";
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e) // ход вперед
        {
            
            if (ходБелых)
            {
                ход++;
                // извлекаем из строки ход
                //1.e4 e5 -> e4
                string move = СписокХодов[ход - 1].Substring(СписокХодов[ход - 1].IndexOf('.') + 1, СписокХодов[ход - 1].IndexOf(' ') - СписокХодов[ход - 1].IndexOf('.') - 1);
                // рашрифровуем ход и ходим (меняем в массиве фигур положение)
                // N = knight
                // B = bishop
                // R = rock
                // Q = queen
                // K = king
                // _ = pawn
                label1.Text = СписокХодов[ход - 1];
                
                bool взятиеФигуры = false;
                if (move[1] == 'x')
                {
                    взятиеФигуры = true;
                    move = move.Remove(1, 1);
                }

                if (move[0] == 'N') // Nf3 --- Nef3 N1g5 ---
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (
                               ф.тип == "конь"
                               &&
                               (
                                    (Math.Abs(ф.x - toDigit(move[1])) == 1 && Math.Abs(ф.y - (move[2] - 48)) == 2)
                                    ||
                                    (Math.Abs(ф.x - toDigit(move[1])) == 2 && Math.Abs(ф.y - (move[2] - 48)) == 1)
                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'R') // Rf3 --- Ref3 R1g5 ---
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (
                               ф.тип == "тура"
                               &&
                               (
                                    (ф.x == toDigit(move[1])  && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48))
                                    ||
                                    (ф.y == move[2]  && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48))

                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'B') // Bf3 --- Bef4 B1g5 ---
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (
                               ф.тип == "слон"
                               &&
                               (
                                    (Math.Abs(ф.x - toDigit(move[1])) == Math.Abs(ф.y - (move[2] - 48))) && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48)

                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'Q') // Qf3 
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (
                               ф.тип == "ферзь"
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'K') // Kf3 
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (
                               ф.тип == "король"
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                
                if (move[0] == 'a' || move[0] == 'b' || move[0] == 'c' || move[0] == 'd' || move[0] == 'e' || move[0] == 'f' || move[0] == 'g' || move[0] == 'h') //e2 -> e4
                {

                    if (взятиеФигуры == false) // e4
                    {
                        bool s = false;
                        foreach (Фигура ф in БелыеФигуры)
                        {
                            if (
                                    ф.тип == "пешка"
                                    && ф.x == toDigit(move[0])
                                    && ф.y == (move[1] - 48) - 1
                               )
                            {
                                ф.x = toDigit(move[0]);
                                ф.y = move[1] - 48;
                                s = true;
                                break;
                            }
                        }
                        if (s == false)
                        {
                            foreach (Фигура ф in БелыеФигуры)
                            {
                                if (
                                        ф.тип == "пешка"
                                        && ф.x == toDigit(move[0])
                                        && ф.y == (move[1] - 48) - 2
                                   )
                                {
                                    ф.x = toDigit(move[0]);
                                    ф.y = move[1] - 48;
                                    break;
                                }
                            }
                        }
                    }
                    if (взятиеФигуры == true) // de4
                    {
                        foreach (Фигура ф in БелыеФигуры)
                        {
                            if (
                                    ф.тип == "пешка" 
                                    &&  ф.x == toDigit(move[0]) 
                                    && ф.y == (move[2] - 48) - 1
                                )
                            {
                                ф.x = toDigit(move[1]);
                                ф.y = move[2] - 48;
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in ЧерныеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                ЧерныеФигуры.Remove(фигураНаУдаление);
                                break;
                            }
                        }
                    }
                }
                if (move == "O-O")
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (ф.тип == "король")
                        {
                            ф.x = 7;
                            ф.y = 1;
                            break;
                        }
                    }
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (ф.тип == "тура" && ф.x == 8)
                        {
                            ф.x = 6;
                            ф.y = 1;
                        }
                    }
                    
                }
                if (move == "O-O-O")
                {
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (ф.тип == "король")
                        {
                            ф.x = 3;
                            ф.y = 1;
                            break;
                        }
                    }
                    foreach (Фигура ф in БелыеФигуры)
                    {
                        if (ф.тип == "тура" && ф.x == 1)
                        {
                            ф.x = 4;
                            ф.y = 1;
                        }
                    }

                }

                // вызываем перерисовку
                Invalidate();

                // передаем ход противоположной стороне
                ходБелых = false;
            }
        
        else  
            {
               
                // извлекаем из строки ход
                //1.e4 e5 -> e4
                string move = СписокХодов[ход - 1].Substring(СписокХодов[ход - 1].IndexOf(' ') + 1);
                // рашрифровуем ход и ходим (меняем в массиве фигур положение)
                // N = knight
                // B = bishop
                // R = rock
                // Q = queen
                // K = king
                // _ = pawn
                label1.Text = СписокХодов[ход - 1];
                bool взятиеФигуры = false;
                if (move[1] == 'x')
                {
                    взятиеФигуры = true;
                    move = move.Remove(1, 1);
                }
                if (move[0] == 'N') // Nf3 --- Nef3 N1g5 ---
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                               ф.тип == "конь"
                               &&
                               (
                                    (Math.Abs(ф.x - toDigit(move[1])) == 1 && Math.Abs(ф.y - (move[2] - 48)) == 2)
                                    ||
                                    (Math.Abs(ф.x - toDigit(move[1])) == 2 && Math.Abs(ф.y - (move[2] - 48)) == 1)
                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'R') // Rf3 --- Ref3 R1g5 ---
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                               ф.тип == "тура"
                               &&
                               (
                                    (ф.x == toDigit(move[1])  && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48))
                                    ||
                                    (ф.y == move[2]  && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48))

                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'B') // Bf3 --- Bef4 B1g5 ---
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                               ф.тип == "слон"
                               &&
                               (
                                    (Math.Abs(ф.x - toDigit(move[1])) == Math.Abs(ф.y - (move[2] - 48))) && свободно(ф.x, ф.y, toDigit(move[1]), move[2] - 48)

                               )
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'Q') // Qf3 
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                               ф.тип == "ферзь"
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'K') // Kf3 
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                               ф.тип == "король"
                           )
                        {
                            ф.x = toDigit(move[1]);
                            ф.y = move[2] - 48;
                            if (взятиеФигуры == true)
                            {
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                
                //to do 2 пешки на 1-й вертикале 
                if (move[0] == 'a' || move[0] == 'b' || move[0] == 'c' || move[0] == 'd' || move[0] == 'e' || move[0] == 'f' || move[0] == 'g' || move[0] == 'h') //e2 -> e4
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (
                                ф.тип == "пешка" 
                                &&  ф.x == toDigit(move[0])
                           )
                        {
                            
                            ф.x = toDigit(move[0]);
                            ф.y = move[1] - 48;
                            if (взятиеФигуры == false) // e4
                            {
                                ф.x = toDigit(move[0]);
                                ф.y = move[1] - 48;
                            }
                            if (взятиеФигуры == true) // de4
                            {
                                ф.x = toDigit(move[1]);
                                ф.y = move[2] - 48;
                                Фигура фигураНаУдаление = new Фигура();
                                foreach (Фигура ф1 in БелыеФигуры)
                                {
                                    if (ф1.x == ф.x && ф1.y == ф.y)
                                    {
                                        фигураНаУдаление = ф1;
                                        break;
                                    }
                                }
                                БелыеФигуры.Remove(фигураНаУдаление);
                            }
                            break;
                        }

                    }
                }
                if (move.Length >= 5 && move.Substring(0, 5) == "O-O-O")
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (ф.тип == "король")
                        {
                            ф.x = 3;
                            ф.y = 8;
                            break;
                        }
                    }
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (ф.тип == "тура" && ф.x == 1)
                        {
                            ф.x = 4;
                            ф.y = 8;
                        }
                    }

                }
                else if (move.Length >= 3 && move.Substring(0, 3) == "O-O")
                {
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (ф.тип == "король")
                        {
                            ф.x = 7;
                            ф.y = 8;
                            break;
                        }
                    }
                    foreach (Фигура ф in ЧерныеФигуры)
                    {
                        if (ф.тип == "тура" && ф.x == 8)
                        {
                            ф.x = 6;
                            ф.y = 8;
                        }
                    }

                }


                // вызываем перерисовку
                Invalidate();
                // передаем ход противоположной стороне
                ходБелых = true;
            }
        }
    }
}
