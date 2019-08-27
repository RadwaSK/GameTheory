using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace led_board
{

    public partial class game : Form
    {
        int count = 0;
        Button[,] b = new Button[5, 5];
        public game()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            b[0, 0] = button00; b[0, 1] = button01; b[0, 2] = button02; b[0, 3] = button03; b[0, 4] = button04;
            b[1, 0] = button10; b[1, 1] = button11; b[1, 2] = button12; b[1, 3] = button13; b[1, 4] = button14;
            b[2, 0] = button20; b[2, 1] = button21; b[2, 2] = button22; b[2, 3] = button23; b[2, 4] = button24;
            b[3, 0] = button30; b[3, 1] = button31; b[3, 2] = button32; b[3, 3] = button33; b[3, 4] = button34;
            b[4, 0] = button40; b[4, 1] = button41; b[4, 2] = button42; b[4, 3] = button43; b[4, 4] = button44;
           
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        b[i, j].BackgroundImage = refon.BackgroundImage;

            }
            Moves.Text = count.ToString();
            Time.Text = (timer1.Interval /1000).ToString();
            
        }

        private void check_win()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (b[i, j].BackgroundImage != refoff.BackgroundImage)
                        return;
            MessageBox.Show("You Win");
            timer1.Stop();

        }
        private void Rest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    b[i, j].BackgroundImage = refon.BackgroundImage;
            count = 0;
            Moves.Text = count.ToString();
            Time.Text = "0";
            timer1.Stop();
        }

        private void button00_Click(object sender, EventArgs e)
        {

            button00.BackgroundImage = button00.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button10.BackgroundImage = button10.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button01.BackgroundImage = button01.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++;
            Moves.Text = count.ToString();
            check_win();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.BackgroundImage = button10.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button00.BackgroundImage = button00.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button20.BackgroundImage = button20.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button11.BackgroundImage = button11.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++;
            Moves.Text = count.ToString();
            check_win();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button20.BackgroundImage = button20.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button10.BackgroundImage = button10.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button30.BackgroundImage = button30.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button21.BackgroundImage = button21.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++;
            Moves.Text = count.ToString();
            check_win();

        }

        private void button30_Click(object sender, EventArgs e)
        {
            button30.BackgroundImage = button30.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button40.BackgroundImage = button40.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button20.BackgroundImage = button20.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button31.BackgroundImage = button31.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            button40.BackgroundImage = button40.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button41.BackgroundImage = button41.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button30.BackgroundImage = button30.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button01_Click(object sender, EventArgs e)
        {
            button01.BackgroundImage = button01.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button00.BackgroundImage = button00.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button02.BackgroundImage = button02.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button11.BackgroundImage = button11.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button02_Click(object sender, EventArgs e)
        {
            button02.BackgroundImage = button02.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button01.BackgroundImage = button01.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button03.BackgroundImage = button03.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button12.BackgroundImage = button12.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button03_Click(object sender, EventArgs e)
        {
            button03.BackgroundImage = button03.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button04.BackgroundImage = button04.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button02.BackgroundImage = button02.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button13.BackgroundImage = button13.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button04_Click(object sender, EventArgs e)
        {
            button04.BackgroundImage = button04.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button03.BackgroundImage = button03.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button14.BackgroundImage = button14.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.BackgroundImage = button11.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button01.BackgroundImage = button01.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button21.BackgroundImage = button21.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button12.BackgroundImage = button12.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button10.BackgroundImage = button10.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button21_Click(object sender, EventArgs e)
        {

            button21.BackgroundImage = button21.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button11.BackgroundImage = button11.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button31.BackgroundImage = button31.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button22.BackgroundImage = button22.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button20.BackgroundImage = button20.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button31_Click(object sender, EventArgs e)
        {

            button31.BackgroundImage = button31.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button21.BackgroundImage = button21.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button41.BackgroundImage = button41.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button32.BackgroundImage = button32.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button30.BackgroundImage = button30.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button41_Click(object sender, EventArgs e)
        {

            button41.BackgroundImage = button41.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button31.BackgroundImage = button31.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button42.BackgroundImage = button42.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button40.BackgroundImage = button40.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            button12.BackgroundImage = button12.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button02.BackgroundImage = button02.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button22.BackgroundImage = button22.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button11.BackgroundImage = button11.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button13.BackgroundImage = button13.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button22_Click(object sender, EventArgs e)
        {

            button22.BackgroundImage = button22.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button12.BackgroundImage = button12.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button32.BackgroundImage = button32.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button21.BackgroundImage = button21.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button23.BackgroundImage = button23.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button32_Click(object sender, EventArgs e)
        {

            button32.BackgroundImage = button32.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button22.BackgroundImage = button22.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button42.BackgroundImage = button42.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button31.BackgroundImage = button31.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button33.BackgroundImage = button33.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button42_Click(object sender, EventArgs e)
        {

            button42.BackgroundImage = button42.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button32.BackgroundImage = button32.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button41.BackgroundImage = button41.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button43.BackgroundImage = button43.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            button13.BackgroundImage = button13.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button03.BackgroundImage = button03.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button23.BackgroundImage = button23.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button12.BackgroundImage = button12.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button14.BackgroundImage = button14.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button23_Click(object sender, EventArgs e)
        {

            button23.BackgroundImage = button23.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button13.BackgroundImage = button13.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button33.BackgroundImage = button33.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button24.BackgroundImage = button24.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button22.BackgroundImage = button22.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button33_Click(object sender, EventArgs e)
        {

            button33.BackgroundImage = button33.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button23.BackgroundImage = button23.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button43.BackgroundImage = button43.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button34.BackgroundImage = button34.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button32.BackgroundImage = button32.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            button43.BackgroundImage = button43.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button33.BackgroundImage = button33.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button44.BackgroundImage = button44.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button42.BackgroundImage = button42.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            button14.BackgroundImage = button14.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button04.BackgroundImage = button04.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button24.BackgroundImage = button24.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button13.BackgroundImage = button13.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button24_Click(object sender, EventArgs e)
        {

            button24.BackgroundImage = button24.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button14.BackgroundImage = button14.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button34.BackgroundImage = button34.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button23.BackgroundImage = button23.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button34_Click(object sender, EventArgs e)
        {

            button34.BackgroundImage = button34.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button24.BackgroundImage = button24.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button44.BackgroundImage = button44.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button33.BackgroundImage = button33.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
        }

        private void button44_Click(object sender, EventArgs e)
        {

            button44.BackgroundImage = button44.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button34.BackgroundImage = button34.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            button43.BackgroundImage = button43.BackgroundImage == refoff.BackgroundImage ? refon.BackgroundImage : refoff.BackgroundImage;
            count++; Moves.Text = count.ToString();
            check_win();
            
        }

        private void Start_Click(object sender, EventArgs e)
        {
            
            count = 0;
            Moves.Text = count.ToString();
            timer1.Interval = 1000 ;
            timer1.Enabled = true;
            timer1.Start();
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time.Text =(Int32.Parse(Time.Text)+1).ToString();
  
        }
    }
    
}
