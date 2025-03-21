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
    public partial class Students : Form
    {
        // Couleurs du thème
        private Color mainColor = Color.CornflowerBlue;
        private Color secondaryColor = Color.MidnightBlue;
        private Color accentColor = Color.FromArgb(255, 255, 255);
        private Color bgColor = Color.FromArgb(245, 247, 250);
        private Color cardColor = Color.White;

        // Polices
        private Font regularFont = new Font("Segoe UI", 10);
        private Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private Font largeFont = new Font("Segoe UI", 12);
        private Font largeBoldFont = new Font("Segoe UI", 12, FontStyle.Bold);

        // Composants globaux
        private DataGridView studentsGridView;
        private Label totalEtudiantsLabel;
        private Panel sidePanel;
        private Panel mainContentPanel;
        private Panel searchPanel;
        private Panel actionButtonsPanel;
        private Panel cardPanel;

        // Variables de pagination
        private int currentPage = 1;
        private int rowsPerPage = 10; // Nombre d'éléments par page (variable dynamique)
        private int totalPages = 1;
        private List<DataGridViewRow> allRows = new List<DataGridViewRow>();
        private Panel paginationPanel;

        public Students()
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

            // Création du tableau des étudiants
            studentsGridView = CreateStudentsDataGridView();

            // Création du panneau d'actions
            actionButtonsPanel = CreateActionButtonsPanel();


            // Création du panneau de pagination
            paginationPanel = CreatePaginationPanel();

            // Assemblage des composants
            cardPanel.Controls.Add(studentsGridView);
            cardPanel.Controls.Add(actionButtonsPanel);
            cardPanel.Controls.Add(paginationPanel);
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

            // Amélioration de l'icône de recherche
            // Option 1: Utiliser une PictureBox au lieu d'un Label
            PictureBox searchIcon = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

          

            // Champ de recherche - ajusté pour mieux s'aligner avec l'icône
            TextBox searchTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = regularFont,
                Size = new Size(240, 30),
                Location = new Point(40, 10),
                BackColor = Color.FromArgb(235, 240, 245),
                PlaceholderText = "Rechercher un étudiant..."
            };

            searchContainer.Controls.Add(searchIcon);
            searchContainer.Controls.Add(searchTextBox);

            // Reste du code inchangé
            ComboBox filterComboBox = new ComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(330, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            filterComboBox.Items.AddRange(new object[] { "Par Nom", "Par Matricule", "Par Classe" });
            filterComboBox.SelectedIndex = 0;

            ComboBox classeComboBox = new ComboBox
            {
                Size = new Size(150, 40),
                Location = new Point(500, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            classeComboBox.Items.AddRange(new object[] { "Toutes les classes", "L1", "L2", "L3" });
            classeComboBox.SelectedIndex = 0;

            ComboBox sortComboBox = new ComboBox
            {
                Size = new Size(180, 40),
                Location = new Point(670, 15),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            sortComboBox.Items.AddRange(new object[] { "Trier par Nom", "Trier par Matricule", "Trier par Résultats" });
            sortComboBox.SelectedIndex = 0;

            Button refreshButton = CreateModernButton("", Color.FromArgb(240, 240, 240), Color.Gray, 40, 40);
            refreshButton.Text = "🔄";
            refreshButton.Font = new Font("Segoe UI", 14);
            refreshButton.Location = new Point(870, 15);

            filterComboBox.SelectedIndexChanged += (sender, e) =>
            {
                classeComboBox.Visible = filterComboBox.SelectedItem.ToString() == "Par Classe";
            };

            panel.Controls.AddRange(new Control[] { searchContainer, filterComboBox, classeComboBox, sortComboBox, refreshButton });
            return panel;
        }

        private DataGridView CreateStudentsDataGridView()
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
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternance des couleurs des lignes
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 251, 253);

            // Création des colonnes avec AutoSizeMode personnalisé pour chaque colonne
            DataGridViewTextBoxColumn matriculeCol = new DataGridViewTextBoxColumn
            {
                Name = "Matricule",
                HeaderText = "Matricule",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 150
            };

            DataGridViewTextBoxColumn nomCol = new DataGridViewTextBoxColumn
            {
                Name = "Nom",
                HeaderText = "Nom",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 250
            };

            DataGridViewTextBoxColumn prenomCol = new DataGridViewTextBoxColumn
            {
                Name = "Prenom",
                HeaderText = "Prénom",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 250
            };

            DataGridViewTextBoxColumn dateNaissanceCol = new DataGridViewTextBoxColumn
            {
                Name = "DateNaissance",
                HeaderText = "Date de Naissance",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 200
            };

            DataGridViewTextBoxColumn sexeCol = new DataGridViewTextBoxColumn
            {
                Name = "Sexe",
                HeaderText = "Sexe",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 150
            };

            DataGridViewTextBoxColumn classeCol = new DataGridViewTextBoxColumn
            {
                Name = "Classe",
                HeaderText = "Classe",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 120
            };

            DataGridViewTextBoxColumn emailCol = new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,  // Remplira l'espace restant

            };

            DataGridViewTextBoxColumn telephoneCol = new DataGridViewTextBoxColumn
            {
                Name = "Telephone",
                HeaderText = "Téléphone",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 200
            };

            // Ajout des colonnes
            dgv.Columns.AddRange(new DataGridViewColumn[] {
        matriculeCol, nomCol, prenomCol, dateNaissanceCol,
        sexeCol, classeCol, emailCol, telephoneCol
    });

            // Ajout de données d'exemple
            dgv.Rows.Add("MAT001", "Diop", "Mamadou", "12/05/2002", "M", "L1", "mamadou.diop@example.com", "77 123 45 67");
            dgv.Rows.Add("MAT002", "Ndiaye", "Fatou", "23/09/2003", "F", "L2", "fatou.ndiaye@example.com", "76 234 56 78");
            dgv.Rows.Add("MAT003", "Sow", "Ousmane", "05/11/2001", "M", "L1", "ousmane.sow@example.com", "70 345 67 89");
            dgv.Rows.Add("MAT004", "Fall", "Aminata", "17/03/2002", "F", "L3", "aminata.fall@example.com", "78 456 78 90");
            dgv.Rows.Add("MAT005", "Gueye", "Ibrahima", "29/07/2003", "M", "L2", "ibrahima.gueye@example.com", "77 567 89 01");
            dgv.Rows.Add("MAT006", "Diop", "Mamadou", "12/05/2002", "M", "L1", "mamadou.diop@example.com", "77 123 45 67");
            dgv.Rows.Add("MAT007", "Ndiaye", "Fatou", "23/09/2003", "F", "L2", "fatou.ndiaye@example.com", "76 234 56 78");
            dgv.Rows.Add("MAT008", "Sow", "Ousmane", "05/11/2001", "M", "L1", "ousmane.sow@example.com", "70 345 67 89");
            dgv.Rows.Add("MAT009", "Fall", "Aminata", "17/03/2002", "F", "L3", "aminata.fall@example.com", "78 456 78 90");
            dgv.Rows.Add("MAT0010", "Gueye", "Ibrahima", "29/07/2003", "M", "L2", "ibrahima.gueye@example.com", "77 567 89 01");

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
            addButton.Click += (sender, e) => ShowAddEditStudentForm(false, null);

            // Bouton de modification
            Button editButton = CreateModernButton("✏️ Modifier", Color.FromArgb(70, 130, 180), Color.White, 130, 45);
            editButton.Location = new Point(150, 12);
            editButton.Click += (sender, e) =>
            {
                if (studentsGridView.SelectedRows.Count > 0)
                {
                    ShowAddEditStudentForm(true, studentsGridView.SelectedRows[0]);
                }
                else
                {
                    ShowNotification("Veuillez sélectionner un étudiant à modifier.");
                }
            };

            // Bouton de suppression
            Button deleteButton = CreateModernButton("🗑️ Supprimer", Color.FromArgb(220, 53, 69), Color.White, 130, 45);
            deleteButton.Location = new Point(290, 12);
              deleteButton.Click += (sender, e) =>
              {
                  if (studentsGridView.SelectedRows.Count > 0)
                  {
                      if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet étudiant?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                      {
                          studentsGridView.Rows.Remove(studentsGridView.SelectedRows[0]);
                          UpdateStudentCount();
                          ShowNotification("Étudiant supprimé avec succès.");
                      }
                  }
                  else
                  {
                      ShowNotification("Veuillez sélectionner un étudiant à supprimer.");
                  }
              };
            //update new deletebutton
            deleteButton.Click += (sender, e) =>
            {
                if (studentsGridView.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet étudiant?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Supprimer de la liste principale
                        int index = -1;
                        string matricule = studentsGridView.SelectedRows[0].Cells["Matricule"].Value.ToString();

                        for (int i = 0; i < allRows.Count; i++)
                        {
                            if (allRows[i].Cells["Matricule"].Value.ToString() == matricule)
                            {
                                index = i;
                                break;
                            }
                        }

                        if (index != -1)
                            allRows.RemoveAt(index);

                        // Supprimer de la vue actuelle
                        studentsGridView.Rows.Remove(studentsGridView.SelectedRows[0]);

                        // Mettre à jour la pagination
                        UpdatePagination();
                        ShowNotification("Étudiant supprimé avec succès.");
                    }
                }
                else
                {
                    ShowNotification("Veuillez sélectionner un étudiant à supprimer.");
                }
            };

            // Bouton d'exportation
            Button exportButton = CreateModernButton("📊 Exporter", secondaryColor, Color.White, 130, 45);
            exportButton.Location = new Point(430, 12);

            // Étiquette pour l'effectif total
            totalEtudiantsLabel = new Label
            {
                Text = "Effectif total: 5 étudiants",
                Font = boldFont,
                ForeColor = secondaryColor,
                TextAlign = ContentAlignment.MiddleRight,
                Size = new Size(200, 30),
                Location = new Point(panel.Width - 220, 20)
            };
            panel.Layout += (sender, e) =>
            {
                totalEtudiantsLabel.Location = new Point(panel.Width - 220, 20);
            };

            panel.Controls.AddRange(new Control[] { addButton, editButton, deleteButton, exportButton, totalEtudiantsLabel });
            return panel;
        }

        //paginationnew
        private Panel CreatePaginationPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = cardColor
            };

            // Créer les contrôles de pagination
            Label pageInfoLabel = new Label
            {
                Name = "pageInfoLabel",
                Text = $"Page {currentPage} sur {totalPages}",
                Font = regularFont,
                Size = new Size(150, 30),
                Location = new Point(panel.Width / 2 - 75, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Créer le bouton précédent
            Button prevButton = CreateModernButton("◀", Color.FromArgb(240, 240, 240), Color.Gray, 40, 40);
            prevButton.Name = "prevButton";
            prevButton.Location = new Point(pageInfoLabel.Left - 50, 5);
            prevButton.Click += (sender, e) => ChangePage(currentPage - 1);
            prevButton.Enabled = currentPage > 1;

            // Créer le bouton suivant
            Button nextButton = CreateModernButton("▶", Color.FromArgb(240, 240, 240), Color.Gray, 40, 40);
            nextButton.Name = "nextButton";
            nextButton.Location = new Point(pageInfoLabel.Right + 10, 5);
            nextButton.Click += (sender, e) => ChangePage(currentPage + 1);
            nextButton.Enabled = currentPage < totalPages;

            // Créer le ComboBox pour le nombre d'éléments par page
            ComboBox rowsPerPageComboBox = new ComboBox
            {
                Name = "rowsPerPageComboBox",
                Size = new Size(80, 30),
                Location = new Point(panel.Width - 220, 10),
                Font = regularFont,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            rowsPerPageComboBox.Items.AddRange(new object[] { 5, 10, 20, 50, 100 });
            rowsPerPageComboBox.SelectedItem = rowsPerPage;
            rowsPerPageComboBox.SelectedIndexChanged += (sender, e) =>
            {
                rowsPerPage = (int)rowsPerPageComboBox.SelectedItem;
                UpdatePagination();
            };

            // Ajouter un label pour le combobox
            Label rowsPerPageLabel = new Label
            {
                Text = "Lignes par page:",
                Font = regularFont,
                Size = new Size(120, 30),
                Location = new Point(rowsPerPageComboBox.Left - 130, 10),
                TextAlign = ContentAlignment.MiddleRight
            };

            // Ajustement de la position des contrôles lors du redimensionnement
            panel.Layout += (sender, e) =>
            {
                pageInfoLabel.Location = new Point(panel.Width / 2 - 75, 10);
                prevButton.Location = new Point(pageInfoLabel.Left - 50, 5);
                nextButton.Location = new Point(pageInfoLabel.Right + 10, 5);
                rowsPerPageComboBox.Location = new Point(panel.Width - 220, 10);
                rowsPerPageLabel.Location = new Point(rowsPerPageComboBox.Left - 130, 10);
            };

            panel.Controls.AddRange(new Control[] { pageInfoLabel, prevButton, nextButton, rowsPerPageComboBox, rowsPerPageLabel });
            return panel;
        }


        private void UpdatePagination()
        {
            // Sauvegarder toutes les lignes si c'est la première fois
            if (allRows.Count == 0)
            {
                foreach (DataGridViewRow row in studentsGridView.Rows)
                {
                    allRows.Add(row);
                }
            }

            // Calculer le nombre total de pages
            totalPages = (int)Math.Ceiling((double)allRows.Count / rowsPerPage);
            if (totalPages == 0) totalPages = 1;

            // Assurer que la page actuelle est valide
            if (currentPage > totalPages)
                currentPage = totalPages;

            // Mettre à jour les contrôles de pagination
            if (paginationPanel != null)
            {
                ((Label)paginationPanel.Controls["pageInfoLabel"]).Text = $"Page {currentPage} sur {totalPages}";
                ((Button)paginationPanel.Controls["prevButton"]).Enabled = currentPage > 1;
                ((Button)paginationPanel.Controls["nextButton"]).Enabled = currentPage < totalPages;
            }

            // Afficher les lignes de la page actuelle
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            // Effacer le DataGridView
            studentsGridView.Rows.Clear();

            // Calculer les index de début et de fin
            int startIndex = (currentPage - 1) * rowsPerPage;
            int endIndex = Math.Min(startIndex + rowsPerPage, allRows.Count);

            // Ajouter les lignes pour la page actuelle
            for (int i = startIndex; i < endIndex; i++)
            {
                DataGridViewRow row = allRows[i].Clone() as DataGridViewRow;
                for (int j = 0; j < allRows[i].Cells.Count; j++)
                {
                    row.Cells[j].Value = allRows[i].Cells[j].Value;
                }
                studentsGridView.Rows.Add(row);
            }

            // Mettre à jour le compteur d'étudiants
            UpdateStudentCount();
        }

        private void ChangePage(int page)
        {
            if (page >= 1 && page <= totalPages)
            {
                currentPage = page;
                UpdatePagination();
            }
        }



        //new rafraichir

        private void RefreshDataGridView()
        {
            // Réinitialiser la liste des lignes
            allRows.Clear();

            // Reconstruire la liste
            foreach (DataGridViewRow row in studentsGridView.Rows)
            {
                allRows.Add(row);
            }

            // Mettre à jour la pagination
            UpdatePagination();
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

        private void ShowAddEditStudentForm(bool isEdit, DataGridViewRow selectedRow)
        {
            // Création du formulaire flottant
            Form addEditForm = new Form
            {
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(620, 620),
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
                Text = isEdit ? "Modifier l'étudiant" : "Ajouter un étudiant",
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

            // Matricule
            yPos = AddFormField(fieldsPanel, "Matricule", "matriculeTextBox", yPos, isEdit);
            TextBox matriculeTextBox = (TextBox)fieldsPanel.Controls["matriculeTextBox"];

            // Nom
            yPos = AddFormField(fieldsPanel, "Nom", "nomTextBox", yPos);
            TextBox nomTextBox = (TextBox)fieldsPanel.Controls["nomTextBox"];

            // Prénom
            yPos = AddFormField(fieldsPanel, "Prénom", "prenomTextBox", yPos);
            TextBox prenomTextBox = (TextBox)fieldsPanel.Controls["prenomTextBox"];

            // Date de naissance
            yPos = AddFormField(fieldsPanel, "Date de naissance", "dateNaissancePicker", yPos, isDatePicker: true);
            DateTimePicker dateNaissancePicker = (DateTimePicker)fieldsPanel.Controls["dateNaissancePicker"];

            // Sexe
            yPos = AddFormField(fieldsPanel, "Sexe", "sexeComboBox", yPos, isComboBox: true);
            ComboBox sexeComboBox = (ComboBox)fieldsPanel.Controls["sexeComboBox"];
            sexeComboBox.Items.AddRange(new object[] { "M", "F" });
            sexeComboBox.SelectedIndex = 0;

            // Adresse
            yPos = AddFormField(fieldsPanel, "Adresse", "adresseTextBox", yPos, isMultiline: true);
            TextBox adresseTextBox = (TextBox)fieldsPanel.Controls["adresseTextBox"];

            // Téléphone
            yPos = AddFormField(fieldsPanel, "Téléphone", "telephoneTextBox", yPos);
            TextBox telephoneTextBox = (TextBox)fieldsPanel.Controls["telephoneTextBox"];

            // Email
            yPos = AddFormField(fieldsPanel, "Email", "emailTextBox", yPos);
            TextBox emailTextBox = (TextBox)fieldsPanel.Controls["emailTextBox"];

            // Classe
            yPos = AddFormField(fieldsPanel, "Classe", "classeComboBox", yPos, isComboBox: true);
            ComboBox classeComboBox = (ComboBox)fieldsPanel.Controls["classeComboBox"];
            classeComboBox.Items.AddRange(new object[] { "L1", "L2", "L3" });
            classeComboBox.SelectedIndex = 0;

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
                 ShowNotification(isEdit ? "Étudiant modifié avec succès." : "Étudiant ajouté avec succès.");
                 addEditForm.Close();
             };

            //save enregistrement

            saveButton.Click += (sender, e) =>
            {
                // Ici vous ajouteriez la logique de validation

                if (isEdit && selectedRow != null)
                {
                    // Mettre à jour la ligne dans la liste principale
                    string matricule = selectedRow.Cells["Matricule"].Value.ToString();
                    for (int i = 0; i < allRows.Count; i++)
                    {
                        if (allRows[i].Cells["Matricule"].Value.ToString() == matricule)
                        {
                            // Mise à jour des valeurs
                            allRows[i].Cells["Nom"].Value = nomTextBox.Text;
                            allRows[i].Cells["Prenom"].Value = prenomTextBox.Text;
                            allRows[i].Cells["DateNaissance"].Value = dateNaissancePicker.Value.ToShortDateString();
                            allRows[i].Cells["Sexe"].Value = sexeComboBox.SelectedItem.ToString();
                            allRows[i].Cells["Email"].Value = emailTextBox.Text;
                            allRows[i].Cells["Telephone"].Value = telephoneTextBox.Text;
                            allRows[i].Cells["Classe"].Value = classeComboBox.SelectedItem.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    // Créer une nouvelle ligne
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(studentsGridView);
                    newRow.Cells[0].Value = matriculeTextBox.Text;
                    newRow.Cells[1].Value = nomTextBox.Text;
                    newRow.Cells[2].Value = prenomTextBox.Text;
                    newRow.Cells[3].Value = dateNaissancePicker.Value.ToShortDateString();
                    newRow.Cells[4].Value = sexeComboBox.SelectedItem.ToString();
                    newRow.Cells[5].Value = classeComboBox.SelectedItem.ToString();
                    newRow.Cells[6].Value = emailTextBox.Text;
                    newRow.Cells[7].Value = telephoneTextBox.Text;

                    // Ajouter à la liste principale
                    allRows.Add(newRow);
                }

                // Mettre à jour la pagination
                UpdatePagination();
                ShowNotification(isEdit ? "Étudiant modifié avec succès." : "Étudiant ajouté avec succès.");
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
                matriculeTextBox.Text = selectedRow.Cells["Matricule"].Value.ToString();
                nomTextBox.Text = selectedRow.Cells["Nom"].Value.ToString();
                prenomTextBox.Text = selectedRow.Cells["Prenom"].Value.ToString();
                dateNaissancePicker.Value = DateTime.Parse(selectedRow.Cells["DateNaissance"].Value.ToString());
                sexeComboBox.SelectedItem = selectedRow.Cells["Sexe"].Value.ToString();
                emailTextBox.Text = selectedRow.Cells["Email"].Value.ToString();
                telephoneTextBox.Text = selectedRow.Cells["Telephone"].Value.ToString();
                classeComboBox.SelectedItem = selectedRow.Cells["Classe"].Value.ToString();
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
            // Création du label avec une largeur suffisante
            Label label = new Label
            {
                Text = labelText,
                Font = regularFont,
                Size = new Size(150, 25),  // Augmentation de la largeur de 120 à 150
                Location = new Point(0, yPos + 10),
                ForeColor = Color.FromArgb(90, 90, 90),
                AutoSize = false,  // Désactiver l'auto-size pour assurer la taille fixe
                TextAlign = ContentAlignment.MiddleLeft  // Alignement du texte à gauche
            };
            panel.Controls.Add(label);

            // Création du contrôle approprié avec position ajustée
            Control control;
            int controlXPosition = 160;  // Nouvelle position horizontale pour les contrôles

            if (isComboBox)
            {
                control = new ComboBox
                {
                    Name = controlName,
                    Size = new Size(340, 35),  // Ajustement de la taille
                    Location = new Point(controlXPosition, yPos),
                    Font = regularFont,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
            }
            else if (isDatePicker)
            {
                control = new DateTimePicker
                {
                    Name = controlName,
                    Size = new Size(340, 35),  // Ajustement de la taille
                    Location = new Point(controlXPosition, yPos),
                    Font = regularFont,
                    Format = DateTimePickerFormat.Short
                };
            }
            else
            {
                TextBox textBox = new TextBox
                {
                    Name = controlName,
                    Size = new Size(340, isMultiline ? 80 : 35),  // Ajustement de la taille
                    Location = new Point(controlXPosition, yPos),
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

       
        private void UpdateStudentCount()
        {
            if (totalEtudiantsLabel != null)
            {
                int total = allRows.Count > 0 ? allRows.Count : studentsGridView.Rows.Count;
                totalEtudiantsLabel.Text = $"Effectif total: {total} étudiants";
            }
        }

         private void Professeurs_Load(object sender, EventArgs e)
         {
             // Code à exécuter lors du chargement du formulaire
             UpdateStudentCount();
         }
        private void Etudiants_Load(object sender, EventArgs e)
        {
            // Code à exécuter lors du chargement du formulaire
            UpdateStudentCount();
            UpdatePagination();
        }
    }
}

