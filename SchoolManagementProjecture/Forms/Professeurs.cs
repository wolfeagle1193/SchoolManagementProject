using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SchoolManagementProjecture.Forms
{
    public partial class Professeurs : Form
    {
        // Couleurs du thème
        private Color mainColor = Color.ForestGreen;       // Changé pour distinguer de la forme Etudiants
        private Color secondaryColor = Color.DarkGreen;    // Changé pour distinguer de la forme Etudiants
        private Color accentColor = Color.FromArgb(255, 255, 255);
        private Color bgColor = Color.FromArgb(245, 247, 250);
        private Color cardColor = Color.White;

        // Polices
        private Font regularFont = new Font("Segoe UI", 10);
        private Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private Font largeFont = new Font("Segoe UI", 12);
        private Font largeBoldFont = new Font("Segoe UI", 12, FontStyle.Bold);

        // Composants globaux
        private DataGridView professorsGridView;
        private Label totalProfesseursLabel;
        private Panel sidePanel;
        private Panel mainContentPanel;
        private Panel searchPanel;
        private Panel actionButtonsPanel;
        private Panel cardPanel;

        public Professeurs()
        {
            InitializeComponent();
            CustomizeDesign();
        }



        private void CustomizeDesign()
        {
            // Configuration de base du formulaire
            this.BackColor = bgColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(15);

            // Création du panneau principal
            mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = bgColor,
                Padding = new Padding(0, 0, 0, 15)
            };

            // Création de la carte principale
            cardPanel = CreateRoundedPanel(5);
            cardPanel.Dock = DockStyle.Fill;
            cardPanel.BackColor = cardColor;
            cardPanel.Padding = new Padding(20);

            // Création du panneau de recherche
            searchPanel = CreateSearchPanel();

            // Création du tableau des professeurs
            professorsGridView = CreateProfessorsDataGridView();

            // Création du panneau d'actions
            actionButtonsPanel = CreateActionButtonsPanel();

            // Assemblage des composants
            cardPanel.Controls.Add(professorsGridView);
            cardPanel.Controls.Add(actionButtonsPanel);
            cardPanel.Controls.Add(searchPanel);
            mainContentPanel.Controls.Add(cardPanel);
            this.Controls.Add(mainContentPanel);
        }

        private Panel CreateRoundedPanel(int radius)
        {
            Panel panel = new Panel();
            panel.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                    path.AddArc(panel.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
                    path.AddArc(panel.Width - radius * 2, panel.Height - radius * 2, radius * 2, radius * 2, 0, 90);
                    path.AddArc(0, panel.Height - radius * 2, radius * 2, radius * 2, 90, 90);
                    path.CloseAllFigures();
                    panel.Region = new Region(path);
                }
            };
            return panel;
        }

        private Panel CreateSearchPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = cardColor,
                Padding = new Padding(10)
            };

            // Création du conteneur de recherche arrondi
            Panel searchContainer = CreateRoundedPanel(20);
            searchContainer.Size = new Size(300, 40);
            searchContainer.Location = new Point(10, 15);
            searchContainer.BackColor = Color.FromArgb(235, 240, 245);
            searchContainer.Padding = new Padding(10, 0, 10, 0);

            // Icône de recherche (simulée avec un label)
            Label searchIcon = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 14),
                Size = new Size(30, 30),
                Location = new Point(10, 5),
                ForeColor = Color.FromArgb(150, 150, 150)
            };

            // Champ de recherche
            TextBox searchTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = regularFont,
                Size = new Size(240, 30),
                Location = new Point(40, 10),
                BackColor = Color.FromArgb(235, 240, 245),
                PlaceholderText = "Rechercher un professeur..."
            };

            searchContainer.Controls.Add(searchIcon);
            searchContainer.Controls.Add(searchTextBox);

            // Filtre ComboBox avec style moderne
            ComboBox filterComboBox = new ComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(330, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            filterComboBox.Items.AddRange(new object[] { "Par Nom", "Par ID", "Par Matière" });
            filterComboBox.SelectedIndex = 0;

            // ComboBox pour les matières
            ComboBox matiereComboBox = new ComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(500, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            matiereComboBox.Items.AddRange(new object[] { "Toutes les matières", "Mathématiques", "Physique", "Informatique", "Économie" });
            matiereComboBox.SelectedIndex = 0;

            // Bouton de tri
            ComboBox sortComboBox = new ComboBox
            {
                Size = new Size(180, 40),
                Location = new Point(670, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            sortComboBox.Items.AddRange(new object[] { "Trier par Nom", "Trier par ID", "Trier par Email" });
            sortComboBox.SelectedIndex = 0;

            // Bouton d'actualisation
            Button refreshButton = CreateModernButton("", Color.FromArgb(240, 240, 240), Color.Gray, 40, 40);
            refreshButton.Text = "🔄";
            refreshButton.Font = new Font("Segoe UI", 14);
            refreshButton.Location = new Point(870, 15);

            filterComboBox.SelectedIndexChanged += (sender, e) =>
            {
                matiereComboBox.Visible = filterComboBox.SelectedItem.ToString() == "Par Matière";
            };

            panel.Controls.AddRange(new Control[] { searchContainer, filterComboBox, matiereComboBox, sortComboBox, refreshButton });
            return panel;
        }

        private DataGridView CreateProfessorsDataGridView()
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = cardColor,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                RowTemplate = { Height = 50 }
            };

            // Style des en-têtes
            dgv.ColumnHeadersDefaultCellStyle.BackColor = mainColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = largeBoldFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgv.ColumnHeadersHeight = 50;

            // Style des cellules
            dgv.DefaultCellStyle.Font = regularFont;
            dgv.DefaultCellStyle.Padding = new Padding(10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 250, 230);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternance des couleurs des lignes
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 252, 249);

            // Création des colonnes
            dgv.Columns.Add("Id", "ID");
            dgv.Columns.Add("Nom", "Nom");
            dgv.Columns.Add("Prenom", "Prénom");
            dgv.Columns.Add("Email", "Email");
            dgv.Columns.Add("Telephone", "Téléphone");
            dgv.Columns.Add("Specialite", "Spécialité");
            dgv.Columns.Add("DateEmbauche", "Date d'embauche");

            // Définition des largeurs relatives
            /* dgv.Columns["Id"].Width = 100;
             dgv.Columns["Nom"].Width = 250;
             dgv.Columns["Prenom"].Width = 250;
             dgv.Columns["Telephone"].Width = 250;
             dgv.Columns["Specialite"].Width = 250;
             dgv.Columns["DateEmbauche"].Width = 250;
             dgv.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/
            // Configuration des largeurs des colonnes
            dgv.Columns["Id"].Width = 10; // Largeur fixe pour la colonne Id
            dgv.Columns["Nom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant
            dgv.Columns["Prenom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant
            dgv.Columns["Telephone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant
            dgv.Columns["Specialite"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant
            dgv.Columns["DateEmbauche"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant
            dgv.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Remplit l'espace restant

            // Ajout de données d'exemple
            dgv.Rows.Add("PROF001", "Camara", "Abdoulaye", "abdoulaye.camara@example.com", "77 123 45 67", "Mathématiques", "15/09/2018");
            dgv.Rows.Add("PROF002", "Diallo", "Mariama", "mariama.diallo@example.com", "76 234 56 78", "Physique", "01/10/2019");
            dgv.Rows.Add("PROF003", "Ba", "Moussa", "moussa.ba@example.com", "70 345 67 89", "Informatique", "20/02/2017");
            dgv.Rows.Add("PROF004", "Sall", "Aminata", "aminata.sall@example.com", "78 456 78 90", "Économie", "05/11/2020");
            dgv.Rows.Add("PROF005", "Mbaye", "Ibrahim", "ibrahim.mbaye@example.com", "77 567 89 01", "Droit", "18/03/2021");
            dgv.Rows.Add("PROF006", "ILIZABALIZA", "Honore", "honore.iliza@example.com", "77 567 89 01", "Droit", "18/03/2021");
            return dgv;
        }

        private Panel CreateActionButtonsPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = cardColor,
                Padding = new Padding(10)
            };

            // Bouton d'ajout
            Button addButton = CreateModernButton("+ Ajouter", mainColor, Color.White, 130, 45);
            addButton.Location = new Point(10, 12);
            addButton.Click += (sender, e) => ShowAddEditProfessorForm(false, null);

            // Bouton de modification
            Button editButton = CreateModernButton("✏️ Modifier", Color.FromArgb(70, 130, 80), Color.White, 130, 45);
            editButton.Location = new Point(150, 12);
            editButton.Click += (sender, e) =>
            {
                if (professorsGridView.SelectedRows.Count > 0)
                {
                    ShowAddEditProfessorForm(true, professorsGridView.SelectedRows[0]);
                }
                else
                {
                    ShowNotification("Veuillez sélectionner un professeur à modifier.");
                }
            };

            // Bouton de suppression
            Button deleteButton = CreateModernButton("🗑️ Supprimer", Color.FromArgb(220, 53, 69), Color.White, 130, 45);
            deleteButton.Location = new Point(290, 12);
            deleteButton.Click += (sender, e) =>
            {
                if (professorsGridView.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce professeur?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        professorsGridView.Rows.Remove(professorsGridView.SelectedRows[0]);
                        UpdateProfessorCount();
                        ShowNotification("Professeur supprimé avec succès.");
                    }
                }
                else
                {
                    ShowNotification("Veuillez sélectionner un professeur à supprimer.");
                }
            };

            // Bouton d'exportation
            Button exportButton = CreateModernButton("📊 Exporter", secondaryColor, Color.White, 130, 45);
            exportButton.Location = new Point(430, 12);

            // Étiquette pour l'effectif total
            totalProfesseursLabel = new Label
            {
                Text = "Effectif total: 5 professeurs",
                Font = boldFont,
                ForeColor = secondaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                Size = new Size(200, 30),
                Location = new Point(panel.Width - 220, 20)
            };
            panel.Layout += (sender, e) =>
            {
                totalProfesseursLabel.Location = new Point(panel.Width - 220, 20);
            };

            panel.Controls.AddRange(new Control[] { addButton, editButton, deleteButton, exportButton, totalProfesseursLabel });
            return panel;
        }

        private Button CreateModernButton(string text, Color bgColor, Color fgColor, int width, int height)
        {
            Button button = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = bgColor,
                ForeColor = fgColor,
                Font = boldFont,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            button.FlatAppearance.BorderSize = 0;

            // Arrondir les coins du bouton
            GraphicsPath path = new GraphicsPath();
            int radius = 10;
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(width - radius * 2, height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();

            button.Region = new Region(path);
            return button;
        }

        private void ShowAddEditProfessorForm(bool isEdit, DataGridViewRow selectedRow)
        {
            // Création du formulaire flottant
            Form addEditForm = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(620, 580),
                FormBorderStyle = FormBorderStyle.None,
                BackColor = bgColor,
                ShowInTaskbar = false
            };

            // Création du panneau principal du formulaire
            Panel mainPanel = CreateRoundedPanel(15);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = cardColor;
            mainPanel.Padding = new Padding(25);

            // En-tête du formulaire
            Label formTitle = new Label
            {
                Text = isEdit ? "Modifier le professeur" : "Ajouter un professeur",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = secondaryColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Bouton de fermeture
            Button closeButton = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 12),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand,
                Location = new Point(addEditForm.Width - 65, 25)
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (sender, e) => addEditForm.Close();

            // Création du panneau de défilement pour les champs
            Panel fieldsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 10, 0, 0)
            };

            // Création des champs du formulaire - utilisation de la méthode utilitaire
            int yPos = 10;

            // ID
            yPos = AddFormField(fieldsPanel, "ID", "idTextBox", yPos, isEdit);
            TextBox idTextBox = (TextBox)fieldsPanel.Controls["idTextBox"];

            // Nom
            yPos = AddFormField(fieldsPanel, "Nom", "nomTextBox", yPos);
            TextBox nomTextBox = (TextBox)fieldsPanel.Controls["nomTextBox"];

            // Prénom
            yPos = AddFormField(fieldsPanel, "Prénom", "prenomTextBox", yPos);
            TextBox prenomTextBox = (TextBox)fieldsPanel.Controls["prenomTextBox"];

            // Email
            yPos = AddFormField(fieldsPanel, "Email", "emailTextBox", yPos);
            TextBox emailTextBox = (TextBox)fieldsPanel.Controls["emailTextBox"];

            // Téléphone
            yPos = AddFormField(fieldsPanel, "Téléphone", "telephoneTextBox", yPos);
            TextBox telephoneTextBox = (TextBox)fieldsPanel.Controls["telephoneTextBox"];

            // Spécialité
            yPos = AddFormField(fieldsPanel, "Spécialité", "specialiteComboBox", yPos, isComboBox: true);
            ComboBox specialiteComboBox = (ComboBox)fieldsPanel.Controls["specialiteComboBox"];
            specialiteComboBox.Items.AddRange(new object[] { "Mathématiques", "Physique", "Informatique", "Économie", "Droit", "Langues", "Histoire-Géographie" });
            specialiteComboBox.SelectedIndex = 0;

            // Date d'embauche
            yPos = AddFormField(fieldsPanel, "Date d'embauche", "dateEmbauchePicker", yPos, isDatePicker: true);
            DateTimePicker dateEmbauchePicker = (DateTimePicker)fieldsPanel.Controls["dateEmbauchePicker"];

            // Panel pour les boutons d'action
            Panel buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = cardColor
            };

            // Bouton d'enregistrement
            Button saveButton = CreateModernButton("Enregistrer", mainColor, Color.White, 150, 45);
            saveButton.Location = new Point(buttonsPanel.Width / 2 - 160, 13);
            saveButton.Click += (sender, e) =>
            {
                // Ici, vous ajouteriez la logique de validation et d'enregistrement
                ShowNotification(isEdit ? "Professeur modifié avec succès." : "Professeur ajouté avec succès.");
                addEditForm.Close();
            };

            // Bouton d'annulation
            Button cancelButton = CreateModernButton("Annuler", Color.FromArgb(108, 117, 125), Color.White, 150, 45);
            cancelButton.Location = new Point(buttonsPanel.Width / 2 + 10, 13);
            cancelButton.Click += (sender, e) => addEditForm.Close();

            // Ajuster la position des boutons lors du redimensionnement
            buttonsPanel.Layout += (sender, e) =>
            {
                saveButton.Location = new Point(buttonsPanel.Width / 2 - 160, 13);
                cancelButton.Location = new Point(buttonsPanel.Width / 2 + 10, 13);
            };

            // Si c'est une modification, remplir les champs avec les données existantes
            if (isEdit && selectedRow != null)
            {
                idTextBox.Text = selectedRow.Cells["Id"].Value.ToString();
                nomTextBox.Text = selectedRow.Cells["Nom"].Value.ToString();
                prenomTextBox.Text = selectedRow.Cells["Prenom"].Value.ToString();
                emailTextBox.Text = selectedRow.Cells["Email"].Value.ToString();
                telephoneTextBox.Text = selectedRow.Cells["Telephone"].Value.ToString();
                specialiteComboBox.Text = selectedRow.Cells["Specialite"].Value.ToString();
                dateEmbauchePicker.Value = DateTime.Parse(selectedRow.Cells["DateEmbauche"].Value.ToString());
            }

            // Assemblage du formulaire
            buttonsPanel.Controls.AddRange(new Control[] { saveButton, cancelButton });
            mainPanel.Controls.AddRange(new Control[] { formTitle, closeButton, fieldsPanel, buttonsPanel });
            addEditForm.Controls.Add(mainPanel);

            // Rendre le formulaire déplaçable
            MakeDraggable(addEditForm, mainPanel);

            // Afficher le formulaire en modal
            addEditForm.ShowDialog();
        }

        private int AddFormField(Panel panel, string labelText, string controlName, int yPos, bool isReadOnly = false, bool isComboBox = false, bool isDatePicker = false, bool isMultiline = false)
        {
            // Création du label
            Label label = new Label
            {
                Text = labelText,
                Font = regularFont,
                Size = new Size(120, 25),
                Location = new Point(0, yPos + 10),
                ForeColor = Color.FromArgb(90, 90, 90)
            };
            panel.Controls.Add(label);

            // Création du contrôle approprié
            Control control;

            if (isComboBox)
            {
                control = new ComboBox
                {
                    Name = controlName,
                    Size = new Size(350, 35),
                    Location = new Point(150, yPos),
                    Font = regularFont,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
            }
            else if (isDatePicker)
            {
                control = new DateTimePicker
                {
                    Name = controlName,
                    Size = new Size(350, 35),
                    Location = new Point(150, yPos),
                    Font = regularFont,
                    Format = DateTimePickerFormat.Short
                };
            }
            else
            {
                TextBox textBox = new TextBox
                {
                    Name = controlName,
                    Size = new Size(350, isMultiline ? 80 : 35),
                    Location = new Point(150, yPos),
                    Font = regularFont,
                    ReadOnly = isReadOnly,
                    Multiline = isMultiline,
                    BorderStyle = BorderStyle.FixedSingle
                };

                if (isMultiline)
                {
                    textBox.ScrollBars = ScrollBars.Vertical;
                }

                control = textBox;
            }

            panel.Controls.Add(control);

            // Retourner la nouvelle position y pour le prochain champ
            return yPos + (isMultiline ? 100 : 50);
        }

        private void MakeDraggable(Form form, Control control)
        {
            bool isDragging = false;
            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;

            control.MouseDown += (sender, e) =>
            {
                isDragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = form.Location;
            };

            control.MouseMove += (sender, e) =>
            {
                if (isDragging)
                {
                    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    form.Location = Point.Add(dragFormPoint, new Size(dif));
                }
            };

            control.MouseUp += (sender, e) =>
            {
                isDragging = false;
            };
        }

        private void ShowNotification(string message)
        {
            // Création d'une notification toast personnalisée
            Form notificationForm = new Form
            {
                Size = new Size(300, 70),
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true
            };

            // Positionner la notification en bas à droite de l'écran
            notificationForm.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - notificationForm.Width - 20,
                Screen.PrimaryScreen.WorkingArea.Height - notificationForm.Height - 20
            );

            // Créer un panneau arrondi pour la notification
            Panel notificationPanel = CreateRoundedPanel(10);
            notificationPanel.Dock = DockStyle.Fill;
            notificationPanel.BackColor = Color.FromArgb(50, 50, 50);
            notificationPanel.Padding = new Padding(15);

            // Label du message
            Label messageLabel = new Label
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            notificationPanel.Controls.Add(messageLabel);
            notificationForm.Controls.Add(notificationPanel);

            // Afficher la notification et la faire disparaître après 3 secondes
            notificationForm.Show();
            // Timer timer = new Timer();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                notificationForm.Close();
                notificationForm.Dispose();
            };
            timer.Start();
        }

        private void UpdateProfessorCount()
        {
            if (totalProfesseursLabel != null && professorsGridView != null)
            {
                totalProfesseursLabel.Text = $"Effectif total: {professorsGridView.Rows.Count} professeurs";
            }
        }

        private void Professeurs_Load(object sender, EventArgs e)
        {
            // Code à exécuter lors du chargement du formulaire
            UpdateProfessorCount();
        }
    }
}
