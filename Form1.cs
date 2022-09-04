using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_Vp
{
    public partial class Form1 : Form
    {
        private static Dictionary<String, String> mkTranslations = new Dictionary<String, String>() {
            { "playerX", "Играч X: " },
            { "playerO", "Играч О: " },
            { "draws", "Нерешени: " },
            { "alreadyClicked", "Ова поле е веќе кликнато." },
            { "error", "Грешка!" },
            { "victory", "Победа!" },
            { "playAgain", "Играј повторно" }
        };
        private static Dictionary<String, String> enTranslations = new Dictionary<String, String>() {
            { "playerX", "Player X: " },
            { "playerO", "Player O: " },
            { "draws", "Draws: " },
            { "alreadyClicked", "Field already clicked by a player." },
            { "error", "Error!" },
            { "victory", "Victory!" },
            { "playAgain", "Play Again" }

        };

        private Dictionary<String, Button> buttonsMap = new Dictionary<String, Button>();
        private Boolean playerFlag = true;
        private Boolean gameOver = false;



        public Form1()
        {
            InitializeComponent();
            buttonsMap.Add("button1", button1);
            buttonsMap.Add("button2", button2);
            buttonsMap.Add("button3", button3);
            buttonsMap.Add("button4", button4);
            buttonsMap.Add("button5", button5);
            buttonsMap.Add("button6", button6);
            buttonsMap.Add("button7", button7);
            buttonsMap.Add("button8", button8);
            buttonsMap.Add("button9", button9);
        }

        private void MK_Lang_CheckedChanged(object sender, EventArgs e)
        {
            this.playerXLabel.Text = mkTranslations["playerX"];
            this.playerOLabel.Text = mkTranslations["playerO"];
            this.draws.Text = mkTranslations["draws"];
            this.playAgain.Text = mkTranslations["playAgain"];
            this.Text = "Икс-Нула";
        }

        private void EN_Lang_CheckedChanged(object sender, EventArgs e)
        {
            this.playerXLabel.Text = enTranslations["playerX"];
            this.playerOLabel.Text = enTranslations["playerO"];
            this.draws.Text = enTranslations["draws"];
            this.playAgain.Text = enTranslations["playAgain"];
            this.Text = "TicTacToe";
        }

        private void button_Click(object sender, EventArgs e)
        {
            string buttonName = (sender as Button).Name;
            if (buttonsMap[buttonName].Text != "")
            {
                MessageBox.Show(radioButton1.Checked ? mkTranslations["alreadyClicked"] : enTranslations["alreadyClicked"],
                                radioButton1.Checked ? mkTranslations["error"] : enTranslations["error"],
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            buttonsMap[buttonName].Text = playerFlag ? "X" : "O";

            playerFlag = !playerFlag;
            checkIfGameIsOver();
        }

        void checkIfGameIsOver()
        {
            List<String> btns = buttonsMap.Values.ToList().Select(d => d.Text).ToList();
            List<int> winningIndexes = new List<int>();

            for (int i = 0; i <= 6; i+= 3)
            { 
                if (btns[i] != "" && btns[i] == btns[i + 1] && btns[i] == btns[i + 2])
                {
                    getVictoryMessage(btns[i].ToString());
                    winningIndexes.Add(i);
                    winningIndexes.Add(i + 1);
                    winningIndexes.Add(i + 2);
                    gameOver = true;
                    break;
                }
            }

            for (int i = 0; i <= 2; i ++)
            {
                if (btns[i] != "" && btns[i] == btns[i + 3] && btns[i] == btns[i + 6])
                {
                    getVictoryMessage(btns[i].ToString());
                    winningIndexes.Add(i);
                    winningIndexes.Add(i + 3);
                    winningIndexes.Add(i + 6);
                    gameOver = true;
                    break;
                }
            }

            for (int i = 0; i <= 2; i += 2)
            {
                if (i == 0 && btns[i] != "" && btns[i] == btns[4] && btns[i] == btns[8])
                {
                    getVictoryMessage(btns[i].ToString());
                    winningIndexes.Add(0);
                    winningIndexes.Add(4);
                    winningIndexes.Add(8);
                    gameOver = true;
                    break;
                }
                if (i == 2 && btns[i] != "" && btns[i] == btns[4] && btns[i] == btns[6])
                {
                    getVictoryMessage(btns[i].ToString());
                    winningIndexes.Add(2);
                    winningIndexes.Add(4);
                    winningIndexes.Add(6);
                    gameOver = true;
                    break;
                }
            }

            winningIndexes.ForEach(i => {
                buttonsMap["button" + (i + 1)].BackColor = Color.Green;
            });

            if (gameOver)
            {
                if (playerFlag)
                {
                    this.label1.Text = (Int64.Parse(this.label1.Text) + 1).ToString();
                }
                else
                {
                    this.label2.Text = (Int64.Parse(this.label2.Text) + 1).ToString();
                }
            }

            if (btns.TrueForAll(b => b != "") && !gameOver)
            {
                MessageBox.Show(radioButton1.Checked ? "Нерешено" : "Draw",
                                radioButton1.Checked ? "Нерешено" : "Draw",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                gameOver = true;
                buttonsMap.Values.ToList().ForEach(b =>
                {
                    b.BackColor = Color.Yellow;
                });
                this.label3.Text = (Int64.Parse(this.label3.Text) + 1).ToString();
                return;
            }

        }

        private void playAgain_Click(object sender, EventArgs e)
        {
            gameOver = false;
            buttonsMap = new Dictionary<String, Button>();
            buttonsMap.Add("button1", button1);
            buttonsMap.Add("button2", button2);
            buttonsMap.Add("button3", button3);
            buttonsMap.Add("button4", button4);
            buttonsMap.Add("button5", button5);
            buttonsMap.Add("button6", button6);
            buttonsMap.Add("button7", button7);
            buttonsMap.Add("button8", button8);
            buttonsMap.Add("button9", button9);

            buttonsMap.Values.ToList().ForEach(b =>
            {
                b.Text = "";
                b.BackColor = Color.White;
                b.Click -= new System.EventHandler(this.button_Click);
                b.Click += new System.EventHandler(this.button_Click);
            });


        }

        private void getVictoryMessage(string player)
        {
            MessageBox.Show(radioButton1.Checked ? "Играчот " + player + " победи" : "Player " + player + " has won", 
                            radioButton1.Checked ? mkTranslations["victory"] : enTranslations["victory"],
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
