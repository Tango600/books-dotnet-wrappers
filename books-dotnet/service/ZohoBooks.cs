using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zohobooks.api;
using zohobooks.util;

namespace zohobooks.service
{
    public class ZohoCredentials
    {
        public string ClientID;
        public string ClientSecret;
        public string ServerDomen;
        public string RefreshToken;
        public string OrganizationID;
    }

    /// <summary>
    /// Class ZohoBooks is used to provide all api instances for the Zoho Books services.
    /// </summary>
    public class ZohoBooks
    {
        /// <summary>
        /// The refresh token token
        /// </summary>
        readonly string refresh_token;

        /// <summary>
        /// The organisation identifier
        /// </summary>
        readonly string organisationId;

        public ZohoBooks(ZohoCredentials zohoCredentials)
        {
            SessionManager.Sessions.TryAdd(zohoCredentials.RefreshToken, new SessionData
            {
                RefreshToken = zohoCredentials.RefreshToken,
                ClientID = zohoCredentials.ClientID,
                ClientSecret = zohoCredentials.ClientSecret,
                OrganizationID = zohoCredentials.OrganizationID,
                APIdomen = zohoCredentials.ServerDomen
            });

            this.refresh_token = zohoCredentials.RefreshToken;
            this.organisationId = zohoCredentials.OrganizationID;
        }

        /// <summary>
        /// Gets an instance of invoices API.
        /// </summary>
        /// <returns>InvoicesApi object.</returns>
        public InvoicesApi GetInvoicesApi()
        {
            var invoicesApi = new InvoicesApi(refresh_token, organisationId);
            return invoicesApi;
        }
        /// <summary>
        /// Gets an instance of bank accounts API.
        /// </summary>
        /// <returns>BankAccountsApi object.</returns>
        public BankAccountsApi GetBankAccountsApi()
        {
            var bankAccountsApi = new BankAccountsApi(refresh_token, organisationId);
            return bankAccountsApi;
        }
        /// <summary>
        /// Gets an instance of bank rules API.
        /// </summary>
        /// <returns>BankRulesApi object.</returns>
        public BankRulesApi GetBankRulesApi()
        {
            var bankrulesApi = new BankRulesApi(refresh_token, organisationId);
            return bankrulesApi;
        }
        /// <summary>
        /// Gets an instance of bank transactions API.
        /// </summary>
        /// <returns>BankTransactionsApi object.</returns>
        public BankTransactionsApi GetBankTransactionsApi()
        {
            var bankTransactionsApi = new BankTransactionsApi(refresh_token, organisationId);
            return bankTransactionsApi;
        }
        /// <summary>
        /// Gets an instance of base currency adjustments API.
        /// </summary>
        /// <returns>BaseCurrencyAdjustmentsApi object.</returns>
        public BaseCurrencyAdjustmentsApi GetBaseCurrencyAdjustmentsApi()
        {
            var baseCurrencyAdjustmentApi = new BaseCurrencyAdjustmentsApi(refresh_token, organisationId);
            return baseCurrencyAdjustmentApi;
        }
        /// <summary>
        /// Gets an instance of bills API.
        /// </summary>
        /// <returns>BillsApi object.</returns>
        public BillsApi GetBillsApi()
        {
            var billsApi = new BillsApi(refresh_token, organisationId);
            return billsApi;
        }
        /// <summary>
        /// Gets an instance of chart of accounts API.
        /// </summary>
        /// <returns>ChartOfAccountsApi object.</returns>
        public ChartOfAccountsApi GetChartOfAccountsApi()
        {
            var chartOfAccountsApi = new ChartOfAccountsApi(refresh_token, organisationId);
            return chartOfAccountsApi;
        }
        /// <summary>
        /// Gets an instance of contacts API.
        /// </summary>
        /// <returns>ContactsApi object.</returns>
        public ContactsApi GetContactsApi()
        {
            var contactsApi = new ContactsApi(refresh_token, organisationId);
            return contactsApi;
        }
        /// <summary>
        /// Gets an instance of credit note API.
        /// </summary>
        /// <returns>CreditNotesApi object.</returns>
        public CreditNotesApi GetCreditNoteApi()
        {
            var creditNotesApi = new CreditNotesApi(refresh_token, organisationId);
            return creditNotesApi;
        }
        /// <summary>
        /// Gets an instance of customer payments API.
        /// </summary>
        /// <returns>CustomerPaymentsApi object.</returns>
        public CustomerPaymentsApi GetCustomerPaymentsApi()
        {
            var customerPaymentsApi = new CustomerPaymentsApi(refresh_token, organisationId);
            return customerPaymentsApi;
        }
        /// <summary>
        /// Gets an instance of estimates API.
        /// </summary>
        /// <returns>EstimatesApi object.</returns>
        public EstimatesApi GetEstimatesApi()
        {
            var estimatesApi = new EstimatesApi(refresh_token, organisationId);
            return estimatesApi;
        }
        /// <summary>
        /// Gets an instance of expenses API.
        /// </summary>
        /// <returns>ExpensesApi object.</returns>
        public ExpensesApi GetExpensesApi()
        {
            var expensesApi = new ExpensesApi(refresh_token, organisationId);
            return expensesApi;
        }
        /// <summary>
        /// Gets an instance of items API.
        /// </summary>
        /// <returns>ItemsApi object.</returns>
        public ItemsApi GetItemsApi()
        {
            var itemsApi = new ItemsApi(refresh_token, organisationId);
            return itemsApi;
        }
        /// <summary>
        /// Gets an instance of journals API.
        /// </summary>
        /// <returns>JournalsApi object.</returns>
        public JournalsApi GetJournalsApi()
        {
            var journalsApi = new JournalsApi(refresh_token, organisationId);
            return journalsApi;
        }
        /// <summary>
        /// Gets an instance of organizations API.
        /// </summary>
        /// <returns>OrganizationsApi object.</returns>
        public OrganizationsApi GetOrganizationsApi()
        {
            var organizationsApi = new OrganizationsApi(refresh_token, organisationId);
            return organizationsApi;
        }
        /// <summary>
        /// Gets an instance of projects API.
        /// </summary>
        /// <returns>ProjectsApi object.</returns>
        public ProjectsApi GetProjectsApi()
        {
            var projectsApi = new ProjectsApi(refresh_token, organisationId);
            return projectsApi;
        }
        /// <summary>
        /// Gets an instance of recurring expenses API.
        /// </summary>
        /// <returns>RecurringExpensesApi object.</returns>
        public RecurringExpensesApi GetRecurringExpensesApi()
        {
            var recurringExpensesApi = new RecurringExpensesApi(refresh_token, organisationId);
            return recurringExpensesApi;
        }
        /// <summary>
        /// Gets an instance of recurring invoices API.
        /// </summary>
        /// <returns>RecurringInvoicesApi object.</returns>
        public RecurringInvoicesApi GetRecurringInvoicesApi()
        {
            var recurringInvoicesApi = new RecurringInvoicesApi(refresh_token, organisationId);
            return recurringInvoicesApi;
        }
        /// <summary>
        /// Gets an instance of settings API.
        /// </summary>
        /// <returns>SettingsApi object.</returns>
        public SettingsApi GetSettingsApi()
        {
            var settingsApi = new SettingsApi(refresh_token, organisationId);
            return settingsApi;
        }
        /// <summary>
        /// Gets an instance of users API.
        /// </summary>
        /// <returns>UsersApi object.</returns>
        public UsersApi GetUsersApi()
        {
            var usersApi = new UsersApi(refresh_token, organisationId);
            return usersApi;
        }
        /// <summary>
        /// Gets an instance of vendor payments API.
        /// </summary>
        /// <returns>VendorPaymentsApi object.</returns>
        public VendorPaymentsApi GetVendorPaymentsApi()
        {
            var vendorPaymentsApi = new VendorPaymentsApi(refresh_token, organisationId);
            return vendorPaymentsApi;
        }
        /// <summary>
        /// Gets an instance of salesorders API.
        /// </summary>
        /// <returns>SalesordersApi.</returns>
        public SalesordersApi GetSalesordersApi()
        {
            var salesordersApi = new SalesordersApi(refresh_token, organisationId);
            return salesordersApi;
        }
        /// <summary>
        /// Gets the purchaseorders API.
        /// </summary>
        /// <returns>PurchaseordersApi.</returns>
        public PurchaseordersApi GetPurchaseordersApi()
        {
            var purchaseordersApi = new PurchaseordersApi(refresh_token, organisationId);
            return purchaseordersApi;
        }

        /// <summary>
        /// Gets the vendor credits API.
        /// </summary>
        /// <returns>VendorCreditsApi.</returns>
        public VendorCreditsApi GetVendorCreditsApi()
        {
            var vendorCreditsApi = new VendorCreditsApi(refresh_token, organisationId);
            return vendorCreditsApi;
        }
    }
}
