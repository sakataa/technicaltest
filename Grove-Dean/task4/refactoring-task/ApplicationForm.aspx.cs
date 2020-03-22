using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using Legacy.Core.PageTypes;
using Legacy.Core.Services;
using Legacy.Web.Templates.Base;
using Legacy.Web.Utilities;

namespace Legacy.Web.Templates.Pages
{
    public partial class ApplicationForm : TemplatePageBase<ApplicationFormPage>
    {
        private ContactPersonManager _contactPersonManager = new ContactPersonManager();
        private CountryManager _countryManager = new CountryManager();
        private EmailService _emailService = new EmailService();

        protected List<ContactPerson> contactPersonList;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                DataBind();
                PopulateCountyList();
            }
        }

        #region Fill GUI controls
        /// <summary>
        /// Populates the County dropDownList
        /// </summary>
        protected void PopulateCountyList()
        {
            Ddl_County.DataSource = _countryManager.GetCountryList();
            Ddl_County.DataBind();
        }

        /// <summary>
        /// Creates as many FileUpload controls as configured on the page.
        /// </summary>
        private void BuildDynamicControls()
        {
            if (!pnlFileUpload.Visible || CurrentPage.Property["NumberOfFileUploads"].IsNull)
            {
                return;
            }

            //Create dummy datasource to display the correct number of FileUpload controls.
            int numberOfFiles = (int)CurrentPage.Property["NumberOfFileUploads"].Value;
            if (numberOfFiles == 0)
            {
                return;
            }

            List<int> list = new List<int>();
            for (int i = 0; i < numberOfFiles; i++)
            {
                list.Add(i);
            }

            rptFileUpload.DataSource = list;
            rptFileUpload.DataBind();
        }
        #endregion

        /// <summary>
        /// Populate Ddl_Municipality with municipality from the given county
        /// </summary>
        /// <param name="country"></param>
        private void PopulateMunicipalityList(string country)
        {
            InitContactPersonList();

            Ddl_Municipality.Items.Clear();
            Ddl_Municipality.Items.Add(new ListItem("", ""));

            var municipalityList = _countryManager.GetMunicipalityList(country, contactPersonList);
            foreach (var item in municipalityList)
            {
                Ddl_Municipality.Items.Add(new ListItem(item.Key, item.Value));
            }
        }

        private EmailParam CreateEmailParam()
        {
            var emailParam = new EmailParam();
            emailParam.Subject = PropertyService.GetStringProperty(CurrentPage, "EmailSubject");
            emailParam.content = BuildEmailContent();
            emailParam.FromAdress = Txt_Email.Text;
            emailParam.AttachmentList = RequestHelper.GetAttachments(RequestHelper, ApplicationPath);

            InitContactPersonList();
            var emailList = _contactPersonManager.GetEmailList(contactPersonList, Ddl_Municipality.SelectedValue);
            emailParam.ToAddresses = string.Join(";", emailList);

            return emailParam;
        }

        private void InitContactPersonList()
        {
            if (contactPersonList == null || contactPersonList.Count == 0)
            {
                contactPersonList = _contactPersonManager.InitDataList();
            }
        }

        #region Events
        /// <summary>
        /// Attachement button clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        protected void btnShowFileUpload_Click(object sender, EventArgs e)
        {
            pnlFileUpload.Visible = true;
            BuildDynamicControls();
            btnShowFileUpload.Visible = false;
        }

        /// <summary>
        /// Submit button clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        protected void Btn_SubmitForm_Click(object sender, EventArgs e)
        {
            // Server side validation, if javascript is disabled
            Page.Validate();
            if (Page.IsValid)
            {
                var emailParam = CreateEmailParam();
                if (_emailService.SendMail(emailParam))
                {
                    string receiptUrl = PropertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormReceiptPage");
                    Response.Redirect(receiptUrl);
                }
                else
                {
                    string errorUrl = PropertyService.GetPageDataPropertyLinkUrl(CurrentPage, "FormErrorPage");
                    Response.Redirect(errorUrl);
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the Ddl_County control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Ddl_County_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Ddl_County.SelectedValue.Equals(string.Empty))
            {
                PopulateMunicipalityList(Ddl_County.SelectedValue);
            }
            else
            {
                Ddl_Municipality.Items.Clear();
                Ddl_Municipality.DataBind();
            }
        }
        #endregion
    }
}