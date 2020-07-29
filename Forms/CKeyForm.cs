using System;
using System.Windows.Forms;
using MobileCaller.Localization;

namespace MobileCaller.Forms
{
    public partial class CKeyForm : Form, ILocalizable
    {
        #region Properties

        public string Login
        {
            get
            {
                return tbLogin.Text;
            }
        }

        public string Email
        {
            get
            {
                return tbEmail.Text;
            }
        }

        public string IMEI
        {
            get
            {
                return tbIMEI.Text;
            }
        }

        public string Key
        {
            get
            {
                return tbKey.Text;
            }
        }

        #endregion

        public CKeyForm()
        {
            InitializeComponent();
        }

        public void PerformTranslation()
        {
            var culture = Application.CurrentCulture;

            this.Text = ResourceManagerProvider.GetLocalizedString("WND_KEY_FORM_TITLE", culture);

            lRegistrationWarning.Text = ResourceManagerProvider.GetLocalizedString("L_REGISTRATION_WARNING", culture);
            lRegistrationInvitation.Text = ResourceManagerProvider.GetLocalizedString("L_REGISTRATION_INVITATION", culture);
            lUser.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_REGISTERED_USER", culture));
            lEmail.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_REGISTERED_EMAIL", culture));
            lIMEI.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_REGISTERED_IMEI", culture));
            lActivationKey.Text = String.Format("{0}:", ResourceManagerProvider.GetLocalizedString("L_REGISTERED_ACTIVATION_KEY", culture));

            btnEnter.Text = ResourceManagerProvider.GetLocalizedString("BTN_ENTER", culture);
            btnCancel.Text = ResourceManagerProvider.GetLocalizedString("BTN_CANCEL", culture);
        }

        private void CKeyForm_Load(object sender, EventArgs e)
        {
            PerformTranslation();
        }
    }
}
