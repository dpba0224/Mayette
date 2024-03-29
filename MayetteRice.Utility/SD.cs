using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.Utility
{
    public static class SD
    {
        /* CONSTANTS FOR THE ROLES */

        // The Admin will perform all the CRUD operations on product and other content management
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        /*
            Company as the special category because of its specific roles or functionalities:
                1. The user that will be categorized under this will not have to make the payment right away
                2. The user must be specified which company they belong with
                3. They will have 30 days (NET30) to make payment after the order has been placed
                4. The company will have a different functionality and a company user can be registered by an admin user
        */
        public const string Role_Company = "Company";
        
        // Employee will have access to modifying the shipping of a product and other details
        public const string Role_Employee = "Employee";


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

        // Constants for Session
        public const string SessionCart = "SessionShoppingCart";
	}
}
