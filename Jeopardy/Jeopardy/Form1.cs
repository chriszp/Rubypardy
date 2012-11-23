using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeopardy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pnlPregunta.BringToFront();

            ReadQuestionsAnswersFile();

            foreach (Control btn in panel1.Controls)
            {
                if (btn.GetType().Name == "Button")
                {
                    btn.Click += new EventHandler(btnA100_Click);
                    btn.BackColor = Color.LightGoldenrodYellow;
                    btn.Font =  new Font(btn.Font, FontStyle.Bold);
                }
            }

        }

        Dictionary<string, string> AnswersQuestions = new Dictionary<string, string>();

        private void btnA100_Click(object sender, EventArgs e)
        {

            List<string> questionAnswer = GetQuestionAnswer(((Button)sender).Tag.ToString());


            if (questionAnswer.Count == 2)
            {
                BringQuestionPanel(questionAnswer[0], questionAnswer[1]);
                ((Button)sender).Enabled = false;
            }
        }

        private List<string> GetQuestionAnswer(string p)
        {
            List<string> result = new List<string>();

            if (AnswersQuestions.ContainsKey(p))
            {
                result.AddRange(AnswersQuestions[p].Split('`'));
            }

            return result;
        }


        private void ReadQuestionsAnswersFile()
        {
            string[] lines = System.IO.File.ReadAllLines("QuestionsAnswers.txt");

            foreach (string line in lines)
            {
                AnswersQuestions.Add(line.Substring(0, 4), line.Substring(line.IndexOf("~") + 1));
            }


        }

        private void BringQuestionPanel(string question, string answer)
        {

            ((TextBox)pnlPregunta.Controls["lblQuestion"]).Lines = question.Split('\\');
            pnlPregunta.Controls["lblAnswer"].Text = "Answer:\n" + answer;
            pnlPregunta.Controls["lblAnswer"].Visible = false;

            pnlPregunta.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlPregunta.Visible = false;
            pnlPregunta.Controls["lblQuestion"].Text = string.Empty;
            pnlPregunta.Controls["lblAnswer"].Text = string.Empty;
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            pnlPregunta.Controls["lblAnswer"].Visible = true;
        }
    }
}
