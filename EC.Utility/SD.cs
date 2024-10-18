using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utility
{
    public static class SD
    {
        public const string Role_User_Cust = "Customer";
        public const string Role_User_Comp = "Company";
        public const string Role_User_Admin = "Admin";
        public const string Role_User_Employee = "Employee";


		public const string StatusPending = "Pending";
		public const string StatusApproved = "Approved";
		public const string StatusInProcess = "Processing";
		public const string StatusShipped = "Shipped";
		public const string StatusCancelled = "Cancelled";
		public const string StatusRefunded = "Refunded";

		public const string PaymentStatusPending = "Pending";
		public const string PaymentStatusApproved = "Approved";
		public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
		public const string PaymentStatusRejected = "Rejected";

		public const string SessionCart = "SessionShoppingCart";
	}
}
