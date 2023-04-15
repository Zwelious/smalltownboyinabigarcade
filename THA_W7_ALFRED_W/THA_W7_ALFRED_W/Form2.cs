using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THA_W7_ALFRED_W
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int seatHeight, seatWidth, x, y, reserved, status, reservedSeats, abjadKursi;
        public static int movieNo;
        Button[,] seats;
        Random randomizer;
        List<string> posters;
        List<string> titles;
        Label chosenSeat, seatCode;
        Movies movies;

        private void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Beige;
            movieNo = Convert.ToInt32(Movies.filmNo);
            seatHeight = 10;
            seatWidth = 10;
            randomizer = new Random();

            seats = new Button[seatHeight, seatWidth];

            posters = new List<string>();
            foreach (string path in Form1.moviePosters)
            {
                posters.Add(path);
            }
            titles = new List<string>();
            foreach (string judul in Form1.titles)
            {
                titles.Add(judul);
            }

            PictureBox movieposter = new PictureBox();
            movieposter.Location = new Point(380, 30);
            movieposter.SizeMode = PictureBoxSizeMode.StretchImage;
            movieposter.Size = new Size(130, 180);
            this.Controls.Add(movieposter);

            Label title = new Label();
            title.Location = new Point(520, 30);
            title.Size = new Size(200, 200);
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            this.Controls.Add(title);

            chosenSeat = new Label();
            chosenSeat.Location = new Point(25, 350);
            chosenSeat.Text = "Picked seat(s): ";
            chosenSeat.Font = new Font("Arial", 10);
            chosenSeat.Size = new Size(120, 70);

            Button back = new Button();
            back.Location = new Point(600, 350);
            back.Text = "Back";
            back.Size = new Size(70, 25);
            back.Click += back_Click;
            this.Controls.Add(back);

            x = 400;
            for(int i = 0; i < 3; i++)
            {
                Button jam = new Button();
                jam.Location = new Point(x, 240);
                jam.Size = new Size(80, 25);
                jam.Text = Form1.timePlaying[0][i].time;
                jam.BackColor = Color.White;
                jam.Tag = i;
                jam.Click += jam_Click;
                this.Controls.Add(jam);
                x += 100;
            }

            switch (movieNo)
            {
                case 0:
                    movieposter.ImageLocation = @posters[0];
                    title.Text = titles[0];
                    break;
                case 1:
                    movieposter.ImageLocation = @posters[1];
                    title.Text = titles[1];
                    break;
                case 2:
                    movieposter.ImageLocation = @posters[2];
                    title.Text = titles[2];
                    break;
                case 3:
                    movieposter.ImageLocation = @posters[3];
                    title.Text = titles[3];
                    break;
                case 4:
                    movieposter.ImageLocation = @posters[4];
                    title.Text = titles[4];
                    break;
                case 5:
                    movieposter.ImageLocation = @posters[5];
                    title.Text = titles[5];
                    break;
                case 6:
                    movieposter.ImageLocation = @posters[6];
                    title.Text = titles[6];
                    break;
                case 7:
                    movieposter.ImageLocation = @posters[7];
                    title.Text = titles[7];
                    break;
            }
            
        }
        private void seatsGenerator(int filmKe, int jamKe)
        {
            x = 25;
            y = 25;
            reservedSeats = 0;
            abjadKursi = 65;
            reserved = randomizer.Next(1, 71);
            for (int i = 0; i < seatWidth; i++)
            {
                abjadKursi = 65 + i;
                for (int j = 0; j < seatHeight; j++)
                {
                    status = randomizer.Next(2);
                    seats[i, j] = new Button();
                    seats[i, j].Size = new Size(30, 30);
                    seats[i, j].Location = new Point(x, y);
                    seats[i, j].Tag = Convert.ToChar(abjadKursi) + (j + 1).ToString();
                    seats[i, j].Text = seats[i, j].Tag.ToString();
                    seats[i, j].Font = new Font("Arial", 6);
                    if (status == 0 && reservedSeats <= reserved)
                    {
                        seats[i, j].BackColor = Color.Red;
                        reservedSeats++;
                    }
                    else
                    {
                        seats[i, j].BackColor = Color.Gray;
                    }
                    Form1.timePlaying[filmKe][jamKe].seats.Add(seats[i, j]);
                    x += 30;
                }
                y += 30;
                x = 25; 
            }
        }

        private void jam_Click(object sender, EventArgs e)
        {
            this.panel_seat.Controls.Clear();
            var send = sender as Button;
            int jam = Convert.ToInt32(send.Tag);
            if (Form1.timePlaying[movieNo][jam].seats.Count == 100)
            {
                foreach (Button seat in Form1.timePlaying[movieNo][jam].seats)
                {
                    this.panel_seat.Controls.Add(seat);
                }
            }
            else
            {
                seatsGenerator(movieNo, jam);
                foreach (Button seat in Form1.timePlaying[movieNo][jam].seats)
                {
                    this.panel_seat.Controls.Add(seat);
                }
            }
            this.panel_seat.Controls.Add(chosenSeat);
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.panel_seat.Controls.Clear();
            this.Controls.Clear();
            movies = new Movies();
            movies.TopLevel = false;
            movies.Dock = DockStyle.Fill;
            this.Controls.Add(movies);
            movies.Show();
        }
    }
}
