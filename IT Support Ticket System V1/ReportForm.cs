
////////////////////////////////////
//Developer & Owner: Matthew Hyndman
////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IT_Support_Ticket_System_V1
{
    public partial class ReportForm : Form
    {
        //var declariations
        string userName = "";
        string password = "";
        string server = "";


        SqlConnection conn;
        string connStr;
        DataTable dataTable = new DataTable();

        public ReportForm()
        {
            InitializeComponent();
        }
        
        //when form loads
        private void ReportForm_Load(object sender, EventArgs e)
        {
            //check if permeit txt file exixts
            if (File.Exists("Permit.txt"))
            {
                string userFile = File.ReadAllText("User.txt");//get User Deatils
                int toggle = 0;
                for (int i = 0; i < userFile.Length; i++)
                {
                    if (userFile[i] == '|')
                    {
                        toggle++;
                        continue;
                    }

                    if (toggle == 2) userName += userFile[i];
                    else if (toggle == 3) password += userFile[i];
                    else if (toggle == 4) server += userFile[i];
                       
                    
                }


                string fileTxt = File.ReadAllText("Permit.txt");//get permit

                connStr = @"Data Source = " + server + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";
                conn = new SqlConnection(connStr);

                //pervents window duplication
                if (fileTxt == "0 Individual")
                {
                    CrystalReportClientTicket crct = new CrystalReportClientTicket();
                    crct.SetParameterValue(0, userName);
                    crct.SetParameterValue(1, password);

                    crystalReportViewer1.ReportSource = crct;
                }
            }
        }

        private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tells the system that this window can be opened again
            File.Delete("Permit.txt");
            File.AppendAllText("Permit.txt", "1");
        }
    }
}
