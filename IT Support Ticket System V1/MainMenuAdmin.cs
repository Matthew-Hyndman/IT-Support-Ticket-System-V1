using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_Support_Ticket_System_V1
{
    public partial class MainMenuAdmin : Form
    {

        SqlDataAdapter daTicket, daTicketHistory, daDesc, daHistoryDesc, daTicketSearch, daHistoryTicketSearch, daClient,
            daDefultTicket, daDefultLogin, daDefultClient, daDeafultDepartment, daDeafultContractor, daViewContractor, daViewClient, daViewAllClients, daDeafultPriority,
            daDeafultContractorSpcialties, daDeafultSpcialties, daDeafultTicketHistory, daDeafultKeyWord, daDeafultKeyWordSpec, daDeafultNewKeyWords,
            daTicketEditView, daTicketEditView2, daLoginAdmin, daViewAllAssociationsDetail, daWordSearchNum, daWordSearchWord, daDeleteConSpecialities;

        //for peramiterised/custom queries only
        DataSet ds = new DataSet();//Tables: Ticket_History, Ticket, History_Desc, Desc, Client, TicketOfAccount
        DataSet ds2 = new DataSet();//Tables: Ticket_History, Ticket, Cilent, Contractor
        DataSet ds3 = new DataSet();//Tables: Ticket, Client, Login_, Key_Words


        //fills with only the raw tables with no preamiters
        DataSet dsDefult = new DataSet();

        SqlConnection conn;
        SqlCommand cmdTicket, cmdTicketHistory, cmdDesc, cmdHistoryDesc, cmdTicketSearch, cmdHistoryTicketSearch, cmdClient, cmdTicketEditView,
            cmdTicketEditView2, cmdLoginAdmin, cmdWordSearchNum, cmdWordSearchWord, cmdDeleteConSpecialities;

        SqlCommandBuilder cmdBDefultTicket, cmdBDefultLogin, cmdBDefultClient, cmdBViewContractor, cmdBViewClient,
            cmdBViewAllClients, cmdBViewAllAssociationsDetail, cmdBDeafultPriority,
            cmdBDeafultContractorSpcialties, cmdBDeafultSpcialties, cmdBDeafultContractor, cmdBDeafultTicketHistory,
            cmdBDeafultKeyWord, cmdBDeafultKeyWordSpec, cmdBDeafultNewKeyWords;

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
        string sqlTicketAdminEdit = @"exec Get_Tickets_Admin @uname = @un, @pword = @pw";

        string sqlHistoryDesc = @"exec Get_History_Desc @UpdateNo = @UpNo";
        string sqlWordSearchNum = @"exec Key_Word_Search_Num @word = @num";
        string sqlWordSearchWord = @"exec Key_Word_Search_Varchar @word = @w";

        string sqlDeleteConSpecialities = @"delete from Contractor_Spcialties where Contractor_ID = @id";

        //Search Queris
        string sqlFindTicket = @"exec Find_Ticket @uname = @un, @pword = @pw, @from = @f, @to = @t";
        string sqlFindTicketHistory = @"exec Find_Ticket_History @uname = @un, @pword = @pw,  @from = @f, @to = @t";

        //General/defualt Queries
        string sqlDefultTicket = @"select * from Ticket";
        string sqlDefultLogin = @"select * from Login_";
        string sqlDefultClient = @"select * from Client";
        string sqlDeafultPriority = @"select * from Priority_";
        string sqlDeafultContractorSpcialties = @"select * from Contractor_Spcialties";
        string sqlDeafultSpcialties = @"select * from Spcialties";
        string sqlDeafultDepartment = @"select * from Department";
        string sqlDeafultContractor = @"select * from Contractor";
        string sqlDeafultTicketHistory = @"select * from Ticket_History";
        string sqlDeafultKeyWord = @"select * from Key_Words";
        string sqlDeafultKeyWordSpec = @"select * from Spcialty_Key_Words";
        string sqlDeafultNewKeyWords = @"select * from New_Key_Words";

        //View Queries
        string sqlViewContractor = @"select * from ComboBox_Contractor";
        string sqlViewClient = @"select * from ComboBox_Client";
        string sqlViewAllClients = @"select * from ViewAllClients";
        string sqlViewAllAssociationsDetail = @"select * from Get_All_Associations";

        //validate Login Admin
        string sqlLoginAdmin = @"exec Find_Login_Admin @uname = @un, @pword = @pw";


        string remember;

        bool showPassword = false;

        List<int> ticketNoList = new List<int>();

        bool is_empty = false;
        bool is_empty2 = false;

        List<ListViewItem> specList = new List<ListViewItem>();

        public MainMenuAdmin()
        {
            InitializeComponent();
        }

        private void buttonAISearch_Click(object sender, EventArgs e)
        {
            string search = textBoxAIWordSearch.Text;
            int searchNum;
            try
            {
                if (search == "") throw new ArgumentNullException();
                searchNum = int.Parse(search);
            }
            catch (FormatException)
            {
                cmdWordSearchWord.Parameters["@w"].Value = search;
                daWordSearchWord.Fill(ds3, "SearchViaWords");

                dataGridViewAI.DataSource = ds3.Tables["SearchViaWords"];
                if (ds3.Tables["SearchViaWords"].Rows.Count == 0) ep.SetError(textBoxAIWordSearch, "Word does not exist");
                return;
            }
            catch (ArgumentNullException)
            {

                dataGridViewAI.DataSource = dsDefult.Tables["Key_Words"];
                return;
            }
            cmdWordSearchNum.Parameters["@num"].Value = searchNum;
            ds3.Tables["SearchViaNum"].Clear();
            daWordSearchNum.Fill(ds3, "SearchViaNum");
            dataGridViewAI.DataSource = ds3.Tables["SearchViaNum"];
            try { int test = int.Parse(ds3.Tables["SearchViaNum"].Rows[0].ItemArray[2].ToString()); }
            catch (NullReferenceException){
                if (searchNum == 0) ep.SetError(textBoxAIWordSearch, "Aknowledged words do not exist");
                if (searchNum == 1) ep.SetError(textBoxAIWordSearch, "Ignoired words do not exist");
                if (ds3.Tables["SearchViaNum"].Rows.Count == 0) ep.SetError(textBoxAIWordSearch, "ID does not exist");
            }
        }

        private void buttonAIDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = dsDefult.Tables["Key_Words"].Rows.Find(dataGridViewAI.SelectedRows[0].Cells[0].Value);
            dr.Delete();
            daDeafultKeyWord.Update(dsDefult, "Key_Words");
        }

        private void buttonAIContinue_Click(object sender, EventArgs e)
        {
            int index = dsDefult.Tables["Key_Words"].Rows.Count;
            int index2 = listViewAIDiscoveredWords.SelectedItems[0].Index;
            string str = listViewAIDiscoveredWords.SelectedItems[0].SubItems[2].Text;
            DataRow dr = dsDefult.Tables["Key_Words"].NewRow();
            DataRow dr2 = dsDefult.Tables["New_Key_Words"].Rows.Find(int.Parse(listViewAIDiscoveredWords.SelectedItems[0].SubItems[0].Text));
            DataRow dr3 = dsDefult.Tables["Key_Words"].Rows.Find((int)dsDefult.Tables["Key_Words"].Rows[index - 1][0]);


            dr["Word_ID"] = (int)dr3[0] + 1;
            dr["Word"] = dr2[1];
            if (str == "Yes") dr["Ignore_Word"] = 1;
            else dr["Ignore_Word"] = 0;

            dsDefult.Tables["Key_Words"].Rows.Add(dr);
            dr2.Delete();

            daDeafultKeyWord.Update(dsDefult, "Key_Words");
            daDeafultNewKeyWords.Update(dsDefult, "New_Key_Words");

            listViewAIDiscoveredWords.Items[index2].Remove();
        }

        private void buttonAIAdd_Click(object sender, EventArgs e)
        {
            if (textBoxAINewWord.Text.Length < 2)
            {
                ep.SetError(buttonAIAdd, "Textbox requiers 2 \nor more charactor");
            }
            else
            {
                DataRow dr = dsDefult.Tables["New_Key_Words"].NewRow();

                string[] strArr = { (dsDefult.Tables["New_Key_Words"].Rows.Count + 100).ToString(), textBoxAINewWord.Text, "No" };
                ListViewItem listViewItem = new ListViewItem(strArr);
                listViewAIDiscoveredWords.Items.Add(listViewItem);

                dr["Word_ID"] = int.Parse(strArr[0]);
                dr["Word"] = strArr[1];

                dsDefult.Tables["New_Key_Words"].Rows.Add(dr);
                daDeafultNewKeyWords.Update(dsDefult, "New_Key_Words");
            }
        }

        private void listViewAIDiscoveredWords_MouseClick(object sender, MouseEventArgs e)
        {
            string str = listViewAIDiscoveredWords.SelectedItems[0].SubItems[2].Text;

            if (str == "No") listViewAIDiscoveredWords.SelectedItems[0].SubItems[2].Text = "Yes";
            else listViewAIDiscoveredWords.SelectedItems[0].SubItems[2].Text = "No";
        }

        private void buttonAIAssociate_Click(object sender, EventArgs e)
        {
            DataRow dr = dsDefult.Tables["Spcialty_Key_Words"].NewRow();

            string[] strArr = { listViewAISpcialitiy.SelectedItems[0].Text, listViewAISpcialitiy.SelectedItems[0].SubItems[1].Text,
                dataGridViewAI.SelectedRows[0].Cells[0].Value.ToString(), dataGridViewAI.SelectedRows[0].Cells[1].Value.ToString() };

            ListViewItem listViewItem = new ListViewItem(strArr);

            dr["Spcialties_ID"] = int.Parse(strArr[0]);
            dr["Word_ID"] = int.Parse(strArr[2]);

            dsDefult.Tables["Spcialty_Key_Words"].Rows.Add(dr);
            daDeafultKeyWordSpec.Update(dsDefult, "Spcialty_Key_Words");

            listViewAIAssociations.Items.Add(listViewItem);
        }
        private void buttonAIDisassociate_Click(object sender, EventArgs e)
        {
            //string[] strItem = { listViewAIAssociations.SelectedItems[0].SubItems[0].Text, listViewAIAssociations.SelectedItems[0].SubItems[1].Text, 
            //    listViewAIAssociations.SelectedItems[0].SubItems[2].Text, listViewAIAssociations.SelectedItems[0].SubItems[3].Text };

            int index = listViewAIAssociations.SelectedItems[0].Index;
            string[] strIDs = { listViewAIAssociations.SelectedItems[0].SubItems[0].Text, listViewAIAssociations.SelectedItems[0].SubItems[2].Text };
            DataRow dr = dsDefult.Tables["Spcialty_Key_Words"].Rows.Find(strIDs);
            dr.Delete();
            daDeafultKeyWordSpec.Update(dsDefult, "Spcialty_Key_Words");

            listViewAIAssociations.Items[index].Remove();
        }

        private void buttonAIToggelIgnore_Click(object sender, EventArgs e)
        {
            DataRow dr = dsDefult.Tables["Key_Words"].Rows.Find(dataGridViewAI.SelectedRows[0].Cells[0].Value);

            dr.BeginEdit();
            if (dr[2].ToString() == "0") dr["Ignore_Word"] = 1;
            else dr["Ignore_Word"] = 0;
            dr.EndEdit();

            daDeafultKeyWord.Update(dsDefult, "Key_Words");

        }

        private void buttonClientAdd_Click(object sender, EventArgs e)
        {
            int id = dsDefult.Tables["login_"].Rows.Count + 101;
            DataRow logindr = dsDefult.Tables["login_"].NewRow();
            logindr["Log_ID"] = id;
            logindr["User_Name_"] = textBoxClientFN.Text + "_" + textBoxClientLN.Text;
            logindr["Password_"] = textBoxClientFN.Text.ToUpper()[0] + textBoxClientLN.Text.ToUpper()[0] + id.ToString();

            dsDefult.Tables["login_"].Rows.Add(logindr);

            DataRow clientdr = dsDefult.Tables["Client"].NewRow();

            clientdr["Client_ID"] = dsDefult.Tables["Client"].Rows.Count + 10001;
            clientdr["First_Name"] = textBoxClientFN.Text;
            clientdr["Last_Name"] = textBoxClientLN.Text;
            clientdr["Department_No"] = comboBoxClientDepartment.SelectedValue;
            clientdr["Phone"] = textBoxClientPhone.Text;
            clientdr["Email"] = textBoxInfoEamil.Text;
            clientdr["Log_ID"] = id;

            dsDefult.Tables["Client"].Rows.Add(clientdr);

            if (checkBoxAccountMakeAdmin.Checked)
            {
                DataRow contractordr = dsDefult.Tables["Contractor"].NewRow();
                int conID = dsDefult.Tables["Contractor"].Rows.Count;

                contractordr["Contractor_ID"] = conID;
                contractordr["First_Name"] = textBoxClientFN.Text;
                contractordr["Last_Name"] = textBoxClientLN.Text;
                contractordr["Department_No"] = comboBoxClientDepartment.SelectedValue;
                contractordr["Phone"] = textBoxClientPhone.Text;
                contractordr["Email"] = textBoxClientPhone.Text;
                contractordr["Log_ID"] = id;

                dsDefult.Tables["Contractor"].Rows.Add(contractordr);
                daDeafultContractor.Fill(dsDefult, "Contractor");
            }

            daDefultClient.Update(dsDefult, "Client");
            daClient.Update(ds, "Client");
            daViewClient.Update(ds2, "Client");
            daViewContractor.Update(ds2, "Contractor");
            daViewAllClients.Fill(ds3, "Client");
        }

        private void buttonClientRemove_Click(object sender, EventArgs e)
        {
            cmdTicketEditView2.Parameters["@un"].Value = ds3.Tables["Login_"].Rows[0].ItemArray[1];
            cmdTicketEditView2.Parameters["@pw"].Value = ds3.Tables["Login_"].Rows[0].ItemArray[2];
            daTicketEditView2.Fill(ds, "TicketOfAccount");

            if (ds3.Tables["Login_"].Rows.Count == 1)
            {

                foreach (DataRow drow in ds.Tables["TicketOfAccount"].Rows)
                {

                    DataRow ticket = dsDefult.Tables["Ticket"].Rows.Find(drow[0]);

                    foreach (DataRow drow1 in dsDefult.Tables["Ticket_History"].Rows)
                    {
                        if (drow1[1] == ticket[0])
                        {
                            DataRow drs1 = dsDefult.Tables["Ticket_History"].Rows.Find(drow[1]);
                            drs1.Delete();
                            DataRow dr_2 = ds2.Tables["Ticket_History"].Rows.Find(drow[1]);
                            dr_2.Delete();

                            //drow1.Delete();
                            daTicketHistory.Fill(dsDefult, "Ticket_History");
                            daHistoryTicketSearch.Update(ds2, "Ticket_History");

                        }

                    }

                    ticket.BeginEdit();
                    ticket["Contractor_ID"] = 10000;
                    ticket.EndEdit();
                    daDefultTicket.Fill(dsDefult, "Ticket");
                }



                int adminID = (int)ds3.Tables["Login_"].Rows[0].ItemArray[3];
                DataRow dr = dsDefult.Tables["Contractor"].Rows.Find(adminID);
                dr.Delete();
                DataRow dr_ = ds2.Tables["Contractor"].Rows.Find(adminID);
                dr_.Delete();
                daDeafultContractor.Fill(dsDefult, "Contractor");
                daViewContractor.Fill(ds2, "Contractor");

            }

            int clientID = (int)dataGridViewAccounts.SelectedRows[0].Cells[0].Value;
            foreach (DataRow drow1 in dsDefult.Tables["Ticket_History"].Rows)
            {
                if ((int)drow1[1] == clientID)
                {
                    DataRow dr1_1 = dsDefult.Tables["Ticket"].Rows.Find(clientID);
                    dr1_1.Delete();
                    DataRow dr_2 = ds.Tables["Ticket"].Rows.Find(clientID);
                    dr_2.Delete();
                    DataRow dr_3 = ds2.Tables["Ticket"].Rows.Find(clientID);
                    dr_3.Delete();
                    DataRow dr_4 = ds3.Tables["Ticket"].Rows.Find(clientID);
                    dr_4.Delete();

                }
                drow1.Delete();
                daDefultTicket.Fill(dsDefult, "Ticket");
                daTicket.Fill(ds, "Ticket");
                daTicketSearch.Update(ds2, "Ticket");
                daTicketEditView.Fill(ds3, "Ticket");

            }
            DataRow dr1 = dsDefult.Tables["Client"].Rows.Find(clientID);
            dr1.Delete();
            DataRow dr__1 = ds2.Tables["Client"].Rows.Find(clientID);
            dr__1.Delete();
            DataRow dr___1 = ds3.Tables["Client"].Rows.Find(clientID);
            dr___1.Delete();

            daDefultClient.Fill(dsDefult, "Client");
            daViewClient.Fill(ds2, "Client");
            daViewAllClients.Fill(ds3, "Client");

            int LoginID = (int)ds3.Tables["Login_"].Rows[0].ItemArray[0];
            DataRow dr2 = dsDefult.Tables["Login_"].Rows.Find(LoginID);
            dr2.Delete();
            daDefultLogin.Fill(dsDefult, "Login_");

        }

        private void buttonClientUpdate_Click(object sender, EventArgs e)
        {
            int clientid = (int)dataGridViewAccounts.SelectedRows[0].Cells[0].Value;
            DataRow clientdr = dsDefult.Tables["Client"].Rows.Find(clientid);

            clientdr.BeginEdit();

            clientdr["First_Name"] = textBoxClientFN.Text;
            clientdr["Last_Name"] = textBoxClientLN.Text;
            clientdr["Department_No"] = comboBoxClientDepartment.SelectedValue;
            clientdr["Phone"] = textBoxClientPhone.Text;
            clientdr["Email"] = textBoxAccoutEmail.Text;

            clientdr.EndEdit();

            if (ds3.Tables["Login_"].Rows.Count == 1)
            {
                int adminID = (int)ds3.Tables["Login_"].Rows[0].ItemArray[3];
                DataRow condr = dsDefult.Tables["Contractor"].Rows.Find(adminID);

                if (checkBoxAccountMakeAdmin.Checked)
                {
                    condr.BeginEdit();

                    condr["First_Name"] = textBoxClientFN.Text;
                    condr["Last_Name"] = textBoxClientLN.Text;
                    condr["Department_No"] = comboBoxClientDepartment.SelectedValue;
                    condr["Phone"] = textBoxClientPhone.Text;
                    condr["Email"] = textBoxAccoutEmail.Text;

                    condr.EndEdit();

                    int id = (int)condr[0];
                    int tableCount = dsDefult.Tables["Contractor_Spcialties"].Rows.Count;
                    for (int i = 0; i < tableCount; i++)
                    {
                        try
                        {
                            DataRow dataRow = dsDefult.Tables["Contractor_Spcialties"].Rows.Find(id);
                            dataRow.Delete();
                        }
                        catch (MissingPrimaryKeyException) { break; }
                    }

                    foreach (ListViewItem item in listViewAccoutsSpedialites.Items)
                    {
                        if (item.SubItems[2].Text == "Yes")
                        {
                            DataRow dr = dsDefult.Tables["Contractor_Spcialties"].NewRow();
                            dr["Spcialties_ID"] = item.SubItems[0];
                            dr["Contractor_ID"] = item.SubItems[0];
                            dsDefult.Tables["Contractor_Spcialties"].Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    int id = (int)condr[0];
                    int tableCount = dsDefult.Tables["Contractor_Spcialties"].Rows.Count;
                    for (int i = 0; i < tableCount; i++)
                    {
                        try
                        {
                            DataRow dataRow = dsDefult.Tables["Contractor_Spcialties"].Rows.Find(id);
                            dataRow.Delete();
                        }
                        catch (MissingPrimaryKeyException) { break; }
                    }

                    condr.Delete();
                }
            }

            daDefultClient.Update(dsDefult, "Client");
            daClient.Update(ds, "Client");
            daViewClient.Update(ds2, "Client");
            daViewContractor.Update(ds2, "Contractor");
            daViewAllClients.Update(ds3, "Client");
            daViewAllClients.Fill(ds3, "Client");


        }

        private void buttonClientRemoveSpec_Click(object sender, EventArgs e)
        {
            DataRow dr = dsDefult.Tables["Spcialties"].Rows.Find(int.Parse(listViewAccoutsSpedialites.SelectedItems[0].Text));
            listViewAccoutsSpedialites.Items.Remove(listViewAccoutsSpedialites.SelectedItems[0]);
            specList.Remove(listViewAccoutsSpedialites.SelectedItems[0]);
            dr.Delete();
            daDeafultSpcialties.Update(dsDefult, "Spcialties");
        }

        private void buttonClientsAddSpec_Click(object sender, EventArgs e)
        {
            if (textBoxAccountSpecialtes.Text != "")
            {
                int count = listViewAccoutsSpedialites.Items.Count + 100;
                string sName = textBoxAccountSpecialtes.Text;

                textBoxAccountSpecialtes.Clear();

                DataRow dr = dsDefult.Tables["Spcialties"].NewRow();

                dr["Spcialties_ID"] = count;
                dr["Spcialties_Name"] = sName;

                dsDefult.Tables["Spcialties"].Rows.Add(dr);
                daDeafultSpcialties.Update(dsDefult, "Spcialties");

                string[] strArr = { count.ToString(), sName, "No" };
                ListViewItem viewItem = new ListViewItem(strArr);
                listViewAccoutsSpedialites.Items.Add(viewItem);
            }
        }

        private void listViewAccoutsSpedialites_MouseClick(object sender, MouseEventArgs e)
        {
            string str = listViewAccoutsSpedialites.SelectedItems[0].SubItems[2].Text;

            if (str == "No")
            {
                listViewAccoutsSpedialites.SelectedItems[0].SubItems[2].Text = "Yes";
                specList[listViewAccoutsSpedialites.SelectedItems[0].Index].SubItems[2].Text = "Yes";
            }
            else
            {
                int value = int.Parse(listViewAccoutsSpedialites.SelectedItems[0].SubItems[0].Text);
                listViewAccoutsSpedialites.SelectedItems[0].SubItems[2].Text = "No";
                specList[listViewAccoutsSpedialites.SelectedItems[0].Index].SubItems[2].Text = "No";

            }

            listViewAccoutsSpedialites.SelectedItems[0].Checked = false;

        }


        private void dataGridViewAccounts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            drClient = dsDefult.Tables["Client"].Rows.Find(dataGridViewAccounts.SelectedRows[0].Cells[0].Value);

            textBoxClientFN.Text = drClient.ItemArray[1].ToString();
            textBoxClientLN.Text = drClient.ItemArray[2].ToString();
            comboBoxClientDepartment.SelectedValue = drClient.ItemArray[3].ToString();
            textBoxClientPhone.Text = drClient.ItemArray[4].ToString();
            textBoxAccoutEmail.Text = drClient.ItemArray[5].ToString();

            DataRow dr = dsDefult.Tables["Login_"].Rows.Find(drClient.ItemArray[6]);

            cmdLoginAdmin.Parameters["@un"].Value = dr.ItemArray[1].ToString();
            cmdLoginAdmin.Parameters["@pw"].Value = dr.ItemArray[2].ToString();
            daLoginAdmin.Update(ds3, "Login_");
            daLoginAdmin.Fill(ds3, "Login_");


            if (ds3.Tables["Login_"].Rows.Count == 1)
            {
                checkBoxAccountMakeAdmin.Checked = true;
                for (int i = 0; i < dsDefult.Tables["Contractor_Spcialties"].Rows.Count; i++)
                {
                    for (int j = 0; j < (int)dsDefult.Tables["Spcialties"].Rows.Count; j++)
                        if ((int)ds3.Tables["Login_"].Rows[0][3] == (int)dsDefult.Tables["Contractor_Spcialties"].Rows[i][1] &&
                            (int)dsDefult.Tables["Spcialties"].Rows[j][0] == (int)dsDefult.Tables["Contractor_Spcialties"].Rows[i][0])
                        {
                            int index = 0;
                            foreach (ListViewItem item in specList)
                            {
                                if (dsDefult.Tables["Spcialties"].Rows[j][0].ToString() == item.SubItems[0].Text) index = item.Index;
                                else item.SubItems[2].Text = "No";
                            }

                            listViewAccoutsSpedialites.Items[index].SubItems[2].Text = "Yes";
                        }
                }
            }
            else
                checkBoxAccountMakeAdmin.Checked = false;
        }
        private void dataGridViewTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (is_empty)
                {
                    cmdDesc = new SqlCommand(sqlDesc, conn);
                    cmdDesc.Parameters.Add("@d", SqlDbType.Int);
                    cmdDesc.Parameters["@d"].Value = dataGridViewTicketsEdit.SelectedRows[0].Cells[0].Value;
                    daDesc = new SqlDataAdapter(cmdDesc);
                    daDesc.FillSchema(ds, SchemaType.Source, "Desc");
                    daDesc.Fill(ds, "Desc");
                }
                ds.Tables["Desc"].Clear();
                cmdDesc.Parameters["@d"].Value = dataGridViewTicketsEdit.SelectedRows[0].Cells[0].Value;
                daDesc.Fill(ds, "Desc");
                drDesc = ds.Tables["Desc"].Rows[0];
                richTextBoxTicketsEditDescription.Text = drDesc.ItemArray[1].ToString();
                textBoxEditSubjuet.Text = drDesc.ItemArray[0].ToString();
                comboBoxEditContractor.Text = dataGridViewTicketsEdit.SelectedRows[0].Cells[3].Value.ToString();
                comboBoxEditClient.Text = dataGridViewTicketsEdit.SelectedRows[0].Cells[1].Value.ToString();
                comboBoxEditPriority.Text = dataGridViewTicketsEdit.SelectedRows[0].Cells[4].Value.ToString();
                bool completed = (dataGridViewTicketsEdit.SelectedRows[0].Cells[5].Value.ToString() == "1");
                if (!completed)
                    comboBoxTicketsStatus.Text = "Incomplete";
                else
                    comboBoxTicketsStatus.Text = "Complete";
            }
            catch (Exception) { richTextBoxCreateDescription.Clear(); }

        }

        private void buttonTicketsUpdate_Click(object sender, EventArgs e)
        {
            bool ok = true;
            int id = 0, clientID = 0, priority = 0, completed = 0, contractor = 0;
            string subject = "", desc = "", clientName = "", contractorName = "", priorityDesc = "";

            try
            {
                id = (int)dataGridViewTicketsEdit.SelectedRows[0].Cells[0].Value;

                clientID = (int)comboBoxEditClient.SelectedValue;

                priority = (int)comboBoxEditPriority.SelectedValue;

                completed = (int)comboBoxTicketsStatus.SelectedIndex;

                contractor = (int)comboBoxEditContractor.SelectedValue;

                if (textBoxEditSubjuet.Text != "")
                {
                    subject = textBoxEditSubjuet.Text;
                }
                else
                {
                    ok = false;
                    ep.SetError(textBoxEditSubjuet, "The Subjet is blank");
                    throw new FormatException();
                }

                if (richTextBoxCreateDescription.Text == "")
                {
                    desc = richTextBoxTicketsEditDescription.Text;
                }
                else
                {
                    ok = false;
                    ep.SetError(richTextBoxTicketsEditDescription, "The Description is blank");
                    throw new FormatException();
                }


                clientName = comboBoxEditClient.Text;

                contractorName = comboBoxEditContractor.Text;

                priorityDesc = comboBoxEditPriority.Text;

            }
            catch (NullReferenceException)
            {
                ok = false;
                MessageBox.Show("Please click a row header");
            }
            catch (FormatException) { }



            if (ok)
            {
                string raised = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                string timeComlpeted = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();


                DataRow dr = dsDefult.Tables["Ticket"].Rows.Find(id);

                dr.BeginEdit();

                dr["Client_ID"] = clientID;
                dr["Date_time_Raised"] = raised;
                dr["Priority_No"] = priority;
                dr["Subject_Title"] = subject;
                dr["Description_"] = desc;
                dr["Completed"] = completed;
                if (comboBoxTicketsStatus.SelectedIndex == 1)
                    dr["Date_time_completed"] = timeComlpeted;
                dr["Contractor_ID"] = contractor;

                dr.EndEdit();
                daDefultTicket.Update(dsDefult, "Ticket");

                daTicketEditView.Fill(ds3, "Ticket");
            }
        }

        private void buttonTicketsDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = dsDefult.Tables["Ticket"].Rows.Find(dataGridViewTicketsEdit.SelectedRows[0].Cells[0].Value);
            DataRow dr2 = ds3.Tables["Ticket"].Rows.Find(dataGridViewTicketsEdit.SelectedRows[0].Cells[0].Value);

            if (dr["Completed"].ToString() == "1")
            {
                dr.Delete();
                dr2.Delete();
                daDefultTicket.Update(dsDefult, "Ticket");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete " + dr["Problem_No"].ToString() + "?", "Delete Ticket", MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    dr.Delete();
                    dr2.Delete();
                    daDefultTicket.Update(dsDefult, "Ticket");
                }
            }
        }

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
            // changes the user's password
            if (textBoxInfoEamil.Text == "")
            {
                ep.SetError(buttonInfoEmailChange, "Textbox is empty");
            }
            else if (textBoxInfoEamil.Text.Length < 10 || textBoxInfoEamil.Text.Length > 60)
            {
                ep.SetError(buttonInfoEmailChange, "The password can only be between \n10 and 60 characters long");
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

            if (password.Length < 7 || password.Length > 20)
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

                drDefult["Log_ID"] = userID;
                drDefult["User_Name_"] = userName;
                drDefult["Password_"] = password;

                drDefult.EndEdit();
                daDefultLogin.Update(dsDefult, "Login_");
                daDefultLogin.Fill(dsDefult, "Login_");

                textBoxInfoPasswordChange.Text = "";
                textBoxInfoPassword.Text = password;
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
                DataRow drNewTicket = dsDefult.Tables["Ticket"].NewRow();

                ai.server = server;

                drNewTicket["Problem_No"] = int.Parse(labelCreateTicketIDValue.Text);
                drNewTicket["Client_ID"] = drClient.ItemArray[0];
                drNewTicket["Date_time_Raised"] = labelCrateDTMadeValue.Text;
                drNewTicket["Priority_No"] = 3;
                drNewTicket["Subject_Title"] = subject;
                drNewTicket["Description_"] = desc;
                drNewTicket["Completed"] = 0;
                drNewTicket["Contractor_ID"] = ai.pick_Contractor(desc);

                dsDefult.Tables["Ticket"].Rows.Add(drNewTicket);

                daDefultTicket.Update(dsDefult, "Ticket");
                //daDefultTicket.Fill(dsDefult, "Ticket");

                daTicket.Update(ds, "Ticket");
                daTicket.Fill(ds, "Ticket");

                daDesc.Update(ds, "Desc");
                daDesc.Fill(ds, "Desc");

                int count = dsDefult.Tables["Ticket"].Rows.Count;

                labelCreateTicketIDValue.Text = ((int)dsDefult.Tables["Ticket"].Rows[count - 1][0] + 1).ToString();

                textBoxCreateSubject.Text = "";
                richTextBoxCreateDescription.Text = "";
            }
        }

        private void MainMenuAdmin_Load(object sender, EventArgs e)
        {
            string userFile = File.ReadAllText("User.txt");
            int toggle = 0;
            for (int i = 0; i < userFile.Length; i++)
            {
                if (userFile[i] == '|')
                {
                    toggle++;
                    continue;
                }

                if (toggle == 0) remember += userFile[i];
                else if (toggle == 1) userID += userFile[i];
                else if (toggle == 2) userName += userFile[i];
                else if (toggle == 3) password += userFile[i];
                else server += userFile[i];
            }

            userName.Trim();
            password.Trim();

            //DESKTOP-NSHTTAL
            connStr = @"Data Source = " + server + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";

            conn = new SqlConnection(connStr);
            conn.Open();

            ai.populate();

            //populating the defult Data Set
            daDefultTicket = new SqlDataAdapter(sqlDefultTicket, conn);
            cmdBDefultTicket = new SqlCommandBuilder(daDefultTicket);
            daDefultTicket.FillSchema(dsDefult, SchemaType.Source, "Ticket");
            daDefultTicket.Fill(dsDefult, "Ticket");

            daDeafultTicketHistory = new SqlDataAdapter(sqlDeafultTicketHistory, conn);
            cmdBDeafultTicketHistory = new SqlCommandBuilder(daDeafultTicketHistory);
            daDeafultTicketHistory.FillSchema(dsDefult, SchemaType.Source, "Ticket_History");
            daDeafultTicketHistory.Fill(dsDefult, "Ticket_History");

            daDefultLogin = new SqlDataAdapter(sqlDefultLogin, conn);
            cmdBDefultLogin = new SqlCommandBuilder(daDefultLogin);
            daDefultLogin.FillSchema(dsDefult, SchemaType.Source, "Login_");
            daDefultLogin.Fill(dsDefult, "Login_");

            daDefultClient = new SqlDataAdapter(sqlDefultClient, conn);
            cmdBDefultClient = new SqlCommandBuilder(daDefultClient);
            daDefultClient.FillSchema(dsDefult, SchemaType.Source, "Client");
            daDefultClient.Fill(dsDefult, "Client");

            daDeafultPriority = new SqlDataAdapter(sqlDeafultPriority, conn);
            cmdBDeafultPriority = new SqlCommandBuilder(daDeafultPriority);
            daDeafultPriority.FillSchema(dsDefult, SchemaType.Source, "Priority_");
            daDeafultPriority.Fill(dsDefult, "Priority_");

            daDeafultContractorSpcialties = new SqlDataAdapter(sqlDeafultContractorSpcialties, conn);
            cmdBDeafultContractorSpcialties = new SqlCommandBuilder(daDeafultContractorSpcialties);
            daDeafultContractorSpcialties.FillSchema(dsDefult, SchemaType.Source, "Contractor_Spcialties");
            daDeafultContractorSpcialties.Fill(dsDefult, "Contractor_Spcialties");

            daDeafultSpcialties = new SqlDataAdapter(sqlDeafultSpcialties, conn);
            cmdBDeafultSpcialties = new SqlCommandBuilder(daDeafultSpcialties);
            daDeafultSpcialties.FillSchema(dsDefult, SchemaType.Source, "Spcialties");
            daDeafultSpcialties.Fill(dsDefult, "Spcialties");

            daDeafultDepartment = new SqlDataAdapter(sqlDeafultDepartment, conn);
            cmdBDeafultSpcialties = new SqlCommandBuilder(daDeafultDepartment);
            daDeafultDepartment.FillSchema(dsDefult, SchemaType.Source, "Department");
            daDeafultDepartment.Fill(dsDefult, "Department");

            daDeafultContractor = new SqlDataAdapter(sqlDeafultContractor, conn);
            cmdBDeafultContractor = new SqlCommandBuilder(daDeafultContractor);
            daDeafultContractor.FillSchema(dsDefult, SchemaType.Source, "Contractor");
            daDeafultContractor.Fill(dsDefult, "Contractor");

            daDeafultKeyWord = new SqlDataAdapter(sqlDeafultKeyWord, conn);
            cmdBDeafultKeyWord = new SqlCommandBuilder(daDeafultKeyWord);
            daDeafultKeyWord.FillSchema(dsDefult, SchemaType.Source, "Key_Words");
            daDeafultKeyWord.Fill(dsDefult, "Key_Words");
            dataGridViewAI.DataSource = dsDefult.Tables["Key_Words"];

            daDeafultKeyWordSpec = new SqlDataAdapter(sqlDeafultKeyWordSpec, conn);
            cmdBDeafultKeyWordSpec = new SqlCommandBuilder(daDeafultKeyWordSpec);
            daDeafultKeyWordSpec.FillSchema(dsDefult, SchemaType.Source, "Spcialty_Key_Words");
            daDeafultKeyWordSpec.Fill(dsDefult, "Spcialty_Key_Words");

            daDeafultNewKeyWords = new SqlDataAdapter(sqlDeafultNewKeyWords, conn);
            cmdBDeafultNewKeyWords = new SqlCommandBuilder(daDeafultNewKeyWords);
            daDeafultNewKeyWords.FillSchema(dsDefult, SchemaType.Source, "New_Key_Words");
            daDeafultNewKeyWords.Fill(dsDefult, "New_Key_Words");

            //queuey views
            daViewContractor = new SqlDataAdapter(sqlViewContractor, conn);
            cmdBViewContractor = new SqlCommandBuilder(daViewContractor);
            daViewContractor.FillSchema(ds2, SchemaType.Source, "Contractor");
            daViewContractor.Fill(ds2, "Contractor");

            daViewClient = new SqlDataAdapter(sqlViewClient, conn);
            cmdBViewClient = new SqlCommandBuilder(daViewClient);
            daViewClient.FillSchema(ds2, SchemaType.Source, "Client");
            daViewClient.Fill(ds2, "Client");

            daViewAllClients = new SqlDataAdapter(sqlViewAllClients, conn);
            cmdBViewAllClients = new SqlCommandBuilder(daViewAllClients);
            daViewAllClients.FillSchema(ds3, SchemaType.Source, "Client");
            daViewAllClients.Fill(ds3, "Client");
            dataGridViewAccounts.DataSource = ds3.Tables["Client"];

            daViewAllAssociationsDetail = new SqlDataAdapter(sqlViewAllAssociationsDetail, conn);
            cmdBViewAllAssociationsDetail = new SqlCommandBuilder(daViewAllAssociationsDetail);
            daViewAllAssociationsDetail.FillSchema(ds3, SchemaType.Source, "Spcialty_Key_Words");
            daViewAllAssociationsDetail.Fill(ds3, "Spcialty_Key_Words");

            //preamiterised queries for the sereate Data Set
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


            //Ticket Edit View
            cmdTicketEditView = new SqlCommand(sqlTicketAdminEdit, conn);
            cmdTicketEditView.Parameters.Add("@un", SqlDbType.VarChar);
            cmdTicketEditView.Parameters.Add("@pw", SqlDbType.VarChar);
            cmdTicketEditView.Parameters["@un"].Value = userName;
            cmdTicketEditView.Parameters["@pw"].Value = password;
            daTicketEditView = new SqlDataAdapter(cmdTicketEditView);
            daTicketEditView.FillSchema(ds3, SchemaType.Source, "Ticket");
            daTicketEditView.Fill(ds3, "Ticket");
            dataGridViewTicketsEdit.DataSource = ds3.Tables["Ticket"];

            //Ticket Edit View (Custom)
            cmdTicketEditView2 = new SqlCommand(sqlTicketAdminEdit, conn);
            cmdTicketEditView2.Parameters.Add("@un", SqlDbType.VarChar);
            cmdTicketEditView2.Parameters.Add("@pw", SqlDbType.VarChar);
            daTicketEditView2 = new SqlDataAdapter(cmdTicketEditView2);
            daTicketEditView2.FillSchema(ds, SchemaType.Source, "TicketOfAccount");


            //Validate Admin Login
            cmdLoginAdmin = new SqlCommand(sqlLoginAdmin, conn);
            cmdLoginAdmin.Parameters.Add("@un", SqlDbType.VarChar);
            cmdLoginAdmin.Parameters.Add("@pw", SqlDbType.VarChar);
            daLoginAdmin = new SqlDataAdapter(cmdLoginAdmin);
            daLoginAdmin.FillSchema(ds3, SchemaType.Source, "Login_");

            cmdWordSearchNum = new SqlCommand(sqlWordSearchNum, conn);
            cmdWordSearchNum.Parameters.Add("@num", SqlDbType.Int);
            daWordSearchNum = new SqlDataAdapter(cmdWordSearchNum);
            daWordSearchNum.FillSchema(ds3, SchemaType.Source, "SearchViaNum");

            cmdWordSearchWord = new SqlCommand(sqlWordSearchWord, conn);
            cmdWordSearchWord.Parameters.Add("@w", SqlDbType.VarChar);
            daWordSearchWord = new SqlDataAdapter(cmdWordSearchWord);
            daWordSearchWord.FillSchema(ds3, SchemaType.Source, "SearchViaWords");

            cmdDeleteConSpecialities = new SqlCommand(sqlDeleteConSpecialities, conn);
            cmdDeleteConSpecialities.Parameters.Add("@id", SqlDbType.Int);
            daDeleteConSpecialities = new SqlDataAdapter(cmdDeleteConSpecialities);
            daDeleteConSpecialities.FillSchema(ds2, SchemaType.Source, "Contractor_Spcialties");

            drClient = ds.Tables["Client"].Rows[0];

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

            dataGridViewViewTickets.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewTicketHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewTicketsEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewAccounts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridViewAI.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //try { dataGridViewTicketsEdit.Rows[0].Selected = true; } catch (ArgumentOutOfRangeException) { }



            labelInfoNameValue.Text = drClient.ItemArray[1].ToString();
            labelInfoUserNameValue.Text = userName;
            textBoxInfoPassword.Text = password;
            labelInfoPhoneValue.Text = drClient.ItemArray[3].ToString();
            textBoxInfoEamil.Text = drClient.ItemArray[4].ToString();

            comboBoxEditContractor.DataSource = ds2.Tables["Contractor"];
            comboBoxEditContractor.DisplayMember = "Contractor_Name";
            comboBoxEditContractor.ValueMember = "Contractor_ID";

            comboBoxEditClient.DataSource = ds2.Tables["Client"];
            comboBoxEditClient.DisplayMember = "Client_Name";
            comboBoxEditClient.ValueMember = "Client_ID";

            comboBoxEditPriority.DataSource = dsDefult.Tables["Priority_"];
            comboBoxEditPriority.DisplayMember = "Priority_Desc";
            comboBoxEditPriority.ValueMember = "Priority_No";

            comboBoxClientDepartment.DataSource = dsDefult.Tables["Department"];
            comboBoxClientDepartment.DisplayMember = "Department_Name";
            comboBoxClientDepartment.ValueMember = "Department_No";

            comboBoxTicketsStatus.SelectedIndex = 0;

            foreach (DataRow dr in dsDefult.Tables["Spcialties"].Rows)
            {
                string[] strArr = { dr[0].ToString(), dr[1].ToString(), "No" };
                ListViewItem viewItem = new ListViewItem(strArr);
                listViewAccoutsSpedialites.Items.Add(viewItem);

                string[] strArr2 = { dr[0].ToString(), dr[1].ToString() };
                ListViewItem viewItem2 = new ListViewItem(strArr2);
                listViewAISpcialitiy.Items.Add(viewItem2);

                specList.Add(viewItem);

            }

            foreach (DataRow dr in ds3.Tables["Spcialty_Key_Words"].Rows)
            {
                string[] strArr = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString() };
                ListViewItem viewItem = new ListViewItem(strArr);
                listViewAIAssociations.Items.Add(viewItem);
            }

            foreach (DataRow dr in dsDefult.Tables["New_Key_Words"].Rows)
            {
                string[] strArr = { dr[0].ToString(), dr[1].ToString(), "No" };
                ListViewItem listViewItem = new ListViewItem(strArr);
                listViewAIDiscoveredWords.Items.Add(listViewItem);
            }

            int count = dsDefult.Tables["Ticket"].Rows.Count;

            labelCreateTicketIDValue.Text = ((int)dsDefult.Tables["Ticket"].Rows[count - 1][0] + 1).ToString();

            labelCreateNameValue.Text = drClient[1].ToString();
        }

        private void dataGridViewViewTickets_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (is_empty)
                {
                    cmdDesc = new SqlCommand(sqlDesc, conn);
                    cmdDesc.Parameters.Add("@d", SqlDbType.Int);
                    cmdDesc.Parameters["@d"].Value = int.Parse(dataGridViewViewTickets.SelectedRows[0].Cells[0].ToString());
                    daDesc = new SqlDataAdapter(cmdDesc);
                    daDesc.FillSchema(ds, SchemaType.Source, "Desc");
                    daDesc.Fill(ds, "Desc");
                }
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
