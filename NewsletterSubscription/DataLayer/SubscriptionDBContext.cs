using NewsletterSubscription.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsletterSubscription.DataLayer
{
    public class SubscriptionDBContext : DbContext
    {
        public SubscriptionDBContext() : base("NEWSLETTER_SUBSCRIPTIONS")
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
    }
}