


using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace SchoolManagementProjecture
{
    public partial class Main : Form
    {
        //fields
        private Button currentbutton;
        private Random random;
        private int tempIndex;
        private Form activeForm;


        //methods
        private Color SelectThemeColor()
        {

            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {

                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void activateButton(object btnSender)
        {

            if (btnSender != null)
            {

                if (currentbutton != (Button)btnSender)

                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentbutton = (Button)btnSender;
                    currentbutton.BackColor = color;
                    currentbutton.ForeColor = Color.White;
                    currentbutton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
                    PanelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                }


            }
        }

        public void DisableButton()
        {

            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {

                    previousBtn.BackColor = Color.CornflowerBlue;
                    previousBtn.ForeColor = Color.MidnightBlue;
                    previousBtn.Font = new Font("Segoe UI Semibold", 7.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
                }



            }
        }


        private string accountSid = "AC3532cf785270f10ff40a7e954c4aaecd";
        private string authToken = "8fb5c71c334f95529f5103a311df6ef3";
        private string serviceSid = "VA2c8863ded114a93dad75c208ed21a556";

        private string phoneNumber;
        private string userName;
        private TableLayoutPanel tableLayout;

        // Importation des fonctions pour les coins arrondis et le centrage du texte
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        // Constante pour le message de centrage vertical du texte
        private const int EM_SETCUEBANNER = 0x1501;

        // Message pour repositionner le texte
        private const int EM_SETRECT = 0xB3;

        // Correction de la déclaration DllImport pour SendMessage qui accepte une struct RECT par référence
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, ref RECT lp);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Main()
        {
            InitializeComponent();

            // Initialiser Twilio une seule fois au démarrage du formulaire
            TwilioClient.Init(accountSid, authToken);

            // Activer le DoubleBuffered pour réduire le scintillement
            this.DoubleBuffered = true;

            // Ajouter les événements pour gérer le redimensionnement de manière fluide
            this.ResizeBegin += new EventHandler(Form_ResizeBegin);
            this.ResizeEnd += new EventHandler(Form_ResizeEnd);
            this.Resize += new EventHandler(Form_Resize);

            // Configurer le TableLayoutPanel pour un centrage automatique
            SetupTableLayoutPanel();
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {

            if (activeForm != null)
            {
                activeForm.Close();
            }
            activateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.PanelDesktopPanel.Controls.Add(childForm);
            this.PanelDesktopPanel.Tag = childForm; // Ligne corrigée
            childForm.BringToFront();
            childForm.Show();
            MainTitleText.Text = childForm.Text;
        }

        private void SetupTableLayoutPanel()
        {
            // Créer un TableLayoutPanel qui sera utilisé pour centrer le panel automatiquement
            tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.ColumnCount = 1;
            tableLayout.RowCount = 1;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Configurer le panel pour qu'il reste centré dans le TableLayoutPanel
            this.Controls.Remove(panel1);
            panel1.Anchor = AnchorStyles.None;
            tableLayout.Controls.Add(panel1, 0, 0);
            this.Controls.Add(tableLayout);
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            // Appliquer les coins arrondis à tous les contrôles
            ApplyRoundedCornersToAll();
            panelMenu.Visible = true;
            panel1.Visible = true;
            random = new Random();
            MainTitleText.Location = new Point((PanelTitleBar.Width - MainTitleText.Width) / 2, (PanelTitleBar.Height - MainTitleText.Height) / 2);

            // Centrer le texte verticalement dans les TextBox
            CenterTextInAllTextBoxes();
        }

        // Suspendre le dessin lors du début du redimensionnement
        private void Form_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (panel1 != null)
            {
                panel1.Visible = false;
            }
        }

        // Reprendre le dessin à la fin du redimensionnement
        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (panel1 != null)
            {
                panel1.Visible = true;
            }
            this.ResumeLayout();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            // Le TableLayoutPanel s'occupe du centrage automatiquement,
            // donc nous n'avons pas besoin de code supplémentaire ici

            // Si on veut une animation fluide, on peut utiliser cette approche alternative :
            if (this.WindowState != FormWindowState.Minimized && panel1 != null && panel1.Visible)
            {
                this.Invalidate();
            }
        }

        private void ApplyRoundedCornersToAll()
        {
            // Appliquer les coins arrondis au panel principal
            ApplyRoundedCorners(panel1, 15);

            // Appliquer les coins arrondis aux TextBox et Button dans le panel
            foreach (Control c in panel1.Controls)
            {
                if (c is Button)
                {
                    ApplyRoundedCorners(c, 15);
                    // Définir des styles supplémentaires pour les boutons
                    ((Button)c).FlatStyle = FlatStyle.Flat;
                    ((Button)c).FlatAppearance.BorderSize = 0;
                }
                else if (c is TextBox)
                {
                    ApplyRoundedCorners(c, 10);
                }
            }

            // Appliquer les coins arrondis aux boutons du formulaire (hors panel)
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    ApplyRoundedCorners(c, 15);
                    // Définir des styles supplémentaires pour les boutons
                    ((Button)c).FlatStyle = FlatStyle.Flat;
                    ((Button)c).FlatAppearance.BorderSize = 0;
                }
            }
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control != null)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();

                    control.Region = new Region(path);
                }
            }
        }

        private void CenterTextInAllTextBoxes()
        {
            // Centrer le texte dans les TextBox du panel
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox)
                {
                    CenterTextInTextBox((TextBox)c);
                }
            }

            // Centrer le texte dans les TextBox du formulaire (hors panel)
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    CenterTextInTextBox((TextBox)c);
                }
            }
        }

        private void CenterTextInTextBox(TextBox textBox)
        {
            if (textBox != null)
            {
                // Configurer le TextBox pour le centrage vertical
                textBox.Multiline = true;

                // Récupérer la hauteur actuelle pour le calcul
                int height = textBox.Height;

                // Calculer le padding nécessaire pour centrer le texte
                int padding = Math.Max(0, (height - TextRenderer.MeasureText("Tg", textBox.Font).Height) / 2);

                // Structure RECT pour définir les marges
                RECT rect = new RECT();
                rect.Left = 5; // Marge gauche
                rect.Top = padding; // Marge supérieure
                rect.Right = textBox.ClientSize.Width - 5; // Marge droite
                rect.Bottom = height - padding; // Marge inférieure

                // Ajouter l'événement TextChanged pour recalculer lors de la modification
                textBox.TextChanged += (s, e) =>
                {
                    // Recalculer et appliquer les marges
                    SendMessage(textBox.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
                };

                // Appliquer immédiatement
                SendMessage(textBox.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
            }
        }

        // Structure pour le rectangle utilisé par SendMessage
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier que les champs requis sont remplis
                if (string.IsNullOrEmpty(textName.Text) || string.IsNullOrEmpty(textphone.Text))
                {
                    MessageBox.Show("Veuillez remplir tous les champs requis.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Stocker les informations de l'utilisateur
                userName = textName.Text.Trim();
                phoneNumber = FormatPhoneNumber(textphone.Text.Trim());

                // Envoyer une demande de vérification via Twilio Verify
                var verification = VerificationResource.Create(
                    to: phoneNumber,
                    channel: "sms",
                    pathServiceSid: serviceSid
                );

                // Vérifier l'état de la demande de vérification
                if (verification.Status == "pending")
                {
                    MessageBox.Show("Un code de vérification a été envoyé à votre téléphone. Veuillez le saisir pour continuer.",
                        "Code envoyé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Erreur lors de l'envoi du code. État: {verification.Status}",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textCheckPhone.Text) || string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Veuillez saisir le code reçu par SMS.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Vérifier le code OTP entré via Twilio Verify
                var verificationCheck = VerificationCheckResource.Create(
                    to: phoneNumber,
                    code: textCheckPhone.Text,
                    pathServiceSid: serviceSid
                );

                // Vérifier si le code est valide
                if (verificationCheck.Status == "approved")

                {
                    panelMenu.Visible = true;
                    panel1.Visible = false;
                    MessageBox.Show($"Bienvenue {userName}! Connexion réussie", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // Ajouter ici le code pour ouvrir le formulaire principal
                    // Par exemple:
                    // MainForm mainForm = new MainForm();
                    // mainForm.Show();
                    // this.Hide();
                }
                else
                {
                    MessageBox.Show("Code de vérification incorrect ou expiré.",
                        "Échec", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la vérification: {ex.Message}", "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Format un numéro de téléphone pour s'assurer qu'il est au format international E.164
        /// Cette méthode accepte des numéros de téléphone de n'importe quel pays
        /// </summary>
        private string FormatPhoneNumber(string input)
        {
            // Supprimer les espaces et autres caractères non numériques
            string digits = System.Text.RegularExpressions.Regex.Replace(input, @"[^\d+]", "");

            // Si le numéro commence déjà par +, le considérer comme correctement formaté
            if (digits.StartsWith("+"))
                return digits;

            // Si le numéro commence par 00 (format international alternatif), le convertir en format +
            if (digits.StartsWith("00"))
                return "+" + digits.Substring(2);

            // Si c'est un numéro sénégalais sans indicatif
            if (digits.StartsWith("0") && digits.Length == 10) // Si c'est un numéro qui commence par 0 et a 10 chiffres
                return "+221" + digits.Substring(1); // Retirer le 0 initial et ajouter +221 (Sénégal)

            if (digits.Length == 9) // Numéros sénégalais typiques sans 0 au début
                return "+221" + digits;

            // Si aucun des cas ci-dessus ne s'applique, supposer que le numéro est correctement formaté
            // mais ajouter un + s'il n'y en a pas
            if (!digits.StartsWith("+"))
                return "+" + digits;

            return digits;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Vous pouvez ajouter du code ici si nécessaire

        }

        private void textphone_TextChanged(object sender, EventArgs e)
        {
            // Vous pouvez ajouter du code ici si nécessaire

        }

        private void Classes_Click(object sender, EventArgs e)

        {
            OpenChildForm(new Forms.Classes(), sender);
        }

        private void Matieres_Click(object sender, EventArgs e)

        {
            OpenChildForm(new Forms.Matières(), sender);
        }

      /*  private void Etudiants_Click_2(object sender, EventArgs e)

        {
            OpenChildForm(new Forms.Etudiants(), sender);
        }*/

        private void Class_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Classes(), sender);
        }

        private void Cours_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Cours(), sender);
        }

        private void Professeurs_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Professeurs(), sender);
        }

        private void Notes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Notes(), sender);
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Dashboard(), sender);
        }

        private void Utilisateurs_Click(object sender, EventArgs e)

        {
            OpenChildForm(new Forms.Utilisateurs(), sender);
        }

        private void Déconnexion_Click(object sender, EventArgs e)
        {
            activateButton(sender);
        }

        private void Rapports_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Rapports(), sender);
        }

        private void home_Click(object sender, EventArgs e)
        {

        }

        private void PanelTitleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelDesktopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cours_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Cours(), sender);
        }
        private void Students_Click_1 (object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Students(), sender);
        }

        private void Classes_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Classes(), sender);
        }

        private void Matieres_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Matières(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

































































