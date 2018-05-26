﻿using System;
using System.Windows.Forms;
using System.Drawing;
using LogicPart;

namespace BoardPart
{
    public class BoardDamkaWinForm 
    {
        private readonly byte r_SizeOfBoard;
        private Form m_FormOfBoard = new Form();
        private Locat? m_Dest, m_Source;
        private ButtonLocat[,] m_MatOfButton;
        private Label m_LabelPlayer1, m_LabelPlayer2, m_LabelNowPlaying, m_LabelNameOfPlayingNow;

        public event Action<Locat, Locat> BoardUiMove;

        public BoardDamkaWinForm(byte i_SizeOfBoardGame, string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            r_SizeOfBoard = i_SizeOfBoardGame;
            initializeBoardOfGame(i_NameOfPlayer1, i_NameOfPlayer2);
            m_FormOfBoard.FormClosing += Form_Closing;
        }

        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure that you want exit ?", "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public string LabelOfPlayer1
        {
            get
            {
                return m_LabelPlayer1.Text;
            }

            set
            {
                m_LabelPlayer1.Text = value;
            }
        }

        public string LabelOfPlayer2
        {
            get
            {
                return m_LabelPlayer2.Text;
            }

            set
            {
                m_LabelPlayer2.Text = value;
            }
        }

        public string LabelNameOfPlayingNow
        {
            get
            {
                return m_LabelNameOfPlayingNow.Text;
            }

            set
            {
                m_LabelNameOfPlayingNow.Text = value;
            }
        }

        public void ChangeTextOnButton(Locat i_LocateOfButton, string i_NewTextToButton)
        {
            m_MatOfButton[i_LocateOfButton.X, i_LocateOfButton.Y].Text = i_NewTextToButton;
        }

        public void ShowBoard()
        {
            m_FormOfBoard.ShowDialog();
        }

        private void initializeBoardOfGame(string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            initializeLabels(i_NameOfPlayer1, i_NameOfPlayer2);
            initializeFormStyleAndSize();
            initializeButtonBoard();
        }

        private void initializeLabels(string i_NameOfPlayer1, string i_NameOfPlayer2)
        {
            m_LabelNowPlaying = new Label();
            m_LabelNowPlaying.Text = "Now Playing:";
            m_LabelNowPlaying.Top = 12;
            m_LabelNowPlaying.Height = 50;
            m_LabelNowPlaying.Width = 110;
            m_LabelNowPlaying.Left = 0;
            m_LabelNowPlaying.Font = new Font("Segoe Print", 10);
            m_LabelNowPlaying.AutoSize = false;
            m_FormOfBoard.Controls.Add(m_LabelNowPlaying);

            m_LabelNameOfPlayingNow = new Label();
            m_LabelNameOfPlayingNow.Text = i_NameOfPlayer1;
            m_LabelNameOfPlayingNow.Top = m_LabelNowPlaying.Top;
            m_LabelNameOfPlayingNow.Height = m_LabelNowPlaying.Height;
            m_LabelNameOfPlayingNow.Width = m_LabelNowPlaying.Width;
            m_LabelNameOfPlayingNow.Left = m_LabelNowPlaying.Right;
            m_LabelNameOfPlayingNow.Font = m_LabelNowPlaying.Font;
            m_LabelNameOfPlayingNow.AutoSize = false;
            m_FormOfBoard.Controls.Add(m_LabelNameOfPlayingNow);

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = string.Format("{0}:{1}", i_NameOfPlayer1, "0");
            m_LabelPlayer1.Top = m_LabelNowPlaying.Top;
            m_LabelPlayer1.Height = m_LabelNowPlaying.Height;
            m_LabelPlayer1.Width = m_LabelNowPlaying.Width;
            m_LabelPlayer1.Left = m_LabelNameOfPlayingNow.Right + 30;
            m_LabelPlayer1.Font = m_LabelNowPlaying.Font;
            m_LabelPlayer1.AutoSize = false;
            m_FormOfBoard.Controls.Add(m_LabelPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = string.Format("{0}:{1}", i_NameOfPlayer2, "0");
            m_LabelPlayer2.Top = m_LabelPlayer1.Top;
            m_LabelPlayer2.Height = m_LabelPlayer1.Height;
            m_LabelPlayer2.Width = m_LabelPlayer1.Width;
            m_LabelPlayer2.Left = m_LabelPlayer1.Right + 30;
            m_LabelPlayer2.Font = m_LabelPlayer1.Font;
            m_LabelPlayer2.AutoSize = false;
            m_FormOfBoard.Controls.Add(m_LabelPlayer2);
        }

        private void initializeButtonBoard()
        {
            const string player1Sign = "O", player2Sign = "X", emptyPlace = " ";
            Locat locateForButtons = new Locat();
            int space = m_LabelPlayer1.Height + m_LabelPlayer1.Top;
            m_MatOfButton = new ButtonLocat[r_SizeOfBoard, r_SizeOfBoard];

            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if (i < ((r_SizeOfBoard / 2) - 1) && (i + j) % 2 != 0)
                    {
                        locateForButtons.X = (byte)j;
                        locateForButtons.Y = (byte)i;
                        ButtonLocat buttonOfPlayr1 = new ButtonLocat(locateForButtons);
                        buttonOfPlayr1.Text = player1Sign;
                        buttonOfPlayr1.Width = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfPlayr1.Height = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfPlayr1.Left = j * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard);
                        buttonOfPlayr1.Top = (i * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard)) + space;
                        buttonOfPlayr1.Click += Button_Cliked;

                        m_FormOfBoard.Controls.Add(buttonOfPlayr1);
                        m_MatOfButton[j, i] = buttonOfPlayr1;
                    }
                    else if (i >= ((r_SizeOfBoard / 2) + 1) && (i + j) % 2 != 0)
                    {
                        locateForButtons.X = (byte)j;
                        locateForButtons.Y = (byte)i;
                        ButtonLocat buttonOfPlayr2 = new ButtonLocat(locateForButtons);
                        buttonOfPlayr2.Text = player2Sign;
                        buttonOfPlayr2.Width = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfPlayr2.Height = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfPlayr2.Left = j * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard);
                        buttonOfPlayr2.Top = (i * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard)) + space;
                        buttonOfPlayr2.Click += Button_Cliked;

                        m_FormOfBoard.Controls.Add(buttonOfPlayr2);
                        m_MatOfButton[j, i] = buttonOfPlayr2;
                    }
                    else
                    {
                        locateForButtons.X = (byte)j;
                        locateForButtons.Y = (byte)i;
                        ButtonLocat buttonOfEmptyPlace = new ButtonLocat(locateForButtons);
                        buttonOfEmptyPlace.Text = emptyPlace;
                        buttonOfEmptyPlace.Width = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfEmptyPlace.Height = m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;
                        buttonOfEmptyPlace.Left = j * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard);
                        buttonOfEmptyPlace.Top = (i * (m_FormOfBoard.ClientSize.Width / r_SizeOfBoard)) + space;

                        if ((i + j) % 2 == 0)
                        {
                            buttonOfEmptyPlace.BackColor = Color.Gray;
                            buttonOfEmptyPlace.Enabled = false;
                        }
                        else
                        {
                            buttonOfEmptyPlace.Click += Button_Cliked;
                        }

                        m_FormOfBoard.Controls.Add(buttonOfEmptyPlace);
                        m_MatOfButton[j, i] = buttonOfEmptyPlace;
                    }
                }
            }
        }

        private void initializeFormStyleAndSize()
        {
            int space = m_LabelPlayer1.Top;
            Size sizeToClient = new Size();
            sizeToClient.Height = (((2 * r_SizeOfBoard) + 2) * m_FormOfBoard.ClientSize.Width / r_SizeOfBoard) + space;
            sizeToClient.Width = (2 * r_SizeOfBoard) * m_FormOfBoard.ClientSize.Width / r_SizeOfBoard;

            m_FormOfBoard.ClientSize = sizeToClient;
            m_FormOfBoard.FormBorderStyle = FormBorderStyle.FixedSingle;
            m_FormOfBoard.StartPosition = FormStartPosition.CenterScreen;
            m_FormOfBoard.MaximizeBox = false;
        }

        public void RestBoard()
        {
            const string player1Sign = "O", player2Sign = "X", emptyPlace = " ";
            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                for (int j = 0; j < r_SizeOfBoard; j++)
                {
                    if (i < ((r_SizeOfBoard / 2) - 1) && (i + j) % 2 != 0)
                    {
                        m_MatOfButton[j, i].Text = player1Sign;
                    }
                    else if (i >= ((r_SizeOfBoard / 2) + 1) && (i + j) % 2 != 0)
                    {
                        m_MatOfButton[j, i].Text = player2Sign;
                    }
                    else
                    {
                        m_MatOfButton[j, i].Text = emptyPlace;
                    }
                }
            }
        }

        public void CloseBoard()
        {
            m_FormOfBoard.Close();
        }

        private void Button_Cliked(object sender, EventArgs e)
        {
            ButtonLocat currentButton = sender as ButtonLocat;

            if (currentButton.BackColor != Color.Blue)
            {
                currentButton.BackColor = Color.Blue;
                if (m_Source == null)
                {
                    m_Source = currentButton.LocatOfButton;
                }
            }
            else
            {
                currentButton.BackColor = Color.White;
                m_Source = null;
            }

            if (m_Source != null && !m_Source.Value.Equals(currentButton.LocatOfButton))
            {
                m_Dest = currentButton.LocatOfButton;
                OnBoardUiMove(m_Source.Value, m_Dest.Value);
                cleanAfterMove();
            }
        }

        private void cleanAfterMove()
        {
            m_MatOfButton[m_Source.Value.X, m_Source.Value.Y].BackColor = Color.White;
            m_MatOfButton[m_Dest.Value.X, m_Dest.Value.Y].BackColor = Color.White;
            m_Source = null;
            m_Dest = null;
        }

        protected virtual void OnBoardUiMove(Locat i_Source, Locat i_Dest)
        {
            if (BoardUiMove != null)
            {
                BoardUiMove.Invoke(i_Source, i_Dest);
            }
        }
    }
}
