using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MobileCaller.Localization;

namespace MobileCaller.Forms
{
    public partial class CommentForm : Form, ILocalizable
    {
        #region Properties

        public string PhoneNumber { get; set; }

        public string Comment
        {
            get { return tbComment.Text; }
            set { tbComment.Text = value; }
        }

        #endregion

        public CommentForm()
        {
            InitializeComponent();
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            this.Text = string.Format(ResourceManagerProvider.GetLocalizedString("WND_COMMENT_FORM_TITLE", culture), PhoneNumber);

            btnEnter.Text = ResourceManagerProvider.GetLocalizedString("BTN_ENTER", culture);
            btnCancel.Text = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
        }

        private void CommentForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();
        }
    }
}
