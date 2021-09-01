using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace IT_Support_Ticket_System_V1
{
    public partial class MainMenu : Form
    {
        SqlDataAdapter daTicket, daTicketHistory, daDesc, daHistoryDesc, daTicketSearch, daHistoryTicketSearch, daClient,
            daDefultTicket, daDefultLogin, daDefultClient;
        //for peramiterised queries only
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();

        //fills with only the raw tables with no preamiters
        DataSet dsDefult = new DataSet();

        SqlConnection conn;
        SqlCommand cmdTicket, cmdTicketHistory, cmdDesc, cmdHistoryDesc, cmdTicketSearch, cmdHistoryTicketSearch, cmdClient;
        SqlCommandBuilder cmdBDefultTicket, cmdBDefultLogin, cmdBDefultClient;
        DataRow drTicket, drDesc, drClient, drDefult, drHistoryDesc;

        Allocation_AI ai = new Allocation_AI();


        string connStr;

        string userID, userName, password, server;

        //queries
        //utility Peramiterised Queries
        string sqlGetClient = @"exec Get_Client @uname = @un, @pword = @pw";
        string sqlPeramiterisedTickets = @"exec Get_Tickets_Client @uname = @un, @pword = @pw";
        string sqlPeramiterisedHistoryTickets = @"exec Find_Ticket_History_view @uname = @un, @pword = @pw";
        string sqlDesc = @"exec Get_Desc @problemNo = @d";

        string sqlHistoryDesc = @"exec Get_History_Desc @UpdateNo = @UpNo";

        //Search Queris
        string sqlFindTicket = @"exec Find_Ticket @uname = @un, @pword = @pw, @from = @f, @to = @t";
        string sqlFindTicketHistory = @"exec Find_Ticket_History @uname = @un, @pword = @pw,  @from = @f, @to = @t";

        //General/defualt Queries
        string sqlDefultTicket = @"select * from Ticket";
        string sqlDefultLogin = @"select * from Login_";
        string sqlDefultClient = @"select * from Client";


        string remember;

        bool showPassword = false;

        List<int> ticketNoList = new List<int>();

        bool is_empty = false;
        bool is_empty2 = false;

        private void buttonMakeReport_Click(object sender, EventArgs e)
        {
            if (File.Exists("Permit.txt"))
            {
                string file = File.ReadAllText("Permit.txt");
                if (file[0] == '1')
                {

                    File.Delete("Permit.txt");
                    File.AppendAllText("Permit.txt", "0 Individual");
                    var reportFrom = new ReportForm();
                    reportFrom.ShowDialog();
                }

            }
            else
            {
                File.WriteAllText("Permit.txt", "0 Individual");
                var reportFrom = new ReportForm();
                reportFrom.ShowDialog();
            }
        }

        private void checkBoxTiceketSearchEnabler_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBoxTiceketSearchEnabler.Checked;

            if (check)
            {
                dateTimePickerFrom.Enabled = true;
                dateTimePickerTo.Enabled = true;
                buttonSearchViewTicket.Enabled = true;

                if (checkBoxHistory.Checked)
                    dataGridViewTicketHistory.DataSource = ds2.Tables["Ticket_History"];

                else
                    dataGridViewViewTickets.DataSource = ds2.Tables["Ticket"];

            }
            else
            {
                dateTimePickerFrom.Enabled = false;
                dateTimePickerTo.Enabled = false;
                buttonSearchViewTicket.Enabled = false;

                ds2.Tables["Ticket"].Clear();
                ds2.Tables["Ticket_History"].Clear();

                if (checkBoxHistory.Checked)
                    dataGridViewTicketHistory.DataSource = ds.Tables["Ticket_History"];

                else
                    dataGridViewViewTickets.DataSource = ds.Tables["Ticket"];

            }

        }

        private void checkBoxHistory_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBoxHistory.Checked;

            if (check)
            {
                dataGridViewTicketHistory.Enabled = true;
                dataGridViewTicketHistory.Visible = true;

                dataGridViewViewTickets.Enabled = false;
                dataGridViewViewTickets.Visible = false;
            }
            else
            {
                dataGridViewTicketHistory.Enabled = false;
                dataGridViewTicketHistory.Visible = false;

                dataGridViewViewTickets.Enabled = true;
                dataGridViewViewTickets.Visible = true;
            }
        }

        private void dataGridViewTicketHistory_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {


                if (is_empty2)
                {
                    cmdHistoryDesc = new SqlCommand(sqlHistoryDesc, conn);
                    cmdHistoryDesc.Parameters.Add("@UpNo", SqlDbType.Int);
                    cmdHistoryDesc.Parameters["@UpNo"].Value = int.Parse(dataGridViewTicketHistory.SelectedRows[0].Cells[0].ToString());
                    daHistoryDesc = new SqlDataAdapter(cmdHistoryDesc);
                    daHistoryDesc.FillSchema(ds, SchemaType.Source, "History_Desc");
                    daHistoryDesc.Fill(ds, "History_Desc");
                }

                ds.Tables["History_Desc"].Clear();
                cmdHistoryDesc.Parameters["@UpNo"].Value = int.Parse((dataGridViewTicketHistory.SelectedRows[0].Cells[0].Value).ToString());
                daHistoryDesc.Fill(ds, "History_Desc");
                drHistoryDesc = ds.Tables["History_Desc"].Rows[0];
                richTextBoxViewDescription.Text = drHistoryDesc.ItemArray[0].ToString() + ":\n\n" + drHistoryDesc.ItemArray[1].ToString();
            }
            catch (Exception) { richTextBoxCreateDescription.Clear(); }
        }



        public MainMenu()
        {
            InitializeComponent();
        }
        private void buttonSearchViewTicket_Click(object sender, EventArgs e)
        {
            string from = dateTimePickerFrom.Value.ToShortDateString() + " 00:00:00";
            string to = dateTimePickerTo.Value.ToShortDateString() + " 00:00:00";

            if (!checkBoxHistory.Checked)
            {
                cmdTicketSearch.Parameters["@f"].Value = from;
                cmdTicketSearch.Parameters["@t"].Value = to;

                daTicketSearch.Fill(ds2, "Ticket");
            }
            else
            {
                cmdHistoryTicketSearch.Parameters["@f"].Value = from;
                cmdHistoryTicketSearch.Parameters["@t"].Value = to;

                daHistoryTicketSearch.Fill(ds2, "Ticket_History");
            }
        }


        private void buttonInfoEmailChange_Click(object sender, EventArgs e)
        {
            // changes the user's email
            if (textBoxInfoEamil.Text == "")
            {
                ep.SetError(buttonInfoEmailChange, "Textbox is empty");
            }
            else if (textBoxInfoEamil.Text.Length < 10 || textBoxInfoEamil.Text.Length > 60)
            {
                ep.SetError(buttonInfoEmailChange, "The email can only be between \n10 and 60 characters long");
            }
            else
            {
                string email = textBoxInfoEamil.Text;

                foreach (DataRow dr in dsDefult.Tables["Client"].Rows)
                {
                    if (dr.ItemArray[6].ToString() == userID)
                    {
                        drDefult = dr;
                    }
                }

                drDefult.BeginEdit();

                drDefult["Email"] = email;
                drDefult["Log_ID"] = userID;

                drDefult.EndEdit();

                daDefultLogin.Update(dsDefult, "Login_");
                daDefultLogin.Fill(dsDefult, "Login_");
            }

        }

        private void buttonInfoPasswordChange_Click(object sender, EventArgs e)
        {
            // changes the user's password
            string password = textBoxInfoPasswordChange.Text;
            bool ok = true,
                 isLetter = false,
                 isNumber = false;

            if (password.Length < 7 || password.Length > 40)
            {
                ep.SetError(buttonInfoPasswordChange, "Please Enter a password the fills the follwing criteria:\n1.Must have at least 7 characters (20 characters Maximum)\n2.Must have letter(s) and Number(s)");
                ok = false;
            }

            foreach (char c in password)
            {
                if (char.IsLetter(c)) isLetter = true;
                else if (char.IsDigit(c)) isNumber = true;
            }

            if (!isLetter && !isNumber)
            {
                ep.SetError(buttonInfoPasswordChange, "Please Enter a password the fills the follwing criteria:\n1.Must have at least 7 characters (20 characters Maximum)\n2.Must have letter(s) and Number(s)");
                ok = false;
            }

            if (ok)
            {
                foreach (DataRow dr in dsDefult.Tables["Login_"].Rows)
                {
                    if (dr.ItemArray[0].ToString() == userID)
                    {
                        drDefult = dr;
                    }
                }

                drDefult.BeginEdit();

                drDefult["Password_"] = password;

                drDefult.EndEdit();
                daDefultLogin.Update(dsDefult, "Login_");
                daDefultLogin.Fill(dsDefult, "Login_");

                textBoxInfoPasswordChange.Text = "";
                textBoxInfoPassword.Text = password;
                textBoxInfoPassword.PasswordChar = '*';
                this.password = password;
            }

        }

        private void buttonInfoPasswordView_Click(object sender, EventArgs e)
        {
            if (!showPassword)
            {
                textBoxInfoPassword.PasswordChar = '\0';
                showPassword = true;
            }
            else
            {
                textBoxInfoPassword.PasswordChar = '*';
                showPassword = false;
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (remember == "Remember-Login-and-Server")
            {
                return;
            }
            else if (remember == "Remember-Server")
            {
                File.Delete("User.txt");
                File.AppendAllText("User.txt", "Remember-Server|" + userID + "|" + userName + "|" + password + "|" + server);
            }
            else if (remember == "Remember-Login")
            {
                File.Delete("User.txt");
                File.AppendAllText("User.txt", "Remember-Login|" + userID + "|" + userName + "|" + password + "|" + server);
            }
            else
            {
                File.Delete("User.txt");
            }

        }


        private void timer_Tick(object sender, EventArgs e)
        {
            labelCrateDTMadeValue.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void buttonMakeTicket_Click(object sender, EventArgs e)
        {

            string subject = textBoxCreateSubject.Text;
            string desc = richTextBoxCreateDescription.Text;
            int desc_word_count = ai.word_Count(desc);
            DataRow drNewTicket = dsDefult.Tables["Ticket"].NewRow();
            bool ok = true;

            if (subject.Length == 0)
            {
                ep.SetError(textBoxCreateSubject, "Please enter the subject of the issue(s)");
                ok = false;
            }

            if (desc.Length == 0)
            {
                ep.SetError(richTextBoxCreateDescription, "Please enter the Description of the issue(s)");
                ok = false;
            }
            else if (desc.Length < 5)
            {
                ep.SetError(richTextBoxCreateDescription, "The entered description is below \nthe requiered word count (5 words minimum)");
                ok = false;
            }


            if (ok)
            {
                ai.server = server;

                drNewTicket["Problem_No"] = int.Parse(labelCreateProblemIDValue.Text);
                drNewTicket["Client_ID"] = drClient.ItemArray[0];
                drNewTicket["Date_time_Raised"] = labelCrateDTMadeValue.Text;
                drNewTicket["Priority_No"] = 3;
                drNewTicket["Subject_Title"] = subject;
                drNewTicket["Description_"] = desc;
                drNewTicket["Completed"] = 0;
                drNewTicket["Contractor_ID"] = ai.pick_Contractor(desc);

                dsDefult.Tables["Ticket"].Rows.Add(drNewTicket);

                daDefultTicket.Update(dsDefult, "Ticket");

                daTicket.Update(ds, "Ticket");
                daTicket.Fill(ds, "Ticket");

                daDesc.Update(ds, "Desc");
                daDesc.Fill(ds, "Desc");

                int count = dsDefult.Tables["Ticket"].Rows.Count;

                labelCreateProblemIDValue.Text = ((int)dsDefult.Tables["Ticket"].Rows[count - 1][0] + 1).ToString();

                textBoxCreateSubject.Text = "";
                richTextBoxCreateDescription.Text = "";
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            string userFile = File.ReadAllText("User.txt"); //read from User.txt file
            int toggle = 0;//toggle to next string

            for (int i = 0; i < userFile.Length; i++)//loop through file string
            {
                if (userFile[i] == '|')//next string and skip to next loop iteration
                {
                    toggle++;
                    continue;
                }

                if (toggle == 0) remember += userFile[i];//fill should remember
                else if (toggle == 1) userID += userFile[i];//fill user ID
                else if (toggle == 2) userName += userFile[i];//fill user name
                else if (toggle == 3) password += userFile[i];//fill password
                else server += userFile[i];//fill server name
            }

            //DESKTOP-NSHTTAL
            connStr = @"Data Source = " + server + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";//connection string

            //create population
            conn = new SqlConnection(connStr);
            conn.Open();

            //populate AI Class
            ai.populate();

            //populating the defult Data Set
            //deafult ticket
            daDefultTicket = new SqlDataAdapter(sqlDefultTicket, conn);
            cmdBDefultTicket = new SqlCommandBuilder(daDefultTicket);
            daDefultTicket.FillSchema(dsDefult, SchemaType.Source, "Ticket");
            daDefultTicket.Fill(dsDefult, "Ticket");

            //deafult Login
            daDefultLogin = new SqlDataAdapter(sqlDefultLogin, conn);
            cmdBDefultLogin = new SqlCommandBuilder(daDefultLogin);
            daDefultLogin.FillSchema(dsDefult, SchemaType.Source, "Login_");
            daDefultLogin.Fill(dsDefult, "Login_");

            //deafult client
            daDefultClient = new SqlDataAdapter(sqlDefultClient, conn);
            cmdBDefultClient = new SqlCommandBuilder(daDefultClient);
            daDefultClient.FillSchema(dsDefult, SchemaType.Source, "Client");
            daDefultClient.Fill(dsDefult, "Client");



            //preamiterised queries for the selected Data Set
            cmdClient = new SqlCommand(sqlGetClient, conn);
            cmdClient.Parameters.Add("@un", SqlDbType.VarChar);
            cmdClient.Parameters.Add("@pw", SqlDbType.VarChar);
            daClient = new SqlDataAdapter(cmdClient);
            daClient.FillSchema(ds, SchemaType.Source, "Client");
            cmdClient.Parameters["@un"].Value = userName;
            cmdClient.Parameters["@pw"].Value = password;
            daClient.Fill(ds, "Client");

            //view all main tickets
            cmdTicket = new SqlCommand(sqlPeramiterisedTickets, conn);
            cmdTicket.Parameters.Add("@un", SqlDbType.VarChar);
            cmdTicket.Parameters.Add("@pw", SqlDbType.VarChar);
            daTicket = new SqlDataAdapter(cmdTicket);
            daTicket.FillSchema(ds, SchemaType.Source, "Ticket");
            cmdTicket.Parameters["@un"].Value = userName;
            cmdTicket.Parameters["@pw"].Value = password;
            daTicket.Fill(ds, "Ticket");
            dataGridViewViewTickets.DataSource = ds.Tables["Ticket"];


            //view all ticket History update
            cmdTicketHistory = new SqlCommand(sqlPeramiterisedHistoryTickets, conn);
            cmdTicketHistory.Parameters.Add("@un", SqlDbType.VarChar).Value = userName;
            cmdTicketHistory.Parameters.Add("@pw", SqlDbType.VarChar).Value = password;
            daTicketHistory = new SqlDataAdapter(cmdTicketHistory);
            daTicketHistory.FillSchema(ds, SchemaType.Source, "Ticket_History");
            cmdTicketHistory.Parameters["@un"].Value = userName;
            cmdTicketHistory.Parameters["@pw"].Value = password;
            daTicketHistory.Fill(ds, "Ticket_History");
            dataGridViewTicketHistory.DataSource = ds.Tables["Ticket_history"];


            //ticket search
            cmdTicketSearch = new SqlCommand(sqlFindTicket, conn);
            cmdTicketSearch.Parameters.Add("@un", SqlDbType.VarChar);
            cmdTicketSearch.Parameters.Add("@pw", SqlDbType.VarChar);
            cmdTicketSearch.Parameters.Add("@f", SqlDbType.DateTime2);
            cmdTicketSearch.Parameters.Add("@t", SqlDbType.DateTime2);
            cmdTicketSearch.Parameters["@un"].Value = userName;
            cmdTicketSearch.Parameters["@pw"].Value = password;
            daTicketSearch = new SqlDataAdapter(cmdTicketSearch);
            daTicketSearch.FillSchema(ds2, SchemaType.Source, "Ticket");


            //ticket histoy sreach
            cmdHistoryTicketSearch = new SqlCommand(sqlFindTicketHistory, conn);
            cmdHistoryTicketSearch.Parameters.Add("@un", SqlDbType.VarChar);
            cmdHistoryTicketSearch.Parameters.Add("@pw", SqlDbType.VarChar);
            cmdHistoryTicketSearch.Parameters.Add("@f", SqlDbType.DateTime2);
            cmdHistoryTicketSearch.Parameters.Add("@t", SqlDbType.DateTime2);
            cmdHistoryTicketSearch.Parameters["@un"].Value = userName;
            cmdHistoryTicketSearch.Parameters["@pw"].Value = password;
            daHistoryTicketSearch = new SqlDataAdapter(cmdHistoryTicketSearch);
            daHistoryTicketSearch.FillSchema(ds2, SchemaType.Source, "Ticket_History");

            drClient = ds.Tables["Client"].Rows[0];//get current loggedin client

            //creates description tabel
            try
            {
                drTicket = ds.Tables["Ticket"].Rows[0];

                cmdDesc = new SqlCommand(sqlDesc, conn);
                cmdDesc.Parameters.Add("@d", SqlDbType.Int);
                cmdDesc.Parameters["@d"].Value = int.Parse(drTicket.ItemArray[0].ToString());
                daDesc = new SqlDataAdapter(cmdDesc);
                daDesc.FillSchema(ds, SchemaType.Source, "Desc");
                daDesc.Fill(ds, "Desc");
            }
            catch (IndexOutOfRangeException) { is_empty = true; }

            //creates description Ticket history tabel
            try
            {
                drHistoryDesc = ds.Tables["Ticket_history"].Rows[0];

                cmdHistoryDesc = new SqlCommand(sqlHistoryDesc, conn);
                cmdHistoryDesc.Parameters.Add("@UpNo", SqlDbType.Int);
                cmdHistoryDesc.Parameters["@UpNo"].Value = int.Parse(drHistoryDesc.ItemArray[0].ToString());
                daHistoryDesc = new SqlDataAdapter(cmdHistoryDesc);
                daHistoryDesc.FillSchema(ds, SchemaType.Source, "History_Desc");
            }
            catch (IndexOutOfRangeException) { is_empty2 = true; }

            //auto resizes the columns of the data grid view
            dataGridViewViewTickets.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewTicketHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //assinges vales to requied fields
            labelInfoNameValue.Text = drClient.ItemArray[1].ToString();
            labelInfoUserNameValue.Text = userName;
            textBoxInfoPassword.Text = password;
            labelInfoPhoneValue.Text = drClient.ItemArray[3].ToString();
            textBoxInfoEamil.Text = drClient.ItemArray[4].ToString();
            int count = dsDefult.Tables["Ticket"].Rows.Count;
            labelCreateProblemIDValue.Text = ((int)dsDefult.Tables["Ticket"].Rows[count - 1][0] + 1).ToString();
            labelCreateNameValue.Text = drClient[1].ToString();
        }

        private void dataGridViewViewTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (is_empty)
                {
                    //creats and fill the data table
                    cmdDesc = new SqlCommand(sqlDesc, conn);
                    cmdDesc.Parameters.Add("@d", SqlDbType.Int);
                    cmdDesc.Parameters["@d"].Value = int.Parse(dataGridViewViewTickets.SelectedRows[0].Cells[0].ToString());
                    daDesc = new SqlDataAdapter(cmdDesc);
                    daDesc.FillSchema(ds, SchemaType.Source, "Desc");
                    daDesc.Fill(ds, "Desc");
                }
                //executs comand and fills rich text box
                ds.Tables["Desc"].Clear();
                cmdDesc.Parameters["@d"].Value = int.Parse((dataGridViewViewTickets.SelectedRows[0].Cells[0].Value).ToString());
                daDesc.Fill(ds, "Desc");
                drDesc = ds.Tables["Desc"].Rows[0];
                richTextBoxViewDescription.Text = drDesc.ItemArray[0].ToString() + ":\n\n" + drDesc.ItemArray[1].ToString();
            }
            catch (Exception) { richTextBoxCreateDescription.Clear(); }
        }
    }
}
