using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace NewsletterSubscription.Models
{
    // We use Enum instead of common string in order to save space in DB (also because the View 
    // will detect this is a dropdown, so easy and efficiently to develop).
    public enum Source
    {
        Advert = 0,
        [Display(Name = "Word of Mouth")]
        WordOfMouth = 1,
        Other = 2
    }

    public class Subscription
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "This is a required field")]
        [MaxLength(50, ErrorMessage = "'Email' length cannot exceed 50")]
        [DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Email format is not valid")]
        public string Email { get; set; }

        [Display(Name = "How you met us?"), Required(ErrorMessage = "Email is a required field")]
        public Source Source { get; set; }

        [Display(Name = "Reason to sign up")]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "'Reason' length cannot exceed 100")]
        public string ReasonSignUp { get; set; }

        [Display(Name = "Subscription Date")]
        [DataType(DataType.DateTime)]
        public DateTime SubscriptionDate { get; set; }
    }
}