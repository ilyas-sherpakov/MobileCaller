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
    public partial class SearchForm : Form
    {
        public string SearchedPhone
        {
            get
            {
                return tbSearchedPhone.Text;
            }
        }

        public SearchForm()
        {
            InitializeComponent();
            tbSearchedPhone.Select(); // set the focus on input textbox
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            this.Text = ResourceManagerProvider.GetLocalizedString("WND_SEARCH_FORM_TITLE", culture);

            lEnterSearchedPhone.Text = ResourceManagerProvider.GetLocalizedString("L_ENTER_SEARCHED_PHONE", culture);
            
            btnSearch.Text = ResourceManagerProvider.GetLocalizedString("BTN_SEARCH", culture);
            btnCancel.Text = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
        }

        private void SearchPhoneForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();
        }

    }
}
