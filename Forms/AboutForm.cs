using System;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using MobileCaller.Localization;

namespace MobileCaller.Forms
{
    public partial class AboutForm : Form, ILocalizable
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;
            this.Text = ResourceManagerProvider.GetLocalizedString("WND_ABOUT_FORM_TITLE", culture);

            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            lProgramVersion.Text = String.Format(ResourceManagerProvider.GetLocalizedString("L_PROGRAM_VERSION", culture), fileVersionInfo.ProductVersion);
            
            lCopyright.Text = ResourceManagerProvider.GetLocalizedString("L_COPYRIGHT", culture);
            lAllRightsReserved.Text = ResourceManagerProvider.GetLocalizedString("L_ALL_RIGHTS_RESERVED", culture);
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();

            //Enable AboutForm_KeyDown to be worked
            KeyPreview = true;

            // Add a link to the LinkLabel.
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.taycoon.com/";
            taycoonLink.Links.Add(link);
        }

        private void taycoonLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
