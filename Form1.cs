using System;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot;
using Timer = System.Windows.Forms.Timer;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        private double birdY = 270;
        private double birdRadius = 15;
        private double gravity = 2.0;
        private double jumpForce = -17.0;
        private double birdVelocity = 0;

        private double pipeWidth = 50;
        private double gapHeight = 150;
        private double pipeSpeed = 5;
        private double pipeX = 960;
        private double gapY = 200;

        private ScottPlot.Plottables.Ellipse birdShape;
        private ScottPlot.Plottables.Polygon pipeTopShape;
        private ScottPlot.Plottables.Polygon pipeBottomShape;

        private Timer gameTimer;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            InitializePlot();
            InitializeGame();

            FP.KeyDown += FP_KeyDown;
        }

        private void FP_KeyDown(object? sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                birdVelocity = jumpForce; // Zýplama kuvvetini uygula
            }
        }

        private void InitializePlot()
        {
            FP.Plot.Axes.SetLimits(0, 960, 0, 540);
            FP.Plot.Layout.Frameless();
            FP.Plot.HideAxesAndGrid();
            FP.Refresh();
        }

        private void InitializeGame()
        {
            // Kuþ
            birdShape = FP.Plot.Add.Ellipse(100, birdY, birdRadius * 2, birdRadius * 2);
            birdShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.LightBlue);

            // Engeller
            pipeTopShape = FP.Plot.Add.Polygon(CreateRectangle(pipeX, gapY + gapHeight / 2, pipeWidth, 540 - (gapY + gapHeight / 2)));
            pipeTopShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.Green);

            pipeBottomShape = FP.Plot.Add.Polygon(CreateRectangle(pipeX, 0, pipeWidth, gapY - gapHeight / 2));
            pipeBottomShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.Green);

            // Zamanlayýcý
            gameTimer = new Timer { Interval = 20 }; // 20 ms
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Kuþ hareketi
            birdVelocity += gravity;
            birdY -= birdVelocity;

            // Kuþun ekrandan çýkmasýný engelle
            if (birdY - birdRadius < 0)
                birdY = birdRadius; // Üst sýnýr
            else if (birdY + birdRadius > 540)
                birdY = 540 - birdRadius; // Alt sýnýr

            birdShape.Center = new(birdShape.Center.X, birdY);

            // Engellerin hareketi
            pipeX -= pipeSpeed;
            if (pipeX < -pipeWidth)
            {
                pipeX = 960;
                gapY = new Random().Next(100, 440);
                score++;
            }

            // Engellerin pozisyon ve boyut güncellemeleri
            FP.Plot.Clear(); // Eski þekilleri temizle
            birdShape = FP.Plot.Add.Ellipse(100, birdY, birdRadius * 2, birdRadius * 2);
            birdShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.LightBlue);

            pipeTopShape = FP.Plot.Add.Polygon(CreateRectangle(pipeX, gapY + gapHeight / 2, pipeWidth, 540 - (gapY + gapHeight / 2)));
            pipeTopShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.Green);

            pipeBottomShape = FP.Plot.Add.Polygon(CreateRectangle(pipeX, 0, pipeWidth, gapY - gapHeight / 2));
            pipeBottomShape.FillColor = ScottPlot.Color.FromColor(System.Drawing.Color.Green);

            // Çarpýþma kontrolü
            if (CheckCollision())
            {
                EndGame();
            }

            // Ekraný güncelle
            FP.Refresh();
        }

        private bool CheckCollision()
        {
            // Kuþ ekran sýnýrlarýný aþmasý
            if (birdY - birdRadius < 0 || birdY + birdRadius > 540)
                return true;

            // Kuþ ve engel çarpýþmasý
            if ((birdShape.Center.X + birdRadius > pipeX && birdShape.Center.X - birdRadius < pipeX + pipeWidth) &&
                (birdY + birdRadius > gapY + gapHeight / 2 || birdY - birdRadius < gapY - gapHeight / 2))
                return true;

            return false;
        }

        private void EndGame()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game Over! Your Score: {score}");
            Application.Restart();
        }
        private Coordinates[] CreateRectangle(double x, double y, double width, double height)
        {
            // Dikdörtgen oluþturmak için köþe noktalarý
            return new Coordinates[]
            {
                new Coordinates(x, y), // Sol alt köþe
                new Coordinates(x + width, y), // Sað alt köþe
                new Coordinates(x + width, y + height), // Sað üst köþe
                new Coordinates(x, y + height) // Sol üst köþe
            };
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    if (e.KeyCode == Keys.Space)
        //    {
        //        birdVelocity = jumpForce;
        //    }
        //}
    }
}
