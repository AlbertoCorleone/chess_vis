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
        public class Figure
        {
            public string type;
            public int x;
            public int y;

            public Figure() { }
            public Figure(string _type, int _x, int _y)
            {
                type = _type;
                x = _x;
                y = _y;
            }
        }
        
        public Form1()
        {
            InitializeComponent();
        }
        #region глобальные переменные
        static int step = 0;
        static bool stepWhite = true;
        static bool stepBlack = true;
        static Image dock;
        static Image whitePawn;
        static Image whiteTour;
        static Image whiteHourse;
        static Image whiteBishop;
        static Image whiteQueen;
        static Image whiteKing;
        static Image blackPawn;
        static Image blackTour;
        static Image blackHourse;
        static Image blackBishop;
        static Image blackKing;
        static Image blackQueen;
        static List<Figure> WhiteFigures;
        static List<Figure> BlackFigures;
        static string[] ListSteps;
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            dock = new Bitmap("images/доска.png");
            //СписокХодов = new string[]
            whitePawn = new Bitmap("images/пешка1.png");
            whiteTour = new Bitmap("images/тура1.png");
            whiteHourse = new Bitmap("images/конь1.png");
            whiteBishop = new Bitmap("images/слон1.png");
            whiteQueen = new Bitmap("images/ферзь1.png");
            whiteKing = new Bitmap("images/король1.png");
            blackPawn = new Bitmap("images/пешка.png");
            blackTour = new Bitmap("images/тура.png");
            blackHourse = new Bitmap("images/конь.png");
            blackBishop = new Bitmap("images/слон.png");
            blackQueen = new Bitmap("images/ферзь.png");
            blackKing = new Bitmap("images/король.png");
            WhiteFigures = new List<Figure>();
            WhiteFigures.Add(new Figure("пешка", 1, 2));
            WhiteFigures.Add(new Figure("пешка", 2, 2));
            WhiteFigures.Add(new Figure("пешка", 3, 2));
            WhiteFigures.Add(new Figure("пешка", 4, 2));
            WhiteFigures.Add(new Figure("пешка", 5, 2));
            WhiteFigures.Add(new Figure("пешка", 6, 2));
            WhiteFigures.Add(new Figure("пешка", 7, 2));
            WhiteFigures.Add(new Figure("пешка", 8, 2));
            WhiteFigures.Add(new Figure("тура", 1, 1));
            WhiteFigures.Add(new Figure("конь", 2, 1));
            WhiteFigures.Add(new Figure("слон", 3, 1));
            WhiteFigures.Add(new Figure("король", 5, 1));
            WhiteFigures.Add(new Figure("ферзь", 4, 1));
            WhiteFigures.Add(new Figure("тура", 8, 1));
            WhiteFigures.Add(new Figure("конь", 7, 1));
            WhiteFigures.Add(new Figure("слон", 6, 1));
            blackFigures = new List<Figure>();
            blackFigures.Add(new Figure("пешка", 1, 7));
            blackFigures.Add(new Figure("пешка", 2, 7));
            blackFigures.Add(new Figure("пешка", 3, 7));
            blackFigures.Add(new Figure("пешка", 4, 7));
            blackFigures.Add(new Figure("пешка", 5, 7));
            blackFigures.Add(new Figure("пешка", 6, 7));
            blackFigures.Add(new Figure("пешка", 7, 7));
            blackFigures.Add(new Figure("пешка", 8, 7));
            blackFigures.Add(new Figure("тура", 1, 8));
            blackFigures.Add(new Figure("конь", 2, 8));
            blackFigures.Add(new Figure("слон", 3, 8));
            blackFigures.Add(new Figure("король", 5, 8));
            blackFigures.Add(new Figure("ферзь", 4, 8));
            blackFigures.Add(new Figure("тура", 8, 8));
            blackFigures.Add(new Figure("конь", 7, 8));
            blackFigures.Add(new Figure("слон", 6, 8));
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
        bool free(int x1, int y1, int x2, int y2)
        {
            #region для туры
            if (x1 == x2)  // Ra5 -> a1
            {
                foreach (Figure f in WhiteFigures)
                {
                    if (y1 < y2)
                    {
                        if (f.x == x1 && f.y > y1 && f.y < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (f.x == x1 && f.y < y1 && f.y > y2)
                        {
                            return false;
                        }
                    }
                }
                foreach (Figure f in BlackFigures)
                {
                    if (y1 < y2)
                    {
                        if (f.x == x1 && f.y > y1 && f.y < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (f.x == x1 && f.y < y1 && f.y > y2)
                        {
                            return false;
                        }
                    }

                }
                return true;

            }
            if (y1 == y2) //c1 e1
            {
                foreach (Figure f in WhiteFigures)
                {
                    if (x1 < x2)
                    {
                        if (f.y == y1 && f.x > x1 && f.x < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (f.y == y1 && f.x < x1 && f.x > y2)
                        {
                            return false;
                        }
                    }
                }
                foreach (Figure f in BlackFigures)
                {
                    if (x1 < x2)
                    {
                        if (f.y == y1 && f.x > x1 && f.x < y2)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (f.y == y1 && f.x < x1 && f.x > y2)
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
                List<Point> PointsBetweenCells = new List<Point>();
                int x = x1;
                int y = y1;
                while (x != x2)
                {
                    if (x2 > x1) { x++; }
                    else { x--; }
                    if (y2 > y1) { y++; }
                    else { y--; }
                    PointsBetweenCells.Add(new Point(x, y));
                }

                foreach (Figure f in WhiteFigures)
                {
                    if (PointsBetweenCells.Contains(new Point(f.x, f.y)))
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
            g.DrawImage(dock, 250, 30, 400, 400);
            #endregion
            #region рисуем фигуры
            foreach (Figure f in WhiteFigures)
            {
                if (f.type == "пешка")
                {
                    g.DrawImage(whitePawn, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "тура")
                {
                    g.DrawImage(whiteTour, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "конь")
                {
                    g.DrawImage(whiteHorse, toScreenX(f.x), toScreenY(f.y)-3);
                }
                if (f.type == "слон")
                {
                    g.DrawImage(whiteBishop, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "король")
                {
                    g.DrawImage(wgiteQueen, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "ферзь")
                {
                    g.DrawImage(whiteKing, toScreenX(f.x), toScreenY(f.y));
                }
                
            }
            foreach (Figure f in BlackFigures)
            {
                if (f.type == "пешка")
                {
                    g.DrawImage(blacPawn, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "тура")
                {
                    g.DrawImage(blackTour, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "слон")
                {
                    g.DrawImage(blackBishop, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "конь")
                {
                    g.DrawImage(blackHorse, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "король")
                {
                    g.DrawImage(blackQueen, toScreenX(f.x), toScreenY(f.y));
                }
                if (f.type == "ферзь")
                {
                    g.DrawImage(blackKing, toScreenX(f.x), toScreenY(f.y));
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
                ListSteps = File.ReadAllLines(filename);
            }
            foreach (string p in ListSteps)
            {
                richTextBox1.Text += p + "\n";
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e) // ход вперед
        {
            
            if (stepWhite)
            {
                step++;
                // извлекаем из строки ход
                //1.e4 e5 -> e4
                string move = StepsList[step - 1].Substring(StepsList[step - 1].IndexOf('.') + 1, StepsList[step - 1].IndexOf(' ') - StepsList[step - 1].IndexOf('.') - 1);
                // рашрифровуем ход и ходим (меняем в массиве фигур положение)
                // N = knight
                // B = bishop
                // R = rock
                // Q = queen
                // K = king
                // _ = pawn
                label1.Text = StepsList[step - 1];
                
                bool takeFigure = false;
                if (move[1] == 'x')
                {
                    takeFigure = true;
                    move = move.Remove(1, 1);
                }

                if (move[0] == 'N') // Nf3 --- Nef3 N1g5 ---
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (
                               f.type == "конь"
                               &&
                               (
                                    (Math.Abs(f.x - toDigit(move[1])) == 1 && Math.Abs(f.y - (move[2] - 48)) == 2)
                                    ||
                                    (Math.Abs(f.x - toDigit(move[1])) == 2 && Math.Abs(f.y - (move[2] - 48)) == 1)
                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'R') // Rf3 --- Ref3 R1g5 ---
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (
                               f.type == "тура"
                               &&
                               (
                                    (f.x == toDigit(move[1])  && free(f.x, f.y, toDigit(move[1]), move[2] - 48))
                                    ||
                                    (f.y == move[2]  && free(f.x, f.y, toDigit(move[1]), move[2] - 48))

                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'B') // Bf3 --- Bef4 B1g5 ---
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (
                               f.type == "слон"
                               &&
                               (
                                    (Math.Abs(f.x - toDigit(move[1])) == Math.Abs(f.y - (move[2] - 48))) && free(f.x, f.y, toDigit(move[1]), move[2] - 48)

                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'Q') // Qf3 
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (
                               f.type == "ферзь"
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'K') // Kf3 
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (
                               f.type == "король"
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                
                if (move[0] == 'a' || move[0] == 'b' || move[0] == 'c' || move[0] == 'd' || move[0] == 'e' || move[0] == 'f' || move[0] == 'g' || move[0] == 'h') //e2 -> e4
                {

                    if (takeFigure == false) // e4
                    {
                        bool s = false;
                        foreach (Figure f in WhiteFigures)
                        {
                            if (
                                    f.type == "пешка"
                                    && f.x == toDigit(move[0])
                                    && f.y == (move[1] - 48) - 1
                               )
                            {
                                f.x = toDigit(move[0]);
                                f.y = move[1] - 48;
                                s = true;
                                break;
                            }
                        }
                        if (s == false)
                        {
                            foreach (Figure f in WhiteFigures)
                            {
                                if (
                                        f.type == "пешка"
                                        && f.x == toDigit(move[0])
                                        && f.y == (move[1] - 48) - 2
                                   )
                                {
                                    f.x = toDigit(move[0]);
                                    f.y = move[1] - 48;
                                    break;
                                }
                            }
                        }
                    }
                    if (takeFigure == true) // de4
                    {
                        foreach (Figure f in WhiteFigures)
                        {
                            if (
                                    f.type == "пешка" 
                                    &&  f.x == toDigit(move[0]) 
                                    && f.y == (move[2] - 48) - 1
                                )
                            {
                                f.x = toDigit(move[1]);
                                f.y = move[2] - 48;
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in BlackFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                BlackFigures.Remove(figureForRemove);
                                break;
                            }
                        }
                    }
                }
                if (move == "O-O")
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (f.type == "король")
                        {
                            f.x = 7;
                            f.y = 1;
                            break;
                        }
                    }
                    foreach (Figure f in WhiteFigures)
                    {
                        if (f.type == "тура" && f.type == 8)
                        {
                            f.x = 6;
                            f.y = 1;
                        }
                    }
                    
                }
                if (move == "O-O-O")
                {
                    foreach (Figure f in WhiteFigures)
                    {
                        if (f.type == "король")
                        {
                            f.x = 3;
                            f.y = 1;
                            break;
                        }
                    }
                    foreach (Figure f in WhiteFigures)
                    {
                        if (f.type == "тура" && f.x == 1)
                        {
                            f.x = 4;
                            f.y = 1;
                        }
                    }

                }

                // вызываем перерисовку
                Invalidate();

                // передаем ход противоположной стороне
                stepWhite = false;
            }
        
        else  
            {
               
                // извлекаем из строки ход
                //1.e4 e5 -> e4
                string move = ListSteps[step - 1].Substring(ListSteps[step - 1].IndexOf(' ') + 1);
                // рашрифровуем ход и ходим (меняем в массиве фигур положение)
                // N = knight
                // B = bishop
                // R = rock
                // Q = queen
                // K = king
                // _ = pawn
                label1.Text = ListSteps[step - 1];
                bool takeFigure = false;
                if (move[1] == 'x')
                {
                    takeFigure = true;
                    move = move.Remove(1, 1);
                }
                if (move[0] == 'N') // Nf3 --- Nef3 N1g5 ---
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                               f.type == "конь"
                               &&
                               (
                                    (Math.Abs(f.x - toDigit(move[1])) == 1 && Math.Abs(f.y - (move[2] - 48)) == 2)
                                    ||
                                    (Math.Abs(f.x - toDigit(move[1])) == 2 && Math.Abs(f.y - (move[2] - 48)) == 1)
                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'R') // Rf3 --- Ref3 R1g5 ---
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                               f.type == "тура"
                               &&
                               (
                                    (f.x == toDigit(move[1])  && free(f.x, f.y, toDigit(move[1]), move[2] - 48))
                                    ||
                                    (f.y == move[2]  && free(f.x, f.y, toDigit(move[1]), move[2] - 48))

                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'B') // Bf3 --- Bef4 B1g5 ---
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                               f.type == "слон"
                               &&
                               (
                                    (Math.Abs(f.x - toDigit(move[1])) == Math.Abs(f.y - (move[2] - 48))) && free(f.x, f.y, toDigit(move[1]), move[2] - 48)

                               )
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'Q') // Qf3 
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                               f.type == "ферзь"
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure ф1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move[0] == 'K') // Kf3 
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                               f.type == "король"
                           )
                        {
                            f.x = toDigit(move[1]);
                            f.y = move[2] - 48;
                            if (takeFigure == true)
                            {
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                
                //to do 2 пешки на 1-й вертикале 
                if (move[0] == 'a' || move[0] == 'b' || move[0] == 'c' || move[0] == 'd' || move[0] == 'e' || move[0] == 'f' || move[0] == 'g' || move[0] == 'h') //e2 -> e4
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (
                                f.type == "пешка" 
                                &&  f.x == toDigit(move[0])
                           )
                        {
                            
                            f.x = toDigit(move[0]);
                            f.y = move[1] - 48;
                            if (takeFigure == false) // e4
                            {
                                f.x = toDigit(move[0]);
                                f.y = move[1] - 48;
                            }
                            if (takeFigure == true) // de4
                            {
                                ф.x = toDigit(move[1]);
                                ф.y = move[2] - 48;
                                Figure figureForRemove = new Figure();
                                foreach (Figure f1 in WhiteFigures)
                                {
                                    if (f1.x == f.x && f1.y == f.y)
                                    {
                                        figureForRemove = f1;
                                        break;
                                    }
                                }
                                WhiteFigures.Remove(figureForRemove);
                            }
                            break;
                        }

                    }
                }
                if (move.Length >= 5 && move.Substring(0, 5) == "O-O-O")
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (f.type == "король")
                        {
                            f.x = 3;
                            f.y = 8;
                            break;
                        }
                    }
                    foreach (Figure f in BlackFigures)
                    {
                        if (f.type == "тура" && f.x == 1)
                        {
                            f.x = 4;
                            f.y = 8;
                        }
                    }

                }
                else if (move.Length >= 3 && move.Substring(0, 3) == "O-O")
                {
                    foreach (Figure f in BlackFigures)
                    {
                        if (f.type == "король")
                        {
                            f.x = 7;
                            f.y = 8;
                            break;
                        }
                    }
                    foreach (Figure f in BlackFigures)
                    {
                        if (f.type == "тура" && f.x == 8)
                        {
                            f.x = 6;
                            f.y = 8;
                        }
                    }

                }


                // вызываем перерисовку
                Invalidate();
                // передаем ход противоположной стороне
                stepWhite = true;
            }
        }
    }
}
