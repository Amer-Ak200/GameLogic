using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLogic
{
    public partial class Form1 : Form
    {
        PictureBox pcbFrom = null;
        PictureBox pcbTo = null;
        Color selectableColor = Color.LightGray;
        Color alreadyUsedColor = Color.Brown;
        Color playerLeftColor = Color.Red;
        Color playerRightColor = Color.Blue;
        Color playersTurnColor;  // Declare it here
        string playersTurn = "";
        GroupBox selectedGroupbox = null;
        Random randomGenerator = new Random();
        int championsPickedCount = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gbxChampions.Enabled = false;
            btnFight.Enabled = false;

            foreach (PictureBox pictureBox in gbxChampions.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = selectableColor;
            }
                foreach (PictureBox pictureBox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = playerLeftColor;
                pictureBox.AllowDrop = true;
            }
            foreach (PictureBox pictureBox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                pictureBox.BackColor = playerRightColor;
                pictureBox.AllowDrop = true;
            }
        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            gbxChampions.Enabled = true;
            btnStart.Enabled = false;
            playersTurn = "Left";
            championsPickedCount = 0;
            lblPlayersTuns.Text = "< Turn red";
            ChangeTurns();
        }
        /// <summary>
        /// Show the player who's turn it is 
        /// </summary>
        public void ChangeTurns()
        {
            if (playersTurn =="Left")
            {
                lblPlayersTuns.Text = "< Turn red";
                playersTurnColor = playerLeftColor;
                selectedGroupbox = gbxPlayerOne;
            }
            else
            {
                lblPlayersTuns.Text = "Turn blue >";
                playersTurnColor = playerRightColor;
                selectedGroupbox = gbxPlayerTwo;
            }

            ResetPlayersColor();
        }

        private void pcbChampionsAll_MouseDown(object sender, MouseEventArgs e)
        {
            pcbFrom = (PictureBox)sender;
            //Only allowe you to pic up an image when backcolor is the correct color
            if (pcbFrom.BackColor == selectableColor)
            {
                if (playersTurn == "Left")
                {
                    selectedGroupbox = gbxPlayerOne;
                }
                else
                {
                    selectedGroupbox = gbxPlayerTwo;
                }

                //Makes all the pictureboxes that dont have an image green 
                foreach (PictureBox pictureBox in selectedGroupbox.Controls.OfType<PictureBox>())
                {
                    if (pictureBox.Image == null)
                    {
                        pictureBox.BackColor = Color.Green;
                    }
                    else
                    {
                        pictureBox.BackColor = Color.Transparent;
                    }
                }
                //This actualy lest you hold the image while you keep mouse down
                pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy);
            }
        }

        private void pcbPlayers_DragOver(object sender, DragEventArgs e)
        {
            // Only lets the image be dropped if pcb == green
            pcbTo = (PictureBox)sender;
            if (pcbTo.BackColor == Color.Green)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        } 

        private void pcbPlayers_DragDrop(object sender, DragEventArgs e)
        {
            pcbFrom.BackColor = alreadyUsedColor;
            //Drops the image your holding into the new picturebox
            pcbTo = (PictureBox)sender;
            Image getPicture = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            pcbTo.Image = getPicture;

            ResetPlayersColor();

            //Switches the current players turn
            if (playersTurn == "Left")
            {
                playersTurn = "Right";
            }
            else
            {
                playersTurn = "Left";
            }
            ChangeTurns();
            championsPickedCount++;
            if (championsPickedCount == 6)
            {
                lblPlayersTuns.Text = "All champions are picked. Let them fight!";
                btnFight.Enabled = true;
            }
        }
        
        public void ResetPlayersColor ()
        {
            //Makes all the pictureboxes that dont have an image there own players color
            foreach(PictureBox pictureBox in selectedGroupbox.Controls.OfType<PictureBox>())
            {
                if (pictureBox.Image == null)
                {
                    pictureBox.BackColor = playersTurnColor;
                }
                else 
                {
                    pictureBox.BackColor = Color.Transparent;              
                }
            }
        }
        /// <summary>
        /// Who wins doesnt matter but this is a way of showing it 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnFight_Click(object sender, EventArgs e)
        {
            int playerLeftPowerlevel = randomGenerator.Next(1 , 10001);
            int playerRightPowerlevel = randomGenerator.Next(1, 10001);

            if (playerLeftPowerlevel > playerRightPowerlevel)
            {
                gbxPlayerOne.BackColor = Color.Orange;
                lblPlayersTuns.Text = "Red team won withe a power level of : " + playerLeftPowerlevel;
            }
            else if (playerRightPowerlevel > playerLeftPowerlevel)
            {
                gbxPlayerTwo.BackColor = Color.Orange;
                lblPlayersTuns.Text = "Blue team won withe a power level of : " + playerRightPowerlevel;
            }
            else 
            {
                lblPlayersTuns.Text = "Its a draw , both teams had :" + playerLeftPowerlevel + "as a power level";
            }
            btnStart.Enabled = true;
            btnFight.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Do you want to play again ?", "Play again?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Application.Exit();
            }
        }
        public void RestartGame()
        {
            gbxChampions.Enabled = true;
            btnStart.Enabled = false;
            playersTurn = "left";
            championsPickedCount = 0;
            lblPlayersTuns.Text = "< Turn red";
            ChangeTurns();
            gbxPlayerOne.BackColor = Color.Transparent;
            gbxPlayerTwo.BackColor = Color.Transparent;

            //Remove all images and reset the colors
            foreach (PictureBox pictureBox in gbxPlayerOne.Controls.OfType<PictureBox>())
            {
                pictureBox.Image = null;
                pictureBox.BackColor = playerLeftColor;
            }
            foreach (PictureBox pictureBox in gbxPlayerTwo.Controls.OfType<PictureBox>())
            {
                pictureBox.Image = null;
                pictureBox.BackColor = playerRightColor;
            }
            //Makes all champion pictureboxes pickaple again
            foreach (PictureBox picturebox in gbxChampions.Controls.OfType<PictureBox>())
            {
                picturebox.BackColor = selectableColor;
            }

        }
    }
}
