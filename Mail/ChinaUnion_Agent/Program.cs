using ChinaUnion_DataAccess;
using KnightsWarriorAutoupdater;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace ChinaUnion_Agent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process[] pary = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (pary.Length > 1)
            {
                
                MessageBox.Show("已经有相同的程序在运行,请关闭已经打开的程序！");
                Application.Exit();
                return;
            }


            #region check and download new version program
            bool bHasError = false;
            IAutoUpdater autoUpdater = new AutoUpdater();
            try
            {
                autoUpdater.Update();
            }
            catch (WebException exp)
            {
                MessageBox.Show("Can not find the specified resource");
                bHasError = true;
            }
            catch (XmlException exp)
            {
                bHasError = true;
                MessageBox.Show("Download the upgrade file error");
            }
            catch (NotSupportedException exp)
            {
                bHasError = true;
                MessageBox.Show("Upgrade address configuration error");
            }
            catch (ArgumentException exp)
            {
                bHasError = true;
                MessageBox.Show("Download the upgrade file error");
            }
            catch (Exception exp)
            {
                bHasError = true;
                MessageBox.Show("An error occurred during the upgrade process");
            }
            finally
            {
                if (bHasError == true)
                {
                    try
                    {
                        autoUpdater.RollBack();
                    }
                    catch (Exception)
                    {
                        //Log the message to your file or database
                    }
                }
            }
            #endregion

            bool isNewVersion = true ;
            if (isNewVersion)
            {
                frmLogin fLogin = new frmLogin();
                fLogin.ShowDialog();
                if (fLogin.DialogResult != DialogResult.OK)
                    return;


                frmMain frmMain = new ChinaUnion_Agent.frmMain();
                frmMain.isNewVersion = isNewVersion;
                frmMain.menuTable = fLogin.menuTable;
                frmMain.loginUser = fLogin.loginUser;
                Application.Run(frmMain);
            }
            else
            {
                Application.Run(new frmMain());
            }
        }
    }
}
