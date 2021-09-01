
namespace IT_Support_Ticket_System_V1
{
    partial class MainMenu
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
            this.components = new System.ComponentModel.Container();
            this.tabControlMainMenu = new System.Windows.Forms.TabControl();
            this.tabPageTicketViews = new System.Windows.Forms.TabPage();
            this.dataGridViewTicketHistory = new System.Windows.Forms.DataGridView();
            this.labelViewDescription = new System.Windows.Forms.Label();
            this.richTextBoxViewDescription = new System.Windows.Forms.RichTextBox();
            this.buttonMakeReport = new System.Windows.Forms.Button();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.checkBoxTiceketSearchEnabler = new System.Windows.Forms.CheckBox();
            this.labelDTPTo = new System.Windows.Forms.Label();
            this.labelDTPFrom = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.buttonSearchViewTicket = new System.Windows.Forms.Button();
            this.checkBoxHistory = new System.Windows.Forms.CheckBox();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewViewTickets = new System.Windows.Forms.DataGridView();
            this.tabPageMakeTicket = new System.Windows.Forms.TabPage();
            this.buttonMakeTicket = new System.Windows.Forms.Button();
            this.labelCrateDTMadeValue = new System.Windows.Forms.Label();
            this.labelCreateDTMade = new System.Windows.Forms.Label();
            this.labelCreateDescription = new System.Windows.Forms.Label();
            this.richTextBoxCreateDescription = new System.Windows.Forms.RichTextBox();
            this.textBoxCreateSubject = new System.Windows.Forms.TextBox();
            this.labelCreateSubject = new System.Windows.Forms.Label();
            this.labelCreateNameValue = new System.Windows.Forms.Label();
            this.labelCreateName = new System.Windows.Forms.Label();
            this.labelCreateProblemIDValue = new System.Windows.Forms.Label();
            this.labelCreateProblemID = new System.Windows.Forms.Label();
            this.tabPageInfoAndPersonalisation = new System.Windows.Forms.TabPage();
            this.buttonInfoEmailChange = new System.Windows.Forms.Button();
            this.textBoxInfoEamil = new System.Windows.Forms.TextBox();
            this.labelInfoEmail = new System.Windows.Forms.Label();
            this.groupBoxPresonalise = new System.Windows.Forms.GroupBox();
            this.radioButtonLightMode = new System.Windows.Forms.RadioButton();
            this.radioButtonDarkMode = new System.Windows.Forms.RadioButton();
            this.labelInfoPhoneValue = new System.Windows.Forms.Label();
            this.labelInfoPhoneNo = new System.Windows.Forms.Label();
            this.textBoxInfoPasswordChange = new System.Windows.Forms.TextBox();
            this.labelInfoNewPassword = new System.Windows.Forms.Label();
            this.buttonInfoPasswordChange = new System.Windows.Forms.Button();
            this.buttonInfoPasswordView = new System.Windows.Forms.Button();
            this.textBoxInfoPassword = new System.Windows.Forms.TextBox();
            this.labelInfoPassword = new System.Windows.Forms.Label();
            this.labelInfoUserNameValue = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelInfoNameValue = new System.Windows.Forms.Label();
            this.labelInfoName = new System.Windows.Forms.Label();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlMainMenu.SuspendLayout();
            this.tabPageTicketViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketHistory)).BeginInit();
            this.groupBoxFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewViewTickets)).BeginInit();
            this.tabPageMakeTicket.SuspendLayout();
            this.tabPageInfoAndPersonalisation.SuspendLayout();
            this.groupBoxPresonalise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMainMenu
            // 
            this.tabControlMainMenu.Controls.Add(this.tabPageTicketViews);
            this.tabControlMainMenu.Controls.Add(this.tabPageMakeTicket);
            this.tabControlMainMenu.Controls.Add(this.tabPageInfoAndPersonalisation);
            this.tabControlMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMainMenu.Location = new System.Drawing.Point(0, 0);
            this.tabControlMainMenu.Name = "tabControlMainMenu";
            this.tabControlMainMenu.SelectedIndex = 0;
            this.tabControlMainMenu.Size = new System.Drawing.Size(800, 450);
            this.tabControlMainMenu.TabIndex = 0;
            this.tabControlMainMenu.Click += new System.EventHandler(this.buttonInfoPasswordView_Click);
            // 
            // tabPageTicketViews
            // 
            this.tabPageTicketViews.Controls.Add(this.dataGridViewTicketHistory);
            this.tabPageTicketViews.Controls.Add(this.labelViewDescription);
            this.tabPageTicketViews.Controls.Add(this.richTextBoxViewDescription);
            this.tabPageTicketViews.Controls.Add(this.buttonMakeReport);
            this.tabPageTicketViews.Controls.Add(this.groupBoxFilter);
            this.tabPageTicketViews.Controls.Add(this.dataGridViewViewTickets);
            this.tabPageTicketViews.Location = new System.Drawing.Point(4, 25);
            this.tabPageTicketViews.Name = "tabPageTicketViews";
            this.tabPageTicketViews.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTicketViews.Size = new System.Drawing.Size(792, 421);
            this.tabPageTicketViews.TabIndex = 0;
            this.tabPageTicketViews.Text = "Your Tickets";
            this.tabPageTicketViews.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTicketHistory
            // 
            this.dataGridViewTicketHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTicketHistory.Enabled = false;
            this.dataGridViewTicketHistory.Location = new System.Drawing.Point(9, 6);
            this.dataGridViewTicketHistory.Name = "dataGridViewTicketHistory";
            this.dataGridViewTicketHistory.Size = new System.Drawing.Size(552, 374);
            this.dataGridViewTicketHistory.TabIndex = 9;
            this.dataGridViewTicketHistory.Visible = false;
            this.dataGridViewTicketHistory.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewTicketHistory_RowHeaderMouseClick);
            // 
            // labelViewDescription
            // 
            this.labelViewDescription.AutoSize = true;
            this.labelViewDescription.Location = new System.Drawing.Point(567, 193);
            this.labelViewDescription.Name = "labelViewDescription";
            this.labelViewDescription.Size = new System.Drawing.Size(79, 16);
            this.labelViewDescription.TabIndex = 8;
            this.labelViewDescription.Text = "Description:";
            // 
            // richTextBoxViewDescription
            // 
            this.richTextBoxViewDescription.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.richTextBoxViewDescription.Enabled = false;
            this.richTextBoxViewDescription.Location = new System.Drawing.Point(567, 212);
            this.richTextBoxViewDescription.Name = "richTextBoxViewDescription";
            this.richTextBoxViewDescription.Size = new System.Drawing.Size(216, 201);
            this.richTextBoxViewDescription.TabIndex = 6;
            this.richTextBoxViewDescription.Text = "";
            // 
            // buttonMakeReport
            // 
            this.buttonMakeReport.Location = new System.Drawing.Point(9, 386);
            this.buttonMakeReport.Name = "buttonMakeReport";
            this.buttonMakeReport.Size = new System.Drawing.Size(103, 29);
            this.buttonMakeReport.TabIndex = 5;
            this.buttonMakeReport.Text = "Make Report";
            this.buttonMakeReport.UseVisualStyleBackColor = true;
            this.buttonMakeReport.Click += new System.EventHandler(this.buttonMakeReport_Click);
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.checkBoxTiceketSearchEnabler);
            this.groupBoxFilter.Controls.Add(this.labelDTPTo);
            this.groupBoxFilter.Controls.Add(this.labelDTPFrom);
            this.groupBoxFilter.Controls.Add(this.dateTimePickerTo);
            this.groupBoxFilter.Controls.Add(this.buttonSearchViewTicket);
            this.groupBoxFilter.Controls.Add(this.checkBoxHistory);
            this.groupBoxFilter.Controls.Add(this.dateTimePickerFrom);
            this.groupBoxFilter.Location = new System.Drawing.Point(567, 11);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(217, 179);
            this.groupBoxFilter.TabIndex = 4;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Search Filters";
            // 
            // checkBoxTiceketSearchEnabler
            // 
            this.checkBoxTiceketSearchEnabler.AutoSize = true;
            this.checkBoxTiceketSearchEnabler.Location = new System.Drawing.Point(56, 18);
            this.checkBoxTiceketSearchEnabler.Name = "checkBoxTiceketSearchEnabler";
            this.checkBoxTiceketSearchEnabler.Size = new System.Drawing.Size(120, 20);
            this.checkBoxTiceketSearchEnabler.TabIndex = 11;
            this.checkBoxTiceketSearchEnabler.Text = "Search Enabler";
            this.checkBoxTiceketSearchEnabler.UseVisualStyleBackColor = true;
            this.checkBoxTiceketSearchEnabler.CheckedChanged += new System.EventHandler(this.checkBoxTiceketSearchEnabler_CheckedChanged);
            // 
            // labelDTPTo
            // 
            this.labelDTPTo.AutoSize = true;
            this.labelDTPTo.Location = new System.Drawing.Point(9, 76);
            this.labelDTPTo.Name = "labelDTPTo";
            this.labelDTPTo.Size = new System.Drawing.Size(28, 16);
            this.labelDTPTo.TabIndex = 10;
            this.labelDTPTo.Text = "To:";
            // 
            // labelDTPFrom
            // 
            this.labelDTPFrom.AutoSize = true;
            this.labelDTPFrom.Location = new System.Drawing.Point(12, 28);
            this.labelDTPFrom.Name = "labelDTPFrom";
            this.labelDTPFrom.Size = new System.Drawing.Size(42, 16);
            this.labelDTPFrom.TabIndex = 9;
            this.labelDTPFrom.Text = "From:";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Enabled = false;
            this.dateTimePickerTo.Location = new System.Drawing.Point(12, 95);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(199, 22);
            this.dateTimePickerTo.TabIndex = 8;
            // 
            // buttonSearchViewTicket
            // 
            this.buttonSearchViewTicket.Enabled = false;
            this.buttonSearchViewTicket.Location = new System.Drawing.Point(118, 135);
            this.buttonSearchViewTicket.Name = "buttonSearchViewTicket";
            this.buttonSearchViewTicket.Size = new System.Drawing.Size(75, 24);
            this.buttonSearchViewTicket.TabIndex = 7;
            this.buttonSearchViewTicket.Text = "Search";
            this.buttonSearchViewTicket.UseVisualStyleBackColor = true;
            this.buttonSearchViewTicket.Click += new System.EventHandler(this.buttonSearchViewTicket_Click);
            // 
            // checkBoxHistory
            // 
            this.checkBoxHistory.AutoSize = true;
            this.checkBoxHistory.Location = new System.Drawing.Point(12, 135);
            this.checkBoxHistory.Name = "checkBoxHistory";
            this.checkBoxHistory.Size = new System.Drawing.Size(101, 20);
            this.checkBoxHistory.TabIndex = 6;
            this.checkBoxHistory.Text = "View History";
            this.checkBoxHistory.UseVisualStyleBackColor = true;
            this.checkBoxHistory.CheckedChanged += new System.EventHandler(this.checkBoxHistory_CheckedChanged);
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Enabled = false;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(12, 47);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(199, 22);
            this.dateTimePickerFrom.TabIndex = 2;
            // 
            // dataGridViewViewTickets
            // 
            this.dataGridViewViewTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewViewTickets.Location = new System.Drawing.Point(8, 6);
            this.dataGridViewViewTickets.Name = "dataGridViewViewTickets";
            this.dataGridViewViewTickets.Size = new System.Drawing.Size(553, 373);
            this.dataGridViewViewTickets.TabIndex = 0;
            this.dataGridViewViewTickets.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewViewTickets_RowHeaderMouseClick);
            // 
            // tabPageMakeTicket
            // 
            this.tabPageMakeTicket.Controls.Add(this.buttonMakeTicket);
            this.tabPageMakeTicket.Controls.Add(this.labelCrateDTMadeValue);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateDTMade);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateDescription);
            this.tabPageMakeTicket.Controls.Add(this.richTextBoxCreateDescription);
            this.tabPageMakeTicket.Controls.Add(this.textBoxCreateSubject);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateSubject);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateNameValue);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateName);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateProblemIDValue);
            this.tabPageMakeTicket.Controls.Add(this.labelCreateProblemID);
            this.tabPageMakeTicket.Location = new System.Drawing.Point(4, 25);
            this.tabPageMakeTicket.Name = "tabPageMakeTicket";
            this.tabPageMakeTicket.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMakeTicket.Size = new System.Drawing.Size(792, 421);
            this.tabPageMakeTicket.TabIndex = 1;
            this.tabPageMakeTicket.Text = "Make Ticket";
            this.tabPageMakeTicket.UseVisualStyleBackColor = true;
            // 
            // buttonMakeTicket
            // 
            this.buttonMakeTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMakeTicket.Location = new System.Drawing.Point(709, 338);
            this.buttonMakeTicket.Name = "buttonMakeTicket";
            this.buttonMakeTicket.Size = new System.Drawing.Size(75, 28);
            this.buttonMakeTicket.TabIndex = 10;
            this.buttonMakeTicket.Text = "Finnish";
            this.buttonMakeTicket.UseVisualStyleBackColor = true;
            this.buttonMakeTicket.Click += new System.EventHandler(this.buttonMakeTicket_Click);
            // 
            // labelCrateDTMadeValue
            // 
            this.labelCrateDTMadeValue.AutoSize = true;
            this.labelCrateDTMadeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCrateDTMadeValue.Location = new System.Drawing.Point(12, 338);
            this.labelCrateDTMadeValue.Name = "labelCrateDTMadeValue";
            this.labelCrateDTMadeValue.Size = new System.Drawing.Size(72, 25);
            this.labelCrateDTMadeValue.TabIndex = 9;
            this.labelCrateDTMadeValue.Text = " --/--/--";
            // 
            // labelCreateDTMade
            // 
            this.labelCreateDTMade.AutoSize = true;
            this.labelCreateDTMade.Location = new System.Drawing.Point(12, 318);
            this.labelCreateDTMade.Name = "labelCreateDTMade";
            this.labelCreateDTMade.Size = new System.Drawing.Size(165, 16);
            this.labelCreateDTMade.TabIndex = 8;
            this.labelCreateDTMade.Text = "Date and Time of creation:";
            // 
            // labelCreateDescription
            // 
            this.labelCreateDescription.AutoSize = true;
            this.labelCreateDescription.Location = new System.Drawing.Point(12, 91);
            this.labelCreateDescription.Name = "labelCreateDescription";
            this.labelCreateDescription.Size = new System.Drawing.Size(79, 16);
            this.labelCreateDescription.TabIndex = 7;
            this.labelCreateDescription.Text = "Description:";
            // 
            // richTextBoxCreateDescription
            // 
            this.richTextBoxCreateDescription.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.richTextBoxCreateDescription.Location = new System.Drawing.Point(12, 110);
            this.richTextBoxCreateDescription.Name = "richTextBoxCreateDescription";
            this.richTextBoxCreateDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxCreateDescription.Size = new System.Drawing.Size(752, 201);
            this.richTextBoxCreateDescription.TabIndex = 6;
            this.richTextBoxCreateDescription.Text = "";
            // 
            // textBoxCreateSubject
            // 
            this.textBoxCreateSubject.Location = new System.Drawing.Point(75, 58);
            this.textBoxCreateSubject.Name = "textBoxCreateSubject";
            this.textBoxCreateSubject.Size = new System.Drawing.Size(299, 22);
            this.textBoxCreateSubject.TabIndex = 5;
            // 
            // labelCreateSubject
            // 
            this.labelCreateSubject.AutoSize = true;
            this.labelCreateSubject.Location = new System.Drawing.Point(9, 58);
            this.labelCreateSubject.Name = "labelCreateSubject";
            this.labelCreateSubject.Size = new System.Drawing.Size(56, 16);
            this.labelCreateSubject.TabIndex = 4;
            this.labelCreateSubject.Text = "Subject:";
            // 
            // labelCreateNameValue
            // 
            this.labelCreateNameValue.AutoSize = true;
            this.labelCreateNameValue.Location = new System.Drawing.Point(64, 38);
            this.labelCreateNameValue.Name = "labelCreateNameValue";
            this.labelCreateNameValue.Size = new System.Drawing.Size(26, 16);
            this.labelCreateNameValue.TabIndex = 3;
            this.labelCreateNameValue.Text = "- - -";
            // 
            // labelCreateName
            // 
            this.labelCreateName.AutoSize = true;
            this.labelCreateName.Location = new System.Drawing.Point(9, 38);
            this.labelCreateName.Name = "labelCreateName";
            this.labelCreateName.Size = new System.Drawing.Size(48, 16);
            this.labelCreateName.TabIndex = 2;
            this.labelCreateName.Text = "Name:";
            // 
            // labelCreateProblemIDValue
            // 
            this.labelCreateProblemIDValue.AutoSize = true;
            this.labelCreateProblemIDValue.Location = new System.Drawing.Point(86, 22);
            this.labelCreateProblemIDValue.Name = "labelCreateProblemIDValue";
            this.labelCreateProblemIDValue.Size = new System.Drawing.Size(26, 16);
            this.labelCreateProblemIDValue.TabIndex = 1;
            this.labelCreateProblemIDValue.Text = "- - -";
            // 
            // labelCreateProblemID
            // 
            this.labelCreateProblemID.AutoSize = true;
            this.labelCreateProblemID.Location = new System.Drawing.Point(9, 22);
            this.labelCreateProblemID.Name = "labelCreateProblemID";
            this.labelCreateProblemID.Size = new System.Drawing.Size(78, 16);
            this.labelCreateProblemID.TabIndex = 0;
            this.labelCreateProblemID.Text = "Problem ID:";
            // 
            // tabPageInfoAndPersonalisation
            // 
            this.tabPageInfoAndPersonalisation.Controls.Add(this.buttonInfoEmailChange);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.textBoxInfoEamil);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoEmail);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.groupBoxPresonalise);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoPhoneValue);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoPhoneNo);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.textBoxInfoPasswordChange);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoNewPassword);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.buttonInfoPasswordChange);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.buttonInfoPasswordView);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.textBoxInfoPassword);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoPassword);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoUserNameValue);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelUserName);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoNameValue);
            this.tabPageInfoAndPersonalisation.Controls.Add(this.labelInfoName);
            this.tabPageInfoAndPersonalisation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageInfoAndPersonalisation.Location = new System.Drawing.Point(4, 25);
            this.tabPageInfoAndPersonalisation.Name = "tabPageInfoAndPersonalisation";
            this.tabPageInfoAndPersonalisation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfoAndPersonalisation.Size = new System.Drawing.Size(792, 421);
            this.tabPageInfoAndPersonalisation.TabIndex = 2;
            this.tabPageInfoAndPersonalisation.Text = "Personal Info and Personalisation";
            this.tabPageInfoAndPersonalisation.UseVisualStyleBackColor = true;
            // 
            // buttonInfoEmailChange
            // 
            this.buttonInfoEmailChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInfoEmailChange.Location = new System.Drawing.Point(338, 191);
            this.buttonInfoEmailChange.Name = "buttonInfoEmailChange";
            this.buttonInfoEmailChange.Size = new System.Drawing.Size(95, 40);
            this.buttonInfoEmailChange.TabIndex = 23;
            this.buttonInfoEmailChange.Text = "Change";
            this.buttonInfoEmailChange.UseVisualStyleBackColor = true;
            this.buttonInfoEmailChange.Click += new System.EventHandler(this.buttonInfoEmailChange_Click);
            // 
            // textBoxInfoEamil
            // 
            this.textBoxInfoEamil.Location = new System.Drawing.Point(90, 196);
            this.textBoxInfoEamil.Name = "textBoxInfoEamil";
            this.textBoxInfoEamil.Size = new System.Drawing.Size(242, 29);
            this.textBoxInfoEamil.TabIndex = 22;
            // 
            // labelInfoEmail
            // 
            this.labelInfoEmail.AutoSize = true;
            this.labelInfoEmail.Location = new System.Drawing.Point(21, 193);
            this.labelInfoEmail.Name = "labelInfoEmail";
            this.labelInfoEmail.Size = new System.Drawing.Size(62, 24);
            this.labelInfoEmail.TabIndex = 21;
            this.labelInfoEmail.Text = "Eamil:";
            // 
            // groupBoxPresonalise
            // 
            this.groupBoxPresonalise.Controls.Add(this.radioButtonLightMode);
            this.groupBoxPresonalise.Controls.Add(this.radioButtonDarkMode);
            this.groupBoxPresonalise.Location = new System.Drawing.Point(584, 28);
            this.groupBoxPresonalise.Name = "groupBoxPresonalise";
            this.groupBoxPresonalise.Size = new System.Drawing.Size(200, 116);
            this.groupBoxPresonalise.TabIndex = 20;
            this.groupBoxPresonalise.TabStop = false;
            this.groupBoxPresonalise.Text = "Personalise";
            // 
            // radioButtonLightMode
            // 
            this.radioButtonLightMode.AutoSize = true;
            this.radioButtonLightMode.Checked = true;
            this.radioButtonLightMode.Location = new System.Drawing.Point(16, 39);
            this.radioButtonLightMode.Name = "radioButtonLightMode";
            this.radioButtonLightMode.Size = new System.Drawing.Size(122, 28);
            this.radioButtonLightMode.TabIndex = 18;
            this.radioButtonLightMode.TabStop = true;
            this.radioButtonLightMode.Text = "Light Mode";
            this.radioButtonLightMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonDarkMode
            // 
            this.radioButtonDarkMode.AutoSize = true;
            this.radioButtonDarkMode.Location = new System.Drawing.Point(16, 73);
            this.radioButtonDarkMode.Name = "radioButtonDarkMode";
            this.radioButtonDarkMode.Size = new System.Drawing.Size(120, 28);
            this.radioButtonDarkMode.TabIndex = 19;
            this.radioButtonDarkMode.Text = "Dark Mode";
            this.radioButtonDarkMode.UseVisualStyleBackColor = true;
            // 
            // labelInfoPhoneValue
            // 
            this.labelInfoPhoneValue.AutoSize = true;
            this.labelInfoPhoneValue.Location = new System.Drawing.Point(168, 165);
            this.labelInfoPhoneValue.Name = "labelInfoPhoneValue";
            this.labelInfoPhoneValue.Size = new System.Drawing.Size(38, 24);
            this.labelInfoPhoneValue.TabIndex = 17;
            this.labelInfoPhoneValue.Text = "- - -";
            // 
            // labelInfoPhoneNo
            // 
            this.labelInfoPhoneNo.AutoSize = true;
            this.labelInfoPhoneNo.Location = new System.Drawing.Point(17, 165);
            this.labelInfoPhoneNo.Name = "labelInfoPhoneNo";
            this.labelInfoPhoneNo.Size = new System.Drawing.Size(145, 24);
            this.labelInfoPhoneNo.TabIndex = 16;
            this.labelInfoPhoneNo.Text = "Phone Number:";
            // 
            // textBoxInfoPasswordChange
            // 
            this.textBoxInfoPasswordChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInfoPasswordChange.Location = new System.Drawing.Point(160, 125);
            this.textBoxInfoPasswordChange.Name = "textBoxInfoPasswordChange";
            this.textBoxInfoPasswordChange.Size = new System.Drawing.Size(196, 29);
            this.textBoxInfoPasswordChange.TabIndex = 15;
            // 
            // labelInfoNewPassword
            // 
            this.labelInfoNewPassword.AutoSize = true;
            this.labelInfoNewPassword.Location = new System.Drawing.Point(13, 128);
            this.labelInfoNewPassword.Name = "labelInfoNewPassword";
            this.labelInfoNewPassword.Size = new System.Drawing.Size(141, 24);
            this.labelInfoNewPassword.TabIndex = 14;
            this.labelInfoNewPassword.Text = "New Password:";
            // 
            // buttonInfoPasswordChange
            // 
            this.buttonInfoPasswordChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInfoPasswordChange.Location = new System.Drawing.Point(362, 121);
            this.buttonInfoPasswordChange.Name = "buttonInfoPasswordChange";
            this.buttonInfoPasswordChange.Size = new System.Drawing.Size(95, 39);
            this.buttonInfoPasswordChange.TabIndex = 13;
            this.buttonInfoPasswordChange.Text = "Change";
            this.buttonInfoPasswordChange.UseVisualStyleBackColor = true;
            this.buttonInfoPasswordChange.Click += new System.EventHandler(this.buttonInfoPasswordChange_Click);
            // 
            // buttonInfoPasswordView
            // 
            this.buttonInfoPasswordView.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInfoPasswordView.Location = new System.Drawing.Point(318, 74);
            this.buttonInfoPasswordView.Name = "buttonInfoPasswordView";
            this.buttonInfoPasswordView.Size = new System.Drawing.Size(68, 41);
            this.buttonInfoPasswordView.TabIndex = 12;
            this.buttonInfoPasswordView.Text = "View";
            this.buttonInfoPasswordView.UseVisualStyleBackColor = true;
            this.buttonInfoPasswordView.Click += new System.EventHandler(this.buttonInfoPasswordView_Click);
            // 
            // textBoxInfoPassword
            // 
            this.textBoxInfoPassword.Enabled = false;
            this.textBoxInfoPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInfoPassword.Location = new System.Drawing.Point(116, 79);
            this.textBoxInfoPassword.Name = "textBoxInfoPassword";
            this.textBoxInfoPassword.PasswordChar = '*';
            this.textBoxInfoPassword.Size = new System.Drawing.Size(196, 29);
            this.textBoxInfoPassword.TabIndex = 11;
            // 
            // labelInfoPassword
            // 
            this.labelInfoPassword.AutoSize = true;
            this.labelInfoPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoPassword.Location = new System.Drawing.Point(13, 82);
            this.labelInfoPassword.Name = "labelInfoPassword";
            this.labelInfoPassword.Size = new System.Drawing.Size(97, 24);
            this.labelInfoPassword.TabIndex = 10;
            this.labelInfoPassword.Text = "Password:";
            // 
            // labelInfoUserNameValue
            // 
            this.labelInfoUserNameValue.AutoSize = true;
            this.labelInfoUserNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoUserNameValue.Location = new System.Drawing.Point(130, 53);
            this.labelInfoUserNameValue.Name = "labelInfoUserNameValue";
            this.labelInfoUserNameValue.Size = new System.Drawing.Size(38, 24);
            this.labelInfoUserNameValue.TabIndex = 9;
            this.labelInfoUserNameValue.Text = "- - -";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserName.Location = new System.Drawing.Point(8, 53);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(110, 24);
            this.labelUserName.TabIndex = 8;
            this.labelUserName.Text = "User Name:";
            // 
            // labelInfoNameValue
            // 
            this.labelInfoNameValue.AutoSize = true;
            this.labelInfoNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoNameValue.Location = new System.Drawing.Point(79, 28);
            this.labelInfoNameValue.Name = "labelInfoNameValue";
            this.labelInfoNameValue.Size = new System.Drawing.Size(38, 24);
            this.labelInfoNameValue.TabIndex = 7;
            this.labelInfoNameValue.Text = "- - -";
            // 
            // labelInfoName
            // 
            this.labelInfoName.AutoSize = true;
            this.labelInfoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoName.Location = new System.Drawing.Point(8, 28);
            this.labelInfoName.Name = "labelInfoName";
            this.labelInfoName.Size = new System.Drawing.Size(66, 24);
            this.labelInfoName.TabIndex = 6;
            this.labelInfoName.Text = "Name:";
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlMainMenu);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.tabControlMainMenu.ResumeLayout(false);
            this.tabPageTicketViews.ResumeLayout(false);
            this.tabPageTicketViews.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketHistory)).EndInit();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewViewTickets)).EndInit();
            this.tabPageMakeTicket.ResumeLayout(false);
            this.tabPageMakeTicket.PerformLayout();
            this.tabPageInfoAndPersonalisation.ResumeLayout(false);
            this.tabPageInfoAndPersonalisation.PerformLayout();
            this.groupBoxPresonalise.ResumeLayout(false);
            this.groupBoxPresonalise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMainMenu;
        private System.Windows.Forms.TabPage tabPageTicketViews;
        private System.Windows.Forms.TabPage tabPageMakeTicket;
        private System.Windows.Forms.DataGridView dataGridViewViewTickets;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.ErrorProvider ep;
        private System.Windows.Forms.Button buttonMakeReport;
        private System.Windows.Forms.Label labelViewDescription;
        private System.Windows.Forms.RichTextBox richTextBoxViewDescription;
        private System.Windows.Forms.CheckBox checkBoxHistory;
        private System.Windows.Forms.Label labelCreateProblemIDValue;
        private System.Windows.Forms.Label labelCreateProblemID;
        private System.Windows.Forms.RichTextBox richTextBoxCreateDescription;
        private System.Windows.Forms.TextBox textBoxCreateSubject;
        private System.Windows.Forms.Label labelCreateSubject;
        private System.Windows.Forms.Label labelCreateNameValue;
        private System.Windows.Forms.Label labelCreateName;
        private System.Windows.Forms.Button buttonMakeTicket;
        private System.Windows.Forms.Label labelCrateDTMadeValue;
        private System.Windows.Forms.Label labelCreateDTMade;
        private System.Windows.Forms.Label labelCreateDescription;
        private System.Windows.Forms.TabPage tabPageInfoAndPersonalisation;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelInfoNameValue;
        private System.Windows.Forms.Label labelInfoName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelInfoNewPassword;
        private System.Windows.Forms.Button buttonInfoPasswordChange;
        private System.Windows.Forms.Button buttonInfoPasswordView;
        private System.Windows.Forms.TextBox textBoxInfoPassword;
        private System.Windows.Forms.Label labelInfoPassword;
        private System.Windows.Forms.Label labelInfoUserNameValue;
        private System.Windows.Forms.Label labelInfoPhoneNo;
        private System.Windows.Forms.TextBox textBoxInfoPasswordChange;
        private System.Windows.Forms.GroupBox groupBoxPresonalise;
        private System.Windows.Forms.RadioButton radioButtonLightMode;
        private System.Windows.Forms.RadioButton radioButtonDarkMode;
        private System.Windows.Forms.Label labelInfoPhoneValue;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonInfoEmailChange;
        private System.Windows.Forms.TextBox textBoxInfoEamil;
        private System.Windows.Forms.Label labelInfoEmail;
        private System.Windows.Forms.Button buttonSearchViewTicket;
        private System.Windows.Forms.Label labelDTPTo;
        private System.Windows.Forms.Label labelDTPFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DataGridView dataGridViewTicketHistory;
        private System.Windows.Forms.CheckBox checkBoxTiceketSearchEnabler;
    }
}