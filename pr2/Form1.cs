using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using pr2;

namespace pr2
{
    public partial class Form1 : Form
    {
        private Timer _timer;
        private List<Snowflake> _snowflakes;
        private Random _random;
        private Image _snowflakeImage;

        public Form1()
        {
            InitializeComponent();
            _snowflakes = new List<Snowflake>();
            _random = new Random();
            _snowflakeImage = Image.FromFile("Resources\\snejinka.png");

            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_random.Next(0, 10) > 7)
            {
                var size = _random.Next(25, 60);
                var snowflake = new Snowflake
                {
                    X = _random.Next(0, this.ClientSize.Width),
                    Y = 0,
                    Size = size,
                    Speed = CalculateSpeed(size)
                };
                _snowflakes.Add(snowflake);
            }

            foreach (var snowflake in _snowflakes)
            {
                snowflake.Y += snowflake.Speed;
            }

            _snowflakes.RemoveAll(s => s.Y > this.ClientSize.Height);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var snowflake in _snowflakes)
            {
                e.Graphics.DrawImage(_snowflakeImage, new Rectangle(snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size));
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private int CalculateSpeed(int size)
        {
            int minSpeed = 2;
            int maxSpeed = 10;

            return minSpeed + (size - 15) * (maxSpeed - minSpeed) / (45 - 15);
        }
    }
}
