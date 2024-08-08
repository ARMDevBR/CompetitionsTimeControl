﻿using System.Diagnostics;
using System.Text;

namespace CompetitionsTimeControl
{
    public partial class AboutForm : Form
    {
        private const string Icons8Link = "https://icons8.com/icons/collections/7o2n2lsu603y1qzzuowv";
        private const string CopilotDesigner = "https://copilot.microsoft.com/images/create";

        public AboutForm()
        {
            InitializeComponent();

            LinkLabelIcons8.Links.Add(0, LinkLabelIcons8.Text.Length, Icons8Link);
            LinkLabelCopilotDesigner.Links.Add(0, LinkLabelIcons8.Text.Length, CopilotDesigner);
        }

        private void LinkLabelIcons8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryOpenLink(e.Link?.LinkData?.ToString() ?? "");

        }

        private void LinkLabelCopilotDesigner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryOpenLink(e.Link?.LinkData?.ToString() ?? "");
        }

        private static void TryOpenLink(string link)
        {
            try
            {
                // Use 'ProcessStartInfo' para abrir o link no navegador padrão
                ProcessStartInfo psi = new()
                {
                    FileName = link,
                    UseShellExecute = true // Garante que o link será aberto com o programa associado (navegador)
                };
                Process.Start(psi);
            }
            catch (Exception)
            {
                StringBuilder sb = new($"Não foi possível abrir o link: {link}!\n\n");
                sb.Append("Talvez o link não seja mais válido ou o programa não tenha permissão.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonOkClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
