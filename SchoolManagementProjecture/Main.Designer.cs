namespace SchoolManagementProjecture
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textName = new TextBox();
            textCheckPhone = new TextBox();
            textphone = new TextBox();
            btnLogin = new Button();
            BtnCheckOTP = new Button();
            panel1 = new Panel();
            label4 = new Label();
            panelMenu = new Panel();
            Déconnexion = new Button();
            Utilisateurs = new Button();
            Rapports = new Button();
            Notes = new Button();
            Professeurs = new Button();
            Matieres = new Button();
            Cours = new Button();
            Classes = new Button();
            Etudiants = new Button();
            Dashboard = new Button();
            panelLogo = new Panel();
            PanelTitleBar = new Panel();
            MainTitleText = new Label();
            PanelDesktopPanel = new Panel();
            panel1.SuspendLayout();
            panelMenu.SuspendLayout();
            PanelTitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(73, 72);
            label1.Name = "label1";
            label1.Size = new Size(53, 25);
            label1.TabIndex = 0;
            label1.Text = "Nom";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(71, 179);
            label2.Name = "label2";
            label2.Size = new Size(198, 25);
            label2.TabIndex = 0;
            label2.Text = "Numéro de Téléphone";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(71, 381);
            label3.Name = "label3";
            label3.Size = new Size(170, 25);
            label3.TabIndex = 0;
            label3.Text = "Code de Validation";
            // 
            // textName
            // 
            textName.BackColor = SystemColors.GradientInactiveCaption;
            textName.BorderStyle = BorderStyle.None;
            textName.Font = new Font("Segoe UI", 14F);
            textName.Location = new Point(79, 102);
            textName.Multiline = true;
            textName.Name = "textName";
            textName.Size = new Size(420, 60);
            textName.TabIndex = 1;
            // 
            // textCheckPhone
            // 
            textCheckPhone.BackColor = SystemColors.GradientActiveCaption;
            textCheckPhone.BorderStyle = BorderStyle.None;
            textCheckPhone.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textCheckPhone.Location = new Point(71, 417);
            textCheckPhone.Multiline = true;
            textCheckPhone.Name = "textCheckPhone";
            textCheckPhone.Size = new Size(207, 60);
            textCheckPhone.TabIndex = 1;
            // 
            // textphone
            // 
            textphone.BackColor = SystemColors.GradientInactiveCaption;
            textphone.BorderStyle = BorderStyle.None;
            textphone.Cursor = Cursors.IBeam;
            textphone.Font = new Font("Segoe UI", 14F);
            textphone.Location = new Point(78, 210);
            textphone.Multiline = true;
            textphone.Name = "textphone";
            textphone.Size = new Size(420, 60);
            textphone.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.MidnightBlue;
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = SystemColors.ButtonHighlight;
            btnLogin.Location = new Point(177, 301);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(220, 60);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Envoyer OTP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += button1_Click;
            // 
            // BtnCheckOTP
            // 
            BtnCheckOTP.BackColor = Color.MidnightBlue;
            BtnCheckOTP.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnCheckOTP.ForeColor = SystemColors.ButtonHighlight;
            BtnCheckOTP.Location = new Point(352, 417);
            BtnCheckOTP.Name = "BtnCheckOTP";
            BtnCheckOTP.Size = new Size(142, 60);
            BtnCheckOTP.TabIndex = 3;
            BtnCheckOTP.Text = "Vérifier";
            BtnCheckOTP.UseVisualStyleBackColor = false;
            BtnCheckOTP.Click += button1_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.CornflowerBlue;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textCheckPhone);
            panel1.Controls.Add(BtnCheckOTP);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textName);
            panel1.Controls.Add(textphone);
            panel1.Location = new Point(359, 53);
            panel1.Name = "panel1";
            panel1.Size = new Size(589, 509);
            panel1.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.HighlightText;
            label4.Location = new Point(135, 26);
            label4.Name = "label4";
            label4.Size = new Size(314, 32);
            label4.TabIndex = 4;
            label4.Text = "Authentification Sécurisée";
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.CornflowerBlue;
            panelMenu.Controls.Add(Déconnexion);
            panelMenu.Controls.Add(Utilisateurs);
            panelMenu.Controls.Add(Rapports);
            panelMenu.Controls.Add(Notes);
            panelMenu.Controls.Add(Professeurs);
            panelMenu.Controls.Add(Matieres);
            panelMenu.Controls.Add(Cours);
            panelMenu.Controls.Add(Classes);
            panelMenu.Controls.Add(Etudiants);
            panelMenu.Controls.Add(Dashboard);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 669);
            panelMenu.TabIndex = 5;
            // 
            // Déconnexion
            // 
            Déconnexion.Dock = DockStyle.Bottom;
            Déconnexion.FlatAppearance.BorderSize = 0;
            Déconnexion.FlatStyle = FlatStyle.Flat;
            Déconnexion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Déconnexion.ForeColor = Color.MidnightBlue;
            Déconnexion.Image = (Image)resources.GetObject("Déconnexion.Image");
            Déconnexion.ImageAlign = ContentAlignment.MiddleLeft;
            Déconnexion.Location = new Point(0, 609);
            Déconnexion.Name = "Déconnexion";
            Déconnexion.Padding = new Padding(20, 0, 0, 0);
            Déconnexion.Size = new Size(220, 60);
            Déconnexion.TabIndex = 15;
            Déconnexion.Text = "  Déconnexion";
            Déconnexion.TextImageRelation = TextImageRelation.ImageBeforeText;
            Déconnexion.UseVisualStyleBackColor = true;
            Déconnexion.Click += Déconnexion_Click;
            // 
            // Utilisateurs
            // 
            Utilisateurs.Dock = DockStyle.Top;
            Utilisateurs.FlatAppearance.BorderSize = 0;
            Utilisateurs.FlatStyle = FlatStyle.Flat;
            Utilisateurs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Utilisateurs.ForeColor = Color.MidnightBlue;
            Utilisateurs.Image = (Image)resources.GetObject("Utilisateurs.Image");
            Utilisateurs.ImageAlign = ContentAlignment.MiddleLeft;
            Utilisateurs.Location = new Point(0, 560);
            Utilisateurs.Name = "Utilisateurs";
            Utilisateurs.Padding = new Padding(20, 0, 0, 0);
            Utilisateurs.Size = new Size(220, 60);
            Utilisateurs.TabIndex = 14;
            Utilisateurs.Text = "  Utilisateurs";
            Utilisateurs.TextImageRelation = TextImageRelation.ImageBeforeText;
            Utilisateurs.UseVisualStyleBackColor = true;
            Utilisateurs.Click += Utilisateurs_Click;
            // 
            // Rapports
            // 
            Rapports.Dock = DockStyle.Top;
            Rapports.FlatAppearance.BorderSize = 0;
            Rapports.FlatStyle = FlatStyle.Flat;
            Rapports.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Rapports.ForeColor = Color.MidnightBlue;
            Rapports.Image = (Image)resources.GetObject("Rapports.Image");
            Rapports.ImageAlign = ContentAlignment.MiddleLeft;
            Rapports.Location = new Point(0, 500);
            Rapports.Name = "Rapports";
            Rapports.Padding = new Padding(20, 0, 0, 0);
            Rapports.Size = new Size(220, 60);
            Rapports.TabIndex = 13;
            Rapports.Text = "  Rapports";
            Rapports.TextImageRelation = TextImageRelation.ImageBeforeText;
            Rapports.UseVisualStyleBackColor = true;
            Rapports.Click += Rapports_Click;
            // 
            // Notes
            // 
            Notes.Dock = DockStyle.Top;
            Notes.FlatAppearance.BorderSize = 0;
            Notes.FlatStyle = FlatStyle.Flat;
            Notes.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Notes.ForeColor = Color.MidnightBlue;
            Notes.Image = (Image)resources.GetObject("Notes.Image");
            Notes.ImageAlign = ContentAlignment.MiddleLeft;
            Notes.Location = new Point(0, 440);
            Notes.Name = "Notes";
            Notes.Padding = new Padding(20, 0, 0, 0);
            Notes.Size = new Size(220, 60);
            Notes.TabIndex = 12;
            Notes.Text = "  Notes";
            Notes.TextImageRelation = TextImageRelation.ImageBeforeText;
            Notes.UseVisualStyleBackColor = true;
            Notes.Click += Notes_Click;
            // 
            // Professeurs
            // 
            Professeurs.Dock = DockStyle.Top;
            Professeurs.FlatAppearance.BorderSize = 0;
            Professeurs.FlatStyle = FlatStyle.Flat;
            Professeurs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Professeurs.ForeColor = Color.MidnightBlue;
            Professeurs.Image = Properties.Resources.teacher__1__imresizer;
            Professeurs.ImageAlign = ContentAlignment.MiddleLeft;
            Professeurs.Location = new Point(0, 380);
            Professeurs.Name = "Professeurs";
            Professeurs.Padding = new Padding(20, 0, 0, 0);
            Professeurs.Size = new Size(220, 60);
            Professeurs.TabIndex = 11;
            Professeurs.Text = "Professeurs";
            Professeurs.TextImageRelation = TextImageRelation.ImageBeforeText;
            Professeurs.UseVisualStyleBackColor = true;
            Professeurs.Click += Professeurs_Click;
            // 
            // Matieres
            // 
            Matieres.Dock = DockStyle.Top;
            Matieres.FlatAppearance.BorderSize = 0;
            Matieres.FlatStyle = FlatStyle.Flat;
            Matieres.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Matieres.ForeColor = Color.MidnightBlue;
            Matieres.Image = (Image)resources.GetObject("Matieres.Image");
            Matieres.ImageAlign = ContentAlignment.MiddleLeft;
            Matieres.Location = new Point(0, 320);
            Matieres.Name = "Matieres";
            Matieres.Padding = new Padding(20, 0, 0, 0);
            Matieres.Size = new Size(220, 60);
            Matieres.TabIndex = 10;
            Matieres.Text = "Matières";
            Matieres.TextImageRelation = TextImageRelation.ImageBeforeText;
            Matieres.UseVisualStyleBackColor = true;
            Matieres.Click += Matieres_Click;
            // 
            // Cours
            // 
            Cours.Dock = DockStyle.Top;
            Cours.FlatAppearance.BorderSize = 0;
            Cours.FlatStyle = FlatStyle.Flat;
            Cours.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Cours.ForeColor = Color.MidnightBlue;
            Cours.Image = Properties.Resources.cours_imresizer;
            Cours.ImageAlign = ContentAlignment.MiddleLeft;
            Cours.Location = new Point(0, 260);
            Cours.Name = "Cours";
            Cours.Padding = new Padding(20, 0, 0, 0);
            Cours.Size = new Size(220, 60);
            Cours.TabIndex = 9;
            Cours.Text = "Cours";
            Cours.TextImageRelation = TextImageRelation.ImageBeforeText;
            Cours.UseVisualStyleBackColor = true;
            Cours.Click += Cours_Click_1;
            // 
            // Classes
            // 
            Classes.Dock = DockStyle.Top;
            Classes.FlatAppearance.BorderSize = 0;
            Classes.FlatStyle = FlatStyle.Flat;
            Classes.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Classes.ForeColor = Color.MidnightBlue;
            Classes.Image = Properties.Resources.classees;
            Classes.ImageAlign = ContentAlignment.MiddleLeft;
            Classes.Location = new Point(0, 200);
            Classes.Name = "Classes";
            Classes.Padding = new Padding(20, 0, 0, 0);
            Classes.Size = new Size(220, 60);
            Classes.TabIndex = 8;
            Classes.Text = "Classes";
            Classes.TextImageRelation = TextImageRelation.ImageBeforeText;
            Classes.UseVisualStyleBackColor = true;
            Classes.Click += Classes_Click_1;
            // 
            // Etudiants
            // 
            Etudiants.Dock = DockStyle.Top;
            Etudiants.FlatAppearance.BorderSize = 0;
            Etudiants.FlatStyle = FlatStyle.Flat;
            Etudiants.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Etudiants.ForeColor = Color.MidnightBlue;
            Etudiants.Image = Properties.Resources.students_imresizer;
            Etudiants.ImageAlign = ContentAlignment.MiddleLeft;
            Etudiants.Location = new Point(0, 140);
            Etudiants.Name = "Etudiants";
            Etudiants.Padding = new Padding(20, 0, 0, 0);
            Etudiants.Size = new Size(220, 60);
            Etudiants.TabIndex = 7;
            Etudiants.Text = "Etudiants";
            Etudiants.TextImageRelation = TextImageRelation.ImageBeforeText;
            Etudiants.UseVisualStyleBackColor = true;
            Etudiants.Click += Students_Click_1;
            // 
            // Dashboard
            // 
            Dashboard.Dock = DockStyle.Top;
            Dashboard.FlatAppearance.BorderSize = 0;
            Dashboard.FlatStyle = FlatStyle.Flat;
            Dashboard.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Dashboard.ForeColor = Color.MidnightBlue;
            Dashboard.Image = (Image)resources.GetObject("Dashboard.Image");
            Dashboard.ImageAlign = ContentAlignment.MiddleLeft;
            Dashboard.Location = new Point(0, 80);
            Dashboard.Name = "Dashboard";
            Dashboard.Padding = new Padding(20, 0, 0, 0);
            Dashboard.Size = new Size(220, 60);
            Dashboard.TabIndex = 6;
            Dashboard.Text = "Dashboard";
            Dashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            Dashboard.UseVisualStyleBackColor = true;
            Dashboard.Click += Dashboard_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.MidnightBlue;
            panelLogo.BackgroundImage = (Image)resources.GetObject("panelLogo.BackgroundImage");
            panelLogo.BackgroundImageLayout = ImageLayout.Center;
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(220, 80);
            panelLogo.TabIndex = 6;
            // 
            // PanelTitleBar
            // 
            PanelTitleBar.BackColor = Color.CornflowerBlue;
            PanelTitleBar.Controls.Add(MainTitleText);
            PanelTitleBar.Dock = DockStyle.Top;
            PanelTitleBar.Location = new Point(220, 0);
            PanelTitleBar.Name = "PanelTitleBar";
            PanelTitleBar.Size = new Size(1103, 80);
            PanelTitleBar.TabIndex = 6;
            PanelTitleBar.Paint += PanelTitleBar_Paint;
            // 
            // MainTitleText
            // 
            MainTitleText.Anchor = AnchorStyles.None;
            MainTitleText.AutoSize = true;
            MainTitleText.BackColor = Color.Transparent;
            MainTitleText.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MainTitleText.ForeColor = Color.GhostWhite;
            MainTitleText.Location = new Point(496, 12);
            MainTitleText.Name = "MainTitleText";
            MainTitleText.Size = new Size(111, 38);
            MainTitleText.TabIndex = 0;
            MainTitleText.Text = "Accueil";
            MainTitleText.TextAlign = ContentAlignment.MiddleCenter;
            MainTitleText.Click += home_Click;
            // 
            // PanelDesktopPanel
            // 
            PanelDesktopPanel.Dock = DockStyle.Fill;
            PanelDesktopPanel.Location = new Point(220, 80);
            PanelDesktopPanel.Name = "PanelDesktopPanel";
            PanelDesktopPanel.Size = new Size(1103, 589);
            PanelDesktopPanel.TabIndex = 7;
            PanelDesktopPanel.Paint += PanelDesktopPanel_Paint;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.africa_graduation;
            ClientSize = new Size(1323, 669);
            Controls.Add(PanelDesktopPanel);
            Controls.Add(PanelTitleBar);
            Controls.Add(panelMenu);
            Controls.Add(panel1);
            Name = "Main";
            Text = "Main";
            Load += loginForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelMenu.ResumeLayout(false);
            PanelTitleBar.ResumeLayout(false);
            PanelTitleBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textName;
        private TextBox textCheckPhone;
        private TextBox textphone;
        private Button btnLogin;
        private Button BtnCheckOTP;
        private Panel panel1;
        private Label label4;
        private Panel panelMenu;
        private Button Dashboard;
        private Button Rapports;
        private Button Notes;
        private Button Professeurs;
        private Button Matieres;
        private Button Cours;
        private Button Classes;
        private Button Etudiants;
        private Button Utilisateurs;
        private Panel panelLogo;
        private Button Déconnexion;
        private Panel PanelTitleBar;
        private Label MainTitleText;
        private Panel PanelDesktopPanel;
    }
}