
////////////////////////////////////
//Developer & Owner: Matthew Hyndman
////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IT_Support_Ticket_System_V1
{
    public partial class MainForm : Form
    {
        //var declairations
        private static SqlDataAdapter daLogin, daLoginAdmin;
        private static DataSet ds = new DataSet();
        private static SqlConnection conn;
        private static SqlCommand cmdLogin, cmdLoginAdmin;
        private static DataRow drLogin, drLoginAdmin;

        string connStr;//connection string

        //quieries
        string sqlLogins = @"exec Find_Login @uname = @un, @pword = @pw";
        string sqlLoginAdmin = @"exec Find_Login_Admin @uname = @un, @pword = @pw";

        //when the login button is clicked
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string user_Name = textBoxUserName.Text;
            string password = textBoxPassword.Text;
            string server = textBoxServerName.Text;
            string id = "";

            if (user_Name.Equals(""))
            {
                ep.SetError(textBoxUserName, "Please enter your Username");
                ok = false;
            }
            if (password.Equals(""))
            {
                ep.SetError(textBoxPassword, "Please enter your Password");
                ok = false;
            }
            if (server.Equals(""))
            {
                ep.SetError(textBoxServerName, "Please enter a Server Name");
                ok = false;
            }
            else
            {
                try
                {
                    connStr = @"Data Source = " + server + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";
                    conn = new SqlConnection(connStr);
                    conn.Open();
                }
                catch (SqlException)
                {
                    ep.SetError(textBoxServerName, "Please  enter a valid Server Name");
                    ok = false;
                }
            }

            if (ok)
            {

                if (ds.Tables.Count == 0)
                {

                    cmdLogin = new SqlCommand(sqlLogins, conn);
                    cmdLogin.Parameters.Add("@un", SqlDbType.VarChar);
                    cmdLogin.Parameters.Add("@pw", SqlDbType.VarChar);
                    daLogin = new SqlDataAdapter(cmdLogin);
                    daLogin.FillSchema(ds, SchemaType.Source, "Login_");

                    cmdLoginAdmin = new SqlCommand(sqlLoginAdmin, conn);
                    cmdLoginAdmin.Parameters.Add("@un", SqlDbType.VarChar);
                    cmdLoginAdmin.Parameters.Add("@pw", SqlDbType.VarChar);
                    daLoginAdmin = new SqlDataAdapter(cmdLoginAdmin);
                    daLoginAdmin.FillSchema(ds, SchemaType.Source, "Contractor");

                }

                cmdLogin.Parameters["@un"].Value = user_Name;
                cmdLogin.Parameters["@pw"].Value = password;

                cmdLoginAdmin.Parameters["@un"].Value = user_Name;
                cmdLoginAdmin.Parameters["@pw"].Value = password;


                daLogin.Fill(ds, "Login_");
                daLoginAdmin.Fill(ds, "Contractor");


                try
                {
                    try
                    {
                        drLoginAdmin = ds.Tables["Contractor"].Rows[0];
                        id = drLoginAdmin.ItemArray[0].ToString();

                        if (drLoginAdmin.ItemArray[1].ToString() == user_Name && drLoginAdmin.ItemArray[2].ToString() == password)
                        {
                            //if login and server are to be remembered
                            if (checkBoxLogin.Checked && checkBoxServer.Checked)
                            {
                                //transfres and mobalises the user name and password in a txt file
                                if (File.Exists("User.txt"))
                                {
                                    File.Delete("User.txt");
                                    File.AppendAllText("User.txt", "Remember-Login-and-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                else
                                    File.WriteAllText("User.txt", "Remember-Login-and-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                            }
                            //if just the login is to be remembered
                            else if (checkBoxLogin.Checked)
                            {
                                //transfres and mobalises the user name and password in a txt file
                                if (File.Exists("User.txt"))
                                {
                                    File.Delete("User.txt");
                                    File.AppendAllText("User.txt", "Remember-Login|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                else
                                    File.WriteAllText("User.txt", "Remember-Login|" + id + "|" + user_Name + "|" + password + "|" + server);
                            }
                            //if just the server is to ber remembered
                            else if (checkBoxServer.Checked)
                            {
                                //transfres and mobalises the user name and password in a txt file
                                if (File.Exists("User.txt"))
                                {
                                    File.Delete("User.txt");
                                    File.AppendAllText("User.txt", "Remember-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                else
                                    File.WriteAllText("User.txt", "Remember-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                            }
                            //if nothing is to be remembered
                            else
                            {
                                //transfres and mobalises the user name and password in a txt file
                                if (File.Exists("User.txt"))
                                {
                                    File.Delete("User.txt");
                                    File.AppendAllText("User.txt", "Forget|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                else
                                    File.WriteAllText("User.txt", "Forget|" + id + "|" + user_Name + "|" + password + "|" + server);
                            }
                            conn.Close();

                            //starsts up the admin menu
                            this.Hide();
                            var mainMenuAdmin = new MainMenuAdmin();
                            mainMenuAdmin.ShowDialog();
                            this.Close();

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        try
                        {
                            drLogin = ds.Tables["Login_"].Rows[0];
                            id = drLogin.ItemArray[0].ToString();

                            if (drLogin.ItemArray[1].ToString() == user_Name && drLogin.ItemArray[2].ToString() == password)
                            {
                                //if login and server are to be remembered
                                if (checkBoxLogin.Checked && checkBoxServer.Checked)
                                {
                                    //transfres and mobalises the user name and password in a txt file
                                    if (File.Exists("User.txt"))
                                    {
                                        File.Delete("User.txt");
                                        File.AppendAllText("User.txt", "Remember-Login-and-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                    }
                                    else
                                        File.WriteAllText("User.txt", "Remember-Login-and-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                //if just the login is to be remembered
                                else if (checkBoxLogin.Checked)
                                {
                                    //transfres and mobalises the user name and password in a txt file
                                    if (File.Exists("User.txt"))
                                    {
                                        File.Delete("User.txt");
                                        File.AppendAllText("User.txt", "Remember-Login|" + id + "|" + user_Name + "|" + password + "|" + server);
                                    }
                                    else
                                        File.WriteAllText("User.txt", "Remember-Login|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                //if just the server is to ber remembered
                                else if (checkBoxServer.Checked)
                                {
                                    //transfres and mobalises the user name and password in a txt file
                                    if (File.Exists("User.txt"))
                                    {
                                        File.Delete("User.txt");
                                        File.AppendAllText("User.txt", "Remember-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                    }
                                    else
                                        File.WriteAllText("User.txt", "Remember-Server|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                //if nothing is to be remembered
                                else
                                {
                                    //transfres and mobalises the user name and password in a txt file
                                    if (File.Exists("User.txt"))
                                    {
                                        File.Delete("User.txt");
                                        File.AppendAllText("User.txt", "Forget|" + id + "|" + user_Name + "|" + password + "|" + server);
                                    }
                                    else
                                        File.WriteAllText("User.txt", "Forget|" + id + "|" + user_Name + "|" + password + "|" + server);
                                }
                                conn.Close();

                                ////starsts up the Client menu
                                this.Hide();
                                var mainMenu = new MainMenu();
                                mainMenu.ShowDialog();
                                this.Close();
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show("User name or Password is incorrect");
                        }
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("User name or Password is incorrect");
                    //MessageBox.Show(drLogin.ItemArray[0].ToString() + "\t" + drLogin.ItemArray[1].ToString());
                }

            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //DESKTOP-NSHTTAL

            if (File.Exists("User.txt"))
            {
                string userFile = File.ReadAllText("User.txt");
                string remember = "";
                int toggle = 0;
                for (int i = 0; i < userFile.Length; i++)
                {
                    if (userFile[i] == '|')
                    {
                        toggle++;
                        continue;
                    }
                    if (toggle == 0) remember += userFile[i];

                    else if (remember == "Remember-Login")
                    {
                        switch (toggle)
                        {
                            case 2:
                                textBoxUserName.Text += userFile[i];
                                break;
                            case 3:
                                textBoxPassword.Text += userFile[i];
                                break;
                        }

                    }
                    else if (remember == "Remember-Server")
                    {
                        if (toggle == 4) textBoxServerName.Text += userFile[i];
                    }
                    else if (remember == "Remember-Login-and-Server")
                    {
                        switch (toggle)
                        {
                            case 2:
                                textBoxUserName.Text += userFile[i];
                                break;
                            case 3:
                                textBoxPassword.Text += userFile[i];
                                break;
                            case 4:
                                textBoxServerName.Text += userFile[i];
                                break;
                        }

                        //if (toggle == 1) textBoxUserName.Text += userFile[i];
                        //else if (toggle == 2) textBoxPassword.Text += userFile[i];
                        //else if (toggle == 3) textBoxServerName.Text += userFile[i];
                    }
                }
                if (textBoxServerName.Text == "")
                {
                    connStr = @"Data Source = " + textBoxServerName.Text + "; Initial Catalog = IT_Support_Tickect_System; Integrated Security = true";
                    //cdc.populate_DataSet();
                    conn = new SqlConnection(connStr);
                    conn.Open();
                }

                if (conn != null)
                {
                    cmdLogin = new SqlCommand(sqlLogins, conn);
                    cmdLogin.Parameters.Add("@un", SqlDbType.VarChar);
                    cmdLogin.Parameters.Add("@pw", SqlDbType.VarChar);
                    daLogin = new SqlDataAdapter(cmdLogin);
                    daLogin.FillSchema(ds, SchemaType.Source, "Login_");

                    cmdLoginAdmin = new SqlCommand(sqlLoginAdmin, conn);
                    cmdLoginAdmin.Parameters.Add("@un", SqlDbType.VarChar);
                    cmdLoginAdmin.Parameters.Add("@pw", SqlDbType.VarChar);
                    daLoginAdmin = new SqlDataAdapter(cmdLoginAdmin);
                    daLoginAdmin.FillSchema(ds, SchemaType.Source, "Contractor");
                }

            }
        }
    }
}
